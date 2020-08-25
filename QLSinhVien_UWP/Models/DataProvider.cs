using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserActivities.Core;
using Windows.Services.Maps;
using Windows.UI.WindowManagement;

namespace QLSinhVien_UWP.Models
{
    public class DataProvider
    {
        public static readonly string ConnectionString = "Data Source=DESKTOP-7UOK1F3;Initial Catalog=QLHocSinh;Integrated Security=True;Trusted_Connection=False;user=sa;password=123";

        #region Private Properties
        private static object lockAsync = new object();
        private Student _Student;
        private Class _Class;
        private ClassType _ClassType;
        private Grade _Grade;
        private Course _Course;
        #endregion

        private volatile static DataProvider _instance;

        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockAsync)
                    {
                        if (_instance == null)
                            _instance = new DataProvider();

                    }
                }
                return _instance;
            }
            private set => _instance = value;
        }

        public Student Student
        {
            get
            {
                lock (lockAsync)
                {
                    if (_Student == null)
                        _Student = new Student();
                }
                return _Student;
            }
            private set => _Student = value;
        }
        public Class Class
        {
            get
            {
                lock (lockAsync)
                {
                    if (_Class == null)
                        _Class = new Class();
                }
                return _Class;
            }
            private set => _Class = value;
        }
        public ClassType ClassType
        {
            get
            {
                lock (lockAsync)
                {
                    if (_ClassType == null)
                        _ClassType = new ClassType();
                }
                return _ClassType;
            }
            private set => _ClassType = value;
        }
        public Grade Grade
        {
            get
            {
                lock (lockAsync)
                {
                    if (_Grade == null)
                        _Grade = new Grade();
                }
                return _Grade;
            }
            private set => _Grade = value;
        }
        public Course Course
        {
            get
            {
                lock (lockAsync)
                {
                    if (_Course == null)
                        _Course = new Course();
                }
                return _Course;
            }
            private set => _Course = value;
        }

        public async Task<ObservableCollection<User>> UsersAsync()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            Task task = Task.Run(async () =>
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "Select [User].UserName, [User].Password from [User]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            User user = new User();
                            user.UserName = reader[0].ToString();
                            user.Password = reader[1].ToString();
                            users.Add(user);
                        }
                    }
                }
            });
            await task;
            return users;
        }

        public async Task<int> ExecuteNonAsync(string query, object[] param = null)
        {
            if (!string.IsNullOrEmpty(query))
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {


                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            if (param != null)
                            {
                                command.CommandType = CommandType.Text;
                                List<string> CmdParam = query.Split(' ').ToList();
                                CmdParam.RemoveAll(x => x == "" || x == ",");
                                int count = 0;
                                foreach (var item in CmdParam)
                                {
                                    if (item.Contains("@"))
                                    {
                                        command.Parameters.AddWithValue(item, param[count]);
                                        count++;
                                    }
                                }
                                int result = 0;
                                try
                                {
                                    result = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                                }
                                catch
                                {
                                    transaction.Rollback();
                                    throw;
                                }
                                transaction.Commit();
                                return result;
                            }
                            else
                            {
                                int result = 0;
                                try
                                {
                                    result = await command.ExecuteNonQueryAsync();
                                }
                                catch
                                {
                                    transaction.Rollback();
                                    throw;
                                }
                                transaction.Commit();
                                return result;
                            }
                        }
                    }
                }
            }
            return -1;
        }

        public DataTable GetDataTable(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                DataTable data = new DataTable();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            dataAdapter.Fill(data);
                            return data;
                        }
                    }
                }
            }
            return null;
        }
    }
}
