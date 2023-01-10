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
        public List<string> FiveWordsArray(string filepath) 
        {
            string dir = Directory.GetCurrentDirectory();
            string dirPathFile = System.IO.Path.Combine(dir + filepath);

            string[] readFile = File.ReadAllLines(dirPathFile);
            List<string> words = new List<string>();
            for (int i = 0; i < readFile.Count(); i++)
            {
                if (Check(readFile[i]) && CheckDouble(readFile[i])) 
                {
                    words.Add(readFile[i]);
                    Console.WriteLine(words[i]);
                }
            }
            return words;
        }

        public bool Check(string word)
        {
            // Checking if lenght is less or more than 5
            int stringLength = word.Length;
            bool passed = true;

            if (stringLength != 5)
                passed = false;

            return passed;
        }

        public bool CheckDouble(string word) 
        {
            bool fits = true;
            int stringLength = word.Length;
            for (int q = 0; q < stringLength; q++)
            {
                for (int w = 0; w < stringLength; w++)
                {
                    if (word[q] == word[w] && q != w)
                    {
                        fits = false;
                    }

                }
            }
            return fits;
        }
    }
}
