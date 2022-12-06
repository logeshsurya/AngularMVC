using System.Data;
using System.Data.SqlClient;

namespace User.Models

{
    public class EmployeeDAL
    {
        string connection_string = "Data Source=ASPLAP2934;Initial Catalog=User_Management_System;Integrated Security=true;Pooling=False";

        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> listEmployees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("GetAllEmployees", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.id = Convert.ToInt32(reader["emp_id"]);
                    employee.Firstname = reader["firstname"].ToString();
                    employee.Lastname = reader["lastname"].ToString();
                    employee.Email = reader["emp_email"].ToString();
                    employee.Gender = reader["gender"].ToString();
                    employee.Department_Name = reader["dept_name"].ToString();
                    employee.Designation_Name = reader["desg_name"].ToString();
                    employee.Organisation_Name = reader["Organisation_name"].ToString();
                    employee.DOB = (DateTime)reader["DOB"];
                    listEmployees.Add(employee);
                }
                connection.Close();

            }

            return listEmployees;
        }
        public void AddEmployee(Employee employee)
        {

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Insert_Employee", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@firstname", employee.Firstname);
                command.Parameters.AddWithValue("@lastname", employee.Lastname);
                command.Parameters.AddWithValue("@emp_email", employee.Email);
                command.Parameters.AddWithValue("@DOB", employee.DOB);
                command.Parameters.AddWithValue("@gender", employee.Gender);
                command.Parameters.AddWithValue("@Dept_id", employee.Department);
                command.Parameters.AddWithValue("@org_id", employee.Organisation);
                command.Parameters.AddWithValue("@Desg_id", employee.Designation);

                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Successfully created");
                }
                else
                {
                    System.Console.WriteLine("Not Created");
                }
                connection.Close();
            }
        }


        public Employee GetEmployeeById(int? id)
        {
            Employee employee = new Employee();

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("GetEmployeeById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@emp_id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    employee.id = Convert.ToInt32(reader["emp_id"]);
                    employee.Firstname = reader["firstname"].ToString();
                    employee.Lastname = reader["lastname"].ToString();
                    employee.Email = reader["emp_email"].ToString();
                    employee.DOB = (DateTime?)reader["DOB"];
                    employee.Department = Convert.ToInt32(reader["Dept_id"]);
                    employee.Organisation = Convert.ToInt32(reader["Org_id"]);
                    employee.Designation = Convert.ToInt32(reader["Desg_id"]);
                    employee.Gender = reader["gender"].ToString();
                    employee.Organisation_Name = reader["Organisation_name"].ToString();
                    employee.Department_Name = reader["dept_name"].ToString();
                    employee.Designation_Name = reader["desg_name"].ToString();
                }
            }
            return employee;
        }


        public void DeleteEmployee(int? id)
        {

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Delete_Employee", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@emp_id", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        
           public void UpdateEmployee(Employee employee)    
        {    
            using (SqlConnection connection = new SqlConnection(connection_string))    
            {    
                SqlCommand command = new SqlCommand("Update_Employee", connection);    
                command.CommandType = CommandType.StoredProcedure;    
    
                command.Parameters.AddWithValue("@emp_id", employee.id);    
                command.Parameters.AddWithValue("@firstname", employee.Firstname);
                command.Parameters.AddWithValue("@lastname", employee.Lastname);  
                command.Parameters.AddWithValue("@emp_email", employee.Email);
                command.Parameters.AddWithValue("@DOB", employee.DOB);    
                command.Parameters.AddWithValue("@gender", employee.Gender);   
                command.Parameters.AddWithValue("@Org_id",employee.Organisation); 
                command.Parameters.AddWithValue("@Dept_id", employee.Department);    
                command.Parameters.AddWithValue("@Desg_id", employee.Designation); 
   
    
                connection.Open();    
                command.ExecuteNonQuery();    
                connection.Close();    
            }    
        }

    }
}