// Course Material "Software Service Engineering"
// (c) 2013 by Distributed and Self-organizing Systems Group, TUC

using System;
using System.Text.RegularExpressions;

namespace Vsr.Teaching.SSE.Sample
{
    /// <summary>
    /// A sample program that demonstrates the parsing of text.
    /// </summary>
    class TextParsingExample
    {
        /// <summary>
        /// The main program routine.
        /// </summary>        
        static void Main(string[] args)
        {
            string myText = "This is a sample sentence.";

            // working with character positions
            int pos = myText.IndexOf(" a ");
            Console.WriteLine(myText.Substring(0, pos));
            Console.WriteLine(Environment.NewLine);

            // splitting text at certain characters
            string[] words = myText.Split(' ');
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine(Environment.NewLine);

            // matching with regular expressions
            Match match = Regex.Match(myText, @"^(?<a>[^s]*s)(?<b>[^p]*p)(?<c>.*)$");
            Console.WriteLine(match.Groups["a"].Value); // This
            Console.WriteLine(match.Groups["b"].Value); //  is a samp
            Console.WriteLine(match.Groups["c"].Value); // le sentence.
            Console.WriteLine(Environment.NewLine);

            // parsing numbers
            myText = "41";
            int myNumber = Int32.Parse(myText);
            Console.WriteLine(myNumber + 1);
            Console.WriteLine(Environment.NewLine);

            // convert between numbers and hexadecimal strings
            string hex = Convert.ToByte(42).ToString("x");
            int number = Int32.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("hex: {0}, dec: {1}", hex, number);
            Console.WriteLine(Environment.NewLine);

            Console.ReadLine();
        }

    }
}

