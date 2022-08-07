// <copyright file="TranslateDialogViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LearnLanguage.ViewModels
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using LearnLanguage.Helpers;
    using LearnLanguage.Models;

    /// <summary>
    /// Viewmodel for TranslateDialog.
    /// </summary>
    public class TranslateDialogViewModel : NotifyPropertyBase<TranslateDialogViewModel>
    {
        private TranslateDialogModel model;
        private string word = string.Empty;
        private string answer = string.Empty;
        private int numberOfWords;
        private int currentTrueAnswers;
        private int currentAnswerNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateDialogViewModel"/> class.
        /// </summary>
        public TranslateDialogViewModel()
        {
            this.TranslateDialogTextBoxBorderColor = Colors.Gray.ToString();
            this.NotifyPropertyChanged(x => x.TranslateDialogTextBoxBorderColor);
            this.OnCheckResultButtonClickCommand = new RelayCommand(this.OnCheckResultButtonClick);
            this.OnShowAnswerButtonClickCommand = new RelayCommand(this.OnShowAnswerButtonClick);
            this.OnNextWordButtonClickCommand = new RelayCommand(this.OnNextWordButtonClick);
            this.OnFinishLessondButtonClickCommand = new RelayCommand(this.OnFinishLessonButtonClick);
            this.CheckAnswerButtonVisibility = Visibility.Visible;
            this.ShowAnswerButtonVisibility = Visibility.Hidden;
            this.NextWordButtonVisibility = Visibility.Hidden;
            this.FinishLessonButtonVisibility = Visibility.Hidden;
            this.model = new TranslateDialogModel();
            this.model.Initialize();
            this.numberOfWords = this.model.NumberOfWords;
        }

        /// <summary>
        /// Event handler for lesson finish.
        /// </summary>.
        public event EventHandler OnLessonFinished;

        /// <summary>
        /// Gets interval between lessons in minutes.
        /// </summary>
        public int IntervalInMinutes
        {
            get
            {
                return this.model.IntervalInMinutes;
            }
        }

        /// <summary>
        /// Gets or sets color of a result of user answer in TranslateDialog.
        /// </summary>
        public string TranslateDialogResultColor { get; set; }

        /// <summary>
        /// Gets or sets a result of user answer in TranslateDialog.
        /// </summary>
        public string TranslateDialogResult { get; set; }

        /// <summary>
        /// Gets or sets color of a textbox border in TranslateDialog.
        /// </summary>
        public string TranslateDialogTextBoxBorderColor { get; set; }

        /// <summary>
        /// Gets visibility of CheckAnswer button.
        /// </summary>
        public Visibility CheckAnswerButtonVisibility { get; private set; }

        /// <summary>
        /// Gets visibility of NextWord button.
        /// </summary>
        public Visibility NextWordButtonVisibility { get; private set; }

        /// <summary>
        /// Gets visibility of ShowAnswer button.
        /// </summary>
        public Visibility ShowAnswerButtonVisibility { get; private set; }

        /// <summary>
        /// Gets visibility of FinishLesson button.
        /// </summary>
        public Visibility FinishLessonButtonVisibility { get; private set; }

        /// <summary>
        /// Gets command that is called when you click on CheckResult button.
        /// </summary>
        public ICommand OnCheckResultButtonClickCommand { get; private set; }

        /// <summary>
        /// Gets command that is called when you click on ShowAnswer button.
        /// </summary>
        public ICommand OnShowAnswerButtonClickCommand { get; private set; }

        /// <summary>
        /// Gets command that is called when you click on NextWord button.
        /// </summary>
        public ICommand OnNextWordButtonClickCommand { get; private set; }

        /// <summary>
        /// Gets command that is called when you click on FinishLesson button.
        /// </summary>
        public ICommand OnFinishLessondButtonClickCommand { get; private set; }

        /// <summary>
        /// Gets or sets user answer.
        /// </summary>
        public string UserAnswer { get; set; }

        /// <summary>
        /// Gets word which is shown to the user.
        /// </summary>
        public string Word
        {
            get
            {
                this.word = this.model.GetWord();
                return this.word;
            }
        }

        /// <summary>
        /// Gets true answer.
        /// </summary>
        public string Answer
        {
            get
            {
                this.answer = this.model.GetAnswer();
                return this.answer;
            }
        }

        /// <summary>
        /// Gets the number of correct answers in the attempt at the moment.
        /// </summary>
        public string CurrentTrueAnswers
        {
            get
            {
                return this.currentTrueAnswers.ToString() + "/" + this.numberOfWords.ToString();
            }
        }

        /// <summary>
        /// Command which is executed when user presses check button.
        /// </summary>
        private void OnCheckResultButtonClick()
        {
            if (string.IsNullOrEmpty(this.UserAnswer))
            {
                this.TranslateDialogTextBoxBorderColor = Colors.Red.ToString();
                this.NotifyPropertyChanged(x => x.TranslateDialogTextBoxBorderColor);
                return;
            }
            else
            {
                if (this.model.CheckResult(this.word, this.UserAnswer))
                {
                    this.TranslateDialogResult = "Correct";
                    this.currentTrueAnswers++;
                    this.TranslateDialogResultColor = Colors.Green.ToString();
                    this.CheckAnswerButtonVisibility = Visibility.Hidden;
                    this.TranslateDialogTextBoxBorderColor = Colors.Green.ToString();
                    this.NotifyPropertyChanged(x => x.TranslateDialogTextBoxBorderColor);
                    if (this.currentAnswerNumber == this.numberOfWords - 1)
                    {
                        this.FinishLessonButtonVisibility = Visibility.Visible;
                        this.NotifyPropertyChanged(x => x.FinishLessonButtonVisibility);
                    }
                    else
                    {
                        this.NextWordButtonVisibility = Visibility.Visible;
                        this.NotifyPropertyChanged(x => x.NextWordButtonVisibility);
                    }
                }
                else
                {
                    this.TranslateDialogResult = "Incorrect";
                    this.TranslateDialogResultColor = Colors.Red.ToString();
                    this.CheckAnswerButtonVisibility = Visibility.Hidden;
                    this.ShowAnswerButtonVisibility = Visibility.Visible;
                    this.NotifyPropertyChanged(x => x.ShowAnswerButtonVisibility);
                    this.TranslateDialogTextBoxBorderColor = Colors.Red.ToString();
                    this.NotifyPropertyChanged(x => x.TranslateDialogTextBoxBorderColor);
                }

                this.currentAnswerNumber++;
                this.NotifyPropertyChanged(x => x.currentTrueAnswers);
                this.NotifyPropertyChanged(x => x.CheckAnswerButtonVisibility);
                this.NotifyPropertyChanged(x => x.TranslateDialogResult);
                this.NotifyPropertyChanged(x => x.TranslateDialogResultColor);
            }
        }

        /// <summary>
        /// Command which is executed when user presses show answer button.
        /// </summary>
        private void OnShowAnswerButtonClick()
        {
            this.UserAnswer = this.Answer;
            this.NotifyPropertyChanged(x => x.UserAnswer);
            this.ShowAnswerButtonVisibility = Visibility.Hidden;
            this.NotifyPropertyChanged(x => x.ShowAnswerButtonVisibility);
            if (this.currentAnswerNumber == this.numberOfWords)
            {
                this.FinishLessonButtonVisibility = Visibility.Visible;
                this.NotifyPropertyChanged(x => x.FinishLessonButtonVisibility);
            }
            else
            {
                this.NextWordButtonVisibility = Visibility.Visible;
                this.NotifyPropertyChanged(x => x.NextWordButtonVisibility);
            }

            this.TranslateDialogTextBoxBorderColor = Colors.Gray.ToString();
            this.NotifyPropertyChanged(x => x.TranslateDialogTextBoxBorderColor);
        }

        /// <summary>
        /// Command which is executed when user presses next word button.
        /// </summary>
        private void OnNextWordButtonClick()
        {
            this.NotifyPropertyChanged(x => x.Word);
            this.TranslateDialogResult = string.Empty;
            this.TranslateDialogResultColor = Colors.WhiteSmoke.ToString();
            this.NotifyPropertyChanged(x => x.TranslateDialogResult);
            this.NotifyPropertyChanged(x => x.TranslateDialogResultColor);
            this.CheckAnswerButtonVisibility = Visibility.Visible;
            this.NextWordButtonVisibility = Visibility.Hidden;
            this.NotifyPropertyChanged(x => x.NextWordButtonVisibility);
            this.NotifyPropertyChanged(x => x.CheckAnswerButtonVisibility);
            this.UserAnswer = string.Empty;
            this.NotifyPropertyChanged(x => x.UserAnswer);
            this.TranslateDialogTextBoxBorderColor = Colors.Gray.ToString();
            this.NotifyPropertyChanged(x => x.TranslateDialogTextBoxBorderColor);
        }

        private void OnFinishLessonButtonClick()
        {
            this.OnLessonFinished?.Invoke(this, new EventArgs());
        }
    }
}
