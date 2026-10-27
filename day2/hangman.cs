using System;
using System.IO;
using System.Collections.Generic;

class Hangman
{
    static void Main()
    {
        string Wordfile = "./word.txt";
        if (!File.Exists(Wordfile))
        {
            Console.WriteLine("wordlist is not there");
            return;
        }

        string[] words = File.ReadAllLines(Wordfile);
        if (words.Length == 0)
        {
            Console.WriteLine("the file is empty");
            return;
        }

        Random random = new Random();
        int randomword = random.Next(words.Length);
        string secretword = words[randomword].Trim().ToLower();

        HashSet<char> guessedLetters = new HashSet<char>();
        int tries = 7;

        while (tries > 0)
        {
            Console.WriteLine();
            bool displayWin = true;

            foreach (char c in secretword)
            {
                if (guessedLetters.Contains(c))
                {
                    Console.Write(c + " ");
                }
                else
                {
                    Console.Write("_ ");
                    displayWin = false;
                }
            }
            Console.WriteLine();

            if (displayWin)
            {
                Console.WriteLine("You won!");
                return;
            }

            Console.WriteLine($"Tries left: {tries}");
            Console.WriteLine("Guess a letter:");
            
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                continue;
            }

            char guess = char.ToLower(input[0]);

            if (guessedLetters.Contains(guess))
            {
                Console.WriteLine("You already guessed that letter.");
                continue;
            }

            guessedLetters.Add(guess);

            if (secretword.Contains(guess.ToString()))
            {
                Console.WriteLine("Correct letter!");
            }
            else
            {
                Console.WriteLine("Wrong letter!");
                tries--;
            }
        }

        Console.WriteLine($"Game over! The word was: {secretword}");
    }
}