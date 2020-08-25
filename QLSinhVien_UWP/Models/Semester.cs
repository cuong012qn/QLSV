using QLSinhVien_UWP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSinhVien_UWP.Models
{
   public class Semester : ISemester
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ObservableCollection<Semester> GetSemesters()
        {
            ObservableCollection<Semester> semesters = new ObservableCollection<Semester>();
            string query = "Select * from Semester";
            var data = DataProvider.Instance.GetDataTable(query);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                Semester semester = new Semester()
                {
                    ID = Convert.ToInt32(data.Rows[i][0]),
                    Name = data.Rows[i][1].ToString()
                };

                semesters.Add(semester);
            }

            return semesters;
        }
    }
}
