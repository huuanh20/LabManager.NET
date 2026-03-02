using System.Collections.ObjectModel;
using LabManager.WPF.Commands;
using LabManager.WPF.Models;

namespace LabManager.WPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string _statusMessage = "Ready";
    private ViewModelBase? _currentView;

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public ViewModelBase? CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public RelayCommand NavigateToLabsCommand { get; }
    public RelayCommand NavigateToPatientsCommand { get; }
    public RelayCommand NavigateToTestOrdersCommand { get; }

    public MainViewModel()
    {
        NavigateToLabsCommand = new RelayCommand(_ =>
        {
            CurrentView = new LabsViewModel();
            StatusMessage = "Viewing Labs";
        });

        NavigateToPatientsCommand = new RelayCommand(_ =>
        {
            CurrentView = new PatientsViewModel();
            StatusMessage = "Viewing Patients";
        });

        NavigateToTestOrdersCommand = new RelayCommand(_ =>
        {
            CurrentView = new TestOrdersViewModel();
            StatusMessage = "Viewing Test Orders";
        });

        // Default view
        CurrentView = new LabsViewModel();
    }
}
