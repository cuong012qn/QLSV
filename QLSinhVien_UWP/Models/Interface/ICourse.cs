using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace QLSinhVien_UWP.Models.Interface
{
    interface ICourse
    {
        ObservableCollection<Course> GetCourses();

        Task<int> Insert(Course course);
        Task<int> Delete(string CourseID);
        Task<int> Update(Course course);
    }
}
