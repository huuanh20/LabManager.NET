using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LabManager.Desktop.Models;
using LabManager.Desktop.Services;

namespace LabManager.Desktop.ViewModels;

public partial class PatientViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    [ObservableProperty]
    private ObservableCollection<PatientModel> _patients = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _newFullName = string.Empty;
    [ObservableProperty]
    private string _newEmail = string.Empty;

    public PatientViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [RelayCommand]
    public async Task LoadPatientsAsync()
    {
        IsBusy = true;
        var data = await _apiService.GetPatientsAsync();
        Patients.Clear();
        foreach (var p in data)
        {
            Patients.Add(p);
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task CreatePatientAsync()
    {
        if (string.IsNullOrWhiteSpace(NewFullName)) return;

        IsBusy = true;
        var newPatient = new CreatePatientModel
        {
            FullName = NewFullName,
            Email = NewEmail,
            Gender = "Unknown",
            PhoneNumber = "00000000"
        };

        var result = await _apiService.CreatePatientAsync(newPatient);
        if (result != null)
        {
            Patients.Insert(0, result);
            NewFullName = string.Empty;
            NewEmail = string.Empty;
        }
        IsBusy = false;
    }
}
