using QLSinhVien_UWP.Dialog;
using QLSinhVien_UWP.Views;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace QLSinhVien_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            ShowLogin();
            //nvName.SelectedItem = nvName.MenuItems.OfType<NavigationViewItem>().First();
        }

        async void ShowLogin()
        {
            MessageDialog message = new MessageDialog();
            var res = await message.ShowAsync();
            if (res == ContentDialogResult.Secondary)
                Application.Current.Exit();
            else if (res == ContentDialogResult.Primary)
            {
                if (await message.IsLogin())
                {
                    this.InitializeComponent();
                    HomeView home = new HomeView();
                    FrameMain.Navigate(home.GetType());
                }
                else
                {
                    ContentDialog Error = new ContentDialog();
                    Error.Title = "Sai mật khẩu";
                    Error.SecondaryButtonText = "OK";
                    var contentResult = await Error.ShowAsync();
                    if (contentResult == ContentDialogResult.Secondary)
                        ShowLogin();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            string item = ((NavigationViewItem)args.SelectedItem).Tag as string;
            if (item == "StudentView")
            {
                StudentView student = new StudentView();
                FrameMain.Navigate(student.GetType());
            }
            else if (item == "HomeView")
            {
                HomeView home = new HomeView();
                FrameMain.Navigate(home.GetType());
            }
            else if (item == "ScoreView")
            {
                ScoreView score = new ScoreView();
                FrameMain.Navigate(score.GetType());
            }
        }
    }
}
