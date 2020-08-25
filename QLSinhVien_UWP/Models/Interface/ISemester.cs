using System.Collections.ObjectModel;

namespace QLSinhVien_UWP.Models.Interface
{
    interface ISemester
    {
        ObservableCollection<Semester> GetSemesters();

    }
}
