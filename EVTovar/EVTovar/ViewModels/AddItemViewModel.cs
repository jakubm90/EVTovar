using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using EVTovar.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using EVTovar.Services;
using System.IO;
using Xamarin.Essentials;

namespace EVTovar.ViewModels
{
    public class AddItemViewModel : BaseViewModel
    {

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
        public Command DeleteImageCommand { get; }
        public Command AddImageFromDiskCommand { get; }
        public Command AddImageFromWebCommand { get; }

        public AddItemViewModel()
        {
            SaveCommand = new Command(SaveItem, ValidateSave);
            AddImageFromDiskCommand = new Command(async () => await AddImageFromDisk());
            AddImageFromWebCommand = new Command(async () => await AddImageFromWeb());
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }


        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Name)
                && !String.IsNullOrWhiteSpace(Description);
        }

        private string _image;

        public string Image 
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        // Load image from disk/ web
        enum SourceOfImage { None, File, Web }

        SourceOfImage sourceOfImage;

        ImageSource _imageSource;
        public ImageSource DisplayImageSource
        {
            get { return _imageSource; }
            set
            {
                SetProperty(ref _imageSource, value);
            }
        }

        Stream stream;
        FileResult imageFile;
        string webAddress;
        private async Task AddImageFromDisk()
        {
            var fileResult = await ImageService.PickImage();

            if (fileResult != null) imageFile = fileResult;
            else return;

            stream?.Dispose();
            stream = await ImageService.StreamImage(imageFile);
            if (stream != null) 
            { 
                DisplayImageSource = ImageSource.FromStream(() => stream);
                sourceOfImage = SourceOfImage.File;
            }
        }

        private async Task AddImageFromWeb()
        {
            string adress = await App.Current.MainPage.DisplayPromptAsync("Add Image From Web", "Enter URL");
            if (adress != null) 
            {

                bool exist = await ImageService.CheckWebURL(adress);
                if (exist) 
                {
                    sourceOfImage = SourceOfImage.Web;
                    stream?.Dispose();
                    DisplayImageSource = ImageSource.FromUri(new Uri(adress));
                    webAddress = adress; 
                }
                else
                    await App.Current.MainPage.DisplayAlert("Error", "Not a valid URL!", "OK");
            }
        }

        /// <summary>
        /// Save Item To Database
        /// </summary>
        private async void SaveItem()
        {
            switch (sourceOfImage)
            {
                case SourceOfImage.File:
                    Image = await ImageService.SaveImageToFileAsync(imageFile);
                    break;
                case SourceOfImage.Web:
                    Image = webAddress;
                    break;
                default:
                    Image = null;
                    break;
            }

            Item item = new Item()
            {
                Name = this.Name,
                Modified = DateTime.Now,
                Image = !String.IsNullOrWhiteSpace(this.Image) ? this.Image : "placeholder.png",
                Price = this.Price,
                Stock = this.Stock,
                Weight = this.Weight,
                Description = this.Description
            };

            await DataService.SaveItemAsync(item);
            await Shell.Current.GoToAsync("..");
        }

        public void OnDisappearing()
        {
            stream?.Dispose();
        }
    }
}
