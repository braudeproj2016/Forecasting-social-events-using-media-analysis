using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class nGram
    {
        protected readonly string s;
        protected int c;
        protected nGram n;
        static int amunt = 0;
        public nGram(string s)
        {
            this.s = s;
            c = 1;
            n = null;
            amunt++;
        }
        public void inc()
        {
            c++;
        }
        public string text
        {
            get
            {
                return s;
            }
        }
        public int count
        {
            get
            {
                return c;
            }
        }
        public nGram next
        {
            get
            {
                return n;
            }
            set
            {
                n = value;
            }
        }
        public nGram operator =(nGram ng)
        {
            this.c = ng.c;
            this.n = ng.n;
            this.t = ng.s;
        }
    }
}
