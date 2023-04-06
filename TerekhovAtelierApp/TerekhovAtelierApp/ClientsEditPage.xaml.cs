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
    public partial class ClientsEditPage : ContentPage
    {
        Client _client;

        public ClientsEditPage(Client client)
        {
            InitializeComponent();
            _client = client;
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(enId.Text))
                error += "Не ввели код!\n";
            if (string.IsNullOrWhiteSpace(enName.Text))
                error += "Не ввели имя!\n";
            if (string.IsNullOrWhiteSpace(enSurname.Text))
                error += "Не ввели фамиилю!\n";
            if (string.IsNullOrWhiteSpace(enPatronymic.Text))
                error += "Не ввели отчество\n";
            if (string.IsNullOrWhiteSpace(enPhNumber.Text))
                error += "Не ввели номер телефона!\n";
            
            if (!string.IsNullOrEmpty(error))
            {
                await DisplayAlert("Ошибка!", "Произошла ошибка! Причины могут быть в следующем\n\n" + error, "Ок");
                return;
            }
            if (_client != null)
            {
                _client.idClient = Convert.ToInt32(enId.Text);
                _client.name = Convert.ToString(enName.Text);
                _client.surname = Convert.ToString(enSurname.Text);
                _client.patronymic = Convert.ToString(enPatronymic.Text);
                _client.phoneNumber = Convert.ToString(enPhNumber.Text);
                if (await PutService (_client.idClient.ToString(), _client))
                    Toast.MakeText(Android.App.Application.Context, "Изменения внесены успешно!", ToastLength.Short).Show();
                else
                {
                    await DisplayAlert("Ошибка!", "Проверьте соединение", "Ок");
                    return;
                }

            }
            else
            {
                _client = new Client()
                {
                    idClient= Convert.ToInt32(enId.Text),
                    name = enName.Text,
                    surname = enSurname.Text,
                    patronymic=enPatronymic.Text,
                    phoneNumber = enPhNumber.Text,
                };
                if (await PostService(_client)) Toast.MakeText(Android.App.Application.Context, "Добавление успешно!", ToastLength.Short).Show();
                else
                {
                    _client = null;
                    await DisplayAlert("error", "", "ok");
                    return;
                }
            }

            Navigation.RemovePage(this);

        }
    
        private async Task<bool> PostService(Client cl)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.43:5207/api/Clients/");
                string context = JsonConvert.SerializeObject(cl);
                request.Content = new StringContent(context, Encoding.UTF8, "application/json");
                var response = await httpclient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
        private async Task<bool> PutService(string c, Client cl)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Put, "http://192.168.1.43:5207/api/Clients/" + c);
                request.Content = new StringContent(JsonConvert.SerializeObject(cl), Encoding.UTF8, "application/json");
                var response = await httpclient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (_client != null)
            {

                enId.Text = _client.idClient.ToString();
                enName.Text = _client.name.ToString();
                enSurname.Text = _client.surname.ToString();
                enPatronymic.Text = _client.patronymic.ToString();
                enPhNumber.Text = _client.phoneNumber;
            }
        }
    }
}