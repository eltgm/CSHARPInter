using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApp5.parser
{
    public class Unit
    {
        public lesson[] lessons { get; set; }
    }

    public class lesson
    {
        public int number { get; set; }
        public string name { get; set; }

        public lection[] lections { get; set; }
        public exam[] exam { get; set; }
    }

    public class exam
    {
        public string name { get; set; }
        public int lesson { get; set; }
        public string text { get; set; }
        public ranswers[] ranswers { get; set; }
    }

    public class lection
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public ans[] ans { get; set; }
        public int lesson { get; set; }
        public photo[] photos { get; set; }
        public opt[] opt { get; set; }
    }

    public class ranswers
    {
        public string ra { get; set; }
    }

    public class ans
    {
        public int num { get; set; }
    }

    public class opt
    {
        public string text { get; set; }
    }

    public class photo
    {
        public int lesson { get; set; }
        public int lec { get; set; }
        public string filepath { get; set; }
    }
}