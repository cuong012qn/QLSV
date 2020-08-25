using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSinhVien_UWP.Models.Interface
{
    interface IGrade
    {
        ObservableCollection<Grade> GetGrades();

        Task<int> Insert(Grade grade);
        Task<int> Delete(string CourseID);
        Task<int> Update(Grade grade);
    }
}
