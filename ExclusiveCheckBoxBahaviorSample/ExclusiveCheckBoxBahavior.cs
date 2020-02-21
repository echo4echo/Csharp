using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ExclusiveCheckBoxBahaviorSample
{
    public class ExclusiveCheckBoxBahavior : IDisposable
    {
        public enum ExclusiveCheckBoxBahaviorStates { Disabled, Enabled, EnabledWithVisibilityReset }
        #region static part
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.RegisterAttached("State", typeof(ExclusiveCheckBoxBahaviorStates), typeof(ExclusiveCheckBoxBahavior),
            new PropertyMetadata(ExclusiveCheckBoxBahaviorStates.Disabled, OnStateChanged));

        public static void SetState(DependencyObject element, ExclusiveCheckBoxBahaviorStates value)
        {
            element.SetValue(StateProperty, value);
        }

        public static ExclusiveCheckBoxBahaviorStates GetState(DependencyObject element)
        {
            return (ExclusiveCheckBoxBahaviorStates)element.GetValue(StateProperty);
        }

        private static void OnStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(GetInstance(d)!=null)
            {
                GetInstance(d).Dispose();
            }

            //GetInstance(d)?.Dispose();
            if ((ExclusiveCheckBoxBahaviorStates)e.NewValue != ExclusiveCheckBoxBahaviorStates.Disabled)
                SetInstance(d, new ExclusiveCheckBoxBahavior(d, (ExclusiveCheckBoxBahaviorStates)e.NewValue == ExclusiveCheckBoxBahaviorStates.EnabledWithVisibilityReset));
        }


        private static readonly DependencyProperty InstanceProperty = DependencyProperty.RegisterAttached("Instance",
            typeof(ExclusiveCheckBoxBahavior), typeof(ExclusiveCheckBoxBahavior), new PropertyMetadata(null));

        private static void SetInstance(DependencyObject element, ExclusiveCheckBoxBahavior value)
        {
            element.SetValue(InstanceProperty, value);
        }

        private static ExclusiveCheckBoxBahavior GetInstance(DependencyObject element)
        {
            return (ExclusiveCheckBoxBahavior)element.GetValue(InstanceProperty);
        }
        #endregion

        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.RegisterAttached("GroupName", typeof(string), typeof(ExclusiveCheckBoxBahavior),
            new PropertyMetadata(string.Empty));

        public static void SetGroupName(DependencyObject element, string value)
        {
            element.SetValue(GroupNameProperty, value);
        }

        public static string GetGroupName(DependencyObject element)
        {
            return (string)element.GetValue(GroupNameProperty);
        }

        #region instance part

        private List<ToggleButton> _checkBoxes;
        private UIElement _dependencyObject;
        public ExclusiveCheckBoxBahavior(DependencyObject d, bool enabledWithVisibilityReset)
        {
            ((FrameworkElement)d).Initialized += (sender, args) =>
            {
                _dependencyObject = (UIElement)d;
                _checkBoxes = FindVisualChildren<ToggleButton>(d).ToList();
                _checkBoxes.ForEach(i => i.Checked += CheckBoxChecked);
                if (enabledWithVisibilityReset)
                    _dependencyObject.IsVisibleChanged += OnIsVisibleChanged;
            };
        }

        private void OnIsVisibleChanged(object sender1, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(bool)dependencyPropertyChangedEventArgs.NewValue)
                _checkBoxes.ForEach(i => i.IsChecked = false);
        }

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            var senderGroupName = GetGroupName((DependencyObject)sender);
            _checkBoxes.ForEach(i =>
            {
                if (!Equals(i, sender) && GetGroupName((DependencyObject)i) == senderGroupName) i.IsChecked = false;
            }
            );
        }

        public void Dispose()
        {
            _checkBoxes.ForEach(i => i.Checked -= CheckBoxChecked);
            _dependencyObject.IsVisibleChanged -= OnIsVisibleChanged;
        }
        #endregion

        #region private static
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject root) where T : DependencyObject
        {
            if (root != null)
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(root, i);
                    if (child is T) yield return (T)child;
                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
        }
        #endregion
    }
}
