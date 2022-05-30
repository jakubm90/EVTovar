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
        private BaseItem _selectedItem;

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

        bool _orderBy;
        public bool OrderBy
        {
            get { return _orderBy; }
            set
            {
                _orderBy = value;
                IsBusy = true;
                OrderByText = value ? "▲ NAME" : "▼ NAME";
            }
        }

        string _orderByText;
        public string OrderByText
        {
            get { return _orderByText; }
            set
            {
                SetProperty(ref _orderByText, value);
            }

        }

        public ObservableCollection<BaseItem> BaseItems { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command ShowOnlyInStockCommand { get; }
        public Command OrderByCommand { get; }
        public Command<Item> ItemTapped { get; }

        public InventoryViewModel()
        {
            Title = "Browse";
            BaseItems = new ObservableCollection<BaseItem>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            ShowOnlyInStockCommand = new Command(() => OnlyInStockCheck = !OnlyInStockCheck);

            OrderByCommand = new Command(() => OrderBy = !OrderBy);

            AddItemCommand = new Command(OnAddItem);

            OnlyInStockCheck = false;

            OrderBy = false;
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                BaseItems.Clear();

                string query = 
                    $@"SELECT {nameof(BaseItem.Id)}, {nameof(BaseItem.Name)}, {nameof(BaseItem.Image)}, {nameof(BaseItem.Stock)}, 
                       substr({nameof(BaseItem.Description)}, 1, 25) as {nameof(BaseItem.Description)} FROM Item ";

                if (OnlyInStockCheck) query += $" WHERE {nameof(BaseItem.Stock)} > 0";
                query += $" ORDER BY {nameof(BaseItem.Name)} {(OrderBy ? "DESC" : "ASC")}";

                var baseItems = await DataService.GetQueryAsync<BaseItem>(query);

                foreach (var item in baseItems)
                {
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

        public BaseItem SelectedItem
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

        async void OnItemSelected(BaseItem item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
