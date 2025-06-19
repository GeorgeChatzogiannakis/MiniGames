//// See https://aka.ms/new-console-template for more information
//using MiniGames;
//using System.ComponentModel.Design;
//using System.Runtime.InteropServices.Marshalling;
//using System.Threading;

using System.ComponentModel;

namespace MiniGames
{
    public class MiniGames
    {
        static void Main(String[] args)
        {
            GameHub();
        }
        
        public static void GameHub() 
        {
            String[] games = ["Guess the Number", "Hangman", "Rock, Paper, Scissors"];
            string inp = "0";

            TypeWriter("Game Hub", 0.015f);
            TypeWriter("---------",.06f);
            Console.WriteLine("");
            TypeWriter("[1] - " + games[0].ToString(),.03f);
            TypeWriter("[2] - " + games[1].ToString(),.03f);
            TypeWriter("[3] - " + games[2].ToString(),.03f);
            Console.WriteLine("");
            if (Settings.tw)
                Thread.Sleep(700);

            TypeWriter("Tip: Type in the number of the number you want to play.\n", .03f);
            if (Settings.tw)
                Thread.Sleep(750);
            TypeWriter("So, this is the game hub.", 0.015f);
            if (Settings.tw)
                Thread.Sleep(200);
            TypeWriter("Here you can choose which games you want us to play.", .03f);
            if (Settings.tw)
                Thread.Sleep(600);
            TypeWriter("Which one should we try out?", .01f);
            Console.Write("> ");
            if (Settings.tw)
                Thread.Sleep(500);
            Console.SetCursorPosition(0, 15);
            TypeWriter("[S] Settings , [Q] Quit");
            Console.SetCursorPosition(2, 12);
            do
            {
                inp = Console.ReadLine();
                if (inp != "1" && inp != "2" && inp != "3" && inp != "q" && inp != "Q")
                {
                    Settings.ClearLine(12);
                    Console.Write("> ");
                }

                switch (inp)
                {
                    case "1":
                    Console.Clear();
                    TypeWriter("[Loading: " + games[0].ToUpper() + "]", 0.07f);
                    Thread.Sleep(475);
                    TypeWriter("...............", 0.01f, false);
                    Thread.Sleep(500);
                    GuessTheNumber.GTN();
                    break;

                    case "2":
                    Console.Clear();
                    TypeWriter("[Loading: " + games[1].ToUpper() + "]", 0.07f);
                    Thread.Sleep(550);
                    TypeWriter("...............", 0.01f, false);
                    Thread.Sleep(500);
                    Hangman.HangMan();
                    break;

                    case "3":
                    Console.Clear();
                    TypeWriter("[Loading: " + games[2].ToUpper() + "]", 0.05f);
                    Thread.Sleep(500);
                    TypeWriter("...............", 0.01f, false);
                    Thread.Sleep(500);
                    RockPaperScissors.RPS();
                    break;
                    case "q":
                    Environment.Exit(0);
                    break;

                    case "Q":
                    Environment.Exit(0);
                    break;

                    case "S":
                    Settings.SettingsManager();
                    break;
                    case "s":
                    Settings.SettingsManager();
                    break;

                }
            } while (inp != "1" || inp != "2" || inp != "3" || inp != "q" || inp != "Q");
            Console.ReadLine();
        }

        public static void exit()
        {
            Console.Clear();
            GameHub();
        }
        public static void TypeWriter(String text, float delay=0.025f, bool newLine=true)
        {            
            if (!Settings.tw) 
            {
                delay = 0f;
            }
            if (Settings.delay != 0)
            {
                delay = Settings.delay;
            }
            
            int Mdelay = (int)(delay * 1000);

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                Console.Write(c);

                if (c == '.')
                {
                    // Check if this dot is part of a multi-dot sequence
                    bool isPartOfMultiDotSequence = (i > 0 && text[i - 1] == '.') || (i + 1 < text.Length && text[i + 1] == '.');

                    if (isPartOfMultiDotSequence)
                        Thread.Sleep(100);  // Special pause for multi-dot sequence
                    else
                        Thread.Sleep(Mdelay); // Just the usual delay
                }
                else
                {
                    Thread.Sleep(Mdelay);
                }
            }

            if (newLine)
            {
                Console.WriteLine();
            }
        }
    }
}