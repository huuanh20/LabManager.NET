using System.Windows;
using System.Windows.Controls;
using LabManager.Desktop.Views;
using LabManager.Desktop.ViewModels;

namespace LabManager.Desktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void ShowView(UserControl view)
    {
        MainContentControl.Content = view;
    }
}