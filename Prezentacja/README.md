# Wzorzec Projektowy: Kompozyt (Composite)

## Schemat UML

Poniższy diagram UML przedstawia strukturę wzorca projektowego Kompozyt.

![Schemat diagramu klas wzorca Kompozyt](kompozyt_uml.png)

## Opis wzorca i elementów schematu

Wzorzec Kompozyt pozwala na traktowanie pojedynczych obiektów oraz ich kompozycji (zbiorów) w sposób jednolity. Przydaje się, gdy obiekty tworzą strukturę drzewiastą.

1. **`IKomponent` (Interfejs / Komponent abstrakcyjny)**  
   Definiuje wspólny interfejs dla wszystkich obiektów w strukturze drzewa – zarówno dla pojedynczych liści, jak i węzłów zawierających dzieci (kompozytów).
   - `WykonajDzialanie()` – główna metoda realizująca zadanie elementu.
   - `Dodaj(komponent)` i `Usun(komponent)` – metody do zarządzania strukturą hierarchiczną.

2. **`Liść` (`Lisc` / `Leaf`)**  
   Reprezentuje najniższy element struktury drzewa. Liść nie może posiadać własnych dzieci.
   - Implementuje metodę `WykonajDzialanie()`, zdefiniowaną w interfejsie `IKomponent` (zawiera logikę dla pojedynczego elementu).
   - Metody `Dodaj` i `Usun` zwykle w jego przypadku nie mają sensu – mogą np. rzucać wyjątek `NotSupportedException` (w przezroczystej implementacji Kompozytu).

3. **`Kompozyt` (`Kompozyt` / `Composite`)**  
   Reprezentuje element posiadający dzieci (zarówno inne Kompozyty, jak i Liście).
   - Posiada prywatną listę (kolekcję) elementów typu interfejsowego: `- dzieci: List<IKomponent>`.
   - W metodzie `WykonajDzialanie()` deleguje / wywołuje tę samą metodę kaskadowo dla każdego dziecka w pętli.
   - Implementuje logicznie podpięcie i usunięcie dziecka poprzez metody `Dodaj()` i `Usun()`.
