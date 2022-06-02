using EVTovar.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EVTovar
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(AddItemPage), typeof(AddItemPage));
            Routing.RegisterRoute(nameof(EditItemPage), typeof(EditItemPage));
        }

    }
}
