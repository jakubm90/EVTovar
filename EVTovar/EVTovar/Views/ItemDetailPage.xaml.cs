using System.ComponentModel;
using Xamarin.Forms;
using EVTovar.ViewModels;
using Xamarin.Forms.Xaml;

namespace EVTovar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}