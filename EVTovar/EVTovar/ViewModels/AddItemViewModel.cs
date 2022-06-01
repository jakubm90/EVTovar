using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using EVTovar.Models;
using System.Diagnostics;

namespace EVTovar.ViewModels
{
    public class AddItemViewModel : BaseViewModel
    {
        //<Entry Text = "{Binding Item.Name, Mode=TwoWay}" FontSize="Medium" />
        //<Label Text = "Description" FontSize="Medium" />
        //<Editor Text = "{Binding Item.Description, Mode=TwoWay}" AutoSize="TextChanges" FontSize="Medium" Margin="0" />
        //<Label Text = "Price" FontSize="Medium" />
        //<Editor Text = "{Binding Item.Price, Mode=TwoWay}" AutoSize="TextChanges" FontSize="Medium" Margin="0" />
        //<Label Text = "Weight" FontSize="Medium" />
        //<Editor Text = "{Binding Item.Weight, Mode=TwoWay}" AutoSize="TextChanges" FontSize="Medium" Margin="0" />
        //<Label Text = "In Stock" FontSize="Medium" />
        //<Editor Text = "{Binding Item.Stock, Mode=TwoWay}" AutoSize="TextChanges" FontSize="Medium" Margin="0" />
        //<Button Text = "Save" Command="{Binding SaveCommand}" HorizontalOptions="FillAndExpand"></Button>

        string _name, _description;
        int _weight, _stock;
        decimal _price;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public int Stock
        {
            get => _stock;
            set => SetProperty(ref _stock, value);
        }

        public int Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public AddItemViewModel()
        {
            SaveCommand = new Command(SaveItem, ValidateSave);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }


        private bool ValidateSave()
        {
            Debug.WriteLine("ValidateSave");
            return !String.IsNullOrWhiteSpace(Name)
                && !String.IsNullOrWhiteSpace(Description);
        }

        private async void SaveItem()
        {
            Item item = new Item()
            {
                Name = this.Name,
                Modified = DateTime.Now,
                Price = this.Price,
                Stock = this.Stock,
                Weight = this.Weight,
                Description = this.Description
            };
        

            await DataService.SaveItemAsync(item);

            await Shell.Current.GoToAsync("..");
        }
    }
}
