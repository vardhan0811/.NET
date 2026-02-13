using System;
using System.Collections.Generic;
using System.Text;

namespace Day42
{
    public class TextEditor
    {
        public static void Run()
        {
            Stack<string> words = new Stack<string>();
            bool exit = false;

            Console.WriteLine("===== TEXT EDITOR =====");
            Console.WriteLine("Available Commands:");
            Console.WriteLine("1. TYPE <word>  -> Add a word");
            Console.WriteLine("2. UNDO         -> Remove last typed word");
            Console.WriteLine("3. EXIT         -> Finish editing");
            Console.WriteLine("Example: TYPE Hello");
            Console.WriteLine();

            while(!exit)
            {
                Console.Write("Enter command: ");
                string input = Console.ReadLine();

                if(input.StartsWith("TYPE ")
                {
                    string word = input.Substring(5).Trim();
                    if(!string.IsNullOrEmpty(word))
                    {
                        words.Push(word);
                        Console.WriteLine($"Typed: {word}");
                    }
                    else
                    {
                        Console.WriteLine("Please provide a word to type.");
                    }
                }

                else if(input == "UNDO")
                {
                    if(words.Count > 0)
                    {
                        string removedWord = words.Pop();
                        Console.WriteLine($"Removed: {removedWord}");
                    }
                    else
                    {
                        Console.WriteLine("No words to undo.");
                    }
                }
                else if(input == "EXIT")
                {
                    exit = true;
                    Console.WriteLine("Exiting Text Editor...");
                }
                else
                {
                    Console.WriteLine("Invalid command. Please try again.");
                }
            }

            // Stack order is reversed → convert properly
            string[] arr = words.ToArray();
            Array.Reverse(arr);

            // Build final text using StringBuilder
            StringBuilder finalText = new StringBuilder();
            foreach(string word in arr)
            {
                finalText.Append(word + " ");
            }

            Console.WriteLine("Final Text: " + finalText.ToString().Trim());
        }
    }
}