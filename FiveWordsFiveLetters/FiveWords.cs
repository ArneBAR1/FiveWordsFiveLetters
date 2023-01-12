using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveWordsFiveLetters
{
    public class FiveWords
    {
        public FiveWords()
        {

        }

        List<string> characters = new List<string>();
        List<string> pureWordList = new List<string>();
        int fiveMatches = 0;

        public List<string> gatherWords(string filepath)
        {
            List<string> readFile = new List<string>();
            string dir = Directory.GetCurrentDirectory();
            string dirFilePath = System.IO.Path.Combine(dir + filepath);
            string[] readingFile = File.ReadAllLines(dirFilePath);

            for (int i = 0; i < readingFile.Count(); i++)
            {
                if (CheckLength(readingFile[i]) && CheckDouble(readingFile[i]))
                    readFile.Add(readingFile[i]);
            }

            return readFile;
        }

        public List<string> FiveWordsArray()
        {
            List<string> readFile = gatherWords("\\Beta.txt");
            for (int i = 0; i < readFile.Count(); i++)
            {
                for (int k = i + 1; k < readFile.Count(); k++)
                {
                    if (string.Concat(readFile[i], readFile[k]).Distinct().Count() != 10) continue;
                    for (int c = k + 1; c < readFile.Count(); c++)
                    {
                        if (string.Concat(readFile[i], readFile[k], readFile[c]).Distinct().Count() != 15) continue;
                        for (int b = c + 1; b < readFile.Count(); b++)
                        {
                            if (string.Concat(readFile[i], readFile[k], readFile[c], readFile[b]).Distinct().Count() != 20) continue;
                            {
                                for (int d = b + 1; d < readFile.Count(); d++)
                                {
                                    if (string.Concat(readFile[i], readFile[k], readFile[c], readFile[b], readFile[d]).Distinct().Count() != 25) continue;
                                    Console.WriteLine($"Word1: {readFile[i]}, Word2: {readFile[k]} Word3: {readFile[c]}, Word4: {readFile[b]}, Word5: {readFile[d]}");
                                    pureWordList.Add(readFile[i]);
                                    fiveMatches++;
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("FiveWordsMatches: " + fiveMatches);
            return pureWordList;
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
