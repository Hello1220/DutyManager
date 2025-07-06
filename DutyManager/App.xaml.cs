using DutyManager.Data.Repositories;
using DutyManager.Data.Services;
using DutyManager.ViewModels;
using DutyManager.Views.Windows;
using System.Windows;

namespace DutyManager
{
    public partial class App : Application
    {
        private SplashScreen _splashScreen;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 显示启动画面
            _splashScreen = new SplashScreen("Resources/splash.png");
            _splashScreen.Show(false);

            // 初始化数据库和服务
            await InitializeAppAsync();

            // 关闭启动画面，打开主窗口
            _splashScreen.Close(TimeSpan.FromSeconds(0.3));
            new MainWindow().Show();
        }

        private async Task InitializeAppAsync()
        {
            var repository = new SqliteDutyRepository();
            await repository.InitializeAsync();

            var dutyService = new DutyService(repository);
            await dutyService.InitializeDefaultDataAsync();

            // 创建主窗口数据上下文
            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(dutyService)
            };
        }
    }
}