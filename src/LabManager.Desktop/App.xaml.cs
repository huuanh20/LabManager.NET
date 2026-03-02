using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using LabManager.Desktop.Services;
using LabManager.Desktop.ViewModels;
using LabManager.Desktop.Views;

namespace LabManager.Desktop;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Http Clients & Services
        services.AddHttpClient<ApiService>();
        // Note: AddHttpClient<T> registers T as Transient by default for Http typed clients
        // But we want ApiService to hold a token state, so let's register it as Singleton:
        // Or inject IHttpClientFactory into a singleton ApiService
        
        // Cập nhật lại: ApiService giữ token nên gán là Singleton, truyền HttpClient vào mỏ neo
        services.AddSingleton<ApiService>(sp => 
        {
            var client = new System.Net.Http.HttpClient();
            return new ApiService(client);
        });

        // ViewModels
        services.AddTransient<LoginViewModel>(sp => 
        {
            return new LoginViewModel(
                sp.GetRequiredService<ApiService>(),
                onLoginSuccess: () => 
                {
                    // Lấy lại window và Navigation sang Main
                    var mainWindow = (MainWindow)Current.MainWindow;
                    var mainView = sp.GetRequiredService<MainView>();
                    var mainVM = sp.GetRequiredService<MainViewModel>();
                    mainView.DataContext = mainVM;
                    mainWindow.ShowView(mainView);
                }
            );
        });
        
        services.AddTransient<MainViewModel>();
        services.AddTransient<PatientViewModel>();
        services.AddTransient<TestOrderViewModel>();

        // Views
        services.AddTransient<MainWindow>();
        services.AddTransient<LoginView>();
        services.AddTransient<MainView>();
        services.AddTransient<PatientView>();
        services.AddTransient<TestOrderView>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        var loginView = _serviceProvider.GetRequiredService<LoginView>();
        var loginViewModel = _serviceProvider.GetRequiredService<LoginViewModel>();
        
        loginView.DataContext = loginViewModel;
        mainWindow.ShowView(loginView);
        
        mainWindow.Show();
    }
}

