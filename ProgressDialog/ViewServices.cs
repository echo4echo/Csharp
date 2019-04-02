
using System;
using System.Windows;
using System.Windows.Controls;
using TestUI.ErrorHandleClass;

namespace TestUI.MainApplication.Services
{
    /// <summary>
    /// Provides services for the View.
    /// </summary>
    public static class ViewServices
    {
        #region Fields

        // Member variables
        private static ProgressDialog m_ProgressDialog;
        #endregion

        #region Service Methods

        /// <summary>
        /// Hides the Progress dialog.
        /// </summary>
        internal static void CloseProgressDialog()
        {
            if (m_ProgressDialog != null)
                m_ProgressDialog.Close();
        }

        /// <summary>
        /// Shows the Progress dialog.
        /// </summary>
        internal static void ShowProgressDialog(Window mainWindow, ProgressDialogViewModel viewModel)
        {
            m_ProgressDialog = new ProgressDialog();
            m_ProgressDialog.Owner = mainWindow;
            m_ProgressDialog.DataContext = viewModel;
            m_ProgressDialog.ShowDialog();
        }

        #endregion
    }
}