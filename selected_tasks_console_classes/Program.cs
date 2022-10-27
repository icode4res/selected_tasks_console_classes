// See https://aka.ms/new-console-template for more information

using System.Net.NetworkInformation;

public class Program
{
    public static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Clear();
        Console.WriteLine("ConsoleGeräteverwaltung");
        Console.WriteLine();
        ///
        //Standorte anlegen
        Standort DV1 = new Standort("I", "1.04");
        Standort DV3 = new Standort("I", "1.08");
        // Geräte anlegen
        Gerät Lenovo1 = new Gerät("Mon105", "Lenovo", "Col24", DV1);
        Gerät Lenovo2 = new Gerät("Mon215", "Lenovo", "Col12", DV3);
        //Geräte ausgeben
        Console.WriteLine();
        Console.WriteLine("Gerät1: " + Lenovo1.ToString());
        Console.WriteLine();
        Console.WriteLine("Gerät2: " + Lenovo2);
        //Zur Überprüfung
        Console.WriteLine();
        Console.WriteLine("Standort DV1: " + DV1);
        Console.WriteLine();
        Console.WriteLine("Standort DV3: " + DV3);
        //Netzwerkgerät anlegen
        Netzwerkgerät pc1 = new Netzwerkgerät("PC217", "CSL", "i7", DV1, "40-B0-76-7B-D7-2E", "192.168.91.44");
        Netzwerkgerät pc2 = new Netzwerkgerät("PC218", "CSL", "i7", DV3, "0A-00-27-00-00-0C", "192.168.91.45");
        Netzwerkgerät pc3 = new Netzwerkgerät("PC219", "CSL", "i7", DV3, "0A-61-01-14-16-BC", "192.168.91.210");
        //Netzwerkgerät ausgeben
        Console.WriteLine();
        Console.WriteLine("PC1: " + pc1);
        Console.WriteLine();
        Console.WriteLine("PC2: " + pc2);
        Console.WriteLine();
        Console.WriteLine("PC3: " + pc3);
        Console.WriteLine();
        Console.ReadLine();
    }



    /// <summary>
    /// Klasse Standort deklarieren
    /// </summary>
    public class Standort
    {
        //Atribute
        private string Gebäude;
        private string Raum;
        //Konstruktor
        public Standort(string gebäude, string raum)
        {
            Gebäude = gebäude;
            Raum = raum;
        }
        //Methoden
        public override string ToString()
        {
            string str = "";
            str = str + "\nGebäude >> " + Gebäude;
            str = str + "\nRaum >> " + Raum;
            return str;
        }
    }



    /// <summary>
    /// Klasse Gerät deklarieren
    /// </summary>
    public class Gerät
    {
        //Atribute
        private string InventarNummer;
        private string Hersteller;
        private string Bezeichnung;
        private Standort Ort;
        //Konstruktor
        public Gerät(string inventarnummer, string hersteller, string bezeichnung, Standort ort)
        {
            InventarNummer = inventarnummer;
            Hersteller = hersteller;
            Bezeichnung = bezeichnung;
            Ort = ort;
        }
        //Methoden
        public override string ToString()
        {
            string str = "";
            str = str + "\nInventarNummer >> " + InventarNummer;
            str = str + "\nHersteller >> " + Hersteller;
            str = str + "\nBezeichnung >> " + Bezeichnung;
            str = str + "\nStandort: " + Ort;
            return str;
        }
    }



    /// <summary>
    /// Interface IErreichbar mit Methode GetStatus deklarieren
    /// </summary>
    public interface IErreichbar
    {
        public string GetStatus();
    }



    /// <summary>
    /// Klasse NetzwerkGerät erbt von Gerät und implementiert das Interface IErreichbar
    /// </summary>
    public class Netzwerkgerät : Gerät, IErreichbar
    {
        //Atribute
        private string MAC;
        private string IP;
        //Konstruktor
        public Netzwerkgerät(string inventarnummer, string hersteller, string bezeichnung, Standort ort, string mac, string ip)
                            : base(inventarnummer, hersteller, bezeichnung, ort)
        {
            // erste 4 Parameter werden mit base an die Basisklasse weitergereicht
            MAC = mac;
            IP = ip;
        }
        //Methoden
        public override string ToString()
        {
            //Mit base.ToString() ToString-Methode von Basisklasse Gerät aufrufen
            string str = base.ToString();
            str = str + "\nNetzwerk: ";
            str = str + "\nMAC >> " + MAC;
            str = str + "\nIP >> " + IP;
            str = str + "\nStatus >> " + GetStatus();
            return str;

        }
        public string GetStatus()
        {
            //Prüfe den Status der IP-Adresse
            PingReply reply = new Ping().Send(IP, 200);
            return reply.Status.ToString();
        }
    }
}
