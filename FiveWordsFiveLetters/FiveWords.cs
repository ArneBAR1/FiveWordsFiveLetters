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
        public string[] FiveWordsArray(string filepath) 
        {
            string dir = Directory.GetCurrentDirectory();
            string dirPathFile = System.IO.Path.Combine(dir + filepath);

            string[] readFile = File.ReadAllLines(dirPathFile);
            for (int i = 0; i < readFile.Count(); i++)
            {
                Check(readFile[i]);
            }
            return readFile;
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
    }
}
