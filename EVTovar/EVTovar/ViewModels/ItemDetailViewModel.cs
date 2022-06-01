using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using EVTovar.Models;

namespace EVTovar.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {        



        private string _itemInfo;
        public string ItemInfo
        {
            get => _itemInfo;
            set => SetProperty(ref _itemInfo, value);
        }

        private Item _item;
        public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        private int _id;
        public int ItemId
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                GetItemById(value);
            }
        }

        public Command DeleteCommand { get; }

        public ItemDetailViewModel()
        {
            DeleteCommand = new Command(async () => await RemoveItem());
        }

        async Task RemoveItem()
        {
            try
            {
                await DataService.RemoveItemAsync(_item);
            }
            catch
            {
                Debug.WriteLine("Failed to Delete Item");
            }

            await Shell.Current.GoToAsync("..");
        }

        public async void GetItemById(int Id)
        {
            try
            {
                var item = await DataService.GetDataByIdAsync<Item>(Id);
                Item = item;
                ItemInfo = 
                    $"Last modified: {item.Modified}\nWeight: {item.Weight} g\nPrice: {item.Price} €\nStock: {item.Stock}";
                Id = item.Id;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
