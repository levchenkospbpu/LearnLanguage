// <copyright file="App.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LearnLanguage
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using LearnLanguage.ViewModels;
    using LearnLanguage.Views;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        private Window translateDialogWindow;
        private TranslateDialogViewModel viewModel;

        /// <summary>
        /// Runs when the application starts.
        /// </summary>
        /// <param name="e"> Startup event arguments. </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.viewModel = new TranslateDialogViewModel();
            this.viewModel.OnLessonFinished += this.ViewModel_OnLessonFinished;
            this.translateDialogWindow = new TranslateDialog();
            this.translateDialogWindow.DataContext = this.viewModel;
            this.translateDialogWindow.Show();
        }

        private void ShowTranslateDialogWindow()
        {
            this.viewModel = new TranslateDialogViewModel();
            this.viewModel.OnLessonFinished += this.ViewModel_OnLessonFinished;
            this.translateDialogWindow.DataContext = this.viewModel;
            this.translateDialogWindow.Show();
        }

        private void ViewModel_OnLessonFinished(object sender, EventArgs e)
        {
            int interval = this.viewModel.IntervalInMinutes * 60000;
            this.translateDialogWindow.Hide();
            TimeManager.Instance.Start(interval, this.ShowTranslateDialogWindow);
        }
    }
}
