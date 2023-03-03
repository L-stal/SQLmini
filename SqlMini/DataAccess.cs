using Dapper;
using Npgsql;
using System.Configuration;
using System.Data;

namespace SqlMini
{
    internal class DataAccess
    {
        internal static List<PersonModel> GetPersonData()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PersonModel>($"SELECT * FROM rls_person ", new DynamicParameters());
                return output.ToList();
            }
        }

        internal static void CreatePerson(PersonModel person)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Query($"INSERT INTO rls_person (person_name) VALUES (@person_name)", person);
            }

        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
