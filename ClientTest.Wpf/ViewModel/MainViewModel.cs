using ClientTest.Wpf.Commands;
using Newtonsoft.Json;
using ProductDB.Entitys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClientTest.Wpf.ViewModel
{
    class MainViewModel : ViewModel
    {
        HttpClient client = new HttpClient();
        private ObservableCollection<ProductEntitys> _product;

        public ObservableCollection<ProductEntitys> Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        private ProductEntitys _selectedProduct;

        public ProductEntitys SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged("Title"); }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        private decimal? _price;
        public decimal? Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged("Price"); }
        }



        public MainViewModel()
        {

            client.BaseAddress = new Uri("https://localhost:44325/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            this.GetProducts();
        }

        private async void GetProducts()
        {
            var responce = await client.GetStringAsync("product");
            var product = JsonConvert.DeserializeObject<IEnumerable<ProductEntitys>>(responce);
            Product = new ObservableCollection<ProductEntitys>(product);
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(param => DeleteProducts(SelectedProduct.Id));
                }
                return _deleteCommand;
            }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(param => AddProducts(new ProductEntitys
                    {
                        Title = Title,
                        Price= (decimal)Price,
                        Description = Description
                    })) ;
                }
                return _addCommand;
            }
        }

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(param => UpdateProducts(SelectedProduct));
                }
                return _editCommand;
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(param => SaveProducts(new ProductEntitys
                    {
                        Id = SelectedProduct.Id,
                        Title = Title,
                        Price = (decimal)Price,
                        Description = Description
                    }));
                }
                return _saveCommand;
            }
        }




        private async void AddProducts(ProductEntitys product)
        {
            await client.PostAsJsonAsync("product", product);
            this.GetProducts();
        }
       
        private async void SaveProducts(ProductEntitys product)
        {   
                            await client.PutAsJsonAsync("product", product);
                Title = string.Empty;
                Description = string.Empty;
                Price = null;
                this.GetProducts();
                      

        }


        private async void UpdateProducts(ProductEntitys product)
        {
            Title = SelectedProduct.Title;
            Price = SelectedProduct.Price;
            Description = SelectedProduct.Description;
        }

        private async void DeleteProducts(int productId)
        {
            await client.DeleteAsync("product/" + productId); this.GetProducts();
        }

    }
}
