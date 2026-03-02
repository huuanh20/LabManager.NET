using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LabManager.Desktop.Models;
using LabManager.Desktop.Services;

namespace LabManager.Desktop.ViewModels;

public partial class TestOrderViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    [ObservableProperty]
    private ObservableCollection<TestOrderModel> _testOrders = new();
    
    [ObservableProperty]
    private ObservableCollection<PatientModel> _patients = new();

    [ObservableProperty]
    private PatientModel? _selectedPatient;

    [ObservableProperty]
    private bool _isBusy;

    public TestOrderViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [RelayCommand]
    public async Task InitAsync()
    {
        IsBusy = true;
        var tests = await _apiService.GetTestOrdersAsync();
        TestOrders.Clear();
        foreach (var t in tests) TestOrders.Add(t);

        var pats = await _apiService.GetPatientsAsync();
        Patients.Clear();
        foreach (var p in pats) Patients.Add(p);

        IsBusy = false;
    }

    [RelayCommand]
    public async Task CreateTestOrderAsync()
    {
        if (SelectedPatient == null) return;

        IsBusy = true;
        var newOrder = new CreateTestOrderModel
        {
            PatientId = SelectedPatient.Id
        };

        var result = await _apiService.CreateTestOrderAsync(newOrder);
        if (result != null)
        {
            // Tải lại để có tên Bệnh nhân do API Get list mới Join
            await InitAsync(); 
        }
        IsBusy = false;
    }
}
