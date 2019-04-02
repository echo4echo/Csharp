
using System;
using System.Threading;
using System.Windows.Input;
using TestUI.ErrorHandleClass.Operations.Commands;
using TestUI.MainApplication.Initial;
using TestUI.MainApplication.Motor.Coeff;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TestUI.ErrorHandleClass
{
    public class ProgressDialogViewModel : ViewModelBase
    {
        #region Fields

        // Property variables
        private int p_Progress;
        private string p_ProgressMessage;
        private int p_ProgressMax;
        private string p_ProgressTitle;
        private bool p_ProgressIsCancellable;

        //Member variables
        private App1ViewModel m_Parent;
        private string m_ProgressMessageTemplate;
        private string m_CancellationMessage;

        private App2ViewModel m1_Parent;

        #endregion

        #region Constructor

        public ProgressDialogViewModel(App1ViewModel App1ViewModel)
        {
            Initialize(App1ViewModel);
        }

        public ProgressDialogViewModel(App2ViewModel App2ViewModel)
        {
            Initialize(App2ViewModel);
        }

        #endregion

        #region Admin Properties

        /// <summary>
        /// A cancellation token source for the background operations.
        /// </summary>
        internal CancellationTokenSource TokenSource { get; set; }

        /// <summary>
        /// Whether the operation in progress has been cancelled.
        /// </summary>
        /// <remarks> 
        /// The Cancel command is invoked by the Cancel button, and on the window
        /// close (in case the user clicks the close box to cancel. The Cancel 
        /// command sets this property and checks it to make sure that the command 
        /// isn't run twice when the user clicks the Cancel button (once for the 
        /// button-click, and once for the window-close.
        /// </remarks>
        public bool IsCancelled { get; set; }
        #endregion

        #region Command Properties

        /// <summary>
        /// The Cancel command.
        /// </summary>
        public ICommand Cancel { get; set; }

        #endregion

        #region Data Properties
        /// <summary>
        /// The progress of an image processing job.
        /// </summary>
        /// <remarks>
        /// The setter for this property also sets the ProgressMessage property.
        /// </remarks>
        public bool ProgressIsCancellable
        {
            get { return p_ProgressIsCancellable; }

            set
            {
                Set(ref p_ProgressIsCancellable, value);
            }
        }

        /// <summary>
        /// The progress of an image processing job.
        /// </summary>
        /// <remarks>
        /// The setter for this property also sets the ProgressMessage property.
        /// </remarks>
        public int Progress
        {
            get { return p_Progress; }

            set
            {
                Set(ref p_Progress, value);
            }
        }

        /// <summary>
        /// The maximum progress value.
        /// </summary>
        /// <remarks>
        /// The 
        /// </remarks>
        public int ProgressMax
        {
            get { return p_ProgressMax; }

            set
            {
                Set(ref p_ProgressMax, value);
            }
        }

        /// <summary>
        /// The initial title View
        /// </summary>

        public string ProgressTitle
        {
            get { return p_ProgressTitle; }

            set
            {
                Set(ref p_ProgressTitle, value);
            }
        }

        /// <summary>
        /// The status message to be displayed in the View.
        /// </summary>
        public string ProgressMessage
        {
            get { return p_ProgressMessage; }

            set
            {
                Set(ref p_ProgressMessage, value);
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Clears the view model.
        /// </summary>
        internal void ClearViewModel()
        {
            p_Progress = 0;
            p_ProgressMax = 0;
            ProgressTitle = "Initial progress";
            p_ProgressMessage = "Preparing to perform works...";
            this.IsCancelled = false;
        }


        #region Deadcode

        /// <summary>
        /// Advances the progress counter for the Progress dialog.
        /// </summary>
        /// <param name="incrementClicks">The number of 'clicks' to advance the counter.</param>
        internal void IncrementProgressCounter(int incrementClicks)
        {
            // Increment counter
            this.Progress += incrementClicks;

            // Update progress message
            var progress = Convert.ToSingle(p_Progress);
            var progressMax = Convert.ToSingle(p_ProgressMax);
            var f = (progress / progressMax) * 100;
            var percentComplete = Single.IsNaN(f) ? 0 : Convert.ToInt32(f);
            this.ProgressMessage = string.Format(m_ProgressMessageTemplate, percentComplete);
        }

        /// <summary>
        /// Sets the progreess message to show that processing was cancelled.
        /// </summary>
        internal void ShowCancellationMessage()
        {
            this.ProgressMessage = m_CancellationMessage;
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes this view model.
        /// </summary>
        /// <param name="mainWindowViewModel">The view model for this application's main window.</param>
        private void Initialize(App1ViewModel App1ViewModel)
        {
            m_Parent = App1ViewModel;
            m_ProgressMessageTemplate = "Simulated work {0}% complete";
            m_CancellationMessage = "Simulated work cancelled";
            this.Cancel = new CancelCommand(this);
            this.ClearViewModel();
        }

        private void Initialize(App2ViewModel App2ViewModel)
        {
            m1_Parent = App2ViewModel;
            m_ProgressMessageTemplate = "Simulated work {0}% complete";
            m_CancellationMessage = "Simulated work cancelled";
            this.Cancel = new CancelCommand(this);
            this.ClearViewModel();
        }

        #endregion
    }
}