using QLSinhVien_UWP.Models.Interface;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace QLSinhVien_UWP.Models
{
    public class Grade : IGrade
    {
        public int ID { get; set; }
        public float Test { get; set; }
        public float MidTerm { get; set; }
        public float Final { get; set; }
        public float AverageScore { get; set; }
        public string IDStudent { get; set; }
        //public int IDCourse { get; set; }
        public Course Course { get; set; }

        public void CalcAverage()
        {
            this.AverageScore = (this.Test + this.MidTerm * 3 + this.Final * 3) / 6;
        }

        public async Task<int> Delete(string CourseID)
        {
            if (!string.IsNullOrEmpty(CourseID))
            {
                string query = @"Delete from Grade Where Grade.CourseID = @CourseID ";
                return await DataProvider.Instance.ExecuteNonAsync(query, new object[] { CourseID });
            }
            return -1;
        }

        public ObservableCollection<Grade> GetGrades()
        {
            ObservableCollection<Grade> grades = new ObservableCollection<Grade>();
            string query = @"select Grade.ID, Test, MidTerm, Final, AverageScore, IDStudent, IDCourse, Course.Name from Grade
                            join Course on Course.ID = Grade.IDCourse";
            var datatable = DataProvider.Instance.GetDataTable(query);

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                Grade grade = new Grade();
                grade.ID = Convert.ToInt32(datatable.Rows[i][0]);
                grade.Test = Convert.ToSingle(datatable.Rows[i][1]);
                grade.MidTerm = Convert.ToSingle(datatable.Rows[i][2]);
                grade.Final = Convert.ToSingle(datatable.Rows[i][3]);
                if (datatable.Rows[i][4].GetType().Name == "DBNull")
                    grade.AverageScore = 0;
                else
                    grade.AverageScore = Convert.ToSingle(datatable.Rows[i][4]);
                grade.IDStudent = datatable.Rows[i][5].ToString();
                grade.Course = new Course()
                {
                    ID = Convert.ToInt32(datatable.Rows[i][6]),
                    Name = datatable.Rows[i][7].ToString()
                };

                grades.Add(grade);
            }

            return grades;
        }

        public async Task<int> Insert(Grade grade)
        {
            if (grade != null)
            {
                grade.CalcAverage();
                string query = @"Insert into Grade 
                            (IDCourse, IDStudent, Test, Midterm, Final, AverageScore) 
                            values ( @IDCourse , @IDStudent , @Test , @Midterm , @Final , @Average )";
                var findCourse = GetGrades().Where(x => x.Course.ID == grade.Course.ID && x.IDStudent == grade.IDStudent).SingleOrDefault();
                if (findCourse == null)
                {
                    return await DataProvider.Instance.ExecuteNonAsync(query, new object[]
                    {
                        grade.Course.ID,
                        grade.IDStudent,
                        grade.Test,
                        grade.MidTerm,
                        grade.Final,
                        grade.AverageScore
                    });
                }
                else return -1;

            }
            return -1;
        }

        public async Task<int> Update(Grade grade)
        {
            if (grade != null)
            {
                grade.CalcAverage();
                string query = @"update Grade 
                                set Test = @Test , Midterm = @Midterm , Final = @Final , AverageScore = @Average 
                                where IDCourse = @IDCourse and IDStudent = @IDStudent ";
                return await DataProvider.Instance.ExecuteNonAsync(query, new object[]
                {
                    grade.Test,
                    grade.MidTerm,
                    grade.Final,
                    grade.AverageScore,
                    grade.Course.ID,
                    grade.IDStudent  
                });
            }
            return -1;
        }
    }
}
