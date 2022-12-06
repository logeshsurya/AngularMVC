using System.Data;
using System.Data.SqlClient;
namespace User.Models
{
    public class DepartmentDAL
    {
        string connection_string = "Data Source=ASPLAP2934;Initial Catalog=User_Management_System;Integrated Security=true;Pooling=False";

        public IEnumerable<Department> GetDepartments()
        {
            List<Department> department = new List<Department>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("GetAllDepartment", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Department dmp = new Department();

                    if (int.TryParse(reader["dept_id"].ToString(), out int id))
                    {
                        dmp.id = id;
                        dmp.department_name = reader["dept_name"].ToString();
                        department.Add(dmp);
                    }


                }
                connection.Close();

            }
            return department;
        }

        public void AddDepartment(Department department)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Insert_Department", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("dept_name", department.department_name);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteDepartment(int? id)
        {

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Delete_department", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Dept_id", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Department GetDepartmentById(int? id)
        {
            Department department = new Department();

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("GetDepartmentById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Dept_id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    department.id = Convert.ToInt32(reader["Dept_id"]);
                    department.department_name = reader["dept_name"].ToString();

                }
            }
            return department;
        }


        public void UpdateDepartment(Department department)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Update_Department", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Dept_id", department.id);
                command.Parameters.AddWithValue("@dept_name", department.department_name);



                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}