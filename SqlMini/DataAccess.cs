using Dapper;
using Npgsql;
using System.Collections.Immutable;
using System.Configuration;
using System.Data;
using System.Xml.Linq;

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
        internal static List<ProjectModel> GetProjectData()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectModel>($"SELECT * FROM rls_project ", new DynamicParameters());
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
        //Made it a bool to return true or false to check if the project exist in database
        internal static bool LoadProjectByName(string projectName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query($"SELECT * FROM rls_project WHERE project_name = '{projectName}'", new DynamicParameters());
                if (output.Count() == 0)
                {
                    return false;
                }
                return true;
            }

        }
        //same as above
        internal static bool CheckPerson(string name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                    var output = cnn.Query($"SELECT * FROM rls_person WHERE person_name='{name}'", new DynamicParameters());
                if (output.Count() == 0)
                {
                    return false;
                }
                return true;
            }

        }
        internal static int GetProjectId(string projectName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
          {
                var output = cnn.Query<ProjectModel>($"SELECT id FROM rls_project WHERE project_name='{projectName}'", new DynamicParameters());
                return output.First().id;
           }

        }
        internal static int GetPersonId(string personName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectModel>($"SELECT id FROM rls_person WHERE person_name='{personName}'", new DynamicParameters());
                return output.First().id;
            }

        }
        internal static List<ProjectPersonModel> ProjectSelection(int personID)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
               var output =  cnn.Query<ProjectPersonModel>($"SELECT project_name, rls_project_person.id ,rls_project_person.hours FROM rls_project_person INNER JOIN rls_project ON rls_project_person.project_id=rls_project.id WHERE person_id='{personID}'");
               return output.ToList();

            }

        }
        internal static void AddHours(ProjectPersonModel project)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Query($"INSERT INTO rls_project_person (project_id,person_id,hours) VALUES (@project_id,@person_id,@hours)", project);

            }

        }
        internal static void UpdateHours(ProjectPersonModel project)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Query($"UPDATE rls_project_person SET hours=@hours WHERE id=@id", project);

            }

        }
        internal static void UpdatePerson(PersonModel project)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Query($"UPDATE rls_person SET person_name=@person_name WHERE id=@id", project);

            }

        }
        internal static void UpdateProject(ProjectModel project)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Query($"UPDATE rls_project SET project_name=@project_name WHERE id=@id", project);

            }

        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
