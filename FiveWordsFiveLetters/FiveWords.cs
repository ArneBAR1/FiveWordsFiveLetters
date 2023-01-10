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
            string dirFilePath = System.IO.Path.Combine(dir + filepath);

            string[] readFile = File.ReadAllLines(dirFilePath);
            List<string> pureWordList = new List<string>();
            for (int i = 0; i < readFile.Count(); i++)
            {
                if (CheckLength(readFile[i]) && CheckDouble(readFile[i])) 
                {
                    pureWordList.Add(readFile[i]);
                    Console.WriteLine(pureWordList[i]);
                }
            }
            return pureWordList;
        }

        public bool CheckLength(string word)
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

        public bool CheckCharacter(string word)
        {
            List<string> characters = new List<string>();
            bool pass = true;

            int stringLength = word.Length;
            for (int q = 0; q < stringLength; q++)
            {
                for (int w = 0; w < characters.Count; w++)
                {
                    if (word[q].ToString() == characters[w] && q != w)
                    {
                        pass = false;
                    }

                }
            }

            return pass;
        }
    }
}
