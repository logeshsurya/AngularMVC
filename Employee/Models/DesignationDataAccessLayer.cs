using System.Data;
using System.Data.SqlClient;
namespace User.Models
{
    public class DesignationDAL
    {
        string connection_string = "Data Source=ASPLAP2934;Initial Catalog=User_Management_System;Integrated Security=true;Pooling=False";

        public IEnumerable<Designation> GetDesignations()
        {
            List<Designation> designation = new List<Designation>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("GetAllDesignation", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Designation dsg = new Designation();


                    if (int.TryParse(reader["desg_id"].ToString(), out int id))
                    {
                        dsg.id = id;
                        dsg.designation_name = reader["desg_name"].ToString();


                        designation.Add(dsg);
                    }



                }
                connection.Close();

            }
            return designation;
        }

        public void AddDesignation(Designation designation)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Insert_designation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("desg_name", designation.designation_name);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void DeleteDesignation(int? id)
        {

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Delete_Designation", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@desg_id", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public Designation GetDesignationById(int? id)
        {
            Designation designation = new Designation();

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("GetDesignationById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@desg_id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    designation.id = Convert.ToInt32(reader["desg_id"]);
                    designation.designation_name = reader["desg_name"].ToString();
                }
            }
            return designation;
        }

        public void UpdateDesignation(Designation designation)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Update_Designation", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@desg_id", designation.id);
                command.Parameters.AddWithValue("@desg_name", designation.designation_name);



                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}