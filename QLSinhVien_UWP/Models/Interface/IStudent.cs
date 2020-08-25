using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSinhVien_UWP.Models
{
    interface IStudent
    {
        object[] GetStudent();

        ObservableCollection<Student> GetStudents();

        Task<int> Insert(Student student);
        Task<int> Delete(string StudentID);
        Task<int> Update(Student student);
    }
}
