using QLSinhVien_UWP.Dialog;
using QLSinhVien_UWP.Extensions;
using QLSinhVien_UWP.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace QLSinhVien_UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentView : Page
    {
        private Student Student;
        private ObservableCollection<Student> AllStudent = new ObservableCollection<Student>(DataProvider.Instance.Student.GetStudents());
        private ObservableCollection<Class> AllClass = new ObservableCollection<Class>(DataProvider.Instance.Class.GetClasses());
        private ObservableCollection<ClassType> AllClassType = new ObservableCollection<ClassType>(DataProvider.Instance.ClassType.GetClassTypes());
        private ObservableCollection<Class> FilterClasses;
        private ObservableCollection<ClassType> FilterClassTypes;
        private ObservableCollection<Student> FilterStudents;

        public StudentView()
        {
            //ObservableCollection<Student> students = DataProvider.Instance.Students();
            this.InitializeComponent();
            //gvStudent.ItemsSource = students;
            //lvStudent.ItemsSource = DataProvider.Instance.Students();

            FilterClasses = new ObservableCollection<Class>(AllClass);
            FilterClassTypes = new ObservableCollection<ClassType>(AllClassType);
            FilterStudents = new ObservableCollection<Student>(AllStudent);

            CbbClass.ItemsSource = FilterClasses;
            CbbClassType.ItemsSource = FilterClassTypes;
            gvStudent.ItemsSource = FilterStudents;

            CbbClass.DisplayMemberPath = "Name";
            CbbClassType.DisplayMemberPath = "Name";

            CbbGender.Items.Add("Nam");
            CbbGender.Items.Add("Nữ");

            Helper.SetDisable(StackButton, "Button");
            BtnAdd.IsEnabled = true;
            Helper.SetDisable(StackTextbox);
        }


        #region Event Button Click
        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CbbGender.SelectedItem == null)
                CbbGender.BorderBrush = new SolidColorBrush(Colors.Red);

            if (CbbClass.SelectedItem == null)
                CbbClass.BorderBrush = new SolidColorBrush(Colors.Red);

            if (!string.IsNullOrEmpty(TxbID.Text) && !string.IsNullOrEmpty(Txbname.Text)
                    && CbbGender.SelectedItem != null && CbbClass.SelectedItem != null)
            {
                CbbGender.BorderBrush = new SolidColorBrush(Colors.DimGray);
                CbbClass.BorderBrush = new SolidColorBrush(Colors.DimGray);

                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Content = "Bạn có muốn thay đổi?";
                contentDialog.PrimaryButtonText = "Có";
                contentDialog.SecondaryButtonText = "Không";

                var res = await contentDialog.ShowAsync();

                if (res == ContentDialogResult.Primary)
                {

                    Student student = new Student()
                    {
                        ID = TxbID.Text,
                        Name = Txbname.Text,
                        BirthDate = DpBirthDate.Date.HasValue ? DpBirthDate.Date.Value.DateTime : DateTime.Now,
                        Email = TxbEmail.Text,
                        Address = TxbAddress.Text,
                        DateCreate = DateTime.Now,
                        DateUpdate = DateTime.Now,
                        Gender = CbbGender.SelectedItem.ToString(),
                        Image = new byte[1],
                        Class = CbbClass.SelectedItem as Class,
                        StartDate = DpStartDate.Date.HasValue ? DpBirthDate.Date.Value.DateTime : DateTime.Now,
                        EndDate = DpEndDate.Date.HasValue ? DpBirthDate.Date.Value.DateTime : DateTime.Now
                    };

                    ContentDialog content = new ContentDialog();
                    content.PrimaryButtonText = "OK";
                    int resultExecute = await DataProvider.Instance.Student.Insert(student);
                    if (resultExecute > 0)
                    {
                        content.Content = "Thêm thành công!";
                        _ = content.ShowAsync();
                        gvStudent.ItemsSource = null;
                        gvStudent.ItemsSource = DataProvider.Instance.Student.GetStudents();
                    }
                    else
                    {
                        content.Content = "Đã có lỗi xảy ra!";
                        _ = content.ShowAsync();
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Helper.SetEnable(StackTextbox);
            BtnSave.IsEnabled = true;
            BtnSkip.IsEnabled = true;
        }

        private void BtnSkip_Click(object sender, RoutedEventArgs e)
        {
            Helper.ClearText(StackTextbox);
            Helper.SetDisable(StackButton, "Button");
            Helper.SetDisable(StackTextbox);

            BtnAdd.IsEnabled = true;
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Content = "Bạn có muốn thay đổi?";
            dialog.PrimaryButtonText = "Có";
            dialog.SecondaryButtonText = "Không";
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Student st = new Student()
                {
                    ID = TxbID.Text,
                    Name = Txbname.Text,
                    Email = TxbEmail.Text,
                    Address = TxbAddress.Text,
                    Gender = CbbGender.SelectedValue.ToString(),
                    BirthDate = DpBirthDate.Date.HasValue ? DpBirthDate.Date.Value.DateTime : DateTime.Now,
                    StartDate = DpStartDate.Date.HasValue ? DpStartDate.Date.Value.Date : DateTime.Now,
                    EndDate = DpEndDate.Date.HasValue ? DpEndDate.Date.Value.Date : DateTime.Now,
                    DateUpdate = DateTime.Now,
                    Class = CbbClass.SelectedItem as Class
                };
                Student.DateUpdate = DateTime.Now;
                int resultExecute = await DataProvider.Instance.Student.Update(st);
                ContentDialog content = new ContentDialog();
                content.PrimaryButtonText = "OK";
                if (resultExecute > 0)
                {
                    content.Content = "Sửa thành công!";
                    _ = content.ShowAsync();
                    gvStudent.ItemsSource = null;
                    gvStudent.ItemsSource = DataProvider.Instance.Student.GetStudents();
                }
                else
                {
                    content.Content = "Đã có lỗi xảy ra!";
                    _ = content.ShowAsync();
                }
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog message = new ContentDialog();
            message.Content = "Bạn có chắc muốn xóa";
            message.PrimaryButtonText = "OK";
            message.SecondaryButtonText = "Canel";

            var showMessage = await message.ShowAsync();
            if (showMessage == ContentDialogResult.Primary)
            {
                if (!string.IsNullOrEmpty(TxbID.Text))
                {
                    int resultExecute = await DataProvider.Instance.Student.Delete(TxbID.Text);
                    ContentDialog content = new ContentDialog();
                    content.PrimaryButtonText = "OK";
                    if (resultExecute > 0)
                    {
                        content.Content = "Xóa thành công!";
                        _ = content.ShowAsync();

                        gvStudent.ItemsSource = null;
                        gvStudent.ItemsSource = DataProvider.Instance.Student.GetStudents();
                    }
                    else
                    {
                        content.Content = "Đã có lỗi xảy ra!";
                        _ = content.ShowAsync();
                    }

                }
            }
        }

        private void BtnAddClass_Click(object sender, RoutedEventArgs e)
        {
            AddClassDialog addClass = new AddClassDialog();
            _ = addClass.ShowAsync();

            CbbClass.ItemsSource = null;
            CbbClassType.ItemsSource = null;

            AllClass = new ObservableCollection<Class>(DataProvider.Instance.Class.GetClasses());
            AllClassType = new ObservableCollection<ClassType>(DataProvider.Instance.ClassType.GetClassTypes());

            FilterClasses = new ObservableCollection<Class>(AllClass);
            FilterClassTypes = new ObservableCollection<ClassType>(AllClassType);

            CbbClass.ItemsSource = FilterClasses;
            CbbClassType.ItemsSource = FilterClassTypes;
        }

        #endregion

        #region Properties Change
        private void TxbID_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (string.IsNullOrEmpty(sender.Text)) sender.BorderBrush = new SolidColorBrush(Colors.Red);
            else sender.BorderBrush = new SolidColorBrush(Colors.DimGray);
        }

        private void CbbClassType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassType Classtype = e.AddedItems.FirstOrDefault() as ClassType;
            if (Classtype != null)
            {
                var items = AllClass.Where(x => x.IDType == Classtype.ID);


                for (int i = AllClass.Count - 1; i >= 0; i--)
                {
                    var item = AllClass[i];
                    if (!items.Contains(item))
                        FilterClasses.Remove(item);
                }

                foreach (var item in items)
                {
                    if (!FilterClasses.Contains(item))
                    {
                        FilterClasses.Add(item);
                    }
                }
            }
        }

        private void gvStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student student = e.AddedItems.FirstOrDefault() as Student;
            if (student != null)
            {
                TxbID.Text = student.ID;
                Txbname.Text = student.Name;
                TxbEmail.Text = student.Email;
                TxbAddress.Text = student.Address;

                //if (student.Gender == "Nam") CbbGender.SelectedIndex = 0;
                //else CbbGender.SelectedIndex = 1;

                CbbGender.SelectedValue = student.Gender;

                DpBirthDate.Date = student.BirthDate;

                //Làm tạm index vì binding không được :<<<
                CbbClass.SelectedIndex = student.Class.ID - 1;
                CbbClassType.SelectedIndex = student.ClassType.ID - 1;

                DpStartDate.Date = student.StartDate;
                DpEndDate.Date = student.EndDate;

                Student = student;
                //BtnSave.IsEnabled = true;

                Helper.SetEnable(StackTextbox);
                Helper.SetDisable(StackButton, "Button");
                BtnSkip.IsEnabled = true;
                BtnEdit.IsEnabled = true;
                BtnDelete.IsEnabled = true;
                TxbID.IsEnabled = false;
            }
        }

        #endregion


        private void OnFilterChanged(object sender, TextChangedEventArgs e)
        {
            var items = AllStudent.Where(x => x.ID.Contains(FilterByID.Text, StringComparison.InvariantCultureIgnoreCase) &&
                        x.Name.Contains(FilterByName.Text, StringComparison.InvariantCultureIgnoreCase));

            for (int i = AllStudent.Count - 1; i >= 0; i--)
            {
                var item = AllStudent[i];
                if (!items.Contains(item))
                {
                    FilterStudents.Remove(item);
                }
            }

            foreach (var item in items)
            {
                if (!FilterStudents.Contains(item))
                {
                    FilterStudents.Add(item);
                }
            }
        }
    }
}
