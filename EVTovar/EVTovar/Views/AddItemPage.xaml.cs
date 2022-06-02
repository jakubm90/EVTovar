using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EVTovar.Models;
using EVTovar.ViewModels;

namespace EVTovar.Views
{
    public partial class AddItemPage : ContentPage
    {
        public Item Item { get; set; }

        AddItemViewModel _viewModel;

        public AddItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AddItemViewModel();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _viewModel.OnDisappearing();
        }
    }
}