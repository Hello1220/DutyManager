using DutyManager.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyManager.Data.Repositories
{
    public class SqliteDutyRepository : IDutyRepository
    {
        private readonly string _dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DutyManager",
            "duty.db");

        public async Task InitializeAsync()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_dbPath)!);

            using var db = new SQLiteConnection(_dbPath);
            await db.CreateTableAsync<Student>();
            await db.CreateTableAsync<DutySchedule>();
            await db.CreateTableAsync<DutyConfig>();
        }

        public async Task SaveStudentsAsync(List<Student> students)
        {
            using var db = new SQLiteConnection(_dbPath);
            await db.DeleteAllAsync<Student>();
            await db.InsertAllAsync(students);
        }

        public async Task<DutyConfig> GetDutyConfigAsync()
        {
            using var db = new SQLiteConnection(_dbPath);
            var config = await db.Table<DutyConfig>().FirstOrDefaultAsync();
            return config ?? new DutyConfig();
        }

        // 其他接口实现...
    }
}
