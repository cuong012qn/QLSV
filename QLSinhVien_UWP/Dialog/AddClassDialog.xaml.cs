using QLSinhVien_UWP.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace QLSinhVien_UWP.Dialog
{
    public sealed partial class AddClassDialog : ContentDialog
    {
        public AddClassDialog()
        {
            this.InitializeComponent();
            CbbClassType.ItemsSource = DataProvider.Instance.ClassType.GetClassTypes();
            CbbClassType.DisplayMemberPath = "Name";
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!string.IsNullOrEmpty(TxbClassName.Text) && CbbClassType.SelectedItem != null)
            {
                var select = CbbClassType.SelectedItem as ClassType;
                _ = await DataProvider.Instance.Class.Insert(new Class() { IDType = select.ID, Name = TxbClassName.Text });
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void BtnClassType_Click(object sender, RoutedEventArgs e)
        {
            TxbClassTypeName.IsEnabled = true;
        }

        private async void BtnAddClassType_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxbClassTypeName.Text))
            {
                _ = await DataProvider.Instance.ClassType.Insert(TxbClassTypeName.Text);
                CbbClassType.ItemsSource = null;
                CbbClassType.ItemsSource = DataProvider.Instance.ClassType.GetClassTypes();

                TxbClassTypeName.IsEnabled = false;
                BtnAddClassType.IsEnabled = false;
            }
        }

        private void TxbClassTypeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxbClassTypeName.Text))
                BtnAddClassType.IsEnabled = false;
            else BtnAddClassType.IsEnabled = true;
        }

        private async void BtnRemoveCType_Click(object sender, RoutedEventArgs e)
        {
            var select = CbbClassType.SelectedItem as ClassType;
            _ = await DataProvider.Instance.ClassType.Delete(select.ID.ToString());
        }
    }
}
