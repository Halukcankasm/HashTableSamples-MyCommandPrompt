using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableSamples_MyommandPrompt
{

    class Islemler
    {
        public void EkranTemizlemek()
        {
            Console.Clear();
            Console.WriteLine("============FreeCommandStarted=============");
            Console.Beep();
        }

        public void IcerikGostermek(string path)
        {
            string[] files = Directory.GetFiles(path);

            foreach (string file in files) 
            {
                Console.WriteLine($">> {file}");
            }
        }

        public void KullanıcıAdi()
        {
            Console.WriteLine("Aktif Kullanıcı:"+Environment.UserName);
        }
    }

    public delegate void Islem();

    public delegate void ParametrikIslem(string parametre);


    class KomutYorumlayıcı
    {
        private Hashtable komutlar = null;
        private Islemler islemler = null;
        public KomutYorumlayıcı()
        { 
            komutlar = new Hashtable(3);

            islemler = new Islemler();

            komutlar.Add("clr", new Islem(islemler.EkranTemizlemek));
            komutlar.Add("who", new Islem(islemler.KullanıcıAdi));
            komutlar.Add("ls", new ParametrikIslem(islemler.IcerikGostermek));

        }

        public void Calistir(string komutAdi, string path = "")
        {
            if (komutlar.ContainsKey(komutAdi)) 
            {
                if (komutlar[komutAdi] is Islem)
                {
                    ((Islem)komutlar[komutAdi]).Invoke();
                }
                else if (komutlar[komutAdi] is ParametrikIslem)
                {
                    ((ParametrikIslem)komutlar[komutAdi]).Invoke(path);
                }
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Beep();
                Console.WriteLine($"'{komutAdi}', böyle bir komut bulunamadı.");
                Console.ResetColor();
            }
        }
    }
    internal class Program
    {
        

        static void Main(string[] args)
        {

            Islemler ıslemler = new Islemler();
            ıslemler.EkranTemizlemek();

            KomutYorumlayıcı interpreter = new KomutYorumlayıcı();

            while (true)            
            {
                string klavyeGirdisi = Console.ReadLine();
                string[] parcalar =  klavyeGirdisi.Split(' ');
                if (parcalar.Length == 1)
                {
                    interpreter.Calistir(parcalar[0]);
                }
                else if(parcalar.Length == 2)
                {
                    interpreter.Calistir(parcalar[0], parcalar[1]);
                }
            }


        }
    }
}
