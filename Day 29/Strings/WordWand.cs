using System;

namespace Day29
{
    class WordWand
    {
        static void Run()
        {
            Console.WriteLine("Enter the sentence");

            string sentence = Console.ReadLine() ?? string.Empty;

            // Validate input
            if (!IsValid(sentence))
            {
                Console.WriteLine("Invalid Sentence");
                return;
            }

            // Split words
            string[] words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int count = words.Length;

            Console.WriteLine("Word Count: " + count);

            // Even → Reverse word order
            if (count % 2 == 0)
            {
                ReverseWordOrder(words);
            }
            // Odd → Reverse letters
            else
            {
                ReverseEachWord(words);
            }
        }

        // Validation
        static bool IsValid(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }
            return true;
        }

        // Reverse words (manual)
        static void ReverseWordOrder(string[] words)
        {
            int left = 0;
            int right = words.Length - 1;

            while (left < right)
            {
                string temp = words[left];
                words[left] = words[right];
                words[right] = temp;

                left++;
                right--;
            }

            Print(words);
        }

        // Reverse letters in each word (manual)
        static void ReverseEachWord(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                char[] letters = words[i].ToCharArray();

                int left = 0;
                int right = letters.Length - 1;

                while (left < right)
                {
                    char temp = letters[left];
                    letters[left] = letters[right];
                    letters[right] = temp;

                    left++;
                    right--;
                }

                words[i] = new string(letters);
            }

            Print(words);
        }

        // Print result
        static void Print(string[] words)
        {
            string result = "";

            for (int i = 0; i < words.Length; i++)
            {
                result += words[i];

                if (i < words.Length - 1)
                    result += " ";
            }
            Console.WriteLine(result);
        }
    }
}
