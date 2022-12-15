using System.Data;
using System.Data.SqlClient;


namespace Database.Models
{
    public class DepartmentDAL
    {
        private string connection_String;

        public DepartmentDAL(string cs)
        {
            connection_String = cs;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            List<Department> listDepartments = new List<Department>();
            DataSet ds = new DataSet();

            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("GetAllDepartment", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataAdapter data = new SqlDataAdapter(command);
                data.Fill(ds);

                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    Department department = new Department();

                    department.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["dept_id"].ToString());
                    department.Department_Name = ds.Tables[0].Rows[i]["dept_name"].ToString();

                    listDepartments.Add(department);

                }

                connection.Close();
            }

            return listDepartments;
        }

        public void AddDepartment(Department department)
        {
            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("Insert_Department", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("dept_name", department.Department_Name);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteDepartment(int? id)
        {

            using (SqlConnection connection = new SqlConnection(connection_String))
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

            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("GetDepartmentById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Dept_id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    department.Id = Convert.ToInt32(reader["Dept_id"]);
                    department.Department_Name = reader["dept_name"].ToString();

                }
            }
            return department;
        }


        public void UpdateDepartment(Department department)
        {
            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("Update_Department", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Dept_id", department.Id);
                command.Parameters.AddWithValue("@dept_name", department.Department_Name);



                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}