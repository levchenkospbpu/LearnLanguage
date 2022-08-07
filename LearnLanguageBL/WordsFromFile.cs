// <copyright file="WordsFromFile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LearnLanguageBL
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This class is used to read words from a file and compile a dictionary from them.
    /// </summary>
    public class WordsFromFile
    {
        private Random random = new Random();
        private string[] allLines;
        private int thisSymbolIndex = 0;
        private Dictionary<string, List<string>> wordsWithTranslationsDictionary = new Dictionary<string, List<string>>();
        private string actualWord = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="WordsFromFile"/> class.
        /// Write file to string array.
        /// </summary>
        public WordsFromFile()
        {
            if (File.Exists("TxtFiles/Words.txt"))
            {
                this.allLines = File.ReadAllLines("TxtFiles/Words.txt");
                this.wordsWithTranslationsDictionary = this.CompileDictionary(this.allLines);
            }
        }

        /// <summary>
        /// Сompilation of a dictionary in which the key is a foreign word,
        /// and the value is the list of translations of the given word into other language.
        /// </summary>
        /// <param name="fileLines"> File which is written in string array. </param>
        /// <returns> Dictionary with words and translations. </returns>
        public Dictionary<string, List<string>> CompileDictionary(string[] fileLines)
        {
            if (fileLines == null)
            {
                throw new ArgumentNullException(nameof(fileLines));
            }

            Dictionary<string, List<string>> wordsWithTranslations = new Dictionary<string, List<string>>();
            string thisLine = string.Empty;
            int lineCount = fileLines.Length;

            for (int i = 0; i < lineCount; i++)
            {
                List<string> answers = new List<string>();
                string word = string.Empty;
                string answer = string.Empty;

                // Pointer to string character index
                this.thisSymbolIndex = 0;

                thisLine = fileLines[i];

                string format = @"(\S+[ ][-][ ]\S+[;])";
                Match trueFormat = Regex.Match(thisLine, format);
                if (!trueFormat.Success)
                {
                    // line has invalid format
                    continue;
                }

                thisLine = trueFormat.Value;

                for (this.thisSymbolIndex = 0; this.thisSymbolIndex < thisLine.Length; this.thisSymbolIndex++)
                {
                    if (thisLine[this.thisSymbolIndex] == ' ')
                    {
                        break;
                    }

                    // Making a word from a line to space
                    word += thisLine[this.thisSymbolIndex];
                }

                this.thisSymbolIndex += 3;

                for (answer = string.Empty; this.thisSymbolIndex < thisLine.Length; this.thisSymbolIndex++)
                {
                    if (this.thisSymbolIndex == thisLine.Length - 1)
                    {
                        if (!string.IsNullOrEmpty(answer))
                        {
                            answers.Add(answer);
                        }
                    }

                    if (thisLine[this.thisSymbolIndex] != ',')
                    {
                        answer += thisLine[this.thisSymbolIndex];
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(answer))
                        {
                            answers.Add(answer);
                            answer = string.Empty;
                        }
                    }
                }

                if (answers.Count != 0)
                {
                    wordsWithTranslations.Add(word, answers);
                }
            }

            return wordsWithTranslations;
        }

        /// <summary>
        /// Get word which is shown to the user.
        /// </summary>
        /// <returns> Word that user must translate. </returns>
        public string GetWord()
        {
            this.actualWord = this.wordsWithTranslationsDictionary.Keys.ElementAt(this.random.Next(this.wordsWithTranslationsDictionary.Count - 1));
            return this.actualWord;
        }

        /// <summary>
        /// Get answer.
        /// </summary>
        /// <returns> True answer. </returns>
        public string GetAnswer()
        {
            bool valueFound = this.wordsWithTranslationsDictionary.TryGetValue(this.actualWord, out List<string> answers);
            if (valueFound)
            {
                string answer = string.Empty;
                for (int i = 0; i < answers.Count; i++)
                {
                    answer += answers[i];
                    if (i < answers.Count - 1)
                    {
                        answer += ", ";
                    }
                }

                return answer;
            }

            return string.Empty;
        }

        /// <summary>
        /// Сomparison of the answer given by the user with each of the translations in the list of the required word.
        /// </summary>
        /// <param name="word"> Word which user must traslate. </param>
        /// <param name="answer"> User answer. </param>
        /// <returns> Is answer correct. </returns>
        public bool CheckResult(string word, string answer)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentException("message", nameof(word));
            }

            bool valueFound = this.wordsWithTranslationsDictionary.TryGetValue(word, out List<string> answers);
            if (valueFound)
            {
                for (int i = 0; i < answers.Count; i++)
                {
                    if (string.Equals(answer, answers[i], StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}