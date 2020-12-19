using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Shipwreck.MusicPlayer.Views
{
    internal static class ControlBehaviors
    {
        #region DoubleClickCommand

        public static ICommand GetDoubleClickCommand(Control obj)
        {
            return (ICommand)obj.GetValue(DoubleClickCommandProperty);
        }

        public static void SetDoubleClickCommand(Control obj, ICommand value)
        {
            obj.SetValue(DoubleClickCommandProperty, value);
        }

        public static readonly DependencyProperty DoubleClickCommandProperty
            = DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(ControlBehaviors), new FrameworkPropertyMetadata(null, (d, e) =>
            {
                if (d is Control c)
                {
                    if (e.NewValue != null)
                    {
                        c.MouseDoubleClick += Control_MouseDoubleClick;
                    }
                    else
                    {
                        c.MouseDoubleClick -= Control_MouseDoubleClick;
                    }
                }
            }));

        private static void Control_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Control c)
            {
                var cmd = GetDoubleClickCommand(c);
                var p = GetDoubleClickCommandParameter(c);
                if (cmd?.CanExecute(p) == true)
                {
                    cmd.Execute(p);
                }
            }
        }

        #endregion DoubleClickCommand

        #region DoubleClickCommandParameter

        public static object GetDoubleClickCommandParameter(Control obj)
        {
            return obj.GetValue(DoubleClickCommandParameterProperty);
        }

        public static void SetDoubleClickCommandParameter(Control obj, object value)
        {
            obj.SetValue(DoubleClickCommandParameterProperty, value);
        }

        public static readonly DependencyProperty DoubleClickCommandParameterProperty
            = DependencyProperty.RegisterAttached("DoubleClickCommandParameter", typeof(object), typeof(ControlBehaviors), new FrameworkPropertyMetadata(null));

        #endregion DoubleClickCommandParameter
    }
}