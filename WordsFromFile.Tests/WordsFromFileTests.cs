// <copyright file="WordsFromFileTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LearnLanguageBLTests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using LearnLanguageBL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Class for making a dictionary from a file.
    /// </summary>
    [TestClass]
    public class WordsFromFileTests
    {
        /// <summary>
        /// Compiling a dictionary from an empty file.
        /// </summary>
        [TestMethod]
        public void CompileDictionaryEmptyFile()
        {
            WordsFromFile wordsFromFile = new WordsFromFile();
            string[] textFromFile = new string[1];
            textFromFile[0] = string.Empty;
            Dictionary<string, List<string>> translationsDictionary = wordsFromFile.CompileDictionary(textFromFile);
            Assert.AreEqual(translationsDictionary.Keys.Count, 0);
            Assert.AreEqual(translationsDictionary.Values.Count, 0);
        }

        /// <summary>
        /// Compiling a dictionary from a file with a line without a word.
        /// </summary>
        [TestMethod]
        public void CompileDictionaryNoWord()
        {
            WordsFromFile wordsFromFile = new WordsFromFile();
            string[] textFromFile = new string[1];
            textFromFile[0] = " - answer1,answer2;";
            Dictionary<string, List<string>> translationsDictionary = wordsFromFile.CompileDictionary(textFromFile);
            Assert.AreEqual(translationsDictionary.Keys.Count, 0);
            Assert.AreEqual(translationsDictionary.Values.Count, 0);
        }

        /// <summary>
        /// Compiling a dictionary from a file with a line with no answers (1).
        /// </summary>
        [TestMethod]
        public void CompileDictionaryNoAnswers1()
        {
            WordsFromFile wordsFromFile = new WordsFromFile();
            string[] textFromFile = new string[1];
            textFromFile[0] = "word - ;";
            Dictionary<string, List<string>> translationsDictionary = wordsFromFile.CompileDictionary(textFromFile);
            Assert.AreEqual(translationsDictionary.Keys.Count, 0);
            Assert.AreEqual(translationsDictionary.Values.Count, 0);
        }

        /// <summary>
        /// Compiling a dictionary from a file with a line with no answers (2).
        /// </summary>
        [TestMethod]
        public void CompileDictionaryNoAnswers2()
        {
            WordsFromFile wordsFromFile = new WordsFromFile();
            string[] textFromFile = new string[1];
            textFromFile[0] = "word - ,;";
            Dictionary<string, List<string>> translationsDictionary = wordsFromFile.CompileDictionary(textFromFile);
            Assert.AreEqual(translationsDictionary.Keys.Count, 0);
            Assert.AreEqual(translationsDictionary.Values.Count, 0);
        }

        /// <summary>
        /// Compiling a dictionary from a file with a line that doesn't contain words and answers.
        /// </summary>
        [TestMethod]
        public void CompileDictionaryNoWordNoAnswers()
        {
            WordsFromFile wordsFromFile = new WordsFromFile();
            string[] textFromFile = new string[1];
            textFromFile[0] = " - ";
            Dictionary<string, List<string>> translationsDictionary = wordsFromFile.CompileDictionary(textFromFile);
            Assert.AreEqual(translationsDictionary.Keys.Count, 0);
            Assert.AreEqual(translationsDictionary.Values.Count, 0);
        }

        /// <summary>
        /// Compiling a dictionary from a file with a line in which there is no second answer.
        /// </summary>
        [TestMethod]
        public void CompileDictionaryNoSecondAnswer()
        {
            WordsFromFile wordsFromFile = new WordsFromFile();
            string[] textFromFile = new string[1];
            textFromFile[0] = "word - answer1,;";
            Dictionary<string, List<string>> translationsDictionary = wordsFromFile.CompileDictionary(textFromFile);
            Assert.AreEqual(translationsDictionary.Keys.Count, 1);
            Assert.AreEqual(translationsDictionary.Keys.First(), "word");
            Assert.AreEqual(translationsDictionary.Values.Count, 1);
            Assert.AreEqual(translationsDictionary.Values.First()[0], "answer1");
        }

        /// <summary>
        /// Compiling a dictionary from a file with a line in which there is no first answer.
        /// </summary>
        [TestMethod]
        public void CompileDictionaryNoFirstAnswer()
        {
            WordsFromFile wordsFromFile = new WordsFromFile();
            string[] textFromFile = new string[1];
            textFromFile[0] = "word - ,answer2;";
            Dictionary<string, List<string>> translationsDictionary = wordsFromFile.CompileDictionary(textFromFile);
            Assert.AreEqual(translationsDictionary.Keys.Count, 1);
            Assert.AreEqual(translationsDictionary.Keys.First(), "word");
            Assert.AreEqual(translationsDictionary.Values.Count, 1);
            Assert.AreEqual(translationsDictionary.Values.First()[0], "answer2");
        }

        /// <summary>
        /// Compiling a dictionary from a file with a line in which there is only one answer.
        /// </summary>
        [TestMethod]
        public void CompileDictionaryOneAnswer()
        {
            WordsFromFile wordsFromFile = new WordsFromFile();
            string[] textFromFile = new string[1];
            textFromFile[0] = "word - answer;";
            Dictionary<string, List<string>> translationsDictionary = wordsFromFile.CompileDictionary(textFromFile);
            Assert.AreEqual(translationsDictionary.Keys.Count, 1);
            Assert.AreEqual(translationsDictionary.Keys.First(), "word");
            Assert.AreEqual(translationsDictionary.Values.Count, 1);
            Assert.AreEqual(translationsDictionary.Values.First()[0], "answer");
        }

        /// <summary>
        /// Compiling a dictionary from a file with a line in which there is two answers.
        /// </summary>
        [TestMethod]
        public void CompileDictionaryTwoAnswers()
        {
            WordsFromFile wordsFromFile = new WordsFromFile();
            string[] textFromFile = new string[1];
            textFromFile[0] = "word - answer1,answer2;";
            Dictionary<string, List<string>> translationsDictionary = wordsFromFile.CompileDictionary(textFromFile);
            Assert.AreEqual(translationsDictionary.Keys.Count, 1);
            Assert.AreEqual(translationsDictionary.Keys.First(), "word");
            Assert.AreEqual(translationsDictionary.Values.Count, 1);
            Assert.AreEqual(translationsDictionary.Values.First()[0], "answer1");
            Assert.AreEqual(translationsDictionary.Values.First()[1], "answer2");
        }

        /// <summary>
        /// Compiling a dictionary from a file with different lines in incorrect format.
        /// </summary>
        [TestMethod]
        public void CompileDictionaryBadFormat()
        {
            WordsFromFile wordsFromFile = new WordsFromFile();
            string[] textFromFile = new string[14];
            textFromFile[0] = "word - answer1,answer2";
            textFromFile[1] = "word - answer1 answer2;";
            textFromFile[2] = "word- answer1,answer2;";
            textFromFile[3] = "word -answer1,answer2;";
            textFromFile[4] = "word-answer1,answer2;";
            textFromFile[5] = "word - answer1 ,answer2;";
            textFromFile[6] = "word - answer1, answer2;";
            textFromFile[7] = "word - answer1,answer2 ;";
            textFromFile[8] = "word1,word2 - answer1,answer2;";
            textFromFile[9] = "word1 word2 - answer1,answer2;";
            textFromFile[10] = "word - answer1 - answer2;";
            textFromFile[11] = " word - answer1,answer2;";
            textFromFile[12] = "word  - answer1,answer2;";
            textFromFile[13] = "word -  answer1,answer2;";
            Dictionary<string, List<string>> translationsDictionary = wordsFromFile.CompileDictionary(textFromFile);
            Assert.AreEqual(translationsDictionary.Keys.Count, 4);
            Assert.AreEqual(translationsDictionary.Keys.First(), "word1,word2");
            Assert.AreEqual(translationsDictionary.Keys.ElementAt(1), "word2");
            Assert.AreEqual(translationsDictionary.Keys.ElementAt(2), "answer1");
            Assert.AreEqual(translationsDictionary.Keys.ElementAt(3), "word");
            Assert.AreEqual(translationsDictionary.Values.Count, 4);
            Assert.AreEqual(translationsDictionary.Values.First()[0], "answer1");
            Assert.AreEqual(translationsDictionary.Values.First()[1], "answer2");
            Assert.AreEqual(translationsDictionary.Values.ElementAt(1)[0], "answer1");
            Assert.AreEqual(translationsDictionary.Values.ElementAt(1)[1], "answer2");
            Assert.AreEqual(translationsDictionary.Values.ElementAt(2)[0], "answer2");
            Assert.AreEqual(translationsDictionary.Values.ElementAt(3)[0], "answer1");
            Assert.AreEqual(translationsDictionary.Values.ElementAt(3)[1], "answer2");
        }
    }
}
