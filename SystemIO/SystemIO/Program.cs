using System;

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
        static bool UserInterface()
        {
            Console.WriteLine("1. View All words in the list");
            Console.WriteLine("2. Add a word");
            Console.WriteLine("3. Remove a word");
            Console.WriteLine("4. Play Game");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ViewWords();
                    return true;
                case "2":
                    AddWord();
                    return true;
                case "3":
                    RemoveWords();
                    return true;
                case "4":
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
        static void ViewWords()
        {
        }
        //Gives the user the ability to remove one of the words in the list
        static void RemoveWords()
        {

        }
        //Add a new word to the list
        static void AddWord()
        {

        }
        //End the game
        static void ExitGame()
        {

        }
        static void StartGame()
        {
            //Actual game logic of guessing a letter of the mystery word
        }
    }
}
