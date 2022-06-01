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

        public AddItemPage()
        {
            InitializeComponent();
            BindingContext = new AddItemViewModel();
        }
    }
}