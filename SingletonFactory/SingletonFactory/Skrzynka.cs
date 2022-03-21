using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonFactory
{
    internal class Skrzynka
    {
        private static Skrzynka instance;
        private List<WiadomoscEmail> skrzynka;
        private DateTime dataUtworzenia;
        private Skrzynka()
        {
            dataUtworzenia = DateTime.Now;

            ZalacznikFactory zwyklyZalacznik = new ZwyklyZalacznikFactory();
            ZalacznikFactory zalacznikJakoPlik = new ZalacznikJakoPlikFactory();

            skrzynka = new List<WiadomoscEmail>
            {
                new WiadomoscEmail
                {
                    Od = "abc123@gmail.com",
                    Do = "123abc@outlook.com",
                    Tytul = "Hello, World!",
                    Tresc = "Hello, World!",
                    Zalaczniki = new List<Zalacznik>
                    {
                        zwyklyZalacznik.CreateZalacznik("dokument"),
                        zwyklyZalacznik.CreateZalacznik("dokument"),
                        zalacznikJakoPlik.CreateZalacznik("plik")
                    }
                },

                new WiadomoscEmail
                {
                    Od = "helloworld@gmail.com",
                    Do = "dawdaw@outlook.com",
                    Tytul = "Inny Hello, World!",
                    Tresc = "HelloWorldHelloWorldHelloWorld",
                    Zalaczniki = new List<Zalacznik>
                    {
                        zwyklyZalacznik.CreateZalacznik("dokument"),
                        zwyklyZalacznik.CreateZalacznik("obraz"),
                        zwyklyZalacznik.CreateZalacznik("obraz"),
                        zwyklyZalacznik.CreateZalacznik("obraz"),
                        zalacznikJakoPlik.CreateZalacznik("plik")
                    }
                },

                new WiadomoscEmail
                {
                    Od = "helloworld@gmail.com",
                    Do = "dawdaw@outlook.com",
                    Tytul = "Inny Hello, World!",
                    Tresc = "HelloWorldHelloWorldHelloWorld",
                    Zalaczniki = new List<Zalacznik>
                    {
                        zwyklyZalacznik.CreateZalacznik("dokument"),
                        zalacznikJakoPlik.CreateZalacznik("plik"),
                        zalacznikJakoPlik.CreateZalacznik("plik"),
                        zalacznikJakoPlik.CreateZalacznik("plik")
                    }
                }

            };
        }

        public void ZobaczWiadomosci()
        {
            Console.WriteLine("Wiadomosci(" + skrzynka.Count + ") w skrzynce: ");
            Console.WriteLine("");
            skrzynka.ForEach(w => Console.WriteLine(w.ToString() + "\n"));
        }

        public static Skrzynka Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Skrzynka();
                }

                return instance;
            }
        }

        public DateTime DataUtworzenia
        {
            get { return this.dataUtworzenia; }
        }
    }
}
