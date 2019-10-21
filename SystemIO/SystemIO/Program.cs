using System;
using System.IO;

namespace SystemIO
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool status = true;
            while (status)
            {
                status = UserInterface();
            }
        }

        //Asks the user what action they should take
        public static bool UserInterface()
        {
            Console.Clear();
            Console.WriteLine("1. View All words in the list");
            Console.WriteLine("2. Add a word");
            Console.WriteLine("3. Remove a word");
            Console.WriteLine("4. Play Game");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string path = "../../../words.txt";

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine(ViewWords(path));
                    Console.WriteLine("Press 'Enter' to go back to the main menu");
                    Console.ReadLine();
                    return true;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Type a word to add to the list");
                    string word = Console.ReadLine();
                    AddWord(path, word);
                    return true;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Type a word to remove from the list");
                    string remove = Console.ReadLine();
                    RemoveWord(path, remove);
                    return true;
                case "4":
                    bool play = true;
                    while (play)
                    {
                        StartGame(path);
                        play = PlayAgain();
                    }
                    return true;
                case "5":
                    return false;
                default:
                    Console.WriteLine("Invalid entry");
                    return true;
            }

        }
        //Reads the words in from the external file and outputs them to the console
        public static string ViewWords(string path)
        {
            string allWords = File.ReadAllText(path);
            return allWords;
        }

        //Gives the user the ability to remove one of the words in the list
        public static void RemoveWord(string path, string remove)
        {
            string[] words = File.ReadAllLines(path);
            string[] newWords = new string[words.Length - 1];
            int index = Array.IndexOf(words, remove);

            for (int i = 0; i < newWords.Length; i++)
            {
                if (i < index)
                {
                    newWords[i] = words[i];
                }
                else if (i >= index)
                {
                    newWords[i] = words[i + 1];
                }
            }
            File.WriteAllLines(path, newWords);
        }

        //Add a new word to the list
        public static void AddWord(string path, string word)
        {
            File.AppendAllLines(path, new string[] { word });
        }

        public static void StartGame(string path)
        {
            Console.Clear();
            string pathGuess = "../../../guess.txt";
            string pathMatch = "../../../match.txt";

            File.Delete(pathGuess);
            File.Delete(pathMatch);

            string randomWord = SelectRandomWord(path);
            string[] currentGuess = new string[randomWord.Length];

            for (int i = 0; i < currentGuess.Length; i++)
            {
                currentGuess[i] = "_";
            }

            Console.WriteLine("");
            Console.WriteLine("" + string.Join(" ", currentGuess) + "");
            Console.WriteLine("");

            bool wrong = true;
            while (wrong)
            {
                try
                {
                    wrong = Game(pathGuess, pathMatch, randomWord, currentGuess);
                }
                catch(Exception e)
                {
                    Console.WriteLine("");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static string SelectRandomWord(string path)
        {
            Random rand = new Random();
            string[] words = File.ReadAllLines(path);
            int random = rand.Next(words.Length);
            string randomWord = words[random];
            return randomWord;
        }

        public static bool Game(string pathGuess, string pathMatch, string randomWord, string[] currentGuess)
        {
            Console.Write("Guess a letter: ");
            string guess = Console.ReadLine();
            File.AppendAllLines(pathMatch, new string[] { guess });
            string[] matchingLetters = File.ReadAllLines(pathMatch);

            currentGuess = RenderLetters(pathMatch, currentGuess, randomWord, matchingLetters);
            Console.WriteLine(string.Join(" ", currentGuess));
            Console.WriteLine("");

            if (randomWord.ToLower() == string.Join("", currentGuess))
            {
                return false;
            }
            else
            {
                return true;

            }
        }

            //if ((randomWord.Contains(guess, StringComparison.CurrentCultureIgnoreCase)))
            //{

            //}
            //else
            //{
            //    File.AppendAllLines(pathMatch, new string[] { guess.ToLower() });
            //}


        public static string[] RenderLetters(string pathMatch, string[] currentGuess, string randomWord, string[] matchingLetters)
        {

            string standarizedWord = randomWord.ToLower();

            foreach (string letter in matchingLetters)
            {
                for (int i = 0; i < randomWord.Length; i++)
                {
                    if (standarizedWord[i] == letter[0])
                    {
                        currentGuess[i] = letter;
                    }
                    else if (currentGuess[i] == null || currentGuess[i] == "_")
                    {
                        currentGuess[i] = "_";
                    }
                }
            }
            return currentGuess;
        }

        public static bool PlayAgain()
        {
            Console.WriteLine("You win!");
            Console.WriteLine("");
            Console.WriteLine("1. Play again");
            Console.WriteLine("2. Exit");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    return true;
                case "2":
                    return false;
                default:
                    return false;
            }
        }

    }
}
