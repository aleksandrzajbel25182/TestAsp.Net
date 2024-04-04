using ClientTest.Wpf.ViewModel;
using Newtonsoft.Json;
using ProductDB.Entitys;
using System.Net.Http;
using System.Windows;

namespace ClientTest.Wpf
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}