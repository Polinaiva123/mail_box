using System;

namespace SingletonFactory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Skrzynka skrzynka1 = Skrzynka.Instance;
            Skrzynka skrzynka2 = Skrzynka.Instance;

            Console.WriteLine("Skrzynke 1 utworozono w " + skrzynka1.DataUtworzenia.ToString());
            Console.WriteLine("Skrzynke 2 utworozono w " + skrzynka2.DataUtworzenia.ToString());

            Console.WriteLine("Skrzynke 1 oraz Skrzynke 2 utworzono w " + (skrzynka1.DataUtworzenia.Equals(skrzynka2.DataUtworzenia) ? "ten sam czas" : "rozny czas"));
            Console.WriteLine("");
            skrzynka1.ZobaczWiadomosci();
        }
    }
}
