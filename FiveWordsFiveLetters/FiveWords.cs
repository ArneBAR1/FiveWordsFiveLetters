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
        public void gatherWords(string filepath, int length)
        {
            string dir = Directory.GetCurrentDirectory();
            string dirFilePath = System.IO.Path.Combine(dir + filepath);
            string[] readFile = File.ReadAllLines(dirFilePath);

            for (int i = 0; i < readFile.Count(); i++)
            {
                if (CheckLength(readFile[i], length) && CheckDouble(readFile[i])) 
                {
                    FromString(readFile[i]);
                }
            }

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
            
            WordsArray(length);
        }

        public int WordsArray(int length)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //If length of word is 6, it'll be 4 words
            if (length == 6)
            {
                FourWordsMatches(0, 0, new List<int>());
                Console.WriteLine("Four Words Matches: " + fiveMatches);
            }

            //If length of word is 5, it'll be 5 words
            if (length == 5)
            {
                FiveWordsMatches(0,0, new List<int>());
                Console.WriteLine("Five Words Matches: " + fiveMatches);
            }

            //If length of word is 4, it'll be 6 words
            if (length == 4)
            {
                SixWordsMatches(0, 0, new List<int>());
                Console.WriteLine("Six Words Matches: " + fiveMatches);
            }

            
            stopwatch.Stop();
            Console.WriteLine($"Done in {stopwatch.ElapsedMilliseconds} ms");
            return fiveMatches;
        }

        private void FourWordsMatches(int usedBits, int pointer, List<int> matchedBits) 
        {
            if (sortedAlphabet is null) return;
            if (matchedBits.Count() == 4)
            {
                Console.WriteLine("{0} {1} {2} {3}", dictionary[matchedBits[0]], dictionary[matchedBits[1]], dictionary[matchedBits[2]], dictionary[matchedBits[3]]);
                fiveMatches++;
                return;
            }

            for (int letter = pointer; letter <= alphabetLists.Count() - (26 - 2 - matchedBits.Count() * 6) && alphabetLists[letter] != null; letter++)
            {
                if ((sortedAlphabet[letter].Key & usedBits) != 0) continue;
                if (matchedBits.Count() == 0)
                {
                    Parallel.ForEach(alphabetLists[letter].Where(x => (x & usedBits) == 0), bit =>
                    {
                        var fourBitCollection = new List<int>(matchedBits);
                        fourBitCollection.Add(bit);
                        FourWordsMatches(usedBits | bit, letter + 1, fourBitCollection);
                    });

                }
                else
                {
                    foreach (int bit in alphabetLists[letter].Where(x => (x & usedBits) == 0))
                    {
                        var fourBitCollection = new List<int>(matchedBits);
                        fourBitCollection.Add(bit);
                        FourWordsMatches(usedBits | bit, letter + 1, fourBitCollection);
                    }
                }


            }

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

            //recursion with threading
            for (int letter = pointer; letter <= alphabetLists.Count() - (26 - 1 - matchedBits.Count() * 5) && alphabetLists[letter] != null; letter++)
            {
                if ((sortedAlphabet[letter].Key & usedBits) != 0) continue;
                if (matchedBits.Count() == 0)
                {
                    Parallel.ForEach(alphabetLists[letter].Where(x => (x & usedBits) == 0), bit =>
                    {
                        var fiveBitCollection = new List<int>(matchedBits);
                        fiveBitCollection.Add(bit);
                        FiveWordsMatches(usedBits | bit, letter + 1, fiveBitCollection);
                    });

                }
                else
                {
                    foreach (int bit in alphabetLists[letter].Where(x => (x & usedBits) == 0))
                    {
                        var fourBitCollection = new List<int>(matchedBits);
                        fourBitCollection.Add(bit);
                        FiveWordsMatches(usedBits | bit, letter + 1, fourBitCollection);
                    }
                }


            }

        }

        private void SixWordsMatches(int usedBits, int pointer, List<int> matchedBits)
        {
            if (sortedAlphabet is null) return;
            if (matchedBits.Count() == 6)
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", dictionary[matchedBits[0]], dictionary[matchedBits[1]], dictionary[matchedBits[2]], dictionary[matchedBits[3]], dictionary[matchedBits[4]], dictionary[matchedBits[5]]);
                fiveMatches++;
                return;
            }

            for (int letter = pointer; letter <= alphabetLists.Count() - (26 - 2 - matchedBits.Count() * 4) && alphabetLists[letter] != null; letter++)
            {
                if ((sortedAlphabet[letter].Key & usedBits) != 0) continue;
                if (matchedBits.Count() == 0)
                {
                    Parallel.ForEach(alphabetLists[letter].Where(x => (x & usedBits) == 0), bit =>
                    {
                        var sixBitCollection = new List<int>(matchedBits);
                        sixBitCollection.Add(bit);
                        SixWordsMatches(usedBits | bit, letter + 1, sixBitCollection);
                    });

                }
                else
                {
                    foreach (int bit in alphabetLists[letter].Where(x => (x & usedBits) == 0))
                    {
                        var sixBitCollection = new List<int>(matchedBits);
                        sixBitCollection.Add(bit);
                        SixWordsMatches(usedBits | bit, letter + 1, sixBitCollection);
                    }
                }
            }
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

        private bool CheckLength(string word, int length)
        {
            int stringLength = word.Length;
            bool passed = true;
            
            //If length of the words should be 6, it'll be four words
            if (length == 6)
            {
                if (stringLength != 6)
                {
                    passed = false;
                }
            }

            //If length of the word is 5, it'll be five words
            if (length == 5)
            {
                if (stringLength != 5)
                    passed = false;
            }

            //If length of the words is 4, it'll be six words
            if (length == 4)
            {
                if (stringLength != 4)
                    passed = false;
            }

            return passed;
        }

        private bool CheckDouble(string word) 
        {
            bool notDouble = true;
            int stringLength = word.Length;
            for (int q = 0; q < stringLength; q++)
            {
                for (int w = 0; w < stringLength; w++)
                {
                    if (word[q] == word[w] && q != w)
                    {
                        notDouble = false;
                    }

                }
            }
            return notDouble;
        }
    }
}
