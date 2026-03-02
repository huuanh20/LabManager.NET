using System.Collections.ObjectModel;
using LabManager.WPF.Commands;
using LabManager.WPF.Models;

namespace LabManager.WPF.ViewModels;

public class PatientsViewModel : ViewModelBase
{
    private PatientModel? _selectedPatient;
    private string _searchText = string.Empty;

    public ObservableCollection<PatientModel> Patients { get; } = new();

    public PatientModel? SelectedPatient
    {
        get => _selectedPatient;
        set => SetProperty(ref _selectedPatient, value);
    }

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public RelayCommand AddPatientCommand { get; }
    public RelayCommand EditPatientCommand { get; }
    public RelayCommand DeletePatientCommand { get; }
    public RelayCommand RefreshCommand { get; }

    public PatientsViewModel()
    {
        AddPatientCommand = new RelayCommand(_ => AddPatient());
        EditPatientCommand = new RelayCommand(_ => EditPatient(), _ => SelectedPatient is not null);
        DeletePatientCommand = new RelayCommand(_ => DeletePatient(), _ => SelectedPatient is not null);
        RefreshCommand = new RelayCommand(_ => LoadPatients());

        LoadPatients();
    }

    private void LoadPatients()
    {
        // In a real implementation, this would call the API service
        Patients.Clear();
        Patients.Add(new PatientModel { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1985, 6, 15), Gender = "Male" });
        Patients.Add(new PatientModel { Id = 2, FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(1990, 3, 22), Gender = "Female" });
        Patients.Add(new PatientModel { Id = 3, FirstName = "Alice", LastName = "Johnson", DateOfBirth = new DateTime(1975, 11, 8), Gender = "Female" });
    }

    private void AddPatient()
    {
        var newPatient = new PatientModel
        {
            Id = Patients.Count + 1,
            FirstName = "New",
            LastName = "Patient",
            DateOfBirth = DateTime.Today,
            Gender = "Unknown"
        };
        Patients.Add(newPatient);
        SelectedPatient = newPatient;
    }

    private void EditPatient()
    {
        // In a real implementation, this would open a dialog
    }

    private void DeletePatient()
    {
        if (SelectedPatient is not null)
        {
            Patients.Remove(SelectedPatient);
            SelectedPatient = null;
        }
    }
}
