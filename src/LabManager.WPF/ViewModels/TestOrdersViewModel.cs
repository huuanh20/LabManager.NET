using System.Collections.ObjectModel;
using LabManager.WPF.Commands;

namespace LabManager.WPF.ViewModels;

public class TestOrdersViewModel : ViewModelBase
{
    private TestOrderModel? _selectedOrder;
    private string _searchText = string.Empty;

    public ObservableCollection<TestOrderModel> TestOrders { get; } = new();

    public TestOrderModel? SelectedOrder
    {
        get => _selectedOrder;
        set => SetProperty(ref _selectedOrder, value);
    }

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public RelayCommand AddOrderCommand { get; }
    public RelayCommand EditOrderCommand { get; }
    public RelayCommand DeleteOrderCommand { get; }
    public RelayCommand RefreshCommand { get; }

    public TestOrdersViewModel()
    {
        AddOrderCommand = new RelayCommand(_ => AddOrder());
        EditOrderCommand = new RelayCommand(_ => EditOrder(), _ => SelectedOrder is not null);
        DeleteOrderCommand = new RelayCommand(_ => DeleteOrder(), _ => SelectedOrder is not null);
        RefreshCommand = new RelayCommand(_ => LoadOrders());

        LoadOrders();
    }

    private void LoadOrders()
    {
        // In a real implementation, this would call the API service
        TestOrders.Clear();
        TestOrders.Add(new TestOrderModel { Id = 1, OrderNumber = "ORD-001", TestName = "Complete Blood Count", Status = "Pending", PatientName = "John Doe" });
        TestOrders.Add(new TestOrderModel { Id = 2, OrderNumber = "ORD-002", TestName = "Urinalysis", Status = "Completed", PatientName = "Jane Smith" });
        TestOrders.Add(new TestOrderModel { Id = 3, OrderNumber = "ORD-003", TestName = "Lipid Panel", Status = "InProgress", PatientName = "Alice Johnson" });
    }

    private void AddOrder()
    {
        var newOrder = new TestOrderModel
        {
            Id = TestOrders.Count + 1,
            OrderNumber = $"ORD-{TestOrders.Count + 1:D3}",
            TestName = "New Test",
            Status = "Pending",
            PatientName = "Unknown"
        };
        TestOrders.Add(newOrder);
        SelectedOrder = newOrder;
    }

    private void EditOrder()
    {
        // In a real implementation, this would open a dialog
    }

    private void DeleteOrder()
    {
        if (SelectedOrder is not null)
        {
            TestOrders.Remove(SelectedOrder);
            SelectedOrder = null;
        }
    }
}

public class TestOrderModel
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string TestName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public DateTime OrderedAt { get; set; } = DateTime.UtcNow;
}
