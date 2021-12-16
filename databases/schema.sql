-- --------------------------------------------------------
-- Hostitel:                     127.0.0.1
-- Verze serveru:                10.6.5-MariaDB - mariadb.org binary distribution
-- OS serveru:                   Win64
-- HeidiSQL Verze:               11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Exportování struktury databáze pro
DROP DATABASE IF EXISTS `vis`;
CREATE DATABASE IF NOT EXISTS `vis` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `vis`;

-- Exportování struktury pro tabulka vis.borrowing
DROP TABLE IF EXISTS `borrowing`;
CREATE TABLE IF NOT EXISTS `borrowing` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `customer` int(11) NOT NULL,
  `late` tinyint(1) NOT NULL DEFAULT 0,
  `lost` tinyint(1) NOT NULL DEFAULT 0,
  `order_item` int(11) NOT NULL,
  `returned` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  KEY `FK_borrowing_customer` (`customer`),
  KEY `FK_borrowing_order_item` (`order_item`),
  CONSTRAINT `FK_borrowing_customer` FOREIGN KEY (`customer`) REFERENCES `customer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_borrowing_order_item` FOREIGN KEY (`order_item`) REFERENCES `order_item` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

-- Exportování dat pro tabulku vis.borrowing: ~0 rows (přibližně)
DELETE FROM `borrowing`;
/*!40000 ALTER TABLE `borrowing` DISABLE KEYS */;
/*!40000 ALTER TABLE `borrowing` ENABLE KEYS */;

-- Exportování struktury pro tabulka vis.branch
DROP TABLE IF EXISTS `branch`;
CREATE TABLE IF NOT EXISTS `branch` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Exportování dat pro tabulku vis.branch: ~2 rows (přibližně)
DELETE FROM `branch`;
/*!40000 ALTER TABLE `branch` DISABLE KEYS */;
INSERT INTO `branch` (`id`) VALUES
	(1),
	(2);
/*!40000 ALTER TABLE `branch` ENABLE KEYS */;

-- Exportování struktury pro tabulka vis.customer
DROP TABLE IF EXISTS `customer`;
CREATE TABLE IF NOT EXISTS `customer` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `address` varchar(256) DEFAULT NULL,
  `branch` int(11) DEFAULT NULL,
  `job` varchar(32) DEFAULT NULL,
  `username` varchar(32) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`),
  KEY `FK_customer_branch` (`branch`),
  CONSTRAINT `FK_customer_branch` FOREIGN KEY (`branch`) REFERENCES `branch` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4;

-- Exportování dat pro tabulku vis.customer: ~7 rows (přibližně)
DELETE FROM `customer`;
/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
INSERT INTO `customer` (`id`, `address`, `branch`, `job`, `username`) VALUES
	(1, 'Rudná 341/17 Ostrava 70020', NULL, NULL, 'chm0065'),
	(2, NULL, NULL, NULL, 'uzivatel'),
	(3, NULL, NULL, NULL, 'Jan Novák'),
	(4, 'U Slunců 57/404 České Budějovice 415 19', 1, 'Pokladní', 'pokladni1'),
	(5, 'Sadová 21/15 České Budějovice 415 19', 1, 'Skladník', 'skladnik1'),
	(6, 'Navrátilova 14 Praha 9 100 14', 2, 'Pokladní', 'pokladni2'),
	(7, 'Hlavní 100 Praha 9 100 15', 2, 'Skladník', 'skladnik2');
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;

-- Exportování struktury pro tabulka vis.order
DROP TABLE IF EXISTS `order`;
CREATE TABLE IF NOT EXISTS `order` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `amount` int(11) DEFAULT NULL,
  `completed` tinyint(1) DEFAULT NULL,
  `confirmed` tinyint(1) NOT NULL DEFAULT 0,
  `customer` int(11) DEFAULT NULL,
  `has_delivery` tinyint(1) DEFAULT NULL,
  `online` tinyint(1) DEFAULT NULL,
  `paid` tinyint(1) DEFAULT NULL,
  `type` varchar(32) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_order_customer` (`customer`),
  CONSTRAINT `FK_order_customer` FOREIGN KEY (`customer`) REFERENCES `customer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

-- Exportování dat pro tabulku vis.order: ~0 rows (přibližně)
DELETE FROM `order`;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
/*!40000 ALTER TABLE `order` ENABLE KEYS */;

-- Exportování struktury pro tabulka vis.order_item
DROP TABLE IF EXISTS `order_item`;
CREATE TABLE IF NOT EXISTS `order_item` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `copy` tinyint(1) NOT NULL DEFAULT 0,
  `order` int(11) NOT NULL,
  `ready` tinyint(1) NOT NULL DEFAULT 0,
  `stock` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_order_item_order` (`order`),
  KEY `FK_order_item_stock` (`stock`),
  CONSTRAINT `FK_order_item_order` FOREIGN KEY (`order`) REFERENCES `order` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_order_item_stock` FOREIGN KEY (`stock`) REFERENCES `stock` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

-- Exportování dat pro tabulku vis.order_item: ~0 rows (přibližně)
DELETE FROM `order_item`;
/*!40000 ALTER TABLE `order_item` DISABLE KEYS */;
/*!40000 ALTER TABLE `order_item` ENABLE KEYS */;

-- Exportování struktury pro tabulka vis.penalty
DROP TABLE IF EXISTS `penalty`;
CREATE TABLE IF NOT EXISTS `penalty` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `amount` int(11) NOT NULL DEFAULT 0,
  `borrowing` int(11) NOT NULL,
  `customer` int(11) NOT NULL,
  `description` varchar(256) NOT NULL,
  `paid` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  KEY `FK_penalty_customer` (`customer`),
  KEY `FK_penalty_borrowing` (`borrowing`),
  CONSTRAINT `FK_penalty_borrowing` FOREIGN KEY (`borrowing`) REFERENCES `borrowing` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_penalty_customer` FOREIGN KEY (`customer`) REFERENCES `customer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

-- Exportování dat pro tabulku vis.penalty: ~0 rows (přibližně)
DELETE FROM `penalty`;
/*!40000 ALTER TABLE `penalty` DISABLE KEYS */;
/*!40000 ALTER TABLE `penalty` ENABLE KEYS */;

-- Exportování struktury pro tabulka vis.product
DROP TABLE IF EXISTS `product`;
CREATE TABLE IF NOT EXISTS `product` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `author` varchar(256) NOT NULL,
  `filename` varchar(255) DEFAULT NULL,
  `name` varchar(256) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

-- Exportování dat pro tabulku vis.product: ~0 rows (přibližně)
DELETE FROM `product`;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
/*!40000 ALTER TABLE `product` ENABLE KEYS */;

-- Exportování struktury pro tabulka vis.request
DROP TABLE IF EXISTS `request`;
CREATE TABLE IF NOT EXISTS `request` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `accepted` tinyint(1) NOT NULL DEFAULT 0,
  `customer` int(11) NOT NULL DEFAULT 0,
  `product` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  KEY `FK_request_customer` (`customer`),
  KEY `FK_request_product` (`product`),
  CONSTRAINT `FK_request_customer` FOREIGN KEY (`customer`) REFERENCES `customer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_request_product` FOREIGN KEY (`product`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

-- Exportování dat pro tabulku vis.request: ~0 rows (přibližně)
DELETE FROM `request`;
/*!40000 ALTER TABLE `request` DISABLE KEYS */;
/*!40000 ALTER TABLE `request` ENABLE KEYS */;

-- Exportování struktury pro tabulka vis.stock
DROP TABLE IF EXISTS `stock`;
CREATE TABLE IF NOT EXISTS `stock` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `borrowing` int(11) DEFAULT NULL,
  `branch` int(11) DEFAULT NULL,
  `copy` tinyint(1) NOT NULL DEFAULT 0,
  `deleted` tinyint(1) NOT NULL DEFAULT 0,
  `location` varchar(64) DEFAULT NULL,
  `product` int(11) NOT NULL,
  `reserved` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  KEY `FK_stock_branch` (`branch`),
  KEY `FK_stock_product` (`product`),
  KEY `FK_stock_borrowing` (`borrowing`),
  CONSTRAINT `FK_stock_borrowing` FOREIGN KEY (`borrowing`) REFERENCES `borrowing` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `FK_stock_branch` FOREIGN KEY (`branch`) REFERENCES `branch` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_stock_product` FOREIGN KEY (`product`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

-- Exportování dat pro tabulku vis.stock: ~0 rows (přibližně)
DELETE FROM `stock`;
/*!40000 ALTER TABLE `stock` DISABLE KEYS */;
/*!40000 ALTER TABLE `stock` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
