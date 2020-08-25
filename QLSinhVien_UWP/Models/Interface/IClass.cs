using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace QLSinhVien_UWP.Models.Interface
{
    interface IClass
    {
        ObservableCollection<Class> GetClasses();

        Task<int> Insert(Class @class);
        Task<int> Delete(string ClassID);
        Task<int> Update(Class student);
    }
}
