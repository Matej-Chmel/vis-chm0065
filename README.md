# Vývoj informačních systémů
Projekt pro předmět Vývoj informačních systémů v zimním semestru 2021.

|||
| :-- | :-- |
| Jméno a příjmení | Matěj Chmel |
| Osobní číslo | CHM0065 |
| Téma | Evidence not |
| Skupina | C/03 |

## Analýza
Všech 6 artefaktů analýzy projektu je spojeno do jednoho souboru [vis-chm0065.pdf](vis-chm0065.pdf).

## Spuštění implementace
1. Otevřete [vis-chm0065.sln](vis-chm0065.sln)
2. Spusťte výchozí konfiguraci.
3. Systém čte data z XML. Pokud chcete využívat MariaDB, musíte ji nejprve nainstalovat.

## Spuštění MariaDB
1. Otevřete Powershell jako administrátor.
2. Nastavte spouštěcí politiku tímto příkazem: `Set-ExecutionPolicy RemoteSigned`
3. Spusťte skript [Start-MariaDB.ps1](Start-MariaDB.ps1)
4. Vyčkejte.
5. Skript otevře okno s procesem `mysqld`. Ten musí zůstat zapnutý po celou dobu běhu webového serveru.
6. Po ukončení práce můžete nastavit spouštěcí politiku zpět tímto příkazem: `Set-ExecutionPolicy Restricted`

### Požadavek o přístup k síti
Databáze je lokální a nemodifikuje registry hostujícího počítače. Pokud vás systém vyzve k povolení přístupu k síti, můžete jej odmítnout.

### Obnova dat MariaDB
1. Nastavte správně spouštěcí politiku.
2. Spusťte příkaz `Start-MariaDB.ps1 -Restore`.
3. Databáze bude spuštěna s daty ze [schema.sql](databases/schema.sql)
4. Pro obnovu XML můžete spustit skript [Restore-XML.ps1](Restore-XML.ps1)

## Kompatibilita s Visual Studiem 2022
Projekt byl vyvíjen ve VS2019. Pro spuštění přes VS2022 je nutné nainstalovat komponentu `.NET 5.0 Runtime`, kterou lze najít v seznamu individuálních komponent při modifikování VS2022 pomocí programu Visual Studio Installer.

## Příkladové PDF soubory
Ve složce [data](data) jsou soubory, kterými lze ověřit funkčnost nahrávání PDF souborů na server a jejich prohlížení. Server ukládá a čte soubory ze složky [uploads](src/Server/wwwroot/uploads), která ve výchozím stavu neobsahuje žádné PDF soubory.

## Licence
Projekt využívá externích knihoven a ikon. Licence jsou přiloženy uvnitř složky každé knihovny a ikon.
- [bootstrap](src/Server/wwwroot/lib/bootstrap/LICENSE)
- [Checkmark Circle](icons/LICENSE)
	- Dostupné na [svgrepo.com](https://www.svgrepo.com/svg/327693/checkmark-circle)
- [jquery](src/Server/wwwroot/lib/jquery/LICENSE.txt)
- [jquery-validation](src/Server/wwwroot/lib/jquery-validation/LICENSE.md)
- [jquery-validation-unobtrusive](src/Server/wwwroot/lib/jquery-validation-unobtrusive/LICENSE.txt)
- MariaDB
	- Licence se nachází v archivu [MariaDB.zip](databases/MariaDB.zip), který bude rozbalen při instalaci této databáze. Licence je poté dostupná pod cestou `databases/MariaDB/COPYING`.
