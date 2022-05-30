using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using EVTovar.Models;
using EVTovar.Views;
using Xamarin.Forms;
namespace EVTovar.ViewModels
{
    public  class InventoryViewModel : BaseViewModel
    {
        private Item _selectedItem;

        bool _onlyInStockCheck;
        public bool OnlyInStockCheck
        {
            get { return _onlyInStockCheck; }
            set 
            { 
                _onlyInStockCheck = value;
                IsBusy = true;
                InStockText = value ? "Show All" : "Show Only In Stock";
            }
        }

        string _inStockText;
        public string InStockText 
        { 
            get { return _inStockText; }
            set
            {
                SetProperty(ref _inStockText, value);
            }

        }

        public ObservableCollection<BaseItem> BaseItems { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command ShowOnlyInStockCommand { get; }
        public Command<Item> ItemTapped { get; }

        public InventoryViewModel()
        {
            Title = "Browse";
            BaseItems = new ObservableCollection<BaseItem>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            ShowOnlyInStockCommand = new Command(() => OnlyInStockCheck = !OnlyInStockCheck);

            AddItemCommand = new Command(OnAddItem);

            OnlyInStockCheck = false;
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                BaseItems.Clear();
                string query = $"SELECT {nameof(BaseItem.Id)}, {nameof(BaseItem.Name)}, {nameof(BaseItem.Image)}, {nameof(BaseItem.Stock)}, {nameof(BaseItem.Description)} FROM Item ";

                if (OnlyInStockCheck) query += $" WHERE {nameof(BaseItem.Stock)} > 0";

                var baseItems = await DataService.GetQueryAsync<BaseItem>(query);

                foreach (var item in baseItems)
                {
                    Debug.WriteLine(item.Name);
                    BaseItems.Add(item);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
