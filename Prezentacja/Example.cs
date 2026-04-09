using System;
using System.Collections.Generic;

namespace CompositePatternExample
{
    // 1. Interfejs Komponentu
    public interface IKomponent
    {
        void WykonajDzialanie();
        void Dodaj(IKomponent komponent);
        void Usun(IKomponent komponent);
    }

    // 2. Liść (Leaf)
    public class Lisc : IKomponent
    {
        private string _nazwa;

        public Lisc(string nazwa)
        {
            _nazwa = nazwa;
        }

        public void WykonajDzialanie()
        {
            // Logika dla pojedynczego elementu
            Console.WriteLine($"- Liść '{_nazwa}' wykonuje swoje działanie.");
        }

        public void Dodaj(IKomponent komponent)
        {
            // Dla liścia dodawanie elementów nie ma sensu
            throw new NotSupportedException("Nie można dodawać elementów do liścia.");
        }

        public void Usun(IKomponent komponent)
        {
            // Dla liścia usuwanie elementów nie ma sensu
            throw new NotSupportedException("Nie można usuwać elementów z liścia.");
        }
    }

    // 3. Kompozyt (Composite)
    public class Kompozyt : IKomponent
    {
        private string _nazwa;

        // prywatna lista dzieci (agregacja)
        private List<IKomponent> dzieci = new List<IKomponent>();

        public Kompozyt(string nazwa)
        {
            _nazwa = nazwa;
        }

        public void WykonajDzialanie()
        {
            Console.WriteLine($"\n[+] Kompozyt '{_nazwa}' wywołuje pętlę po dzieciach:");
            
            // pętla po 'dzieciach' - wywołanie rekurencyjne
            foreach (var dziecko in dzieci)
            {
                dziecko.WykonajDzialanie();
            }
        }

        public void Dodaj(IKomponent komponent)
        {
            dzieci.Add(komponent);
        }

        public void Usun(IKomponent komponent)
        {
            dzieci.Remove(komponent);
        }
    }

    // Kod kliencki (Przykład użycia)
    class Program
    {
        static void Main(string[] args)
        {
            // Budowa struktury drzewiastej (Kompozytu)
            IKomponent drzewo = new Kompozyt("Drzewo Główne");
            
            IKomponent wezelA = new Kompozyt("Węzeł A (z liśćmi)");
            wezelA.Dodaj(new Lisc("Liść 1"));
            wezelA.Dodaj(new Lisc("Liść 2"));

            IKomponent wezelB = new Kompozyt("Węzeł B (z liściem)");
            wezelB.Dodaj(new Lisc("Liść 3"));

            // Dodawanie węzłów i pojedynczych liści pod główny korzeń
            drzewo.Dodaj(wezelA);
            drzewo.Dodaj(wezelB);
            drzewo.Dodaj(new Lisc("Liść 4 - bezpośrednio w korzeniu"));

            // Wywołanie akcji dla całego drzewa
            drzewo.WykonajDzialanie();
        }
    }
}
