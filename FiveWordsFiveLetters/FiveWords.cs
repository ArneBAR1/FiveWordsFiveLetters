using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using static System.Collections.Specialized.BitVector32;

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
        Dictionary<string, int> alphabetDictionary = new Dictionary<string, int>();
        int value = 1;

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
                }
            }

            // & dem sammen vis den ikke giver 0 er bogstavet i ordet.
            // Brug sortedAlphabet og bitlist til at match sammen.
            // match dem i vores query for at undgå en for loop.

            var sortedAlphabet = (
                from entry in alphabetDictionary
                orderby entry.Value ascending
                select entry
            ).ToList();

            for (int i = 0; i < bitList.Count(); i++)
            {
                for (int k = 0; k < sortedAlphabet.Count(); k++)
                {
                    if ((sortedAlphabet[k].Key & bitList[i]) != 0) continue;
                }
            }
            
            foreach (var item in sortedAlphabet)
            {
                Console.WriteLine("Alphabet dictionary: " + item);
            }

            //return readFile;
        }

        public int FiveWordsArray()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            gatherWords("\\Alpha.txt");

            Thread t = new Thread(new ThreadStart(Threading));

            t.Start();

            t.Join();

            Console.WriteLine("FiveWordsMatches: " + fiveMatches);
            stopwatch.Stop();
            Console.WriteLine($"Done in {stopwatch.ElapsedMilliseconds} ms");
            return fiveMatches;
        }
        
        public void Threading()
        {
            Parallel.For(0, bitList.Count(), i =>
            {
                Parallel.For(i + 1, bitList.Count(), k => {
                    if ((bitList[i] & bitList[k]) != 0) return;
                    for (int c = k + 1; c < bitList.Count(); c++)
                    {
                        if (((bitList[i] | bitList[k]) & bitList[c]) != 0) continue;
                        for (int b = c + 1; b < bitList.Count(); b++)
                        {
                            if (((bitList[i] | bitList[k] | bitList[c]) & bitList[b]) != 0) continue;
                            for (int d = b + 1; d < bitList.Count(); d++)
                            {
                                if (((bitList[i] | bitList[k] | bitList[c] | bitList[b]) & bitList[d]) != 0) continue;
                                Console.WriteLine($"Word1: {dictionary[bitList[i]]}, Word2: {dictionary[bitList[k]]} Word3: {dictionary[bitList[c]]}, Word4: {dictionary[bitList[b]]}, Word5: {dictionary[bitList[d]]}");
                                fiveMatches++;
                            }
                        }
                    }
                });
            });
        }

        public void FromString(string word) 
        {
            int bit = 0;
            int bitnum = 0;
            for (int i = 0; i < word.Length; i++)
            {
                bit |= 1 << (word[i] - 'a');
            }
            
            if( !dictionary.ContainsKey(bit))
            {
                dictionary.Add(bit, word);
                bitList.Add(bit);
            }

            bitnum |= 1 << (word[0] - 'a');

            if (!alphabetDictionary.ContainsKey(bitnum.ToString()))
            {
                value = 1;
                alphabetDictionary.Add(bitnum.ToString(), value);
            }
            else
            {
                value++;
                alphabetDictionary[bitnum.ToString()] = value;
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
