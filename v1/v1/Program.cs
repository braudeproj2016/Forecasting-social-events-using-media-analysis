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
            preparefile();
            //runit();
        }

        public static void preparefile()
        {
            var listOfStopWords = readStopWords(@"C:\Users\Semion\Desktop\porj\stopwords.txt");

            var array1 = Directory.GetFiles(@"C:\Users\Semion\Desktop\porj\text1\");
            var array2 = Directory.GetFiles(@"C:\Users\Semion\Desktop\porj\text2\");

            var listOfNewspapers1 = readNewspapers(array1, listOfStopWords); // return newpaper
                                                                             // var listOfNewspapers2 = readNewspapers(array2, listOfStopWords);
            var listOfNewspapers2 = readNewspapers(array2, listOfStopWords);

            //var hh = histHandler(listOfNewspapers1[0], listOfNewspapers1[1]); // for zv only - return histograms 10% that we gonna use
            var hh = histHandler(listOfNewspapers1[0], listOfNewspapers2[0]); // for dzv only

            var spersmenPrepare1 = sortedHist(listOfNewspapers1, hh);
            var spersmenPrepare2 = sortedHist(listOfNewspapers2, hh);

            //reqZV(spersmenPrepare1, 5);
            reqDZV(spersmenPrepare1, spersmenPrepare2, 5);

            Console.Read();
        }

        public static List<string> readStopWords(string path)
        {
            StreamReader sr = new StreamReader(path);
            List<string> listOfStopWords = new List<string>();
            while (!sr.EndOfStream)
                listOfStopWords.Add(sr.ReadLine());
            sr.Dispose();
            return listOfStopWords;
        }

        public static void reqDZV(List<string[]> papersAsHist1, List<string[]> papersAsHist2, int T)
        {
            int endpapers = (papersAsHist1.Count > papersAsHist2.Count) ? papersAsHist2.Count : papersAsHist1.Count;
            for (int i = T; i < endpapers; i++)
            {
                double tmpval = 0;
                tmpval += ZV(papersAsHist1[i], papersAsHist1, i, T);
                tmpval += ZV(papersAsHist2[i], papersAsHist2, i, T);
                tmpval -= ZV(papersAsHist1[i], papersAsHist2, i, T);
                tmpval -= ZV(papersAsHist2[i], papersAsHist1, i, T);
                tmpval = Math.Abs(tmpval);
                Console.WriteLine(tmpval);
            }
        }

        public static void reqZV(List<string[]> papersAsHist,int T)
        {
            for (int i = T; i < papersAsHist.Count; i++)
            {
                double tmpval = ZV(papersAsHist[i], papersAsHist, i, T);
                Console.WriteLine(tmpval);
            }
        }

        public static List<string[]> sortedHist (List<string> listOfNewspapers, Dictionary<string,int> hh)
        {
            var spersmenPrepare = new List<string[]>();
            foreach (string newspaper in listOfNewspapers)
            {
                var tmpHT = hist(newspaper, hh);

                var tmpItems = from pair in tmpHT
                               orderby pair.Value descending
                               select pair;

                string[] stringsArray = new string[hh.Count];
                int counter = 0;
                foreach (KeyValuePair<string, int> pair in tmpItems)
                {
                    stringsArray[counter++] = String.Copy(pair.Key);
                }
                spersmenPrepare.Add(stringsArray);
            }
            return spersmenPrepare;
        }

        public static Dictionary<string, int> hist(string newspaper, Dictionary<string, int> nGrams)
        {
            var tmpHT = new Dictionary<string, int>();
            for (int i = 0; i < newspaper.Length - 2; i++)
            {
                var tmpSt = "" + newspaper[i] + newspaper[i + 1] + newspaper[i + 2];
                if (tmpHT.ContainsKey(tmpSt))
                    tmpHT[tmpSt] += 1;
                else
                    if (nGrams.ContainsKey(tmpSt))
                    tmpHT.Add(tmpSt, 1);
            }
            return tmpHT;
        }

        public static Dictionary<string, int> histHandler(string newPapers1, string newPapers2)
        {
            string np = newPapers1 + " " + newPapers2;
            var template = new Dictionary<string, int>();
            for (int i = 0; i < np.Length - 2; i++)
            {
                var tmpSt = "" + np[i] + np[i + 1] + np[i + 2];
                if (template.ContainsKey(tmpSt))
                    template[tmpSt] += 1;
                else
                    template.Add(tmpSt, 1);
            }
            //sorting
            var items = from pair in template
                        orderby pair.Value descending
                        select pair;
            // 10% of nGrams
            int k = 0;
            var nGrams = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in items)
            {
                nGrams.Add(pair.Key, 0);
                if (k < (template.Count / 10))
                    k++;
                else
                    break;
            }
            return nGrams;
        }

        public static double ZV(string[] Newspaper, List<string[]> allNewspapers, int from, int TWindow)
        {
            double zv = 0;
            for (int i = 0; i < TWindow; i++)
                zv += Spearman(Newspaper, allNewspapers[from - 1 - i]); 
            return (zv/TWindow);
        }

        public static List<string> readNewspapers(string[] arr, List<string> stopW)
        {
            List<string> listOfNewspapers = new List<string>();
            foreach (string name in arr)
            {
                listOfNewspapers.Add(file2String(name, stopW));
            }
            Console.WriteLine("--- read files ---");
            return listOfNewspapers;
        }

        public static double Spearman(string[] h1, string[] h2)
        {
            double sum = 0;
            for (int i = 0; i < h1.Length; i++)
                for (int j = 0; j < h2.Length; j++)
                    if (((h1[i] != null) && (h2[j] != null)) && (h1[i].Equals(h2[j])))
                    {
                        sum += Math.Pow((i - j), 2);
                        break;
                    }
            return (1 - (6 * sum) / ((Math.Pow(h1.Length, 2) - 1) * h1.Length));
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

    }
}
