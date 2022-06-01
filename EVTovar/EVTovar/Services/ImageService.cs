using EVTovar.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EVTovar.Services
{
    public  class ImageService
    {
        public static async Task<string> LoadImageAsync(FileResult file)
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

        public static async Task<string> PickAndSaveImage()
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

        public static async Task<bool> CheckImagePath(string imagePath)
        {
            if (String.IsNullOrWhiteSpace(imagePath)) return false;

            if (File.Exists(imagePath)) return true;
            else if (await CheckWebURL(imagePath)) return true;
            else return false;
        }

        public static async Task<bool> CheckWebURL(string url)
        {
            bool response = false;

            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage res = await client.GetAsync(url);
                response = res.ToString().Contains("Content-Type: image");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Dispose();
            }

            return response;
        }
    }
}
