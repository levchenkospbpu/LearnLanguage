// <copyright file="TranslateDialogModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LearnLanguage.Models
{
    using System.Windows.Media;
    using LearnLanguageBL;

    /// <summary>
    /// Model for TranslateDialogViewModel.
    /// </summary>
    internal class TranslateDialogModel
    {
        private int numberOfWords;
        private WordsFromFile wordsFromFile;
        private int intervalInMinutes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateDialogModel"/> class.
        /// </summary>
        public TranslateDialogModel()
        {
            this.NumberOfWords = 5;
            this.intervalInMinutes = 1;
        }

        /// <summary>
        /// Gets or sets number of words in lesson.
        /// </summary>
        public int NumberOfWords { get => this.numberOfWords; set => this.numberOfWords = value; }

        /// <summary>
        /// Gets or sets interval between lessons in minutes.
        /// </summary>
        public int IntervalInMinutes { get => this.intervalInMinutes; set => this.intervalInMinutes = value; }

        /// <summary>
        /// Reading words from a file.
        /// </summary>
        public void Initialize()
        {
            this.wordsFromFile = new WordsFromFile();
        }

        /// <summary>
        /// Word which is shown to the user.
        /// </summary>
        /// <returns> Word which user must translate. </returns>
        public string GetWord()
        {
            return this.wordsFromFile.GetWord();
        }

        /// <summary>
        /// True anwer.
        /// </summary>
        /// <returns> Answer from dictionary. </returns>
        public string GetAnswer()
        {
            return this.wordsFromFile.GetAnswer();
        }

        /// <summary>
        /// Check result of user response.
        /// </summary>
        /// <param name="word"> Word which user must translate. </param>
        /// <param name="answer"> User answer. </param>
        /// <returns> Is result correct. </returns>
        public bool CheckResult(string word, string answer)
        {
            return this.wordsFromFile.CheckResult(word, answer);
        }
    }
}
