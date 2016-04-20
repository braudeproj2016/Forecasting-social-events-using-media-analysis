using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace v1
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Heelo World!");
            System.Console.Write("Hi");
            runit();
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

        public static int hashValue(string st, int i)
        {
            return (Convert.ToInt32(st[i]) * 3 + Convert.ToInt32(st[i + 1]) * 10 + Convert.ToInt32(st[i + 2]) * 4) % 4567;
        }

        public static void update(out nGram a, ref nGram b)
        {
            a = b;
        }

        static void runit()
        {
            Console.OutputEncoding = Encoding.Unicode;
            //  Program.preparefile();
            StreamReader sr = File.OpenText("out2.txt");
            var text = "";
            if (!sr.EndOfStream)
                text = sr.ReadLine();
            while (!sr.EndOfStream)
                text += " " + sr.ReadLine();
            sr.Dispose();

            var ht = new Dictionary<string, int>();
            for (int i = 0; i < text.Length - 2; i++)
            {
                var tmpSt = "" + text[i] + text[i + 1] + text[i + 2];
                if (ht.ContainsKey(tmpSt))
                    ht[tmpSt] += 1;
                else
                    ht.Add(tmpSt, 1);
            }
            /*
            var list = ht.Keys.ToList();
            list.Sort();
            var sw = File.CreateText("out3.txt");
            foreach (var key in list)
            {
                sw.WriteLine("{0}: {1}", key, ht[key]);
            }
            sw.Dispose();
            */
            var items = from pair in ht
                        orderby pair.Value descending
                        select pair;
            
            var sw = File.CreateText("out4.txt");
            Console.WriteLine(ht.Count);
            int k = 0;
            foreach (KeyValuePair<string, int> pair in items)
            {
                sw.WriteLine("{0}: {1}", pair.Key, pair.Value);
                if (k < (ht.Count / 10))
                    k++;
                else
                    break;
            }
            sw.Dispose();

            /*
            //WriteLine(text);
            nGram[] ht = new nGram[4567];
            //  nGram tmpNG = null;
            Console.WriteLine(ht[1]);
            string tmpSt;
            int tmp;
            for (int i = 0; i < ht.Length - 1; i++)
            {
                ht[0] = new nGram();
            }
            for (int i = 0; i < text.Length - 2; i++)
            { 
                tmpSt = "" + text[i] + text[i + 1] + text[i + 2];
                tmp = hashValue(tmpSt, 0);
                nGram tmpNG = ht[tmp];
                //Console.WriteLine(tmp);
                //tmpNG = &ht[tmp];
                // update(out tmpNG, ref ht[tmp]); //<<===our problem

                while (tmpNG != null && !(tmpNG.text.Equals(tmpSt)))
                    tmpNG = tmpNG.next;
                if (tmpNG == null)
                    tmpNG = new nGram(tmpSt);
                else
                    tmpNG.inc();
            }
            if (ht[57] == null)
                Console.WriteLine("it's null");
            else
                Console.WriteLine(ht[57].count);
            */
            Console.Read();
        }
    }
}
