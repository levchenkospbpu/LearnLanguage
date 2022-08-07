// <copyright file="TimeManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LearnLanguage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LearnLanguage.Views;

    /// <summary>
    /// This class is used to control the timer.
    /// </summary>
    public class TimeManager
    {
        private static TimeManager instance;
        private System.Windows.Forms.Timer timer;
        private Action onTimerAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeManager"/> class.
        /// </summary>
        private TimeManager()
        {
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Tick += this.TimerElapsed;
        }

        /// <summary>
        /// Gets instance of a TimerManager.
        /// </summary>
        public static TimeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TimeManager();
                }

                return instance;
            }
        }

        /// <summary>
        /// Start timer.
        /// </summary>
        /// <param name="interval"> Interval between classes. </param>
        /// <param name="onTimerAct"> Action on expiration of the timer. </param>
        public void Start(int interval, Action onTimerAct)
        {
            this.onTimerAction = onTimerAct;
            this.timer.Interval = interval;
            this.timer.Start();
        }

        /// <summary>
        /// Stop timer.
        /// </summary>
        public void Stop()
        {
            this.timer.Stop();
        }

        private void TimerElapsed(object source, EventArgs e)
        {
            this.timer.Stop();
            this.onTimerAction();
        }
    }
}
