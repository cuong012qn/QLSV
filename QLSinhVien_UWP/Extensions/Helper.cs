using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace QLSinhVien_UWP.Extensions
{
    public class Helper
    {
        private static CultureInfo CultureInfo = new CultureInfo("en-US");

        #region Function
        internal static void SetEnable(Panel panel, string TypeElement = "Textbox")
        {
            if (panel != null)
            {
                if (TypeElement.Equals("Textbox"))
                {
                    foreach (var control in panel.Children)
                    {
                        if (control is TextBox) (control as TextBox).IsEnabled = true;
                        else if (control is ComboBox) (control as ComboBox).IsEnabled = true;
                        else if (control is CalendarDatePicker) (control as CalendarDatePicker).IsEnabled = true;
                    }
                }
                else if (TypeElement.Equals("Button"))
                {
                    foreach (var control in panel.Children)
                        if (control is Button) (control as Button).IsEnabled = true;
                }
            }
        }

        internal static void SetDisable(Panel panel, string TypeElement = "Textbox")
        {
            if (panel != null)
            {
                if (TypeElement.Equals("Textbox"))
                {
                    foreach (var control in panel.Children)
                    {
                        if (control is TextBox) (control as TextBox).IsEnabled = false;
                        else if (control is ComboBox) (control as ComboBox).IsEnabled = false;
                        else if (control is CalendarDatePicker) (control as CalendarDatePicker).IsEnabled = false;
                    }
                }
                else if (TypeElement.Equals("Button"))
                {
                    foreach (var control in panel.Children)
                        if ((control is Button) && (control as Button).Name != "BtnClear")
                            (control as Button).IsEnabled = false;
                }
            }
        }

        internal static void ClearText(Panel panel)
        {
            if (panel != null)
            {
                foreach (var control in panel.Children)
                {
                    if (control is TextBox) (control as TextBox).Text = string.Empty;
                    else if (control is ComboBox) (control as ComboBox).SelectedItem = null;
                    else if (control is CalendarDatePicker) (control as CalendarDatePicker).Date = null;
                }
            }
        }

        internal static bool IsValid(Panel panel)
        {
            var brush = new SolidColorBrush(Colors.Red);
            foreach (var item in panel.Children)
            {
                if (item is TextBox)
                {
                    if ((item as TextBox).BorderBrush.Equals(brush))
                        return false;
                }
            }
            return true;
        }

        internal static float GetFloat(TextBox txb)
        {
            return Convert.ToSingle(txb.Text, CultureInfo);
        }

        internal static string GetString(float input)
        {
            return input.ToString(CultureInfo);
        }
        #endregion
    }
}
