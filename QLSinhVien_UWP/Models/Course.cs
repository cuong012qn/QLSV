using QLSinhVien_UWP.Models.Interface;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using Windows.ApplicationModel.ConversationalAgent;
using System;

namespace QLSinhVien_UWP.Models
{
    public class Course : ICourse
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Task<int> Delete(string CourseID)
        {
            throw new System.NotImplementedException();
        }

        public ObservableCollection<Course> GetCourses()
        {
            ObservableCollection<Course> courses = new ObservableCollection<Course>();
            DataTable data = DataProvider.Instance.GetDataTable("Select * from Course");

            for (int i = 0; i < data.Rows.Count; i++)
            {
                Course course = new Course()
                {
                    ID = Convert.ToInt32(data.Rows[i][0]),
                    Name = data.Rows[i][1].ToString()
                };
                courses.Add(course);
            }

            return courses;
        }

        public Task<int> Insert(Course course)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Update(Course course)
        {
            throw new System.NotImplementedException();
        }
    }
}
