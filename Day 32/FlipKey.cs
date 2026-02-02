using System;

namespace Day32
{
public class Flipkey
{
    public string CleanseAndInvert(string input)
    {
        // 1. Check for null or length < 6
        if (string.IsNullOrEmpty(input) || input.Length < 6)
            return string.Empty;

        // 2. Check for only alphabets (no space, digit, or special char)
        foreach (char c in input)
        {
            if (!char.IsLetter(c))
                return string.Empty;
        }

        // 3. Convert to lowercase
        string lower = input.ToLower();

        // 4. Remove characters with even ASCII values
        string filtered = "";
        foreach (char c in lower)
        {
            if (((int)c) % 2 != 0)
                filtered += c;
        }

        // If nothing left after filtering, return empty string
        if (filtered.Length == 0)
            return string.Empty;

        // 5. Reverse the filtered string
        char[] arr = filtered.ToCharArray();
        Array.Reverse(arr);
        string reversed = new string(arr);

        // 6. Uppercase even-indexed characters in reversed string
        char[] result = reversed.ToCharArray();
        for (int i = 0; i < result.Length; i++)
        {
            if (i % 2 == 0)
                result[i] = char.ToUpper(result[i]);
        }

        return new string(result);
    }

    public static void Run()
    {
        Console.WriteLine("Enter the word");
        string input = Console.ReadLine();

        Flipkey fk = new Flipkey();
        string key = fk.CleanseAndInvert(input);

        if (string.IsNullOrEmpty(key))
            Console.WriteLine("Invalid Input");
        else
            Console.WriteLine("The generated key is - " + key);
    }
}
}