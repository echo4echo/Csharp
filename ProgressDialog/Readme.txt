//How to use this progress window:
//1. copy and reorganization namespace of the code files
//2. In your viewmodel:
//define event Handler and Trigger:
//example:

public event EventHandler WorkEnded;
/// <summary>
/// Raises the WorkEnding event.
/// </summary>
internal void RaiseWorkEndedEvent()
{
    // Exit if no subscribers
    if (WorkEnded == null) return;

    // Raise event
    WorkEnded(this, new EventArgs());
}
//3. In your xaml.cs files:

var progressDialogViewModel = ViewModel.ProgressDialogViewModel;
///your background worker or thread code here
ViewServices.ShowProgressDialog(null, progressDialogViewModel);

//4. Add the view event handler
/// Closes the Progress dialog.
private void OnWorkEnding(object sender, EventArgs e)
{
    ViewServices.CloseProgressDialog();
}
		