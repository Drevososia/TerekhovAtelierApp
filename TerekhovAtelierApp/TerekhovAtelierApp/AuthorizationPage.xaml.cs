using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TerekhovAtelierApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }
        private async Task<bool> Authorization(User userAuth)
        {
            using(HttpClient client = new HttpClient())
            {
                var auth = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.43:5207/api/Users");
                auth.Content = new StringContent(JsonConvert.SerializeObject(userAuth), Encoding.UTF8, "application/json");
                var responce = await client.SendAsync(auth);
                return responce.IsSuccessStatusCode;
            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(EntryLogin.Text))
                error += "Логин\n";
            if (string.IsNullOrWhiteSpace(EntryPassword.Text))
                error += "Пароль\n";
            if (error != "")
            {
                await DisplayAlert("Предупреждение", "Введите данные:\n" + error, "ОК");
                return;
            }
            var Us = new User()
            {
                login = EntryLogin.Text,
                password = EntryPassword.Text,
            };

            if (await Authorization(Us))
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Ошибка", "Такого пользователя не существует!\nПройдите регистрацию, либо проверьте введенные данные", "OK");
            }
        }
    }
}