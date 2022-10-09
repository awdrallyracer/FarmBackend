using Microsoft.AspNetCore.Mvc;
using Farm.Models;
using Farm.Models.DbModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Controllers
{
    [Route("api/[controller]")]
    public class BackupController : Controller
    {
        IWebHostEnvironment _appEnviropment;
        ApplicationContext appCtx;
        IConfiguration _configuration;
        const string DbName = "FarmBD";
        string connectionString;
        public BackupController(IConfiguration configuration, IWebHostEnvironment appEnvironment, ApplicationContext ctx)
        {
            _configuration = configuration;
            _appEnviropment = appEnvironment;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            appCtx = ctx;
        }

        [HttpGet("getBackups")]
        public IActionResult GetBackups()
        {
            IEnumerable<Backup> result = appCtx.Backups.ToList();
            return Json(result);
        }

        [HttpPost("addBackup")]
        public void BackUpDataBase()
        {
            const string rootPath = "BackUps";
            var date = DateTime.Now;
            string fileName = "BackUp" + date.TimeOfDay.ToString().Replace(":", "_").Replace(".", "_") + ".bak";
            string path = String.Format("{0}\\{1}\\", _appEnviropment.WebRootPath, rootPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += fileName;
            string queryString = $@"BACKUP DATABASE {DbName} TO DISK = '{path}'";

            appCtx.Add(new Backup
            {
                FileName = fileName,
                FilePath = path,
                CreationTime = date
            });
            appCtx.SaveChanges();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteReader();
            }
        }

        [HttpDelete("deleteBackup/{id}")]
        public IActionResult DeleteBackUp(int id)
        {
            Backup a = appCtx.Backups.FirstOrDefault(a => a.Id == id);
            if (a != null)
            {
                appCtx.Backups.Remove(a);
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid AnimalId" });

        }


        [HttpPost("updateBackup/{id}")]
        public void RestoreDataBase(int id)
        {
            Backup bkp = appCtx.Backups.FirstOrDefault(d => d.Id == id);
            if (bkp != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string useMaster = "USE master";
                    SqlCommand useMasterCommand = new SqlCommand(useMaster, connection);

                    string alterFirst = $@"ALTER DATABASE {DbName} SET Single_User WITH Rollback Immediate";
                    SqlCommand alterfirstCommand = new SqlCommand(alterFirst, connection);

                    string restore = string.Format("RESTORE DATABASE {0} FROM DISK = '{1}' WITH REPLACE", DbName, bkp.FilePath);
                    SqlCommand restoreCommand = new SqlCommand(restore, connection);

                    string alterSecond = $@"ALTER DATABASE {DbName} SET Multi_User";
                    SqlCommand alterSecondCommand = new SqlCommand(alterSecond, connection);

                    connection.Open();

                    useMasterCommand.ExecuteNonQuery();
                    alterfirstCommand.ExecuteNonQuery();
                    restoreCommand.ExecuteNonQuery();
                    alterSecondCommand.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
    }
}
