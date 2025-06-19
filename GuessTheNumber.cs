//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics.Metrics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MiniGames
{
    class GuessTheNumber
    {
        public static void GTN()
        {
            Console.Clear();
            MiniGames.TypeWriter("GUESS THE NUMBER", .045f);
            MiniGames.TypeWriter("----------------", 0.025f);
            MiniGames.TypeWriter("Instructions: Guess the number I'm thinking of.", 0.025f);
            Console.WriteLine();
            Console.WriteLine();
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
                    InitialiseGame();
                }

                if (keyInfo.Key == ConsoleKey.Q)
                {
                    MiniGames.exit();
                }
            }
        }
        private static void InitialiseGame()
            #region round 1
        {
            Random random = new Random();

            int r1 = random.Next(1, 10);
            int corr_ans = r1;
            bool gtn;
            string number;
            int num = 0;
            int attempts1 = 1;

            MiniGames.TypeWriter("I am thinking of a number between 1 and 10. Guess!");
            
            while (num != corr_ans)
            {
                Console.Write(">");
                number = Console.ReadLine();

                if (number.ToLower() == "q")
                {
                    MiniGames.exit();
                }

                gtn = int.TryParse(number, out num);

                if (gtn == true)
                {
                    if (attempts1 <= 2 && num != corr_ans)
                    {
                        MiniGames.TypeWriter("No, try again!");
                        attempts1++;
                    }
                    else
                    {
                        if (num < corr_ans)
                        {
                            MiniGames.TypeWriter("Nope. Too low!");
                            attempts1++;
                        }
                        if (num > corr_ans)
                        {
                            MiniGames.TypeWriter("Nope. Too high");
                            attempts1++;
                        }
                        if (num == corr_ans)
                        {
                            MiniGames.TypeWriter("You got it!", 0.025f,false);
                            Thread.Sleep(1000);
                        }
                    }
                }
                else
                {
                    MiniGames.TypeWriter("Please input an integer number.");
                }
            }
            
            Console.Clear();
            MiniGames.TypeWriter("Okay. Now I'm thinking of a number from 1 to 15. Guess again!", 0.025f);
            #endregion round 1

            #region round 2
            int r2 = random.Next(1, 15);
            corr_ans = r2;
            int attempts2 = 1;

            while (num != corr_ans)
            {
                Console.Write(">");
                number = Console.ReadLine();

                gtn = int.TryParse(number, out num);

                if (gtn == true)
                {
                    if (attempts2 <= 2 && num != corr_ans)
                    {
                        MiniGames.TypeWriter("No, try again!");
                        attempts2++;
                    }
                    else
                    {
                        if (num < corr_ans)
                        {
                            MiniGames.TypeWriter("Nope. Too low!");
                            attempts2++;
                        }
                        if (num > corr_ans)
                        {
                            MiniGames.TypeWriter("Nope. Too high");
                            attempts2++;
                        }
                        if (num == corr_ans)
                        {
                            MiniGames.TypeWriter("You got it!", 0.025f,false);
                            Thread.Sleep(1000);
                        }
                    }
                }
                else
                {
                    MiniGames.TypeWriter("This game is played with integer numbers. Try again");
                }
            }
            #endregion round2

            #region round 3
            Console.Clear();
            MiniGames.TypeWriter("Now, I'm thinking of a numer between 1 and 1,000,000. Guess!");
            int attempts3 = 1;
            int option = random.Next(1, 4);

            if (option == 1)
            {
                //Random                
                int r3 = random.Next(1, 1000000);
                num = 0;
                corr_ans = r3;
                bool highFound = false, lowFound = false;
                int hi = 1000000, lo = 0;
                int lastHint = 0, lastSuggestion = 1000000;
                while (num != corr_ans)
                {
                    Console.Write(">");
                    number = Console.ReadLine();

                    gtn = int.TryParse(number, out num);

                    if (gtn)
                    {
                        if (num < corr_ans)
                        {
                            MiniGames.TypeWriter("Nope. Too low!", 0.025f);
                            lowFound = true;
                            if (lo < num)
                            {
                                lo = num;
                            }
                            attempts3++;
                        }
                        if (num > corr_ans && num < 1000000)
                        {
                            MiniGames.TypeWriter("Nope. Too high", 0.025f);
                            highFound = true;
                            if (num < hi)
                            {
                                hi = num;
                            }
                        }
                        if(num > 1000000)
                        {
                            MiniGames.TypeWriter("This number is HIGHER THAN THE UPPER BOUND!", 0.01f);
                        }
                            attempts3++;
                        
                        if (attempts3 > 2 && num != corr_ans)
                        {
                            int hint = random.Next(0, 1);
                            if (hint == 0)
                            {
                                if (num.ToString().Length == corr_ans.ToString().Length)
                                {

                                    hint = random.Next(0, 4);
                                    if (hint == 0)
                                    {
                                        MiniGames.TypeWriter(Help1(num, corr_ans), 0.025f);
                                    }
                                    if (hint == 1 && lowFound)
                                    {
                                        MiniGames.TypeWriter(Help2(), 0.025f);
                                    }

                                    if (hint == 2 && highFound)
                                    {
                                        MiniGames.TypeWriter(Help3(), 0.025f);
                                    }

                                    if (hint == 3)
                                    {
                                        MiniGames.TypeWriter($"It is a {Help4()}-digit number", 0.025f);
                                    }
                                }
                                else
                                {
                                    MiniGames.TypeWriter($"HINT: It is a {Help4()}-digit number", 0.025f);
                                }
                            }
                            static int CountMatchingDigits(int num1, int num2)
                            {
                                // Convert digits of num2 into a list so we can "consume" matches
                                List<char> num2Digits = new List<char>(num2.ToString());
                                int count = 0;

                                foreach (char digit in num1.ToString())
                                {
                                    if (num2Digits.Contains(digit))
                                    {
                                        count++;
                                        num2Digits.Remove(digit); // Prevent reusing the same digit
                                    }
                                }
                                return count;
                            }
                            static string Help1(int num, int corr_ans)
                            {
                                int matchCount = CountMatchingDigits(num, corr_ans);

                                return $"But you got {matchCount} digits right.";
                            }
                            string Help2()
                            {
                                int x = random.Next(lo,r3);

                                if (lo > lastHint)
                                {
                                    lastHint = lo;
                                }

                                while (x < lastHint)
                                {
                                    x = random.Next(lo, r3);
                                }
                                    
                                lastHint = x;
                                return "HINT: More than " + x;
                            }
                            string Help3()
                            {
                                int y = random.Next(r3,hi);

                                if (lastSuggestion > hi)
                                {
                                    lastSuggestion = hi;
                                }
                                    
                                while (y > lastSuggestion)
                                {
                                    y = random.Next(r3,hi);
                                }
                                    
                                lastSuggestion = y;
                                    
                                return "HINT: Less than " + y;
                            }
                            int Help4()
                            {
                                int count = 0;
                                for (int i = 0; i < 6; i++)
                                {
                                    string sn = corr_ans.ToString();
                                    {
                                        count = sn.Length;
                                    }
                                }
                                return count;
                            }
                        }
                        if (num == corr_ans)
                        {
                            MiniGames.TypeWriter("WOW! How did you find it?!", 0.01f);
                            Thread.Sleep(1000);
                            results();
                        }
                        if (attempts3 > 10 && (hi - lo) < 10)
                        {
                            MiniGames.TypeWriter("GAME OVER!");
                            Thread.Sleep(1000);
                            MiniGames.TypeWriter("The number was" + corr_ans);
                            InitialiseGame();
                        }
                    }

                    else
                    {
                        MiniGames.TypeWriter("This game is played with integer numbers. Try again", 0.025f);
                    }
                }
            }
            if (option == 2 || option == 3)
            {
                //Time
                DateTime now = DateTime.Now;
                
                if (option == 2)  
                {
                   // Format the time as Hmm (e.g., 2:45 AM = 245)
                    string timeNumeric = now.ToString("Hmm"); // "245"
                    corr_ans = Convert.ToInt32(timeNumeric);
                    num = 0;
                    bool hinted = false;

                    while (num != corr_ans) 
                    {
                        Console.Write(">");
                        number = Console.ReadLine();

                        gtn = int.TryParse(number, out num);

                        if (gtn)
                        {
                            if (num == corr_ans || num == corr_ans + 1)
                            {
                                MiniGames.TypeWriter("Haha! You really thought it was a random number between 1 and 1.000.000?\n", 0.025f, false);
                                Thread.Sleep(1000);
                                MiniGames.TypeWriter("Well, hang on in there. I might do it sometime!");
                                Thread.Sleep(1000);

                                results();
                            }

                            if (attempts3 > 3 && hinted == false && (num != corr_ans || num != corr_ans +1))
                            {
                                if (corr_ans != Convert.ToInt32(timeNumeric))
                                {
                                    corr_ans = Convert.ToInt32(timeNumeric);
                                }
                                MiniGames.TypeWriter("HINT: It's the current time!");
                                hinted = true;
                                
                            }
                            if (num != corr_ans && hinted == false)
                            {
                                MiniGames.TypeWriter("Sorry, that's not it.");
                                attempts3++;
                            }

                            
                        }   
                    }
                }
                

                if (option == 3)
                {
                    //Date
                    num = 0;

                    // Format the date as ddMMyy
                    string dateNumeric = now.ToString("ddMMyy"); // "120425"

                    corr_ans = Convert.ToInt32(dateNumeric);
                    bool hint = false;

                    while (num != corr_ans)
                    {
                        Console.Write(">");
                        number = Console.ReadLine();

                        gtn = int.TryParse(number, out num);

                        if (attempts3 > 3 && hint == false)
                        {
                            MiniGames.TypeWriter("HINT: It's the current date!");
                            hint = true;
                        }

                        if (num == corr_ans)
                        {
                            MiniGames.TypeWriter("Haha! You really thought it was a random number between 1 and 1.000.000?\n", 0.025f, false);
                            Thread.Sleep(1000);
                            MiniGames.TypeWriter("Well, hang on in there. I might do it sometime!");
                            Thread.Sleep(1000);
                            results();
                        }
                        
                        if (num != corr_ans && hint == false)
                        {
                            MiniGames.TypeWriter("Sorry, that's not it.");
                            attempts3++;
                        }
                    }
                }
            }
            #endregion round3
            void results()
            {
                MiniGames.TypeWriter("Do you want to see your statistics? (y/n)\n>",newLine:false);
                ConsoleKeyInfo answer = Console.ReadKey(true);
                if (answer.Key == ConsoleKey.Y)
                {
                    Console.Write(char.ToLower(answer.KeyChar));
                    List<string> results = new List<string>
                    {
                        "Round1: " + attempts1 + " attempts",
                        "Round2: " + attempts2 + " attempts",
                        "Round3: " + attempts3 + " attempts",
                    };
                    Console.WriteLine();
                    printResults(results);

                    void printResults(List<string> lines)
                    {
                        // Get the length of the longest line
                        int maxLength = lines.Max(line => line.Length);
                        string horizontalBorder = "+" + new string('-', maxLength + 2) + "+";

                        MiniGames.TypeWriter(horizontalBorder, 0.025f);
                        foreach (string line in lines)
                        {
                            MiniGames.TypeWriter(("| " + line.PadRight(maxLength) + " |"), 0.025f);
                        }
                        MiniGames.TypeWriter(horizontalBorder, 0.025f);
                    }
                    Thread.Sleep(1000);
                    MiniGames.TypeWriter("\nThanks for playing!", 0.025f);
                    Thread.Sleep(1000);
                    MiniGames.TypeWriter("Press any key to continue",0.025f);
                    Console.Write(">");
                    Console.ReadKey();
                    Console.Clear();
                    GTN();
                }
                else
                {
                    Console.Clear();
                    GTN();
                }
           }
        }
    }
}