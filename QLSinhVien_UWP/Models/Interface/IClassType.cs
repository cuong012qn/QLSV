using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace QLSinhVien_UWP.Models.Interface
{
    interface IClassType
    {

        ObservableCollection<ClassType> GetClassTypes();

        Task<int> Insert(string Name);
        Task<int> Delete(string ClassTypeID);
        Task<int> Update(ClassType classtype);
    }
}
