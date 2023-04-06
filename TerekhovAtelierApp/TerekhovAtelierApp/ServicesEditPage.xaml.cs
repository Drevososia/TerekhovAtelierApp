using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TerekhovAtelierApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServicesEditPage : ContentPage
    {
        Services _services;
        public ServicesEditPage(Services services)
        {
            InitializeComponent();
            _services = services;

        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(enId.Text))
                error += "Не ввели код!\n";
            if (string.IsNullOrWhiteSpace(enName.Text))
                error += "Не ввели название!\n";
            if (string.IsNullOrWhiteSpace(enDesc.Text))
                error += "Не ввели описание!\n";
            if (string.IsNullOrWhiteSpace(enPrice.Text))
                error += "Не ввели цену\n";
            if (string.IsNullOrWhiteSpace(enIdType.Text))
                error += "Не ввели код вида инструмента\n";

            if (!string.IsNullOrEmpty(error))
            {
                await DisplayAlert("Ошибка!", "Произошла ошибка! Причины могут быть в следующем\n\n" + error, "Ок");
                return;
            }
            if (_services != null)
            {
                _services.idService = Convert.ToInt32(enId.Text);
                _services.nameService = Convert.ToString(enName.Text);
                _services.serviceDescription = Convert.ToString(enDesc.Text);
                _services.price = Convert.ToInt32(enPrice.Text);
                _services.idType = Convert.ToInt32(enIdType.Text);
                if (await PutService(_services.idService.ToString(), _services))
                    Toast.MakeText(Android.App.Application.Context, "Изменения внесены успешно!", ToastLength.Short).Show();
                else
                {
                    await DisplayAlert("Ошибка!", "Проверьте соединение", "Ок");
                    return;
                }

            }
            else
            {
                _services = new Services()
                {
                    idService = Convert.ToInt32(enId.Text),
                    nameService = enName.Text,
                    serviceDescription = enDesc.Text,
                    price = Convert.ToInt32(enPrice.Text),
                    idType = Convert.ToInt32(enIdType.Text),
                };
                if (await PostService(_services)) Toast.MakeText(Android.App.Application.Context, "Добавление успешно!", ToastLength.Short).Show();
                else
                {
                    _services = null;
                    await DisplayAlert("error", "", "ok");
                    return;
                }
            }

            Navigation.RemovePage(this);

        }
        private async Task<bool> PostService(Services ser)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.43:5207/api/Services/");
                string context = JsonConvert.SerializeObject(ser);
                request.Content = new StringContent(context, Encoding.UTF8, "application/json");
                var response = await httpclient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
        private async Task<bool> PutService(string c, Services ser)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Put, "http://192.168.1.43:5207/api/Services/" + c);
                request.Content = new StringContent(JsonConvert.SerializeObject(ser), Encoding.UTF8, "application/json");
                var response = await httpclient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (_services != null)
            {
                enId.Text = _services.idService.ToString();
                enName.Text = _services.nameService.ToString();
                enDesc.Text = _services.serviceDescription.ToString();
                enPrice.Text = _services.price.ToString();
                enIdType.Text = _services.idType.ToString();
            }
        }
    }
}