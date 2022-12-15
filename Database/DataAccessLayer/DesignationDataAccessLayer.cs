using System.Data;
using System.Data.SqlClient;


namespace Database.Models
{
    public class DesignationDAL
    {
      private  string connection_String ;

      public DesignationDAL(string cs)
      {
        connection_String = cs;
      }

        public IEnumerable<Designation> GetAllDesignations()
        {
            List<Designation> listDesignations = new List<Designation>();
            DataSet ds = new DataSet();

            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("GetAllDesignation", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataAdapter data = new SqlDataAdapter(command);
                data.Fill(ds);

                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    Designation designation = new Designation();
                    designation.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["desg_id"].ToString());
                    designation.Designation_Name = ds.Tables[0].Rows[i]["desg_name"].ToString();

                    listDesignations.Add(designation);

                }

                connection.Close();

            }
            return listDesignations;
        }

        public void AddDesignation(Designation designation)
        {
            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("Insert_designation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("desg_name", designation.Designation_Name);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void DeleteDesignation(int? id)
        {

            using (SqlConnection connection = new SqlConnection(connection_String))
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

            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("GetDesignationById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@desg_id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    designation.Id = Convert.ToInt32(reader["desg_id"]);
                    designation.Designation_Name = reader["desg_name"].ToString();
                }
            }
            return designation;
        }

        public void UpdateDesignation(Designation designation)
        {
            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("Update_Designation", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@desg_id", designation.Id);
                command.Parameters.AddWithValue("@desg_name", designation.Designation_Name);



                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}