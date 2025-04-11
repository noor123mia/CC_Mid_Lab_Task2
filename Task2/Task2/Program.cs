using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    // Define a structure to store token info
    struct Token
    {
        public string VarName;
        public string SpecialSymbol;
        public string TokenType;
    }

    static void Main()
    {
        Console.WriteLine("Enter your code (in mini-language):");
        string input = Console.ReadLine(); // Simulating textbox input

        // Regex Explanation:
        // (var|float)\s+       => Match declaration keyword (var or float)
        // ([abc]\w*\d)         => Variable name: starts with a/b/c and ends with digit
        // \s*=\s*              => Equal sign (with optional spaces)
        // .*?([\W_]+)          => Match the value, extract special characters (non-alphanumeric)
        // ;                    => End of statement
        string pattern = @"(var|float)\s+([abc]\w*\d)\s*=\s*.*?([\W_]+);";

        MatchCollection matches = Regex.Matches(input, pattern);

        List<Token> tokens = new List<Token>();

        foreach (Match match in matches)
        {
            string varName = match.Groups[2].Value;
            string specialSymbol = match.Groups[3].Value;

            tokens.Add(new Token
            {
                VarName = varName,
                SpecialSymbol = specialSymbol,
                TokenType = "Identifier"
            });
        }

        // Output the results
        Console.WriteLine("\n{0,-10} | {1,-15} | {2}", "VarName", "SpecialSymbol", "Token Type");
        Console.WriteLine(new string('-', 45));

        if (tokens.Count == 0)
        {
            Console.WriteLine("No valid tokens found.");
        }
        else
        {
            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.VarName,-10} | {token.SpecialSymbol,-15} | {token.TokenType}");
            }
        }
    }
}
