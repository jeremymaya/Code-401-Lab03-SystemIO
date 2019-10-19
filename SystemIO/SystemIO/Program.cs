using System;
using System.IO;

namespace SystemIO
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool status = true;
            while(status)
            {
                status = UserInterface();
            }
        }

        //Asks the user what action they should take
        public static bool UserInterface()
        {
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
                    Console.WriteLine(ViewWords(path));
                    return true;
                case "2":
                    Console.WriteLine("Type a word to add to the list");
                    string word = Console.ReadLine();
                    AddWord(path, word);
                    return true;
                case "3":
                    Console.WriteLine("Type a word to remove from the list");
                    string remove = Console.ReadLine();
                    RemoveWord(path, remove);
                    return true;
                case "4":
                    StartGame(path);
                    return true;
                case "5":
                    ExitGame();
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

        //End the game
        public static void ExitGame()
        {

        }

        public static void StartGame(string path)
        {
            Console.WriteLine(RandomWord(path));
            // Show an empty word (_ _ _ _ _ _)
            // Select a random word
            // Make 2 arrays
            // 1 empty
            // 1 word
            // Prompt user to enter a letter
            // Read user input
            // Create another txt file to keep track of the guessed letter
            // If the entered letter == to a letter in array
            // Fill the empty array
            // Repeat until concat word matches each other

        }

        public static string RandomWord(string path)
        {
            Random rand = new Random();
            string[] words = File.ReadAllLines(path);
            int random = rand.Next(words.Length);
            return words[random];
        }
    }
}
