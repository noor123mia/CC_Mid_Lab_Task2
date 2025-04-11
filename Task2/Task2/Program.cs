using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    // Define a structure to hold details of each token
    struct Token
    {
        public string VarName;        // Variable name like a1, b2, etc.
        public string SpecialSymbol;  // Special symbol(s) found in value
        public string TokenType;      // Token type (Identifier in this case)
    }

    static void Main()
    {
        Console.WriteLine("Enter your code (in mini-language):");
        string input = Console.ReadLine(); // Simulates textbox input

        /*
         * REGEX Explanation:
         * ------------------
         * (var|float)\s+        → Match 'var' or 'float' followed by space(s)
         * ([abc]\w*\d)          → Variable name:
         *                          - starts with a, b, or c
         *                          - can have letters/digits (_ included)
         *                          - ends with digit
         * \s*=\s*               → Match '=' with optional surrounding spaces
         * .*?([\W_]+)           → Non-greedy match of value part, capturing non-alphanumeric symbols (special characters)
         * ;                     → Ends with a semicolon
         */
        string pattern = @"(var|float)\s+([abc]\w*\d)\s*=\s*.*?([\W_]+);";

        // Find all matches in the input string
        MatchCollection matches = Regex.Matches(input, pattern);

        // List to store extracted tokens
        List<Token> tokens = new List<Token>();

        // Iterate over each match and extract info
        foreach (Match match in matches)
        {
            string varName = match.Groups[2].Value;       // e.g., a1 or b2
            string specialSymbol = match.Groups[3].Value; // e.g., @ or $$

            // Add the extracted token to the list
            tokens.Add(new Token
            {
                VarName = varName,
                SpecialSymbol = specialSymbol,
                TokenType = "Identifier"
            });
        }

        // Print table header
        Console.WriteLine("\n{0,-10} | {1,-15} | {2}", "VarName", "SpecialSymbol", "Token Type");
        Console.WriteLine(new string('-', 45));

        // If no tokens found, show message
        if (tokens.Count == 0)
        {
            Console.WriteLine("No valid tokens found.");
        }
        else
        {
            // Display all tokens in formatted output
            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.VarName,-10} | {token.SpecialSymbol,-15} | {token.TokenType}");
            }
        }
    }
}
