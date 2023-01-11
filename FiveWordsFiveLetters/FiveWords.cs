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

        public string[] gatherWords(string filepath)
        {
            string dir = Directory.GetCurrentDirectory();
            string dirFilePath = System.IO.Path.Combine(dir + filepath);
            string[] readFile = File.ReadAllLines(dirFilePath);

            return readFile;
        }

        public List<string> FiveWordsArray() 
        {
            string[] readFile = gatherWords("\\Beta.txt");
            for (int i = 0; i < readFile.Count(); i++)
            {
                if (CheckLength(readFile[i]) && CheckDouble(readFile[i]) && CheckCharacter(readFile[i])) 
                {
                    pureWordList.Add(readFile[i]);
                    //Console.WriteLine(pureWordList[i]);
                }
            }
            Combination();
            foreach (var item in characters)
            {
                Console.WriteLine(item);
            }
            foreach (var item in pureWordList) 
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Char: "+characters.Count());
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
            bool pass = true;
            string placeholder = ""; 

            int stringLength = word.Length;
            for (int q = 0; q < stringLength; q++)
            {
                for (int w = 0; w < characters.Count; w++)
                {
                    if (word[q].ToString() == characters[w])
                    {
                        pass = false;
                    }
                    else
                    {
                        placeholder += word[q].ToString();
                    }
                }
            }
            if (pass == true && placeholder.Length >= 25)
            {
                foreach (var item in word)
                {
                    characters.Add(item.ToString());
                }
            }
            if (characters.Count() == 0)
            {
                foreach (var item in word)
                {
                    characters.Add(item.ToString());
                }
            }
            return pass;
        }

        public bool Combination()
        {
            // Tjek om der er mindre end 5 ord
            // Lav en kopi af purewordlist og få den til at ændre starte fra et andet ord vis der ikke er 25 kombinationer

            if(pureWordList.Count() < 5 || characters.Count() < 25)
            {
                Console.Clear();
                Console.WriteLine("Retrying");

                characters.Clear();
                pureWordList.Clear();

                FiveWordsArray();
            }
            // If you forget Inge, Inge will forget you. Welcome to the block :-)
            return false;

        }
    }
}
