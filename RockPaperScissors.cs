//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;

namespace MiniGames
{
    public class RockPaperScissors
    {
        static Dictionary<string, string[]> hands = new()
            {
                { "rockP", new[]
                   {
                    "     ________",
                    "____/   _____)",
                    "       (______)",
                    "       (______)",
                    "____   (______)",
                    "    \\__(______)"
                    }
                },

                { "paperP", new[]
                    {
                    "    _________",
                    "___/     ____)______",
                    "             _______) ",
                    "             _________)",
                    "___         _________)",
                    "   \\______________)"
                    }
                },

                { "scissorsP", new[]
                    {
                    "         __________",
                    "________/    ______)______",
                    "                  ________)___",
                    "           ___________________)",
                    "________  (_________)",
                    "        \\__(________)"
                    }
                },


                { "rockC", new[]
                    {
                    " ________     ",
                    "(_____   \\____",
                    "(______)       ",
                    "(______)       ",
                    "(______)   ____",
                    "(______)__/    "
                    }
                },

                { "paperC", new[]
                    {
                    "         _________    ",
                    "  ______(____     \\___",
                    " (_______             ",
                    "(_________            ",
                    "(_________         ___",
                    "   (______________/   "
                    }
                },

                { "scissorsC", new[]
                    {
                    "           __________         ",
                    "    ______(______    \\________",
                    " __(________                  ",
                    "(___________________           ",
                    "          (_________)  ________",
                    "          (________)__/        "
                    }
                }
            };
        static int playerScore = 0, computerScore = 0;

        public static void drawScores()
        {
            int totalWidth = Console.WindowWidth;
            Console.SetCursorPosition(totalWidth / 10, 0);//Console.WindowHeight / 2 - maxLines / 2 - 2); // Position for Player
            Console.Write("Player");
            Console.SetCursorPosition(totalWidth / 2, 0);// Console.WindowHeight / 2 - maxLines / 2 - 2); // Position for CPU
            Console.WriteLine("CPU");
            Console.SetCursorPosition(totalWidth / 13, 1);
            Console.Write("Rounds won: " + playerScore);
            Console.SetCursorPosition((totalWidth / 2) - 5, 1);
            Console.WriteLine("Rounds won: " + computerScore);
        }

        public static void RPS()
        {
            Console.Clear();
            MiniGames.TypeWriter("Rock, Paper, Scissors");
            MiniGames.TypeWriter("---------------------");
            MiniGames.TypeWriter("Instructions: Let's play a fun game of rock paper scissors.\nIf you are not familiar, the rules are simple.\nROCK beats SCISSORS | SCISSORS beats PAPER | PAPER beats ROCK.\nFirst to 3 wins.");
            Console.WriteLine();
            Console.WriteLine();
            MiniGames.TypeWriter("Press ENTER to begin, Q to quit.");
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

        public static void InitialiseGame()
        {
            drawScores();

            for (int i = 0; i < 3; i++)
            {
                Random rand = new Random();
                rand.Next(0,3);
                string cChoice = "rock";
                string pChoice = "rock";
            
                // Draw both hands on the screen at the same time
                string[] leftHand = hands[pChoice + "P"];
                string[] rightHand = hands[cChoice + "C"];
                
                for (int y = 2; y < Console.WindowHeight; y++)
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                Console.SetCursorPosition(0, 2);

                switch (rand.Next(0,3))
                {
                    case 0:
                    {
                        cChoice = "rock";
                        break;
                    }
                    case 1:
                    {
                        cChoice = "paper";
                        break;
                    }
                    case 2:
                    {
                        cChoice = "scissors";
                        break;
                    }
                }

                DrawHands(leftHand, rightHand, offset: 0); // Draw both hands, no shaking, just static pose

                Console.Write("ROCK, PAPER, or SCISSORS?\n>");
                do
                {
                    pChoice = Console.ReadLine().ToLower();
                    drawScores();
                    Console.SetCursorPosition(0, 15);
                    Console.Write("> ");
                    Console.SetCursorPosition(1, 15);
                }
                while (pChoice != "rock" && pChoice != "r" && pChoice != "paper" && pChoice != "p" && pChoice != "scissors" && pChoice != "s" && pChoice != "q");

                if (pChoice == "rock" || pChoice == "r")
                {
                    pChoice = "rock";
                }
                if (pChoice == "paper" || pChoice == "p")
                {
                    pChoice = "paper";
                }
                if (pChoice == "scissors" || pChoice == "s")
                {
                    pChoice = "scissors";
                }
                if (pChoice == "q")
                {
                    MiniGames.exit();
                }
                    Console.WriteLine("Winner is: "+ AnimateHand(pChoice, cChoice));
                drawScores();
                Console.SetCursorPosition(0, 15);
                Console.Write('>');
                Thread.Sleep(2000);
            }
            playerScore = 0; computerScore = 0; RPS();
        }

        public static string AnimateHand(string pChoice, string cChoice)
        {
            void ClearFromLine(int startLine)
            {
                for (int i = startLine; i < Console.WindowHeight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                Console.SetCursorPosition(0, startLine); // Optional: reset cursor
            }



            // Print the "Player" and "CPU" text above the hands
            int totalWidth = Console.WindowWidth;
            Console.SetCursorPosition(totalWidth / 10, 0);//Console.WindowHeight / 2 - maxLines / 2 - 2); // Position for Player
            Console.Write("Player");
            Console.SetCursorPosition(totalWidth / 2, 0);// Console.WindowHeight / 2 - maxLines / 2 - 2); // Position for CPU
            Console.WriteLine("CPU");
            Console.SetCursorPosition(totalWidth / 13, 1);
            Console.Write("Rounds won: " + playerScore);
            Console.SetCursorPosition((totalWidth / 2) - 5, 1);
            Console.WriteLine("Rounds won: " + computerScore);

            string[] leftHand = RockPaperScissors.hands["rockP"]; // Always animate from rock
            string[] rightHand = RockPaperScissors.hands["rockC"];

            int shakes = 3;
            int delay = 250;
            int verticalOffset = 1;

            for (int i = 0; i < shakes; i++)
            {
                ClearFromLine(2);
                DrawHands(leftHand, rightHand, offset: verticalOffset);
                Thread.Sleep(delay);
                ClearFromLine(2);
                DrawHands(leftHand, rightHand, offset: -verticalOffset);
                Thread.Sleep(delay);
            }
            
            // Final pose: show actual player choice
            rightHand = RockPaperScissors.hands[cChoice + "C"];
            leftHand = RockPaperScissors.hands[pChoice + "P"];

            ClearFromLine(2);
            DrawHands(leftHand, rightHand, offset: 0);

            string winner = DetermineWinner(pChoice, cChoice);
            int[] score = updateScores(winner);

            if (score[0] == 1 && score [1] == 0)
            {
                playerScore++;
            }
            if (score[0] == 0 && score[1] == 1)
            {
                computerScore++;
            }

            switch (winner)
            {
                case "player":
                return "Player";
                
                case "cpu":
                return "CPU";
                
                case "draw":
                return "Draw";
            }
        return "";
        }

        static void DrawHands(string[] leftHand, string[] rightHand, int offset)
        {
            int maxLines = Math.Max(leftHand.Length, rightHand.Length);
            int totalWidth = Console.WindowWidth;
            int rightHandStart = totalWidth / 2 - 60; // Push computer hand more to the right
            int topPadding = (Console.WindowHeight / 3 - maxLines / 2 + offset)/2;

            for (int i = 0; i < topPadding; i++)
                Console.WriteLine();

            for (int i = 0; i < maxLines; i++)
            {
                string leftLine = i < leftHand.Length ? leftHand[i] : "";
                string rightLine = i < rightHand.Length ? rightHand[i] : "";

                string paddedLeft = leftLine.PadRight(totalWidth / 2);
                string paddedRight = rightLine.PadLeft(rightHandStart);

                Console.WriteLine(paddedLeft + paddedRight);
            }
            for (int i = 0; i < topPadding; i++)
                Console.WriteLine();
        }

        public static string DetermineWinner(string player, string cpu)
        {
            var beats = new Dictionary<string, string>()
            {
                { "rock", "scissors"  },
                { "paper", "rock"     },
                { "scissors", "paper" }
            };

            if (player == cpu)
                return "draw";
            else if (beats[player] == cpu) 
            { 
                return "player";
            }
            else 
            {
                return "cpu";
            }
        }

        static int[] updateScores(string match)
        {
            if (match == "player")
            {
                return [1,0];
            }
            if (match == "cpu")
            {
                return [0,1];
            }
            return [0, 0];
        }
    }
}