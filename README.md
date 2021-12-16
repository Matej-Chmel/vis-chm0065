# Vývoj informačních systémů
Projekt pro předmět Vývoj informačních systémů v zimním semestru 2021.

|||
| :-- | :-- |
| Osobní číslo | CHM0065      |
| Téma         | Evidence not |

## Analýza
Všech 6 artefaktů analýzy projektu je spojeno v jeden soubor [vis-chm0065.pdf](vis-chm0065.pdf).

## Spuštění MariaDB
Pro zapnutí MariaDB je potřeba nejdříve spustit skript [Start-MariaDB.ps1](Start-MariaDB.ps1). Ten ji nainstaluje z archivu [MariaDB.zip](databases/MariaDB.zip) do složky `databases/MariaDB`. Databáze je lokální a nemodifikuje registry hostujícího počítače. Skript nakonec otevře okno s procesem `mysqld`. Ten musí zůstat zapnutý po celou dobu běhu webového serveru.

### Požadavek o přístup k síti
Databáze funguje lokálně. Pokud vás systém vyzve k povolení přístupu k síti, můžete jej odmítnout.

### Funkčnost bez MariaDB
Bez spuštění MariaDB lze stále používat evidenci not přes XML úložiště.

### Obnova dat
Pro obnovu dat do původní podoby, jak je popsáno ve [schema.sql](databases/schema.sql) je možné skriptu [Start-MariaDB.ps1](Start-MariaDB.ps1) poskytnout parametr `-Restore`. Skript [Restore-XML.ps1](Restore-XML.ps1) obnovuje XML soubory. Nepřijímá žádné parametry.

## Spuštění webového serveru
Otevřením souboru řešení [vis-chm0065.sln](vis-chm0065.sln) a spuštěním projektu Server s konfigurací IIS Express se otevře okno internetového prohlížeče s webovou stránkou evidence not.

### Kompatibilita s Visual Studiem 2022
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
	- Licence se nachází v archivu [MariaDB.zip](databases/MariaDB.zip). Po rozbalení je dostupná pod cestou `databases/MariaDB/COPYING`.
