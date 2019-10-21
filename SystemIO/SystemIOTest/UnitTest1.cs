using System;
using System.IO;
using Xunit;
using static SystemIO.Program;

namespace SystemIOTest
{
    public class UnitTest1
    {

        [Fact]
        public void CheckWordExists()
        {
            string path = "../../../test.txt";
            File.Delete(path);
            AddWord(path, "Dog");
            Assert.Equal("Dog\r\n", ViewWords(path));
        }

        [Fact]
        public void RetrieveAllWords()
        {
            string path = "../../../test.txt";
            File.Delete(path);
            AddWord(path, "Dog");
            AddWord(path, "Cat");
            string[] testWords = File.ReadAllLines(path);
            Assert.Equal(2, testWords.Length);
        }

        [Fact]
        public void LetterExists()
        {
            string path = "../../../test.txt";
            File.Delete(path);
            AddWord(path, "Dog");
            string randomWord = SelectRandomWord(path);
            string[] currentGuess = new string[randomWord.Length];

            for (int i = 0; i < currentGuess.Length; i++)
            {
                currentGuess[i] = "_";
            }

            Assert.Equal(new string[] { "_", "o", "_"}, RenderLetters(path, currentGuess, randomWord, new string[] { "o" }));
        }

        [Fact]
        public void LetterDoesNotExist()
        {
            string path = "../../../test.txt";
            File.Delete(path);
            AddWord(path, "Dog");
            string randomWord = SelectRandomWord(path);
            string[] currentGuess = new string[randomWord.Length];

            for (int i = 0; i < currentGuess.Length; i++)
            {
                currentGuess[i] = "_";
            }

            Assert.NotEqual(new string[] { "_", "o", "_" }, RenderLetters(path, currentGuess, randomWord, new string[] { "e" }));
        }
    }
}
