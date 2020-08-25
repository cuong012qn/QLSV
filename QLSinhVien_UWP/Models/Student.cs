using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserDataTasks;
using Windows.Networking.NetworkOperators;
using Windows.UI.Xaml.Controls;

namespace QLSinhVien_UWP.Models
{
    public class Student : IStudent
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public byte[] Image { get; set; }

        //public string ClassName { get; set; }
        //public string ClassTypeName { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Class Class { get; set; }
        public ClassType ClassType { get; set; }


        public object[] GetStudent()
        {
            object[] result = new object[12];
            result[0] = ID;
            result[1] = Name;
            result[2] = BirthDate;
            result[3] = Gender;
            result[4] = Address;
            result[5] = Email;
            result[6] = new byte[0];
            result[7] = DateCreate;
            result[8] = DateUpdate;
            result[9] = StartDate;
            result[10] = EndDate;
            result[11] = Class.ID;
            return result;
        }

        public ObservableCollection<Student> GetStudents()
        {
            string query = @"select Student.ID, Student.Name, Student.Gender, Student.BirthDate, Student.Address, Student.Image, 
                                Student.Email, Student.StartDate, Student.EndDate, Student.DateCreate, Student.DateUpdate , Class.ID, Class.Name, ClassType.ID, ClassType.Name from Student
                                join Class on Class.ID = Student.IDClass
                                join ClassType on ClassType.ID = Class.IDType";
            ObservableCollection<Student> students = new ObservableCollection<Student>();

            DataTable dataTable = DataProvider.Instance.GetDataTable(query);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Student student = new Student();
                student.ID = dataTable.Rows[i][0].ToString();
                student.Name = dataTable.Rows[i][1].ToString();
                student.Gender = dataTable.Rows[i][2].ToString();

                student.BirthDate = Convert.ToDateTime(dataTable.Rows[i][3]);
                student.Address = dataTable.Rows[i][4].ToString();

                student.Image = (byte[])dataTable.Rows[i][5];
                student.Email = dataTable.Rows[i][6].ToString();

                student.StartDate = Convert.ToDateTime(dataTable.Rows[i][7]);
                student.EndDate = Convert.ToDateTime(dataTable.Rows[i][8]);

                student.DateCreate = Convert.ToDateTime(dataTable.Rows[i][9]);
                student.DateUpdate = Convert.ToDateTime(dataTable.Rows[i][10]);

                student.Class = new Class()
                {
                    ID = Convert.ToInt32(dataTable.Rows[i][11]),
                    Name = dataTable.Rows[i][12].ToString()
                };

                student.ClassType = new ClassType()
                {
                    ID = Convert.ToInt32(dataTable.Rows[i][13]),
                    Name = dataTable.Rows[i][14].ToString()
                };

                //student.ClassName = dataTable.Rows[i][11].ToString();
                //student.ClassTypeName = dataTable.Rows[i][12].ToString();

                students.Add(student);
            }

            return students;
        }

        public async Task<int> Delete(string StudentID)
        {
            if (!string.IsNullOrEmpty(StudentID))
            {
                string query = "Delete from Student Where Student.ID = @StudentID";
                return await DataProvider.Instance.ExecuteNonAsync(query, new object[] { StudentID });
            }
            return -1;
        }

        public async Task<int> Insert(Student student)
        {
            var res = GetStudents().Where(x => x.ID == student.ID).SingleOrDefault();
            if (student != null && res == null)
            {
                string query = @"insert into [Student] 
                                (ID, Name, BirthDate, Gender, Address, Email, Image, 
                                DateCreate, DateUpdate, StartDate, EndDate, IDClass)
                                values( @ID , @Name , @BirthDate , @Gender , @Address , @Email ,
                                @Image , @DateCreate , @DateUpdate , @StartDate , @EndDate , @IDClass )";
                return await DataProvider.Instance.ExecuteNonAsync(query, student.GetStudent());
            }
            return -1;
        }

        public async Task<int> Update(Student student)
        {
            if (student != null)
            {
                string query = @"update Student
                                    set Name = @Name , Gender = @Gender , BirthDate = @BirthDate , Address = @Address , 
                                        Email = @Email , StartDate = @StartDate , EndDate = @EndDate , 
                                        DateUpdate = @DateUpDate , IDClass = @IDClass 
                                    where Student.ID = @StudentID";

                object[] param = new object[]
                {
                    student.Name, student.Gender, student.BirthDate, student.Address, student.Email,
                    student.StartDate, student.EndDate, student.DateUpdate, student.Class.ID,
                    student.ID
                };

                return await DataProvider.Instance.ExecuteNonAsync(query, param);
                //if (res > 0)
                //{
                //    ContentDialog dialog = new ContentDialog();
                //    dialog.Content = "Sửa thành công";
                //    dialog.PrimaryButtonText = "Đóng";
                //    _ = dialog.ShowAsync();
                //}
                //else
                //{
                //    ContentDialog dialog = new ContentDialog();
                //    dialog.Content = "Có lỗi xảy ra không thể thêm";
                //    dialog.PrimaryButtonText = "Đóng";
                //    _ = dialog.ShowAsync();
                //}
            }
            return -1;
        }
    }
}
