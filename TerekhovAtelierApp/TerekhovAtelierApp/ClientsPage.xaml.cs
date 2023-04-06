﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace TerekhovAtelierApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientsPage : ContentPage
    {
        public ClientsPage()
        {
            InitializeComponent();
        }

        public async Task<List<Client>> GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                var stringData = await client.GetStringAsync("http://192.168.1.43:5207/api/Clients/");
               if (stringData == null)
                {
                    await DisplayAlert("error!", "no respond", "ok");
                }
               return JsonConvert.DeserializeObject<List<Client>>(stringData);
            }
        }
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            LVData.ItemsSource = await GetData();
        }
        private async Task<bool> DeleteData(int a)
        {
            using (HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "http://192.168.1.43:5207/api/Clients/" + a);
                var response = await client.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
        private async void BtnEd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientsEditPage((sender as Button).BindingContext as Client));
            LVData.ItemsSource = await GetData();
        }

        private async void BtnDel_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Удалить?", "", "Да", "Нет"))
            {
                if (await DeleteData(((sender as Button).BindingContext as Client).idClient)) Android.Widget.Toast.MakeText(Android.App.Application.Context, "Удаление успешно!", Android.Widget.ToastLength.Short).Show();
                else
                    await DisplayAlert("error", "check connection", "ok");
                LVData.ItemsSource = await GetData();
            }
        }
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientsEditPage(null));
        }
    }
}