using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

using static System.Console;

namespace ConsoleApplication1
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            //  Program.preparefile();
            StreamReader sr = File.OpenText("out2.txt");
            string text="";
            while (!sr.EndOfStream)
                text = text + " " + sr.ReadLine();
            sr.Dispose();

            //WriteLine(text);
            nGram[] ht = new nGram[4567];
          //  nGram tmpNG = null;
            WriteLine(ht[1]);
            string tmpSt;
            int tmp;
            for (int i = 0; i < text.Length - 2; i++)
            {
                tmpSt = "" + text[i] + text[i + 1] + text[i + 2];
                tmp =hashValue(tmpSt, 0);
                nGram tmpNG;//= ht[tmp];
                //tmpNG = &ht[tmp];
                update(out tmpNG, ref ht[tmp]); //<<===

                while (tmpNG != null && !(tmpNG.text.Equals(tmpSt)))
                    tmpNG = tmpNG.next;
                if (tmpNG == null)
                    tmpNG = new nGram(tmpSt);
                else
                    tmpNG.inc();
            }
            Read();
        }

        public static void update(out nGram a,ref nGram b)
        {
            a = b;
        }

        public static int hashValue (string st, int i)
        {
            return (Convert.ToInt32(st[i]) * 3 + Convert.ToInt32(st[i + 1]) * 10 + Convert.ToInt32(st[i + 2]) * 4) % 4567;
        }

        public static void preparefile()
        {
            Console.OutputEncoding = Encoding.Unicode;
            StreamReader sr = new StreamReader("stopwords.txt");
            List<string> listOfStopWords = new List<string>();
            while (!sr.EndOfStream)
                listOfStopWords.Add(sr.ReadLine());
            sr.Dispose();
            StreamReader sr2 = File.OpenText("tmp.txt");
            StreamWriter sw = File.CreateText("out2.txt");
            string s;
            while (!sr2.EndOfStream)
            {
                s = Regex.Replace(sr2.ReadLine().ToLower(), @"[^\w\s]", " ");
                foreach (string word in listOfStopWords)
                {
                    s = s.Replace(" " + word + " ", " ");
                }
                sw.WriteLine(Regex.Replace(s, @"\s+", " "));
            }
            sr2.Dispose();
            sw.Dispose();
        }



    }
   
}
