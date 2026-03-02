using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LabManager.Desktop.Services;

namespace LabManager.Desktop.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly ApiService _apiService;
    private readonly Action _onLoginSuccess;

    [ObservableProperty]
    private string _email = "admin@lab.com";

    [ObservableProperty]
    private string _password = "Admin@123";

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    public LoginViewModel(ApiService apiService, Action onLoginSuccess)
    {
        _apiService = apiService;
        _onLoginSuccess = onLoginSuccess;
    }

    [RelayCommand]
    public async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Vui lòng nhập Email và Mật khẩu";
            return;
        }

        IsBusy = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _apiService.LoginAsync(Email, Password);
            if (result != null)
            {
                _onLoginSuccess?.Invoke();
            }
            else
            {
                ErrorMessage = "Đăng nhập thất bại. Kiểm tra lại tài khoản.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Lỗi kết nối server: " + ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
