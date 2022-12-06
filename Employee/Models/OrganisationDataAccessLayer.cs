
using System.Data.SqlClient;
using System.Data;
namespace User.Models
{
    public class OrganisationDAL
    {
        string connection_string = "Data Source=ASPLAP2934;Initial Catalog=User_Management_System;Integrated Security=true;Pooling=False";

        public IEnumerable<Organisation> GetOrganisations()
        {
            List<Organisation> organisation = new List<Organisation>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("GetAllOrganisation", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    Organisation org = new Organisation();

                    if (int.TryParse(reader["id"].ToString(), out int id))
                    {
                        org.id = id;
                        org.Organisation_Name = reader["Organisation_name"].ToString();
                        organisation.Add(org);
                    }
                }
                connection.Close();
            }
            return organisation;

        }


        public void AddOrganisation(Organisation organisation)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Insert_Organisation", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@organisation_name", organisation.Organisation_Name);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void DeleteOrganisation(int? id)
        {

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Delete_Organisation", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Organisation GetOrganisationById(int? id)
        {
            Organisation organisation = new Organisation();

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("GetOrganisationById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    organisation.id = Convert.ToInt32(reader["id"]);
                    organisation.Organisation_Name = reader["Organisation_name"].ToString();
                }
            }
            return organisation;
        }

        public void UpdateOrganisation(Organisation organisation)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                SqlCommand command = new SqlCommand("Update_organisation", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", organisation.id);
                command.Parameters.AddWithValue("@Organisation_name", organisation.Organisation_Name);


                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


    }
}