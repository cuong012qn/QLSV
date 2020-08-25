using QLSinhVien_UWP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSinhVien_UWP.Models
{
    public class ClassType : IClassType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public async Task<int> Delete(string ClassTypeID)
        {
            string query = "Delete from [ClassType] Where [ClassType].ID = @ClassTypeID ";
            if (!string.IsNullOrEmpty(ClassTypeID))
            {
                return await DataProvider.Instance.ExecuteNonAsync(query, new object[] { ClassTypeID });
            }
            return -1;
        }

        public ObservableCollection<ClassType> GetClassTypes()
        {
            ObservableCollection<ClassType> ClassType = new ObservableCollection<ClassType>();
            string query = "Select * from ClassType";

            var data = DataProvider.Instance.GetDataTable(query);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                ClassType.Add(new ClassType()
                {
                    ID = Convert.ToInt32(data.Rows[i][0]),
                    Name = data.Rows[i][1].ToString()
                });
            }

            return ClassType;
        }

        public async Task<int> Insert(string Name)
        {
            string query = "insert into [ClassType] (Name) values ( @Name )";
            if (!string.IsNullOrEmpty(Name))
            {
                return await DataProvider.Instance.ExecuteNonAsync(query, new object[] { Name });
            }

            return -1;
        }

        public Task<int> Update(ClassType classtype)
        {
            throw new NotImplementedException();
        }
    }
}
