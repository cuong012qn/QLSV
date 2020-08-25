using QLSinhVien_UWP.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using QLSinhVien_UWP.Extensions;
using Windows.UI;
using Windows.UI.Xaml.Media;
using System.Globalization;
using System.ComponentModel.Design;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace QLSinhVien_UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScoreView : Page
    {
        #region Properties
        private ObservableCollection<Student> AllStudent = new ObservableCollection<Student>(DataProvider.Instance.Student.GetStudents());
        private ObservableCollection<Class> AllClass = new ObservableCollection<Class>(DataProvider.Instance.Class.GetClasses());
        private ObservableCollection<ClassType> AllClassType = new ObservableCollection<ClassType>(DataProvider.Instance.ClassType.GetClassTypes());
        private ObservableCollection<Grade> AllGrade = new ObservableCollection<Grade>();
        private ObservableCollection<Course> AllCourse = new ObservableCollection<Course>(DataProvider.Instance.Course.GetCourses());

        private ObservableCollection<Student> FilterStudent;
        private ObservableCollection<Class> FilterClass;
        private ObservableCollection<ClassType> FilterClassType;
        #endregion

        public ScoreView()
        {
            this.InitializeComponent();

            FilterStudent = new ObservableCollection<Student>(AllStudent);
            FilterClass = new ObservableCollection<Class>(AllClass);
            FilterClassType = new ObservableCollection<ClassType>(AllClassType);



            CbbClassType.ItemsSource = FilterClassType;
            CbbClass.ItemsSource = FilterClass;
            DtgvStudent.ItemsSource = FilterStudent;
            CbbCourse.ItemsSource = AllCourse;

            Helper.SetDisable(SpButton, "Button");
            BtnAdd.IsEnabled = true;
        }

        #region Properties Changed
        private void TxbFindByID_TextChanged(object sender, TextChangedEventArgs e)
        {
            var items = AllStudent.Where(x => x.ID.Contains(TxbFindByID.Text, StringComparison.InvariantCultureIgnoreCase)
                                    && x.Name.Contains(TxbFindByName.Text, StringComparison.InvariantCultureIgnoreCase));

            for (int i = AllStudent.Count - 1; i >= 0; i--)
            {
                var item = AllStudent[i];
                if (!items.Contains(item)) FilterStudent.Remove(item);
            }

            foreach (var item in items)
            {
                if (!FilterStudent.Contains(item)) FilterStudent.Add(item);
            }
        }

        private void DtgvStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student student = (Student)(e.AddedItems.FirstOrDefault());
            if (student != null)
            {
                TxbStudentID.Text = student.ID;
                TxbStudentName.Text = student.Name;
                TxbBirthDate.Text = student.BirthDate.ToString("dd/MM/yyyy");
                TxbAddress.Text = student.Address;
                TxbEmail.Text = student.Email;
                TxbClassName.Text = student.Class.Name;
                TxbGender.Text = student.Gender;

                DtgvScore.ItemsSource = null;
                var FindStudent = DataProvider.Instance.Grade.GetGrades().Where(x => x.IDStudent.ToString().Equals(student.ID));
                DtgvScore.ItemsSource = FindStudent;
            }
        }

        private void CbbClassType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selection = CbbClassType.SelectedItem as ClassType;
            if (selection != null)
            {
                var items = AllClass.Where(x => x.IDType == selection.ID);

                for (int i = AllClass.Count - 1; i >= 0; i--)
                {
                    var item = AllClass[i];
                    if (!items.Contains(item))
                        FilterClass.Remove(item);
                }

                foreach (var item in items)
                {
                    if (!FilterClass.Contains(item)) FilterClass.Add(item);
                }
                CbbClass.IsEnabled = true;
            }

        }

        private void CbbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems.FirstOrDefault() as Class;
            if (selected != null)
            {
                var items = AllStudent.Where(x => x.Class.ID == selected.ID);

                for (int i = AllStudent.Count - 1; i >= 0; i--)
                {
                    var item = AllStudent[i];
                    if (!items.Contains(item)) FilterStudent.Remove(item);
                }

                foreach (var item in items)
                {
                    if (!FilterStudent.Contains(item)) FilterStudent.Add(item);
                }
            }
        }

        private void DtgvScore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selection = e.AddedItems.FirstOrDefault() as Grade;
            if (selection != null)
            {
                TxbTest.Text = Helper.GetString(selection.Test);
                TxbMidTerm.Text = Helper.GetString(selection.MidTerm);
                TxbFinal.Text = Helper.GetString(selection.Final);
                TxbAverage.Text = Helper.GetString(selection.AverageScore);
                CbbCourse.SelectedIndex = selection.ID - 1;
                Helper.SetEnable(SpTxbCourse);
            }
            Helper.SetDisable(SpButton, "Button");
            BtnEdit.IsEnabled = true;
        }

        private void ValidTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txb = sender as TextBox;
            var IsValid = txb.Text.Any(c => !char.IsDigit(c) && !char.IsPunctuation(c));
            if (IsValid)
            {
                txb.BorderBrush = new SolidColorBrush(Colors.Red);
                txb.Text = string.Empty;
            }
            else
            if (!string.IsNullOrEmpty(txb.Text))
            {
                CultureInfo culture = new CultureInfo("en-US");
                float num = Convert.ToSingle(txb.Text, culture);
                if (num < 0 || num > 10)
                    txb.BorderBrush = new SolidColorBrush(Colors.Red);
                else txb.BorderBrush = new SolidColorBrush(Colors.DimGray);
            }
        }

        #endregion

        #region Event Button Click
        private void AddScore_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxbStudentID.Text))
            {
                Helper.SetEnable(SpTxbCourse);
                BtnSave.IsEnabled = true;
            }
        }

        private void BtnSkip_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Helper.ClearText(SpTxbCourse);
            Helper.SetDisable(SpTxbCourse);
            BtnAdd.IsEnabled = true;
        }

        private async void BtnSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxbTest.Text) && !string.IsNullOrEmpty(TxbMidTerm.Text)
                && !string.IsNullOrEmpty(TxbFinal.Text) && CbbCourse.SelectedValue != null)
            {
                if (Helper.IsValid(SpTxbCourse))
                {
                    var grade = new Grade()
                    {
                        IDStudent = TxbStudentID.Text,
                        Test = Convert.ToSingle(TxbTest.Text),
                        MidTerm = Convert.ToSingle(TxbMidTerm.Text),
                        Final = Convert.ToSingle(TxbFinal.Text),
                        Course = CbbCourse.SelectedValue as Course
                    };
                    var res = await DataProvider.Instance.Grade.Insert(grade);
                    if (res > 0)
                    {
                        ContentDialog dialog = new ContentDialog();
                        dialog.Content = "Thêm thành công";
                        dialog.PrimaryButtonText = "OK";
                        _ = dialog.ShowAsync();
                        RefreshData();
                        Helper.ClearText(SpTxbCourse);
                        Helper.SetDisable(SpButton, "Button");
                        BtnAdd.IsEnabled = true;
                    }
                    else
                    {
                        ContentDialog dialog = new ContentDialog();
                        dialog.Content = "Có lỗi xảy ra";
                        dialog.PrimaryButtonText = "OK";
                        _ = dialog.ShowAsync();
                    }
                }
            }
            else
            {
                ContentDialog dialog = new ContentDialog();
                dialog.Content = "Không được bỏ trống";
                dialog.PrimaryButtonText = "OK";
                _ = dialog.ShowAsync();
            }
        }

        private async void BtnEdit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ContentDialog message = new ContentDialog();
            message.Content = "Bạn có chắc muốn sửa?";
            message.PrimaryButtonText = "OK";
            message.SecondaryButtonText = "Cancel";
            var choose = await message.ShowAsync();
            if (choose == ContentDialogResult.Primary)
            {
                if (Helper.IsValid(SpTxbCourse) && CbbCourse.SelectedValue != null)
                {
                    var cbbSelect = CbbCourse.SelectedValue as Course;
                    if (cbbSelect != null)
                    {
                        //var getCourse = DataProvider.Instance.
                        var res = await DataProvider.Instance.Grade.Update(new Grade()
                        {
                            IDStudent = TxbStudentID.Text,
                            Test = Helper.GetFloat(TxbTest),
                            MidTerm = Helper.GetFloat(TxbMidTerm),
                            Final = Helper.GetFloat(TxbFinal),
                            Course = new Course()
                            {
                                ID = cbbSelect.ID,
                                Name = cbbSelect.Name
                            }
                        });
                        if (res > 0)
                        {
                            Helper.ClearText(SpTxbCourse);
                            Helper.SetDisable(SpButton, "Button");
                            BtnAdd.IsEnabled = true;
                        }
                    }
                }
            }
        }
        #endregion

        private void RefreshData()
        {
            DtgvScore.ItemsSource = null;
            AllGrade = null;
            var FindStudent = DataProvider.Instance.Grade.GetGrades().Where(x => x.IDStudent.ToString().Equals(TxbStudentID.Text));
            DtgvScore.ItemsSource = FindStudent;
        }
    }
}
