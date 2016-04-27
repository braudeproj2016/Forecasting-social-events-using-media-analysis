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
            Console.OutputEncoding = Encoding.Unicode;
            //System.Console.WriteLine("Heelo World!");
            //System.Console.Write("Hi");
            preparefile2();
            //runit();
        }
        public static void preparefile1()
        {
            StreamReader sr = new StreamReader("stopwords.txt");
            List<string> listOfStopWords = new List<string>();
            while (!sr.EndOfStream)
                listOfStopWords.Add(sr.ReadLine());
            sr.Dispose();
            sr = File.OpenText(@"newsp1/1.txt");
            string s;
            s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");

            foreach (string word in listOfStopWords)
            {
                s = s.Replace(" " + word + " ", " ");
            }
            string s1 = Regex.Replace(s, @"\s+", " ");

            while (!sr.EndOfStream)
            {
                s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");
                foreach (string word in listOfStopWords)
                {
                    s = s.Replace(" " + word + " ", " ");
                }
                s1 += " " + s;
            }
            s1 = Regex.Replace(s1, @"\s+", " ");
            sr.Dispose();

            var tmpht = new Dictionary<string, int>();
            for (int i = 0; i < s1.Length - 2; i++)
            {
                var tmpSt = "" + s1[i] + s1[i + 1] + s1[i + 2];
                if (tmpht.ContainsKey(tmpSt))
                    tmpht[tmpSt] += 1;
                else
                    tmpht.Add(tmpSt, 1);
            }

            var items = from pair in tmpht
                        orderby pair.Value descending
                        select pair;
            int k = 0;
            var nGrams = new Dictionary<string, int>();
            var htp1n1 = new Dictionary<string, int>();
            var htp1n2 = new Dictionary<string, int>();
            var htp1n3 = new Dictionary<string, int>();
            var htp2n1 = new Dictionary<string, int>();
            var htp2n2 = new Dictionary<string, int>();
            var htp2n3 = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in items)
            {
                nGrams.Add(pair.Key, 0);
                htp1n1.Add(pair.Key, pair.Value);
                htp1n2.Add(pair.Key, 0);
                htp1n3.Add(pair.Key, 0);
                htp2n1.Add(pair.Key, 0);
                htp2n2.Add(pair.Key, 0);
                htp2n3.Add(pair.Key, 0);
                if (k < (tmpht.Count / 10))
                    k++;
                else
                    break;
            }
          //  Console.WriteLine(tmpht.Count);

            ///second newspaper
            sr = File.OpenText(@"newsp1/2.txt");
            s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");

            foreach (string word in listOfStopWords)
            {
                s = s.Replace(" " + word + " ", " ");
            }
            s1 = Regex.Replace(s, @"\s+", " ");

            while (!sr.EndOfStream)
            {
                s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");
                foreach (string word in listOfStopWords)
                {
                    s = s.Replace(" " + word + " ", " ");
                }
                s1 += " " + s;
            }
            s1 = Regex.Replace(s1, @"\s+", " ");
            sr.Dispose();

            for (int i = 0; i < s1.Length - 2; i++)
            {
                var tmpSt = "" + s1[i] + s1[i + 1] + s1[i + 2];
                if (htp1n2.ContainsKey(tmpSt))
                    htp1n2[tmpSt] += 1;
            }
            ///third newspaper
            sr = File.OpenText(@"newsp1/3.txt");
            s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");

            foreach (string word in listOfStopWords)
            {
                s = s.Replace(" " + word + " ", " ");
            }
            s1 = Regex.Replace(s, @"\s+", " ");

            while (!sr.EndOfStream)
            {
                s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");
                foreach (string word in listOfStopWords)
                {
                    s = s.Replace(" " + word + " ", " ");
                }
                s1 += " " + s;
            }
            s1 = Regex.Replace(s1, @"\s+", " ");
            sr.Dispose();

            for (int i = 0; i < s1.Length - 2; i++)
            {
                var tmpSt = "" + s1[i] + s1[i + 1] + s1[i + 2];
                if (htp1n3.ContainsKey(tmpSt))
                    htp1n3[tmpSt] += 1;
            }
            ///1 newspaper2
            sr = File.OpenText(@"newsp2/1.txt");
            s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");

            foreach (string word in listOfStopWords)
            {
                s = s.Replace(" " + word + " ", " ");
            }
            s1 = Regex.Replace(s, @"\s+", " ");

            while (!sr.EndOfStream)
            {
                s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");
                foreach (string word in listOfStopWords)
                {
                    s = s.Replace(" " + word + " ", " ");
                }
                s1 += " " + s;
            }
            s1 = Regex.Replace(s1, @"\s+", " ");
            sr.Dispose();

            for (int i = 0; i < s1.Length - 2; i++)
            {
                var tmpSt = "" + s1[i] + s1[i + 1] + s1[i + 2];
                if (htp2n1.ContainsKey(tmpSt))
                    htp2n1[tmpSt] += 1;
            }
            ///2 newspaper2
            sr = File.OpenText(@"newsp2/2.txt");
            s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");

            foreach (string word in listOfStopWords)
            {
                s = s.Replace(" " + word + " ", " ");
            }
            s1 = Regex.Replace(s, @"\s+", " ");

            while (!sr.EndOfStream)
            {
                s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");
                foreach (string word in listOfStopWords)
                {
                    s = s.Replace(" " + word + " ", " ");
                }
                s1 += " " + s;
            }
            s1 = Regex.Replace(s1, @"\s+", " ");
            sr.Dispose();

            for (int i = 0; i < s1.Length - 2; i++)
            {
                var tmpSt = "" + s1[i] + s1[i + 1] + s1[i + 2];
                if (htp2n2.ContainsKey(tmpSt))
                    htp2n2[tmpSt] += 1;
            }
            ///1 newspaper3
            sr = File.OpenText(@"newsp2/3.txt");
            s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");

            foreach (string word in listOfStopWords)
            {
                s = s.Replace(" " + word + " ", " ");
            }
            s1 = Regex.Replace(s, @"\s+", " ");

            while (!sr.EndOfStream)
            {
                s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");
                foreach (string word in listOfStopWords)
                {
                    s = s.Replace(" " + word + " ", " ");
                }
                s1 += " " + s;
            }
            s1 = Regex.Replace(s1, @"\s+", " ");
            sr.Dispose();

            for (int i = 0; i < s1.Length - 2; i++)
            {
                var tmpSt = "" + s1[i] + s1[i + 1] + s1[i + 2];
                if (htp2n3.ContainsKey(tmpSt))
                    htp2n3[tmpSt] += 1;
            }
            ///sorting all
            var template = from pair in nGrams
                         orderby pair.Value descending
                         select pair;
            var items1 = from pair in htp1n1
                         orderby pair.Value descending
                         select pair;
            var items2 = from pair in htp1n2
                         orderby pair.Value descending
                         select pair;
            var items3 = from pair in htp1n3
                         orderby pair.Value descending
                         select pair;

            /*           var items4 = from pair in htp2n1
                                    orderby pair.Value descending
                                    select pair;
                       var items5 = from pair in htp2n2
                                    orderby pair.Value descending
                                    select pair;
                       var items6 = from pair in htp2n3
                                    orderby pair.Value descending
                                    select pair;
           */
            double sum = 0;
            double[] arr = new double[10];
            int index1,index2,index3;
            index1 = 0;
            foreach (KeyValuePair<string, int> pair1 in template)
            {
                index1++;
                index2 = 0;
                index3 = 0;
                foreach (KeyValuePair<string, int> pair2 in items1)
                {
                    index2++;
                    if (pair1.Key.Equals(pair2.Key))
                        break;
                }
                foreach (KeyValuePair<string, int> pair2 in items2)
                {
                    index3++;
                    if (pair1.Key.Equals(pair2.Key))
                        break;
                }
                sum += Math.Pow((Math.Abs(index1 - index2) - Math.Abs(index1 - index3)), 2);

            }
            arr[0] = (1 - sum * 6 / ((Math.Pow(nGrams.Count, 2) - 1) * nGrams.Count));
            foreach (KeyValuePair<string, int> pair1 in template)
            {
                index1++;
                index2 = 0;
                index3 = 0;
                foreach (KeyValuePair<string, int> pair2 in items1)
                {
                    index2++;
                    if (pair1.Key.Equals(pair2.Key))
                        break;
                }
                foreach (KeyValuePair<string, int> pair2 in items3)
                {
                    index3++;
                    if (pair1.Key.Equals(pair2.Key))
                        break;
                }
                sum += Math.Pow((Math.Abs(index1 - index2) - Math.Abs(index1 - index3)), 2);

            }
            arr[1] = (1 - sum * 6 / ((Math.Pow(nGrams.Count, 2) - 1) * nGrams.Count));

            Console.Read();
        }

        public static void preparefile2()
        {
            StreamReader sr = new StreamReader("stopwords.txt");
            List<string> listOfStopWords = new List<string>();
            while (!sr.EndOfStream)
                listOfStopWords.Add(sr.ReadLine());
            sr.Dispose();

            string[] array1 = Directory.GetFiles(@"texts\");

            List<string> listOfNewspapers = new List<string>();
            foreach (string name in array1)
            {
                listOfNewspapers.Add(file2String(name, listOfStopWords));
            }
            Console.WriteLine("--- read files ---");
            
            var template = new Dictionary<string, int>();
            for (int i = 0; i < listOfNewspapers[0].Length - 2; i++)
            {
                var tmpSt = "" + listOfNewspapers[0][i] + listOfNewspapers[0][i + 1] + listOfNewspapers[0][i + 2];
                if (template.ContainsKey(tmpSt))
                    template[tmpSt] += 1;
                else
                    template.Add(tmpSt, 1);
            }
            //IOrderedEnumerable<string,int>
            var items = from pair in template
                        orderby pair.Value descending
                        select pair;

            int k = 0;
            var nGrams = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in items)
            {
                nGrams.Add(pair.Key, pair.Value);
                if (k < (template.Count / 10))
                    k++;
                else
                    break;
            }
            Console.WriteLine("--- first nGram Done ---");

            //List<List<KeyValuePair<string, int>>> hist = new List<List<KeyValuePair<string, int>>>();

            
            foreach (string newspaper in listOfNewspapers)
            {
                var tmpHT = new Dictionary<string, int>();
                for (int i = 0; i < newspaper.Length - 2; i++)
                {
                    var tmpSt = "" + newspaper[i] + newspaper[i + 1] + newspaper[i + 2];
                    if (template.ContainsKey(tmpSt) && tmpHT.ContainsKey(tmpSt))
                        tmpHT[tmpSt] += 1;
                    else
                        if (template.ContainsKey(tmpSt))
                            tmpHT.Add(tmpSt, 1);
                }

                var tmpItems = from pair in tmpHT
                            orderby pair.Value descending
                            select pair;

                //   typeof(tmpItems);
                Console.WriteLine(tmpItems.GetType());

              //  ht.Add(tmpItems);
            }


            Console.Read();
        }

        public static string file2String(string fileName, List<string> stopWords)
        {
            var sr = File.OpenText(fileName);
            string s;
            s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");

            foreach (string word in stopWords)
            {
                s = s.Replace(" " + word + " ", " ");
            }
            string s1 = Regex.Replace(s, @"\s+", " ");

            while (!sr.EndOfStream)
            {
                s = Regex.Replace(sr.ReadLine().ToLower(), @"[^\w\s]", " ");
                foreach (string word in stopWords)
                {
                    s = s.Replace(" " + word + " ", " ");
                }
                s1 += " " + s;
            }
            s1 = Regex.Replace(s1, @"\s+", " ");
            sr.Dispose();
            return s1;
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

            Console.Read();
        }
    }
}
