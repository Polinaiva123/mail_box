namespace AdapterFacade
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            KlientPoczty klient = KlientPoczty.Instance;

            klient.UtworzSkrzynke();
            klient.UtworzSkrzynke();
            klient.ZaladujPrzykladoweWiadomosci();
            klient.WybierzSkrzynke();
            klient.UsunSkrzynke();
            klient.ZobaczWiadomosci();

        }
    }
}
