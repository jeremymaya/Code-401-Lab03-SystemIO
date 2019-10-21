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

        /// <summary>
        /// UserInterface asking the user what action they should be taken
        /// </summary>
        /// <returns>boolean</returns>
        public static bool UserInterface()
        {
            Console.Clear();
            Console.WriteLine("1. View All words in the list");
            Console.WriteLine("2. Add a word");
            Console.WriteLine("3. Remove a word");
            Console.WriteLine("4. Play Game");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string pathWord = "../../../words.txt";

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine(ViewWords(pathWord));
                    Console.WriteLine("Press 'Enter' to go back to the main menu");
                    Console.ReadLine();
                    return true;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Type a word to add to the list");
                    string word = Console.ReadLine();
                    AddWord(pathWord, word);
                    return true;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Type a word to remove from the list");
                    string remove = Console.ReadLine();
                    RemoveWord(pathWord, remove);
                    return true;
                case "4":
                    bool play = true;
                    while (play)
                    {
                        StartGame(pathWord);
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

        /// <summary>
        /// ViewWords reads the words in from the external file and outputs them to the console
        /// </summary>
        /// <param name="pathWord">path to the words.txt</param>
        /// <returns></returns>
        public static string ViewWords(string pathWord)
        {
            string allWords = File.ReadAllText(pathWord);
            return allWords;
        }

        /// <summary>
        /// RemoveWord remove user typed word from the words.txt
        /// </summary>
        /// <param name="pathWord">path to the words.txt</param>
        /// <param name="remove">word to remove</param>
        public static void RemoveWord(string pathWord, string remove)
        {
            string[] words = File.ReadAllLines(pathWord);
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
            File.WriteAllLines(pathWord, newWords);
        }

        /// <summary>
        /// AddWord adds user typed word to the words.txt
        /// </summary>
        /// <param name="pathWord">path to the words.txt</param>
        /// <param name="word">word to add</param>
        public static void AddWord(string pathWord, string word)
        {
            File.AppendAllLines(pathWord, new string[] { word });
        }

        /// <summary>
        /// StartGame method setup a game to be played and invokes Game method
        /// </summary>
        /// <param name="pathWord">path to the words.txt</param>
        public static void StartGame(string pathWord)
        {
            Console.Clear();
            string pathGuess = "../../../guess.txt";

            File.Delete(pathGuess);

            string randomWord = SelectRandomWord(pathWord);
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
                    wrong = Game(pathGuess, randomWord, currentGuess);
                }
                catch(Exception e)
                {
                    Console.WriteLine("");
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// SelectRandomWord method selects a random word from the wrods.txt
        /// </summary>
        /// <param name="pathWord">path to the words.txt</param>
        /// <returns></returns>
        public static string SelectRandomWord(string pathWord)
        {
            Random rand = new Random();
            string[] words = File.ReadAllLines(pathWord);
            int random = rand.Next(words.Length);
            string randomWord = words[random];
            return randomWord;
        }

        /// <summary>
        /// Game method asks for user guess then invokes RenderLetters method to print out letters
        /// </summary>
        /// <param name="pathGuess">path to the guess.txt</param>
        /// <param name="randomWord">selected random word from the words.txt</param>
        /// <param name="currentGuess">currently guessed letters</param>
        /// <returns></returns>
        public static bool Game(string pathGuess, string randomWord, string[] currentGuess)
        {
            Console.Write("Guess a letter: ");
            string guess = Console.ReadLine();
            if (guess.Length > 1)
            {
                Console.WriteLine("Invalid Entry - Please enter a single letter");
                Console.WriteLine("");
            }
            else
            {
                File.AppendAllLines(pathGuess, new string[] { guess });
                string[] gueesedLetters = File.ReadAllLines(pathGuess);

                currentGuess = RenderLetters(currentGuess, randomWord, gueesedLetters);
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
            return true;
        }

        /// <summary>
        /// RenderLetters checks if there is a matching letter and updates currentGuess
        /// </summary>
        /// <param name="currentGuess">currently guessed letters</param>
        /// <param name="randomWord">selected random word from the words.txt</param>
        /// <param name="matchingLetters"></param>
        /// <returns></returns>
        public static string[] RenderLetters(string[] currentGuess, string randomWord, string[] gueesedLetters)
        {
            string standarizedWord = randomWord.ToLower();

            foreach (string letter in gueesedLetters)
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

        /// <summary>
        /// PlayAgain method asks the user to play/exit the game
        /// </summary>
        /// <returns>boolean</returns>
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
