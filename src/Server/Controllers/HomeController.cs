using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Models;
using ServiceLayer;

namespace Server.Controllers {
	public class HomeController : Controller {
		readonly ILogger<HomeController> _logger;
		readonly string uploadsDir;

		string FilePath(string filename) => Path.Combine(uploadsDir, filename);

		string WriteFileFromForm() {
			var file = Request.Form.Files[0];

			if(file.Length <= 0)
				throw new Exception("File does not contain data.");

			using var stream = new FileStream(FilePath(file.FileName), FileMode.Create);
			file.CopyTo(stream);
			return file.FileName;
		}

		public IActionResult AcceptImport(ImportProcessModel m) {
			if(!ModelState.IsValid)
				return View("ImportProcess1", new ImportProcessModel(m.EmployeeID));

			m.AcceptImport();
			return View(m.ShouldSkipScan() ? "ImportProcess3" : "ImportProcess2", new ImportProcessModel(m));
		}

		public IActionResult AddImportItem(ImportOrderModel m) {
			if(ModelState.IsValid)
				m.AddItem();

			return View("ImportOrder", new ImportOrderModel(m));
		}

		public IActionResult AddOrderItem(int customerID, int stockID, bool copy) {
			var m = new ExportOrderModel(customerID);

			if(m.HasPenalties())
				return View("UnpaidPenalties", new UnpaidPenaltiesModel(m.CustomerID, true));

			var reservationCount = m.ReservationCount();

			if(ExportOrderModel.ReservationLimitReached(reservationCount))
				return View("LimitReached", new LimitReachedModel(m.CustomerID, reservationCount));

			m.AddItem(stockID, copy);
			return View("Order1", m);
		}

		public IActionResult AddProduct(ImportOrderModel m) {
			if(ModelState.IsValid) {
				if(Request.Form.Files.Count > 0)
					m.Product.Filename = WriteFileFromForm();

				m.Product.Create();
			}

			return View("ImportOrder", new ImportOrderModel(m));
		}

		public IActionResult AddRequest(RequestFormModel m) {
			if(!ModelState.IsValid)
				return View("RequestForm", new RequestFormModel(m.CustomerID));

			m.AddRequest();
			return View("Requests", new RequestsModel(m.CustomerID));
		}

		public IActionResult BackFromDetail(int customerID) => View("Catalog", new CatalogModel(customerID));

		public IActionResult BackFromOrder(int customerID, int stage) => stage switch {
			1 => View("Catalog", new CatalogModel(customerID)),
			_ => View($"Order{stage - 1}", new ExportOrderModel(customerID))
		};

		public IActionResult ChangeDelivery(int customerID, bool hasDelivery) {
			var m = new ExportOrderModel(customerID);
			m.Order().ChangeDelivery(hasDelivery);
			return View("Order2", m);
		}

		public IActionResult ChangePaymentMethod(int customerID, bool online) {
			var m = new ExportOrderModel(customerID);
			m.Order().ChangePaymentMethod(online);
			return View("Order3", m);
		}

		public IActionResult ChooseBorrowing(int employeeID, int customerID, int borrowingID) => View(
			"BorrowingReturn", new BorrowingReturnModel(employeeID, customerID, borrowingID)
		);

		public IActionResult ConfirmImportOrder(ImportOrderModel m) {
			if(ModelState.IsValid)
				m.Confirm();

			return View("Menu", new ServiceModel());
		}

		public IActionResult DeleteOrderItem(int customerID, int orderItemID) {
			var m = new ExportOrderModel(customerID);
			m.DeleteItem(orderItemID);
			return View("Order1", m);
		}

		public IActionResult DeliverOrder(int orderID) => View("Delivery", new DeliveryModel(orderID));
		public IActionResult Delivery() => View(new DeliveryModel());

		public IActionResult DenyImport(ImportProcessModel m) {
			if(ModelState.IsValid)
				m.DenyImport();
			return View("ImportProcess1", new ImportProcessModel(m.EmployeeID));
		}

		public IActionResult Detail(int customerID, int productID) => View("Detail", new DetailModel(customerID, productID));

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

		public IActionResult FlipStorage() {
			StartupService.GetInstance().FlipStorage();
			return View("Menu", new ServiceModel());
		}

		public IActionResult ForwardExport(ExportProcessModel m) {
			switch(m.Stage) {
				case 1:
					return m.Copy()
						? View("ExportProcess2", new ExportProcessModel(m.EmployeeID, m.OrderItemID))
						: View("ExportProcess4", new ExportProcessModel(m.EmployeeID, m.OrderItemID, m.ActiveOrderItem().StockID));
				case 2:
					return View("ExportProcess3", new ExportProcessModel(m.EmployeeID, m.OrderItemID, m.CreateCopy()));
				case 3:
					return View("ExportProcess4", new ExportProcessModel(m.EmployeeID, m.OrderItemID, m.StockID));
				case 4: {
					m.ChangeLocation();
					return View("ExportProcess5", new ExportProcessModel(m.EmployeeID, m.OrderItemID));
				}
				case 5: {
					m.Confirm();
					return View("ExportProcess1", new ExportProcessModel(m.EmployeeID));
				}
				default:
					throw new Exception($"Unknown export stage {m.Stage}");
			}
		}

		public IActionResult ForwardOrder(int customerID, int stage) {
			var m = new ExportOrderModel(customerID);

			if(stage > 0 && m.HasPenalties())
				return View("UnpaidPenalties", new UnpaidPenaltiesModel(m.CustomerID, true));

			if(stage == 1)
				m.EnsureCorrectDelivery();
			if(stage == 2 && m.AddressMissing())
				return View("Address", new AddressModel(m.CustomerID));
			if(stage == 4) {
				var orderID = m.Order().ID;
				m.Confirm();
				return View("Order5", new ConfirmedOrderModel(customerID, orderID));
			}

			return View($"Order{stage + 1}", new ExportOrderModel(customerID));
		}

		public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env) {
			_logger = logger;
			uploadsDir = Path.Combine(env.WebRootPath, "uploads");
		}

		public IActionResult ImportOrder() => View(new ImportOrderModel());
		public IActionResult Login(int reason) => View(new LoginModel(reason));

		public IActionResult LoseBorrowing(BorrowingReturnModel m) {
			m.Lose();
			return View("BorrowingReturn", new BorrowingReturnModel(m.EmployeeID, m.ActiveCustomerID));
		}

		public IActionResult Menu() => View(new ServiceModel());
		public IActionResult PayOrder(int orderID) => View("OrderPayment", new OrderPaymentModel(orderID));

		public IActionResult PayOrderResult(int orderID, bool success) => View(
			"PaymentResult", new PaymentResultModel(new OrderPaymentModel(orderID).ProcessResult(success), true)
		);

		public IActionResult PayPenalty(int penaltyID, int customerID) => View("PenaltyPayment", new PenaltyPaymentModel(penaltyID, customerID));

		public IActionResult PayPenaltyResult(int penaltyID, int customerID, bool success) =>
			View("PaymentResult", new PaymentResultModel(new PenaltyPaymentModel(penaltyID, customerID).ProcessResult(success), customerID));

		public IActionResult Requests() => View("Requests", new RequestsModel());

		public IActionResult ReturnBorrowing(BorrowingReturnModel m) {
			m.ReturnToWarehouse();
			return View("BorrowingReturn", new BorrowingReturnModel(m.EmployeeID, m.ActiveCustomerID));
		}

		public IActionResult ReturnStock(StockReturnModel m) {
			if(!ModelState.IsValid)
				View("StockReturn", new StockReturnModel(m.EmployeeID));

			m.ReturnToLocation();
			return View("StockReturn", new StockReturnModel(m.EmployeeID));
		}

		public IActionResult ShowPDF(string filename) => PhysicalFile(FilePath(filename), "application/pdf");

		public IActionResult SubmitAddress(AddressModel m) {
			if(!ModelState.IsValid)
				return View("Address", new AddressModel(m.CustomerID));

			m.ChangeAddress();
			return View("Order3", new ExportOrderModel(m.CustomerID));
		}

		public IActionResult SubmitDamage(BorrowingReturnModel m) {
			m.AddDamage();
			return View("BorrowingReturn", new BorrowingReturnModel(m.EmployeeID, m.ActiveCustomerID, m.BorrowingID));
		}

		public IActionResult SubmitLocation(ImportProcessModel m) {
			if(!ModelState.IsValid)
				return View("ImportProcess3", new ImportProcessModel(m));

			m.ChangeLocation();
			return View("ImportProcess1", new ImportProcessModel(m.EmployeeID));
		}

		public IActionResult SubmitLogin(LoginModel m) => !ModelState.IsValid
				? View("Login", new LoginModel(m.Reason))
				: m.Reason switch {
					0 => View("Catalog", new CatalogModel(m.CustomerID)),
					1 => View("ImportProcess1", new ImportProcessModel(m.CustomerID)),
					2 => View("ExportProcess1", new ExportProcessModel(m.CustomerID)),
					3 => View("BorrowingReturn", new BorrowingReturnModel(m.CustomerID)),
					4 => View("StockReturn", new StockReturnModel(m.CustomerID)),
					5 => View("UnpaidPenalties", new UnpaidPenaltiesModel(m.CustomerID)),
					6 => View("UnpaidOrders", new UnpaidOrdersModel(m.CustomerID)),
					_ => throw new Exception($"Unknown login reason {m.Reason}.")
				};

		public IActionResult SubmitReturningCustomer(BorrowingReturnModel m) {
			if(!ModelState.IsValid)
				return View("BorrowingReturn", new BorrowingReturnModel(m.EmployeeID));
			return View("BorrowingReturn", new BorrowingReturnModel(m.EmployeeID, m.ActiveCustomerID));
		}

		public IActionResult SubmitScan(ImportProcessModel m) {
			if(!ModelState.IsValid)
				return View("ImportProcess2", new ImportProcessModel(m));

			m.FileUploaded(WriteFileFromForm());
			return View("ImportProcess3", new ImportProcessModel(m));
		}

		public IActionResult ToRequestForm(int customerID) => View("RequestForm", new RequestFormModel(customerID));
		public IActionResult ToUnpaidPenalties(int customerID) => View("UnpaidPenalties", new UnpaidPenaltiesModel(customerID));
	}
}
