using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using YRM.Infrastructure.Context;

namespace YRM.Migrations
{
    public class MigrationFunction
    {
        private readonly ILogger logger;
        private readonly ReminderDbContext reminderDbContext;

        public MigrationFunction(ILoggerFactory loggerFactory, ReminderDbContext reminderDbContext)
        {
            logger = loggerFactory.CreateLogger<MigrationFunction>();
            this.reminderDbContext = reminderDbContext;
        }

        [Function("Migrate")]
        public async Task Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            try
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                using (StreamWriter sr = cmd.StandardInput)
                {
                    sr.WriteLine("dotnet");
                    sr.Close();
                }
                // Wait for the write to be completed
                cmd.WaitForExit();
                cmd.Close();


                //ProcessStartInfo procStartInfo =
                //    new ProcessStartInfo("mysqldump", "uroot ppassword databse > c:/data.sql");

                //await reminderDbContext.Database.MigrateAsync();
                //var appliedMigration = await reminderDbContext.Database.GetAppliedMigrationsAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }
        }
    }
}
