using DutyManager.Data.Repositories;
using DutyManager.Data.Services;
using System.Threading.Tasks;
using System.Windows;

public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // 初始化数据库
        var repository = new SqliteDutyRepository();
        await repository.InitializeAsync();

        // 创建服务
        var dutyService = new DutyService(repository);

        // 确保有默认配置
        var config = await repository.GetDutyConfigAsync();
        if (config.Id == 0) // 新配置
        {
            await repository.SaveDutyConfigAsync(config);
        }

        // 确保有默认学生数据（可选）
        var students = await repository.GetStudentsAsync();
        if (!students.Any())
        {
            // 添加示例学生
            var defaultStudents = new List<Student>
            {
                new() { Name = "张三" },
                new() { Name = "李四" },
                new() { Name = "王五" }
            };
            await repository.SaveStudentsAsync(defaultStudents);
        }

        // 创建主窗口
        var mainWindow = new MainWindow
        {
            DataContext = new MainViewModel(dutyService)
        };

        mainWindow.Show();
    }
}