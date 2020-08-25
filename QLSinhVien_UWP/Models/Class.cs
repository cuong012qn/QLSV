using QLSinhVien_UWP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSinhVien_UWP.Models
{
    public class Class : IClass
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int IDType { get; set; }

        public async Task<int> Delete(string ClassID)
        {
            string query = "Delete from Class Where Class.ID = @ClassID ";
            if (!string.IsNullOrEmpty(ClassID))
            {
                return await DataProvider.Instance.ExecuteNonAsync(query, new object[] { ClassID });
            }
            return -1;
        }

        public ObservableCollection<Class> GetClasses()
        {
            ObservableCollection<Class> Classes = new ObservableCollection<Class>();
            string query = "Select * from Class";

            var data = DataProvider.Instance.GetDataTable(query);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                Classes.Add(new Class()
                {
                    ID = Convert.ToInt32(data.Rows[i][0]),
                    Name = data.Rows[i][1].ToString(),
                    IDType = Convert.ToInt32(data.Rows[i][2])
                });
            }

            return Classes;
        }

        public async Task<int> Insert(Class @class)
        {
            string query = "insert into [Class] (IDType,Name) values ( @IDType , @Name )";
            if (@class != null)
            {
                return await DataProvider.Instance.ExecuteNonAsync(query, new object[] { @class.IDType, @class.Name });
            }
            return -1;
        }

        //Khong lam
        public Task<int> Update(Class student)
        {
            throw new NotImplementedException();
        }
    }
}
