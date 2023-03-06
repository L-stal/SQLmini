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

        internal static void CreateProject(ProjectModel project)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Query($"INSERT INTO rls_project (project_name) VALUES (@project_name)", project);
            }
        }

        internal static bool LoadProjcetByName(string projectName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try {
                    var output = cnn.Query($"SELECT * FROM rls_project_person WHERE project_name='{projectName}'", new DynamicParameters());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }

        }
        internal static bool CheckPerson(string name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query($"SELECT * FROM rls_person WHERE person_name='{name}'", new DynamicParameters());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }

        }

        // HÄR ÄR DU 
        internal static void AddHours(string projectName,int hours)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Query($"INSERT INTO rls_project_person ('{hours}') WHERE '{projectName}'=project_id VALUES (@hours) ", new DynamicParameters());
            }

        }


        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
