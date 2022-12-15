
using System.Data.SqlClient;
using System.Data;

namespace Database.Models
{
    public class OrganisationDAL
    {
        private string connection_String;

        public OrganisationDAL(string cs)
        {
            connection_String = cs;
        }

        public IEnumerable<Organisation> GetAllOrganisations()
        {
            List<Organisation> listOrganisations = new List<Organisation>();
            DataSet ds = new DataSet();

            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("GetAllOrganisation", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataAdapter data = new SqlDataAdapter(command);
                data.Fill(ds);
                

                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    Organisation organisation = new Organisation();

                    organisation.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                    organisation.Organisation_Name = ds.Tables[0].Rows[i]["Organisation_name"].ToString();
                    listOrganisations.Add(organisation);

                }

                connection.Close();
            }
                
            return listOrganisations;

        }


        public void AddOrganisation(Organisation organisation)
        {
            using (SqlConnection connection = new SqlConnection(connection_String))
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

            using (SqlConnection connection = new SqlConnection(connection_String))
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

            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("GetOrganisationById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    organisation.Id = Convert.ToInt32(reader["id"]);
                    organisation.Organisation_Name = reader["Organisation_name"].ToString();
                }
            }
            return organisation;
        }

        public void UpdateOrganisation(Organisation organisation)
        {
            using (SqlConnection connection = new SqlConnection(connection_String))
            {
                SqlCommand command = new SqlCommand("Update_organisation", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", organisation.Id);
                command.Parameters.AddWithValue("@Organisation_name", organisation.Organisation_Name);


                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


    }
}