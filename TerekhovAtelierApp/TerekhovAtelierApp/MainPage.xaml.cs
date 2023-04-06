using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TerekhovAtelierApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnCLient_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ClientsPage());
        }

        private void BtnEmployee_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EmployeesPage());
        }

        private void BtnOrder_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OrdersPage());
        }

        private void BtnService_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ServicesPage());
        }
    }
}
