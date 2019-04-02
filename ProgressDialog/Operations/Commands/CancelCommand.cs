
using System;
using System.Windows.Input;
using TestUI.ErrorHandleClass;

namespace TestUI.ErrorHandleClass.Operations.Commands
{
    public class CancelCommand : ICommand
    {
        #region Fields

        // Member variables
        private ProgressDialogViewModel m_ViewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CancelCommand(ProgressDialogViewModel viewModel)
        {
            m_ViewModel = viewModel;
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// Whether the CancelCommand is enabled.
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Actions to take when CanExecute() changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Executes the CancelCommand
        /// </summary>
        public void Execute(object parameter)
        {
            /* The Cancel command is invoked by the Cancel button, and on the window
             * close (in case the user clicks the close box to cancel. The Cancel 
             * command sets this property and checks it to make sure that the command 
             * isn't run twice when the user clicks the Cancel button (once for the 
             * button-click, and once for the window-close. */

            // Exit if dialog has already been cancelled
            if (m_ViewModel.IsCancelled) return;

            /* The DoDemoWorkCommand.Execute() method defines a cancellation token source and
             * passes it to the Progress Dialog view model. The token itself is passed to the 
             * parallel image processing loop defined in the GoCommand.DoWork()  method. We 
             * cancel the loop by calling the TokenSource.Cancel() method. */

            // Validate TokenSource object
            if (m_ViewModel.TokenSource == null)
            {
                System.Windows.MessageBox.Show("ProgressDialogViewModel.TokenSource property is null, cancel failed");
                //throw new ApplicationException("ProgressDialogViewModel.TokenSource property is null");
            }
            else
            {
                // Cancel all pending background tasks
                m_ViewModel.TokenSource.Cancel(true);
            }

        }

        #endregion
    }
}