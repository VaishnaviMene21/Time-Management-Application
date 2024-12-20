using Dapper;
using System.Data;
using System.Data.SqlClient;
using TimeManagement.Data;

namespace TimeManagement.Service
{
    public class TaskService
    {
        private readonly string connectionstring;
        public TaskService(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
            
        }

       
        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = "SELECT ProjectId, ProjectName FROM Project";
                var user = await db.QueryAsync<Project>(query);
                return (user);
            }
        }

      
        public async Task<Client> GetClientByProjectIdAsync(int projectId)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = @"SELECT c.ClientId, c.ClientName
                         FROM Client c
                         JOIN Project p ON p.ClientId = c.ClientId
                         WHERE p.ProjectId = @ProjectId";

                return await db.QueryFirstOrDefaultAsync<Client>(query, new { ProjectId = projectId });
            }
        }

       
        public async Task<int> SaveTaskDetailsAsync(Data.Task taskDetails)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = @"
            INSERT INTO TaskDetails (ProjectId, ClientId, TaskName, BillingType, Date, TimeWorked, BreakTime, Description)
            VALUES (@ProjectId, @ClientId, @TaskName, @BillingType, @Date, @TimeWorked, @BreakTime, @Description);
            SELECT SCOPE_IDENTITY();";

                return await db.ExecuteScalarAsync<int>(query, taskDetails);
            }
        }

        public async Task<IEnumerable<string>> GetTaskNameByProjectIdAsync(int ProjectId)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = "SELECT TaskName From ProjectTask Where ProjectId = @ProjectId";
                return await db.QueryAsync<string>(query, new { ProjectId = ProjectId });
            }
        }

        
    }
}
