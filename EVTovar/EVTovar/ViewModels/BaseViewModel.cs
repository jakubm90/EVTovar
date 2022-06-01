using EVTovar.Models;
using EVTovar.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EVTovar.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataService DataService => App.DataService;

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        async Task<string> LoadImageAsync(FileResult file)
        {
            string imagePath;
            if (file == null)
            {

                imagePath = null;
                return imagePath;
            }

            var newFile = Path.Combine(FileSystem.AppDataDirectory, file.FileName);
            using (var stream = await file.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            imagePath = newFile;

            return imagePath;

        }

        protected async Task<string> PickAndSaveImage()
        {

            string imagePath = null;

            try
            {
                var image = await MediaPicker.PickPhotoAsync();
                imagePath = await LoadImageAsync(image);
                if (!String.IsNullOrWhiteSpace(imagePath))
                {
                    await Task.Delay(100);
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        imagePath = "file://" + imagePath;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return imagePath;
        }

        protected async Task<bool> CheckImagePath(BaseItem baseItem)
        {
            string imagePath = baseItem?.Image;

            if (String.IsNullOrWhiteSpace(imagePath)) return false;
            
            if(File.Exists(imagePath)) return true;
            //web
            else return false;
        }
    }
}
