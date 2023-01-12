using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace FiveWordsFiveLetters
{
    public class FiveWords
    {
        public FiveWords()
        {

        }

        List<string> characters = new List<string>();
        Dictionary<int,string> dictionary = new Dictionary<int, string>();
        List<int> bitList = new List<int>();
        int fiveMatches = 0;

        public void gatherWords(string filepath)
        {
            //List<string> readFile = new List<string>();
            string dir = Directory.GetCurrentDirectory();
            string dirFilePath = System.IO.Path.Combine(dir + filepath);
            string[] readingFile = File.ReadAllLines(dirFilePath);

            for (int i = 0; i < readingFile.Count(); i++)
            {
                if (CheckLength(readingFile[i]) && CheckDouble(readingFile[i])) 
                {
                    FromString(readingFile[i]);
              //      readFile.Add(readingFile[i]);
                    
                }
            }

            //return readFile;
        }

        public int FiveWordsArray()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            gatherWords("\\Beta.txt");
         
            for (int i = 0; i <= bitList.Count(); i++) 
            {
                for (int k = i + 1; k < bitList.Count(); k++)
                {
                    if (string.Concat(bitList[i], bitList[k]).Distinct().Count() !=10) continue;
                    for (int c = k + 1; c < bitList.Count(); c++)
                    {
                        if (string.Concat(bitList[i], bitList[k], bitList[c]).Distinct().Count() != 15) continue;
                        for (int b = c + 1; b < bitList.Count(); b++)
                        {
                            if (string.Concat(bitList[i], bitList[k], bitList[c], bitList[b]).Distinct().Count() != 20) continue;
                            for (int d = b + 1; d < bitList.Count(); d++)
                            {
                                if (string.Concat(bitList[i], bitList[k], bitList[c], bitList[b], bitList[d]).Distinct().Count() != 25) continue;
                                Console.WriteLine($"Word1: {bitList[i]}, Word2: {bitList[k]} Word3: {bitList[c]}, Word4: {bitList[b]}, Word5: {bitList[d]}");
                                fiveMatches++;
                            }
                        }

                    }
                }
            }
            //for (int i = 0; i < readFile.Count(); i++)
            //{
            //    for (int k = i + 1; k < readFile.Count(); k++)
            //    {
            //        if (string.Concat(readFile[i], readFile[k]).Distinct().Count() != 10) continue;
            //        for (int c = k + 1; c < readFile.Count(); c++)
            //        {
            //            if (string.Concat(readFile[i], readFile[k], readFile[c]).Distinct().Count() != 15) continue;
            //            for (int b = c + 1; b < readFile.Count(); b++)
            //            {
            //                if (string.Concat(readFile[i], readFile[k], readFile[c], readFile[b]).Distinct().Count() != 20) continue;
            //                {
            //                    for (int d = b + 1; d < readFile.Count(); d++)
            //                    {
            //                        if (string.Concat(readFile[i], readFile[k], readFile[c], readFile[b], readFile[d]).Distinct().Count() != 25) continue;
            //                        Console.WriteLine($"Word1: {readFile[i]}, Word2: {readFile[k]} Word3: {readFile[c]}, Word4: {readFile[b]}, Word5: {readFile[d]}");
            //                        fiveMatches++;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            
            Console.WriteLine("FiveWordsMatches: " + fiveMatches);
            stopwatch.Stop();
            Console.WriteLine($"Done in {stopwatch.ElapsedMilliseconds} ms");
            return fiveMatches;
        }

        public void FromString(string word) 
        {
            int bit = 0;
            for (int i = 0; i < word.Length; i++)
            {
                bit |= 1 << (word[i] - 'a');
            }
            
            if( !dictionary.ContainsKey(bit))
            {
                dictionary.Add(bit, word);
                bitList.Add(bit);
            }
                
        }

        public bool CheckLength(string word)
        {
            int stringLength = word.Length;
            bool passed = true;

            if (stringLength != 5)
                passed = false;

            return passed;
        }

        public bool CheckDouble(string word) 
        {
            bool unMatched = true;
            int stringLength = word.Length;
            for (int q = 0; q < stringLength; q++)
            {
                for (int w = 0; w < stringLength; w++)
                {
                    if (word[q] == word[w] && q != w)
                    {
                        unMatched = false;
                    }

                }
            }
            return unMatched;
        }
    }
}
