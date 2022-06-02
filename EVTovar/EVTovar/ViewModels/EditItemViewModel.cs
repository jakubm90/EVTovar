using EVTovar.Models;
using EVTovar.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace EVTovar.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public  class EditItemViewModel : AddItemViewModel
    {
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

        private Item _item;
        public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        public async void GetItemById(int Id)
        {
            try
            {
                Item = await DataService.GetDataByIdAsync<Item>(Id);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

            Name = Item.Name;
            Description = Item.Description;
            Weight = Item.Weight;
            Stock = Item.Stock;
            Price = Item.Price;

            sourceOfImage = await ImageService.CheckImagePath(Item.Image);

            switch (sourceOfImage)
            {
                case ImageService.SourceOfImage.File:
                    DisplayImageSource = ImageSource.FromFile(Item.Image);
                    break;
                case ImageService.SourceOfImage.Web:
                    DisplayImageSource = ImageSource.FromUri(new Uri(Item.Image));
                    break;
                default:
                    break;
            }
        }

        protected async override void SaveItem()
        {
            if(imageWasModified)
            {
                switch (sourceOfImage)
                {
                    case ImageService.SourceOfImage.File:
                        Image = await ImageService.SaveImageToFileAsync(imageFile);
                        break;
                    case ImageService.SourceOfImage.Web:
                        Image = webAddress;
                        break;
                    default:
                        Image = null;
                        break;
                }

                Item.Image = this.Image;
            }

            Item.Name = this.Name;
            Item.Modified = DateTime.Now;
            Item.Price = this.Price;
            Item.Stock = this.Stock;
            Item.Weight = this.Weight;
            Item.Description = this.Description;

            await DataService.UpdateItemAsync(Item);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
