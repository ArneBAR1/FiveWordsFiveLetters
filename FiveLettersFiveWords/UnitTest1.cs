using FiveWordsFiveLetters;

namespace FiveLettersFiveWordsTest
{
    public class UnitTest1
    {
        [Fact]
        public void ReadAllWordsTest()
        {
            var fiveWords = new FiveWords();
            string[] result = fiveWords.FiveWordsArray();
            for (int i = 0; i < result.Count(); i++)
            {
                result[i].Length != 5;  
            }
            Assert.Equal();
        }
    }
}