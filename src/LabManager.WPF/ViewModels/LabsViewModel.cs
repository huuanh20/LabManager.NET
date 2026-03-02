using System.Collections.ObjectModel;
using LabManager.WPF.Commands;
using LabManager.WPF.Models;

namespace LabManager.WPF.ViewModels;

public class LabsViewModel : ViewModelBase
{
    private LabModel? _selectedLab;
    private string _searchText = string.Empty;

    public ObservableCollection<LabModel> Labs { get; } = new();

    public LabModel? SelectedLab
    {
        get => _selectedLab;
        set => SetProperty(ref _selectedLab, value);
    }

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public RelayCommand AddLabCommand { get; }
    public RelayCommand EditLabCommand { get; }
    public RelayCommand DeleteLabCommand { get; }
    public RelayCommand RefreshCommand { get; }

    public LabsViewModel()
    {
        AddLabCommand = new RelayCommand(_ => AddLab());
        EditLabCommand = new RelayCommand(_ => EditLab(), _ => SelectedLab is not null);
        DeleteLabCommand = new RelayCommand(_ => DeleteLab(), _ => SelectedLab is not null);
        RefreshCommand = new RelayCommand(_ => LoadLabs());

        LoadLabs();
    }

    private void LoadLabs()
    {
        // In a real implementation, this would call the API service
        Labs.Clear();
        Labs.Add(new LabModel { Id = 1, Name = "Hematology Lab", Location = "Building A, Floor 2", ContactEmail = "hema@lab.com" });
        Labs.Add(new LabModel { Id = 2, Name = "Microbiology Lab", Location = "Building B, Floor 1", ContactEmail = "micro@lab.com" });
        Labs.Add(new LabModel { Id = 3, Name = "Biochemistry Lab", Location = "Building A, Floor 3", ContactEmail = "bio@lab.com" });
    }

    private void AddLab()
    {
        var newLab = new LabModel
        {
            Id = Labs.Count + 1,
            Name = "New Lab",
            Location = "TBD",
            ContactEmail = "newlab@lab.com"
        };
        Labs.Add(newLab);
        SelectedLab = newLab;
    }

    private void EditLab()
    {
        // In a real implementation, this would open a dialog
    }

    private void DeleteLab()
    {
        if (SelectedLab is not null)
        {
            Labs.Remove(SelectedLab);
            SelectedLab = null;
        }
    }
}
