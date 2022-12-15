using System.Data;
using System.Data.SqlClient;

namespace Database.Models

{
    public class EmployeeDAL
    {
       
        private string connection_String;

        public EmployeeDAL(string cs)
        {

            connection_String =cs;
        }

         public IEnumerable<Employee> GetAllEmployees()
        {


            List<Employee> listEmployees = new List<Employee>();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("GetAllEmployees", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                
                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    Employee employee = new Employee();
                    
                    employee.id = Convert.ToInt32(ds.Tables[0].Rows[i]["emp_id"].ToString());
                    employee.Firstname = ds.Tables[0].Rows[i]["firstname"].ToString();
                    employee.Lastname = ds.Tables[0].Rows[i]["lastname"].ToString();
                    employee.Email = ds.Tables[0].Rows[i]["emp_email"].ToString();
                    employee.Gender = ds.Tables[0].Rows[i]["gender"].ToString();
                    employee.Department_Name = ds.Tables[0].Rows[i]["dept_name"].ToString();
                    employee.Designation_Name = ds.Tables[0].Rows[i]["desg_name"].ToString();
                    employee.Organisation_Name = ds.Tables[0].Rows[i]["Organisation_name"].ToString();
                    employee.DOB = (DateTime)ds.Tables[0].Rows[i]["DOB"];

                    listEmployees.Add(employee);
                    
                }
            
                connection.Close();

            }

            return listEmployees;
        }
        public void AddEmployee(Employee employee)
        {

            using (SqlConnection connection = new SqlConnection(connection_String))
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

            using (SqlConnection connection = new SqlConnection(connection_String))
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

            using (SqlConnection connection = new SqlConnection(connection_String))
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
            using (SqlConnection connection = new SqlConnection(connection_String))    
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

        // public IEnumerable<Product> GetProductFilter(int? id)    

        // {    

        //     List<Product> lstProduct = new List<Product>();    

   

        //     using (SqlConnection con = new SqlConnection(connectionString))    

        //     {    

        //         string sqlQuery=("Select * from (Product as p inner join Categories on Categories.Category_id=p.Category_id)where p.Category_id="+id);    

        //         SqlCommand cmd = new SqlCommand(sqlQuery,con);  

   

        //         con.Open();    

        //         SqlDataReader reading = cmd.ExecuteReader();    

   

        //         while (reading.Read())    

        //         {   
        //             Employee employee = new Employee();
                    
        //             employee.id = Convert.ToInt32(ds.Tables[0].Rows[i]["emp_id"].ToString());
        //             employee.Firstname = ds.Tables[0].Rows[i]["firstname"].ToString();
        //             employee.Lastname = ds.Tables[0].Rows[i]["lastname"].ToString();
        //             employee.Email = ds.Tables[0].Rows[i]["emp_email"].ToString();
        //             employee.Gender = ds.Tables[0].Rows[i]["gender"].ToString();
        //             employee.Department_Name = ds.Tables[0].Rows[i]["dept_name"].ToString();
        //             employee.Designation_Name = ds.Tables[0].Rows[i]["desg_name"].ToString();
        //             employee.Organisation_Name = ds.Tables[0].Rows[i]["Organisation_name"].ToString();
        //             employee.DOB = (DateTime)ds.Tables[0].Rows[i]["DOB"];

        //             listEmployees.Add(employee);
                    
        //         }
            
        //         connection.Close();

        //     }

        //     return listEmployees;
        // }

    }
}




         


        // public IEnumerable<Employee> GetAllEmployees()
        // {
        //     List<Employee> listEmployees = new List<Employee>();
        //     using (SqlConnection connection = new SqlConnection(connection_String))
        //     {
        //         SqlCommand command = new SqlCommand("GetAllEmployees", connection);
        //         command.CommandType = CommandType.StoredProcedure;
        //         connection.Open();
        //         SqlDataReader reader = command.ExecuteReader();
        //         while (reader.Read())
        //         {
        //             Employee employee = new Employee();
        //             employee.id = Convert.ToInt32(reader["emp_id"]);
        //             employee.Firstname = reader["firstname"].ToString();
        //             employee.Lastname = reader["lastname"].ToString();
        //             employee.Email = reader["emp_email"].ToString();
        //             employee.Gender = reader["gender"].ToString();
        //             employee.Department_Name = reader["dept_name"].ToString();
        //             employee.Designation_Name = reader["desg_name"].ToString();
        //             employee.Organisation_Name = reader["Organisation_name"].ToString();
        //             employee.DOB = (DateTime)reader["DOB"];
        //             listEmployees.Add(employee);
        //         }
        //         connection.Close();

        //     }

        //     return listEmployees;
        // }