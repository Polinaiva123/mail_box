namespace AdapterFacade
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Wyświetla wiadomości w postaci HTML
            // var klient = KlientPoczty.Instance;

            // Wyświetla wiadomości w postaci tekstu
            var klient = (KlientPocztyTekstowy) KlientPocztyTekstowy.Instance;

            klient.UtworzSkrzynke();
            klient.UtworzSkrzynke();
            klient.ZaladujPrzykladoweWiadomosci();
            klient.WybierzSkrzynke();
            klient.UsunSkrzynke();
            klient.ZobaczWiadomosci();

        }
    }
}
