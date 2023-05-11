Tutorial od Microsoftu, który omawia tematykę wykładu/labów:
  - [Get started with EF Core in an ASP.NET MVC web app](https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-5.0)
  - Migracje są omawiane później, trzeba po lewej kliknąć w link "Migrations", żeby przejść do tamtego rozdziału tutoriala

Lista krok po kroku:
1. Tworzymy projekt MVC jak na liście 9 (upewniamy się, że mamy .NET 5)
2. Instalujemy pakiety (VS 2022 -> Project -> Manage NuGet Dependencies) (slajd 4):
    - `Microsoft.EntityFrameworkCore.SqlServer` w wersji `5.0.17`
    - `Microsoft.EntityFrameworkCore.Tools` w wersji `5.0.17`
    - Wybór wersji jest istotny, bo standardowo jest wybrana wersja najnowsza (a my korzystamy ze starego .NET, więc trzeba wybrać starszą, kompatybilną z nim), wszystkie z zakresu `5.0.x` powinny działać, ja wziąłem najnowsze
3. ==OPCJONALNIE== Włączamy wartości nullowalne (`<Nullable>Enable</Nullable>` w  pliku`.csproj`)
4. Tworzymy klasy **domenowe** `Category`, `Article` (namespace `XYZ.Models`), pamiętając o [odpowiednich atrybutach](https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/data-annotations) (slajd 10):
    - `Key` (jeżeli właściwość nazywa się `Id` z automatu działa jako `Key`) 
      - `Article` powinien mieć też oczywiście pole `Id` (co nie jest wymienione na liście)
    - `ForeignKey` (jeżeli wartość nazywa się `CategoryId`, działa z automatu)
    - `Required`, `MinLength`/`MaxLength`, `Range`, itd.
5. Tworzymy **klasę kontekstu** (namespace `XYZ.Data`) (slajd 10):
    - Dodajemy pusty kontruktor jak na wykładzie
    - Dodajemy właściwości typu `DbSet<Article>`, `DbSet<Category>`
6. Tworzymy bazę danych:
    1. VS 2022 -> View -> SQL Server Object Explorer (slajd 9)
    2. Explorer -> `(localdb)\MSSQLLocalDB` -> Databases -> PPM -> Add New Database
    3. CONNECTION STRING: `Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=<NAZWA_BAZY>;Integrated Security=True`
    4. W `appsettings.json` ustawiamy connection string (slajd 14)
    5. W `ConfigureServices` rejestrujemy kontekst (slajd 14)
7. Wykonujemy początkową migrację:
    1. VS 2022 -> Tools -> NuGet Package Manager -> Package Manager Console
    2. `Add-Migration Init` (slajd 16)
    3. `Update-Database`
    4. Sprawdzamy w SQL Server Object Explorer, czy tabele utworzyły się poprawnie, w szczególności ich:
        - Klucze główne i obce
        - Typy danych i nullowalność
    5. W przypadku pomyłek, trzeba poprawić klasy modeli domenowych, poprawnym rozwiązaniem byłoby zrobienie nowej migracji, ale jako, że baza i tak jest pusta i nieużywana, możemy ją usunąć i stworzyć na nowo, żeby nadmiernie nie komplikować, komendami:
        - `Drop-Database` (usuwa zawartość bazy!!!)
        - `Remove-Migration`
        - Wprowadzamy poprawki w modelach domenowych
        - `Add-Migration Init`
        - `Update-Database`
8. Tworzymy **kontrolery** (slajd 29)
    1. VS 2022 -> Add Item -> MVC Controller with views, using Entity Framework
    2. Wybieramy klasę modelu: `Category`
    3. Wybieramy kontekst (ten jedyny, który stworzyliśmy poprzedni)
    4. Wszystkie checkboxy zaznaczone (puste pole layoutu, wtedy wykorzysta się domyślny)
    5. Podajemy nazwę jeżeli domyślna nam nie pasuje (moim zdaniem domyślna jest git)
    6. Kroki 1-5 powtarzamy dla modelu `Article`
    7. Sprawdzenie:
        1. Odpalamy serwer
        2. Przechodzimy na stronę [/Categories](https://localhost:44360/Categories) (artykuł ma obligatoryjną kategorię, także przed stworzeniem artykułu musimy stworzyć co najmniej jedną kategorię) -> Create New -> podajemy nazwę i tworzymy
        3. Przechodzimy na stronę [/Articles](https://localhost:44360/Articles) i tworzymy nowy artykuł, też powinno być wszystko ok
        4. Sprawdzamy detale, edycję i usuwanie kategorii i artykułów
        5. **Restartujemy serwer**
        6. Nasze artykuły i kategorie zostały zachowane pomimo poprzedniego zamknięcia serwera, czyli baza danych działa
9. Pozostaje realizacja podpunktów 3-6, ale jest to już same pisanie kodu i nie trzeba nic wiedzieć o bazach danych, konfiguracjach, migracjach itd.