using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace namematch
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentdir = Directory.GetCurrentDirectory() + "\\";
            Console.WriteLine("Yeah so this Program basically filters messages out of Discord Dumps");
            Console.WriteLine("Idea def. not stolen from Mr Feelsexy Guy");
            Console.Write("Name: ");
            bool test = false;
            string name = Console.ReadLine();
            Console.WriteLine("Put your input File into the Folder of this program");
            Console.WriteLine("Dont type the full path. only the name of the input file");
            Console.Write("Inputfile:");
            string filein = currentdir + Console.ReadLine();
            Console.WriteLine("Name the output file");
            Console.Write("Outputfile");
            string fileout = currentdir + Console.ReadLine();
            
            string[] linesf = File.ReadAllLines(filein);
            Stats.LineCount = linesf.Length;
            List<string> RW = new List<string>();
            foreach (string line in linesf)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    Stats.WSLines++;
                    continue;
                }
                RW.Add(line);
            }
            string[] lines = RW.ToArray();
            List<string> LinesL = new List<string>();
            foreach (string linenc in lines)
            {
                if (test)
                {
                    Console.WriteLine(linenc);
                    Stats.nameLines++;
                    LinesL.Add(linenc);
                    
                }
                if (Regex.IsMatch(linenc, "[[][0-9]{2}[-][a-z]{3}[-][0-9]{2} [0-9]{2}[:][0-9]{2} [A|P][M][]] " + name, RegexOptions.IgnoreCase))
                {
                    test = true;
                    continue;
                }
                test = false;


            }
            string[] LinesA = LinesL.ToArray();
            List<string> FilteredLines = new List<string>();
            foreach (string linenc in LinesA)
            {
                if (linenc.StartsWith("http://") || linenc.StartsWith("https://"))
                {
                    Stats.LinkLines++;
                    continue;
                }
                FilteredLines.Add(linenc);
            }
            string[] FilteredLinesA = FilteredLines.ToArray();
            foreach (string a in FilteredLinesA)
            {
                
            }
            Stats.PrintStats(name);
            File.WriteAllLines(fileout, FilteredLinesA);
            Console.ReadKey();
            
        }
        
    }
    static class Stats
    {
        public static int nameLines = 0;
        public static int WSLines = 0;
        public static int LinkLines = 0;
        public static int LineCount = 0;
        public static void PrintStats(string name)
        {
            for (int i = 0; i != 5; i++) { Console.WriteLine("\n"); }
            Console.WriteLine("Total Lines: " + LineCount);
            Console.WriteLine($"Lines by {name}: {nameLines}");
            Console.WriteLine($"Lines that were Whitespace: {WSLines}");
            Console.WriteLine($"Lines by {name} that were Links: {LinkLines}");
        }
    }
}
