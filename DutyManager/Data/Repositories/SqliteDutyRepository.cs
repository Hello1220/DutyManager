using DutyManager.Data.Models;
using DutyManager.Data.Repositories;
using Microsoft.Data.Sqlite;
using SQLite; // 添加正确的命名空间引用
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;// 在文件顶部添加明确的 using 指令
using SQLiteNet = SQLite; // 为 SQLite-net-pcl 创建别名

public class SqliteDutyRepository : IDutyRepository
{
    // 在代码中使用别名
    public async Task InitializeAsync()
    {
        using var db = new SQLiteNet.SQLiteConnection(_dbPath);
        await db.CreateTableAsync<Student>();
        // 其他代码...
    }

    // 其他方法也使用 SQLiteNet.SQLiteConnection
}

public class SqliteDutyRepository : IDutyRepository
{
    private readonly string _dbPath;

    public SqliteDutyRepository()
    {
        _dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DutyManager",
            "duty.db");

        Directory.CreateDirectory(Path.GetDirectoryName(_dbPath)!);
    }

    public async Task InitializeAsync()
    {
        using var db = new SQLiteConnection(_dbPath);
        await db.CreateTableAsync<Student>();
        await db.CreateTableAsync<DutySchedule>();
        await db.CreateTableAsync<DutyConfig>();
    }

    // 补全缺失的方法
    public async Task<List<Student>> GetStudentsAsync()
    {
        using var db = new SQLiteConnection(_dbPath);
        return await db.Table<Student>().ToListAsync();
    }

    public async Task SaveStudentsAsync(List<Student> students)
    {
        using var db = new SQLiteConnection(_dbPath);
        await db.DeleteAllAsync<Student>();
        await db.InsertAllAsync(students);
    }

    public async Task<DutySchedule> GetDutyScheduleAsync(DateTime date)
    {
        using var db = new SQLiteConnection(_dbPath);
        var dateStr = date.ToString("yyyy-MM-dd");
        return await db.Table<DutySchedule>()
            .Where(s => s.Date == date)
            .FirstOrDefaultAsync();
    }

    public async Task SaveDutyScheduleAsync(DutySchedule schedule)
    {
        using var db = new SQLiteConnection(_dbPath);
        var existing = await GetDutyScheduleAsync(schedule.Date);
        if (existing != null)
        {
            await db.UpdateAsync(schedule);
        }
        else
        {
            await db.InsertAsync(schedule);
        }
    }

    public async Task<DutyConfig> GetDutyConfigAsync()
    {
        using var db = new SQLiteConnection(_dbPath);
        return await db.Table<DutyConfig>().FirstOrDefaultAsync() ?? new DutyConfig();
    }

    public async Task SaveDutyConfigAsync(DutyConfig config)
    {
        using DutyManager.Data.Repositories.SQLiteConnection db = new DutyManager.Data.Repositories.SQLiteConnection(_dbPath);
        var existing = await db.Table<DutyConfig>().FirstOrDefaultAsync();
        if (existing != null)
        {
            config.Id = existing.Id;
            await db.UpdateAsync(config);
        }
        else
        {
            await db.InsertAsync(config);
        }
    }
}

namespace DutyManager.Data.Repositories
{
    public class SqliteDutyRepository : IDutyRepository
    {
        private readonly string _dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DutyManager",
            "duty.db");

        public SqliteDutyRepository()
        {
        }

        public async Task InitializeAsync()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_dbPath)!);

            var db = new SQLiteAsyncConnection(_dbPath); // 修复：移除 using 语句
            await db.CreateTableAsync<Student>();
            await db.CreateTableAsync<DutySchedule>();
            await db.CreateTableAsync<DutyConfig>();
        }

        public async Task SaveStudentsAsync(List<Student> students)
        {
            var db = new SQLiteAsyncConnection(_dbPath); // 修复：移除 using 语句
            await db.DeleteAllAsync<Student>();
            await db.InsertAllAsync(students);
        }

        public async Task<DutyConfig> GetDutyConfigAsync()
        {
            var db = new SQLiteAsyncConnection(_dbPath); // 修复：移除 using 语句
            var config = await db.Table<DutyConfig>().FirstOrDefaultAsync();
            return config ?? new DutyConfig();
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            var db = new SQLiteAsyncConnection(_dbPath); // 修复：移除 using 语句
            var students = await db.Table<Student>().ToListAsync();
            return students;
        }

        public async Task<DutySchedule> GetDutyScheduleAsync(DateTime date)
        {
            var db = new SQLiteAsyncConnection(_dbPath); // 修复：移除 using 语句
            var schedule = await db.Table<DutySchedule>()
                                   .FirstOrDefaultAsync(s => s.Date == date);
            return schedule ?? new DutySchedule { Date = date };
        }

        public async Task SaveDutyScheduleAsync(DutySchedule schedule)
        {
            var db = new SQLiteAsyncConnection(_dbPath); // 修复：移除 using 语句
            await db.InsertOrReplaceAsync(schedule);
        }

        public async Task SaveDutyConfigAsync(DutyConfig config)
        {
            var db = new SQLiteAsyncConnection(_dbPath); // 修复：移除 using 语句
            await db.InsertOrReplaceAsync(config);
        }
    }
}
