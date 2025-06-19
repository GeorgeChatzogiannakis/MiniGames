using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniGames
{
    public static class Settings
    {
        static char tl = '╔'; // Top left
        static char tr = '╗'; // Top right
        static char bl = '╚'; // bottom left
        static char br = '╝'; // Bottom right
        static char h = '═';  // Horizontal
        static char v = '║';  // Vertical  

        public static bool tw { get; set; } = true;
        public static float delay { get; set; } = 0.025f;

        public static void SettingsManager()
        {
            Console.OutputEncoding = Encoding.UTF8;
            ConsoleKey key;
            bool inEditMode = false;

            RedrawMenu(inEditMode);
            Console.SetCursorPosition(2, 10);
            while (true)
            {
                ClearLine(10);
                Console.WriteLine(v+"Action: ");
                Console.SetCursorPosition(56, 10);
                Console.Write(v);
                ClearLine(11);
                Console.WriteLine(v+"E:Edit, Q:Quit"+new String(' ',41)+v);
                Console.SetCursorPosition(9, 10);
                key = Console.ReadKey(intercept: true).Key;
                
                if (key == ConsoleKey.Q)
                {
                    inEditMode = false;
                    Console.Clear();
                    MiniGames.GameHub();
                }

                if (key == ConsoleKey.E)
                {

                    if (!inEditMode)
                    {
                        inEditMode = true;
                        Console.SetCursorPosition(0, 10);
                        Console.Write(v+"Set Delay: ");
                        Console.SetCursorPosition(0, 11);
                        Console.WriteLine(v+"Editing Mode: Enter a value (0-1). Press 'B' to go back"+v);
                        Console.SetCursorPosition(11, 10);
                        string input = Console.ReadLine()?.Trim().ToLower();
                                                
                        Console.SetCursorPosition(1, 10);

                        if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out float val))
                        {
                            if (val >= 0 && val <= 1)
                            {
                                delay = val; // Update Delay
                                if (val != 0)
                                {
                                    //ClearLine(2);
                                    Console.SetCursorPosition(0,3);
                                    Console.WriteLine(v+"Type writer Effect: ON" + new String(' ', 33) + v);
                                    ClearLine(4);
                                    Console.Write(v + $"Typewriter Delay (in ms): " + val + new String(' ', 56 - (27 + delay.ToString().Length)) + v);
                                    Console.SetCursorPosition(0, 5);
                                    Console.Write(v); 
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("UPDATED!");
                                    Console.ResetColor();
                                    inEditMode = false;
                                }

                                if (val == 0)
                                {
                                    tw = false;
                                    ClearLine(3);
                                    Console.WriteLine(v+"Type writer Effect: OFF" + new String(' ', 32) + v);
                                    ClearLine(4);
                                    Console.SetCursorPosition(0, 4);
                                    Console.WriteLine(v+$"Typewriter Delay (in ms): " + delay + new String(' ', 56 - (27 + delay.ToString().Length)) + v);
                                    Console.Write(v);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("UPDATED!");
                                    Console.ResetColor();
                                    inEditMode = false;
                                }

                                else
                                {
                                    ClearLine(3);
                                    Console.Write(v+"Type writer Effect: ON" + new String(' ',33)+v);
                                }
                            }

                            else
                            {
                                ShowError("Value must me between 0 and 1.");
                                inEditMode = false;
                            }
                        }

                        else if (input == "b")
                        {
                            RedrawMenu(false);
                            inEditMode = false;
                        }

                        else
                        {
                            ShowError("Invalid Input");
                            inEditMode = false;
                        }             
                        
                    }
                } 
            }
        }
        private static void RedrawMenu(bool editing)
        {
            int dynamicInput = delay.ToString().Length;

            Console.Clear();
            Console.Write(tl);
            // FIRTS ROW
            for (int i = 0; i < 55; i++)
                Console.Write(h);
            Console.WriteLine(tr);
            //
            Console.WriteLine(v+new String(' ',21)+"SETTINGS MENU"+new String(' ',21)+v);
            Console.WriteLine(v + new String(' ', 55) + v);
            if (tw) { Console.WriteLine(v+"Type writer Effect: ON"+new String(' ',33)+v); } else { Console.WriteLine(v+"Type writer Effect: OFF"+ new String(' ',32)+v); }
            Console.WriteLine(v+"Typewriter Delay (in ms): " + delay + new String(' ',56-(27+dynamicInput))+v); 
            for (int i = 0; i<8; i++)
                Console.WriteLine(v+new String(' ',55)+v);
            Console.SetCursorPosition(0,13);
            Console.WriteLine(bl + new string(h,55)+ br);
            if (editing)
            {
                Console.SetCursorPosition(2, 11);
                Console.WriteLine(v+"Editing Mode: Enter a value (0-1), 'off' to disable, or 'B' to go back");
                Console.SetCursorPosition(2, 10);
            }
            
        }
        private static void ShowError(string message) 
        { 
            Console.SetCursorPosition(1, 12);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message); 
            Console.ResetColor();
            Console.SetCursorPosition(1, 10);
            Console.Write(new String(" "+ 42)+v);
        }

        public static void ClearLine(int line)
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, line);
            Console.Write(new String(' ',Console.WindowWidth));
            Console.SetCursorPosition(0, line);
        }
    }
}

