using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;

namespace NiFlash.MainView
{
    /// <summary>
    /// Interaction logic for TitleColumUC.xaml
    /// </summary>
    public partial class TitleColumUC : UserControl
    {
        public TitleColumUC()
        {
            InitializeComponent();
            this.Loaded += UserControl1_Loaded;
        }

        private async void UserControl1_Loaded(object sender, RoutedEventArgs e)
        {
            MetroWindow window = Window.GetWindow(this) as MetroWindow;
            if (window != null)
            {
                //await window.show("This is the title", "Some message");
            }
        }


        private bool cmOpenFlg = true;

        /// <summary>
        /// Open Context Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenContextMenu_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            ContextMenu cm = bt.ContextMenu;

            if((bt!=null) &&(cm!=null))
            {
                cm.PlacementTarget = bt;
                bt.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                cm.IsOpen = true;
                cmOpenFlg = true;
            }
        }

        private void MouseEnter_ContextMenu(object sender, RoutedEventArgs e)
        {
            if(cmOpenFlg)
            {
                
            }
        }

        private void MouseLeave_ContextMenu(object sender, RoutedEventArgs e)
        {

            Border childBorder = sender as Border;
            ContextMenu cm = GetParentOfType<ContextMenu>(childBorder);
            cm.IsOpen = false;
        }


        private void ContextMenu_Loaded(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = sender as ContextMenu;
            Border border = GetChildOfType<Border>(cm);
            border.MouseLeave += MouseLeave_ContextMenu;

            var scope = FocusManager.GetFocusScope(cm); // elem is the UIElement to unfocus
            FocusManager.SetFocusedElement(scope, null); // remove logical focus
            Keyboard.ClearFocus(); // remove keyboard focus

        }

        private static T GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
                return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        private static T GetParentOfType<T>( DependencyObject element) where T : DependencyObject
        {
            Type type = typeof(T);
            if (element == null) return null;
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            if (parent == null && ((FrameworkElement)element).Parent is DependencyObject) parent = ((FrameworkElement)element).Parent;
            if (parent == null) return null;
            else if (parent.GetType() == type || parent.GetType().IsSubclassOf(type)) return parent as T;
            return GetParentOfType<T>(parent);
        }

        /// <summary>
        /// Let button lose focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = sender as ContextMenu;

            if(cm!= null)
            {
                Button cmPlacedButton = cm.PlacementTarget as Button;

                if (cmPlacedButton.IsFocused)
                {
                    if (cmPlacedButton != null)
                    {
                        var scope = FocusManager.GetFocusScope(cmPlacedButton); // elem is the UIElement to unfocus
                        FocusManager.SetFocusedElement(scope, null); // remove logical focus
                        Keyboard.ClearFocus(); // remove keyboard focus
                    }
                }
            }

            cmOpenFlg = false;
        }

    }
}
