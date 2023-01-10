Console.WriteLine("FiveWordsFiveLetters");

string dir = Directory.GetCurrentDirectory();
string dirPathFile = System.IO.Path.Combine(dir + "\\words_perfekt_data.txt");

string[] readFile = File.ReadAllLines(dirPathFile);

foreach (var item in readFile)
{
    Console.WriteLine(item);
}