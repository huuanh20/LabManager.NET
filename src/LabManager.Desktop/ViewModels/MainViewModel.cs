using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LabManager.Desktop.Services;

namespace LabManager.Desktop.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    [ObservableProperty]
    private ObservableObject _currentViewModel;

    public PatientViewModel PatientVM { get; }
    public TestOrderViewModel TestOrderVM { get; }

    public MainViewModel(ApiService apiService, PatientViewModel patientVM, TestOrderViewModel testOrderVM)
    {
        _apiService = apiService;
        PatientVM = patientVM;
        TestOrderVM = testOrderVM;
        
        // Mặc định hiện Patient
        CurrentViewModel = PatientVM;
    }

    [RelayCommand]
    public void ShowPatient()
    {
        CurrentViewModel = PatientVM;
        PatientVM.LoadPatientsCommand.Execute(null);
    }

    [RelayCommand]
    public void ShowTestOrder()
    {
        CurrentViewModel = TestOrderVM;
        TestOrderVM.InitCommand.Execute(null);
    }
}
