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
        public string[] FiveWordsArray() 
        {
            string dir = Directory.GetCurrentDirectory();
            string dirPathFile = System.IO.Path.Combine(dir + "\\words_perfekt_data.txt");

            string[] readFile = File.ReadAllLines(dirPathFile);
            foreach (var item in readFile)
            {
                Console.WriteLine(item);
            }
            return readFile;
            
        }
    }
}
