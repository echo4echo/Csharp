var darkwindow = new Window() {
                            Background = Brushes.Black,
                            Opacity = 0.4,
                            AllowsTransparency = true,
                            WindowStyle = WindowStyle.None,
                            WindowState = WindowState.Maximized,
                            Topmost = true
                        };
darkwindow.Show();
MessageBox.Show("Hello");
darkwindow.Close();
///and replace MessageBox.Show("Hello"); with mywindow.ShowModal();. Possibly, you'll need to make mywindow always on top.