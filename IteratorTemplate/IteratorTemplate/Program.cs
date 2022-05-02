Console.WriteLine(new PustaSkrzynka().ToString());
Console.WriteLine(new DomyslnaSkrzynka().ToString());

var wypelniona = new WypelnionaSkrzynka();

Console.WriteLine(wypelniona.ToString());

Console.WriteLine("Iterator:\n");

foreach (var wiadomosc in wypelniona)
{
    Console.WriteLine(wiadomosc.ToString());
}
