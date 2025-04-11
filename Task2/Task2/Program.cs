using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    // Structure to hold token details
    struct Token
    {
        public string VarName;         // Variable name that matches criteria
        public string SpecialSymbol;   // Non-alphanumeric symbols found in the value
        public string TokenType;       // Type of token (fixed as "Identifier" here)
    }

    static void Main()
    {
        Console.WriteLine("Enter your code (you can enter multiple declarations):");
        string input = Console.ReadLine(); // Read full input line

        /*
         * Regex Pattern Explanation:
         * --------------------------
         * (var|float)                → Match declaration keyword: 'var' or 'float'
         * \s+                        → Match one or more spaces
         * ([abc][a-zA-Z0-9_]*\d)     → Match variable name:
         *                             → starts with 'a', 'b', or 'c'
         *                             → followed by any number of letters, digits, or underscores
         *                             → ends with a digit
         * \s*=\s*                    → Match '=' with optional spaces around it
         * ([^;]*)                    → Capture value (everything before the semicolon)
         * ;                          → End of statement
         */
        string pattern = @"(var|float)\s+([abc][a-zA-Z0-9_]*\d)\s*=\s*([^;]*);";
        MatchCollection matches = Regex.Matches(input, pattern); // Apply regex to input

        List<Token> tokens = new List<Token>(); // List to store valid tokens

        // Loop through each matched declaration
        foreach (Match match in matches)
        {
            string varName = match.Groups[2].Value;      // Extract variable name
            string valuePart = match.Groups[3].Value;    // Extract value part

            // Initialize container for special characters
            string specialChars = "";

            // Loop through value to find non-alphanumeric characters
            foreach (char ch in valuePart)
            {
                // Check if the character is not a letter, digit, or whitespace
                if (!char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch))
                {
                    specialChars += ch; // Append special character
                }
            }

            // Only add token if special characters are found in the value
            if (!string.IsNullOrEmpty(specialChars))
            {
                tokens.Add(new Token
                {
                    VarName = varName,
                    SpecialSymbol = specialChars,
                    TokenType = "Identifier"
                });
            }
        }

        // Output table header
        Console.WriteLine("\n{0,-15} | {1,-20} | {2}", "VarName", "SpecialSymbol", "Token Type");
        Console.WriteLine(new string('-', 55));

        // If no valid tokens were found, display appropriate message
        if (tokens.Count == 0)
        {
            Console.WriteLine("No valid tokens found.");
        }
        else
        {
            // Print each valid token in tabular format
            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.VarName,-15} | {token.SpecialSymbol,-20} | {token.TokenType}");
            }
        }
    }
}
