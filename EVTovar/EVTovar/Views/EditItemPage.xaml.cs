using EVTovar.Models;
using EVTovar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EVTovar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditItemPage : ContentPage
    {
        public Item Item { get; set; }

        EditItemViewModel _viewModel;

        public EditItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditItemViewModel();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _viewModel.OnDisappearing();
        }
    }
}