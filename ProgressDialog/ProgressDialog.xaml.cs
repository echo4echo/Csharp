

using System.Windows;

namespace TestUI.ErrorHandleClass
{
    /// <summary>
    /// Interaction logic for ProgressDialog.xaml
    /// </summary>
    public partial class ProgressDialog : Window
    {
        #region Constructor

        public ProgressDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Cancels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var viewModel = (ProgressDialogViewModel)this.DataContext;
            if (viewModel.ProgressIsCancellable)
            {
                viewModel.Cancel.Execute(null);
            }
            else
            {
                MessageBox.Show("Catastrophic Error!!! Please close NiBOT and clear NiBOT thread in Task manager (If any)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }
}