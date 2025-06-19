//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics.Tracing;
//using System.Linq;
//using System.Net.Quic;
//using System.Reflection.Metadata.Ecma335;
//using System.Text;
//using System.Threading.Tasks;

using System.Reflection;

namespace MiniGames
{
    public class Hangman
    {
        public static void HangMan()
        {
            Console.Clear();
            MiniGames.TypeWriter("HANGMAN", 0.025f);
            MiniGames.TypeWriter("-------", 0.025f);
            MiniGames.TypeWriter("Instructions: Try to figure out what word I am thinking of. Type in a letter to guess.\nBut be careful, you only have a limited number of guesses.", 0.025f);
            Console.WriteLine();
            //Console.WriteLine();
            MiniGames.TypeWriter("Press ENTER to begin, Q to quit.", 0.025f);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(">");

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    InitializeGame();
                }

                if (keyInfo.Key == ConsoleKey.Q)
                {
                    MiniGames.exit();
                }
            }
        }

        public static string findRandomWord(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                                          .FirstOrDefault(n => n.EndsWith(filename));

            if (resourceName == null)
            {
                throw new FileNotFoundException("Could not find wordlist");
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                // Read all words (one per line)
                List<string> words = new List<string>();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(line))
                    {
                        words.Add(line);
                    }
                }

                if (words.Count == 0)
                {
                    throw new InvalidOperationException("Word list is empty.");
                }

                Random rand = new Random();
                int index = rand.Next(words.Count);
                return words[index];
            }
        }

        static void InitializeGame()
        {
            string word = findRandomWord("wordlist.txt");
            char [] letters = word.ToCharArray();
            char[] found = new char [letters.Length];
            int lives = 6;
            List <Char> wrongs = new List <char>();
            String [] prompt = ["Guess a letter!", "You got it! Guess another one!", "Sorry, that's not it"];
            int lastprompt = 0;
            string msg = "Game Over! The word was: " + word+"!";


            static void graph(int wrongsCount)
            {
                int rows = 11;
                int cols = 13;
                string[,] grid = new string[rows, cols];

                // Initialize grid with spaces
                for (int r = 0; r < rows; r++)
                    for (int c = 0; c < cols; c++)
                        grid[r, c] = "  ";

                // Top bar
                for (int c = 0; c < cols-3; c++)
                    grid[0, c] = "- ";

                // Scaffold columns
                grid[1, 0] = "| ";
                grid[1, cols - 4] = "| ";
                for (int r = 2; r < rows; r++)
                    grid[r, 0] = "| ";

                int midCol = 9; // Mid-point for symmetry

                if (wrongsCount >= 1)
                    grid[2, midCol] = "O "; // Head

                if (wrongsCount >= 2)
                    grid[3, midCol] = "| "; // Upper torso

                if (wrongsCount >= 3)
                {
                    grid[3, midCol - 3] = "> ";
                    grid[3, midCol - 2] = "- ";
                    grid[3, midCol - 1] = "- ";
                }

                if (wrongsCount >= 4)
                {
                    grid[3, midCol + 1] = "- ";
                    grid[3, midCol + 2] = "- ";
                    grid[3, midCol + 3] = "< " ;
                }

                if (wrongsCount >= 5)
                {
                    grid[4, midCol] = "| ";       // Lower torso
                    grid[5, midCol - 1] = " /";   // Left leg
                    grid[6, midCol - 1] = "| ";   // Support
                    grid[7, midCol - 2] = " -";   // Base (left side)
                }

                if (wrongsCount >= 6)
                {
                    grid[5, midCol + 0] = " \\";  // Right leg
                    grid[6, midCol + 1] = "| ";   // Reinforce support
                    grid[7, midCol + 1] = " -";   // Base (right side)
                }

                // Render
                for (int r = 0; r < rows; r++)
                {
                    string line = "";
                    for (int c = 0; c < cols; c++)
                        line += grid[r, c];
                    Console.WriteLine(line);
                }
            }

            graph(wrongs.Count);

            void DisplayCurrentProgress(char [] letters,char [] found)
            {
                for (int i = 0; i < letters.Length; i++)
                {
                    char c = found[i];
                    Console.Write((c != '\0') ? c + " " : "_ ");
                }
             Console.WriteLine();
            }

            void DrawUI()
            { 
                Console.Clear();
                graph(wrongs.Count);
                Console.WriteLine("Lives left: "+ lives);
                Console.WriteLine();
                DisplayCurrentProgress(letters, found);
                Console.WriteLine("\nLetters used:");
                Console.WriteLine(string.Join(" ", wrongs));
                Console.WriteLine();
                
                if (!found.Contains('\0'))
                {
                    MiniGames.TypeWriter("Congratulations! You guessed the word.\n>",0.025f,false);
                    Thread.Sleep(2000);
                    HangMan();
                }
                else
                {
                    Console.WriteLine(prompt[lastprompt]);
                }
                if (lives != 0)
                {
                    Console.Write(">");
                }
                else
                {
                    MiniGames.TypeWriter(">" + msg,0.025f);
                }
            }
            
            //Initialise found array
            for (int i = 0; i < found.Length; i++)
            {
                found[i] = '\0';
            }
            Console.WriteLine(prompt[0]);
            while (lives >= 0 )
            {
                DrawUI();
                if (lives == 0)
                {
                    Thread.Sleep(3000);
                    HangMan();
                }

                char typedChar = Console.ReadKey(false).KeyChar;

                if (!char.IsLetterOrDigit(typedChar))
                {
                    continue;
                }

                if (typedChar == '\r')
                {
                    continue;
                }

                if (letters.Contains(typedChar)) // If the guessed letter exists in the target word
                {
                    bool alreadyFound = false;

                    // Check if this letter has already been guessed
                    for (int i = 0; i < letters.Length; i++)
                    {
                        if (letters[i] == typedChar && found[i] == typedChar)
                        {
                            alreadyFound = true;
                            break;
                        }
                    }
                    if (!alreadyFound)
                    {
                        for (int i = 0; i < letters.Length; i++)
                        {
                            if (letters[i] == typedChar)
                            {
                                found[i] = typedChar;
                            }
                        }
                        if (!found.Contains('\0'))
                        {
                            DrawUI();
                            Thread.Sleep(3000);
                            HangMan();
                        }
                        lastprompt = 1;
                    }
                }

                else
                {
                    if (!wrongs.Contains(typedChar))
                    {
                        wrongs.Add(typedChar);
                        lives--;
                        lastprompt = 2;
                    }
                }
            }
        }        
    }
}                     