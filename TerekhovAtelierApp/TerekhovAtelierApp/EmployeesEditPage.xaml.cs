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
    public partial class EmployeesEditPage : ContentPage
    {
        Employees _employee;
        public EmployeesEditPage(Employees employee)
        {
            InitializeComponent();
            _employee = employee;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (_employee != null)
            {
                enId.Text = _employee.idEmployee.ToString();
                enName.Text = _employee.name.ToString();
                enSurname.Text = _employee.surname.ToString();
                enPatronymic.Text = _employee.patronymic.ToString();
                enIdEmpType.Text = _employee.idEmpType.ToString();
            }
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
            if (string.IsNullOrWhiteSpace(enIdEmpType.Text))
                error += "Не ввели код вида сотрудника\n";

            if (!string.IsNullOrEmpty(error))
            {
                await DisplayAlert("Ошибка!", "Произошла ошибка! Причины могут быть в следующем\n\n" + error, "Ок");
                return;
            }
            if (_employee != null)
            {
                _employee.idEmployee = Convert.ToInt32(enId.Text);
                _employee.name = Convert.ToString(enName.Text);
                _employee.surname = Convert.ToString(enSurname.Text);
                _employee.patronymic = Convert.ToString(enPatronymic.Text);
                _employee.idEmpType = Convert.ToInt32(enIdEmpType.Text);
                if (await PutService(_employee.idEmployee.ToString(), _employee))
                    Toast.MakeText(Android.App.Application.Context, "Изменения внесены успешно!", ToastLength.Short).Show();
                else
                {
                    await DisplayAlert("Ошибка!", "Проверьте соединение", "Ок");
                    return;
                }

            }
            else
            {
                _employee = new Employees()
                {
                    idEmployee = Convert.ToInt32(enId.Text),
                    name = enName.Text,
                    surname = enSurname.Text,
                    patronymic = enPatronymic.Text,
                    idEmpType = Convert.ToInt32(enIdEmpType.Text),
                };
                if (await PostService(_employee)) Toast.MakeText(Android.App.Application.Context, "Добавление успешно!", ToastLength.Short).Show();
                else
                {
                    _employee = null;
                    await DisplayAlert("error", "", "ok");
                    return;
                }
            }

            Navigation.RemovePage(this);

        }
        private async Task<bool> PostService(Employees em)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.43:5207/api/Employees/");
                string context = JsonConvert.SerializeObject(em);
                request.Content = new StringContent(context, Encoding.UTF8, "application/json");
                var response = await httpclient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
        private async Task<bool> PutService(string c, Employees em)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Put, "http://192.168.1.43:5207/api/Employees/" + c);
                request.Content = new StringContent(JsonConvert.SerializeObject(em), Encoding.UTF8, "application/json");
                var response = await httpclient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
    }
}