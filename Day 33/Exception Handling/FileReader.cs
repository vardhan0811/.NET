using System;
using System.IO;

class FileReader
{
    static void Main()
    {
        string filePath = "data.txt";

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine("File Content:");
                Console.WriteLine(content);
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: The file was not found.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: You do not have permission to access this file.");
        }
    }
}