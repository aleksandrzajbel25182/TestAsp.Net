using ClientTest.Wpf.Commands;
using ClientTest.Wpf.Service;
using ProductDB.Entitys;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace ClientTest.Wpf.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private readonly IPhoneApiService _phoneApiService;
        private readonly HttpClient _httpClient;

        private ObservableCollection<Phone> _phones;
        public ObservableCollection<Phone> Phones
        {
            get { return _phones; }
            set
            {
                _phones = value;
                OnPropertyChanged("Phones");
            }
        }

        private Phone? _selectedPhone;
        public Phone? SelectedPhone
        {
            get { return _selectedPhone; }
            set
            {
                _selectedPhone = value;
                OnPropertyChanged(nameof(SelectedPhone));

                if (_selectedPhone != null)
                {
                    Brand = _selectedPhone.Brand;
                    Model = _selectedPhone.Model;
                    Price = _selectedPhone.Price;
                }
            }
        }

        private string brand;
        public string Brand
        {
            get { return brand; }
            set
            {
                brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }

        private string model;
        public string Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddPhoneCommand { get; private set; }
        public ICommand SavePhoneCommand { get; private set; }
        public ICommand DeletePhoneCommand { get; private set; }

    
        public MainViewModel()
        {
            _httpClient = new HttpClient();
            _phoneApiService = new PhoneApiService(_httpClient);

            LoadDataCommand = new RelayCommand(async () => await LoadDataAsync());
            AddPhoneCommand = new RelayCommand(async () => await AddPhone());
            SavePhoneCommand = new RelayCommand(async () => await SavePhone());
            DeletePhoneCommand = new RelayCommand(async () => await DeletePhone());
        }

        private async Task LoadDataAsync()
        {
            Phones = await _phoneApiService.GetPhonesAsync();
        }

        private async Task AddPhone()
        {           
            Phone newPhone = new Phone { Brand = Brand, Model = Model, Price = Price };
            var response = await _phoneApiService.AddPhoneAsync(newPhone);
                       
            if (response)
            {
                MessageBox.Show("Phone add successfully");
                Phones.Add(newPhone);
            }
            else
            {
                MessageBox.Show("Failed to add phone");
            }
        }

        private async Task SavePhone()
        {
            if (SelectedPhone != null)
            {
                var response = await _phoneApiService.UpdatePhoneAsync(SelectedPhone);

                if (response)
                {
                    MessageBox.Show("Phone updated successfully");
                }
                else
                {
                    MessageBox.Show("Failed to update phone");
                }
            }
            else if (string.IsNullOrEmpty(SelectedPhone.Brand) || string.IsNullOrEmpty(SelectedPhone.Model) || SelectedPhone.Price == 0)
            {
                MessageBox.Show("Please fill in Brand, Model, and Price fields");
                return;
            }
        }

        private async Task DeletePhone()
        {
            if (SelectedPhone != null)
            {
                var response = await _phoneApiService.DeletePhoneAsync(SelectedPhone.Id);

                if (response)
                {
                    MessageBox.Show("Phone deleted successfully");
                    Phones.Remove(SelectedPhone);
                    SelectedPhone = null;
                }
                else
                {
                    MessageBox.Show("Failed to delete phone");
                }
            }
        }
    }
}
