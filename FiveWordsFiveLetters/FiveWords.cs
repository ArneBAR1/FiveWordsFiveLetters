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
using System.Diagnostics.Metrics;

namespace FiveWordsFiveLetters
{
    public class FiveWords
    {
        public FiveWords()
        {

        }

        Dictionary<int,string> dictionary = new Dictionary<int, string>();
        List<int> bitList = new List<int>();
        int fiveMatches = 0;
        Dictionary<int, int> alphabetDictionary = new Dictionary<int, int>();
        int value = 1;
        int[][] alphabetLists;
        List<KeyValuePair<int, int>> sortedAlphabet;
        int bit;
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

            sortedAlphabet = (
                from entry in alphabetDictionary
                orderby entry.Value ascending
                select entry
            ).ToList();

            alphabetLists = new int[sortedAlphabet.Count][];

            for (int i = 0; i < sortedAlphabet.Count; i++)
            {
                alphabetLists[i] = bitList.Where(x => (x & sortedAlphabet[i].Key) != 0).ToArray(); 
                bitList = bitList.Where(x => (x & sortedAlphabet[i].Key) == 0).ToList();
            }
            
            WordsArray();
        }

        public int WordsArray()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            FiveWordsMatches(0,0, new List<int>());

            Console.WriteLine("FiveWordsMatches: " + fiveMatches);
            stopwatch.Stop();
            Console.WriteLine($"Done in {stopwatch.ElapsedMilliseconds} ms");
            return fiveMatches;
        }

        private void FiveWordsMatches(int usedBits, int pointer, List<int> matchedBits)
        {
            if (sortedAlphabet is null) return;
            if (matchedBits.Count() == 5)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", dictionary[matchedBits[0]], dictionary[matchedBits[1]], dictionary[matchedBits[2]], dictionary[matchedBits[3]], dictionary[matchedBits[4]]);
                fiveMatches++;
                return;
            }
            
            for (int letter = pointer; letter <= alphabetLists.Count() - (26 - 1 - matchedBits.Count() * 5) && alphabetLists[letter] != null; letter++) 
            {
                if ((sortedAlphabet[letter].Key & usedBits) != 0) continue;
                foreach (int bit in alphabetLists[letter].Where(x => (x & usedBits) == 0))
                {
                    var fiveBitCollection = new List<int>(matchedBits);
                    fiveBitCollection.Add(bit);
                    FiveWordsMatches(usedBits | bit, letter + 1, fiveBitCollection);
                }


            }



            //Parallel.For(0, bitList.Count(), i =>
            //{
            //    Parallel.For(i + 1, bitList.Count(), k =>
            //    {
            //        if ((bitList[i] & bitList[k]) != 0) return;
            //        for (int c = k + 1; c < bitList.Count(); c++)
            //        {
            //            if (((bitList[i] | bitList[k]) & bitList[c]) != 0) continue;
            //            for (int b = c + 1; b < bitList.Count(); b++)
            //            {
            //                if (((bitList[i] | bitList[k] | bitList[c]) & bitList[b]) != 0) continue;
            //                for (int d = b + 1; d < bitList.Count(); d++)
            //                {
            //                    if (((bitList[i] | bitList[k] | bitList[c] | bitList[b]) & bitList[d]) != 0) continue;
            //                    Console.WriteLine($"Word1: {dictionary[bitList[i]]}, Word2: {dictionary[bitList[k]]} Word3: {dictionary[bitList[c]]}, Word4: {dictionary[bitList[b]]}, Word5: {dictionary[bitList[d]]}");
            //                    fiveMatches++;
            //                }
            //            }
            //        }
            //    });
            //});

        }

        private void FromString(string word) 
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

            for (int i = 0; i < word.Length; i++)
            {

                bitnum = 1 << (word[i] - 'a');

                if (!alphabetDictionary.ContainsKey(bitnum))
                {
                    value = 1;
                    alphabetDictionary.Add(bitnum, value);
                }
                else
                {
                    value++;
                    alphabetDictionary[bitnum] = value;
                }
            }

        }

        private bool CheckLength(string word)
        {
            int stringLength = word.Length;
            bool passed = true;

            if (stringLength != 5)
                passed = false;

            return passed;
        }

        private bool CheckDouble(string word) 
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
