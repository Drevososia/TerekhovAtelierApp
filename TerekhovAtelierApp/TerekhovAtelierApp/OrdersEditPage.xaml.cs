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
    public partial class OrdersEditPage : ContentPage
    {
        Order _order;
        public OrdersEditPage(Order order)
        {
            InitializeComponent();
            _order = order;
    
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            string error = "";
            if (string.IsNullOrWhiteSpace(enId.Text))
                error += "Не ввели код!\n";
            if (string.IsNullOrWhiteSpace(enIdCl.Text))
                error += "Не ввели код клиента!\n";
            if (string.IsNullOrWhiteSpace(enIdEmp.Text))
                error += "Не ввели код работника!\n";
            if (string.IsNullOrWhiteSpace(enDate.Text))
                error += "Не ввели дату\n";
            if (string.IsNullOrWhiteSpace(enIdStatus.Text))
                error += "Не ввели код статуса!\n";
            if (string.IsNullOrWhiteSpace(enDesc.Text))
                error += "Не ввели описание!\n";

            if (_order != null)
            {
                _order.idOrder = Convert.ToInt32(enId.Text);
                _order.idClient = Convert.ToInt32(enIdCl.Text);
                _order.idEmployee = Convert.ToInt32(enIdEmp.Text);
                _order.idServiceNavigation.nameService = Convert.ToString(enIdServ.Text);
                _order.orderDate = Convert.ToDateTime(enDate.Text);
                _order.idOrderStatus = Convert.ToInt32(enIdStatus.Text);
                _order.orderDescription = Convert.ToString(enDate.Text);
                if (await PutService(_order.idOrder.ToString(), _order))
                    Toast.MakeText(Android.App.Application.Context, "Изменения внесены успешно!", ToastLength.Short).Show();
                else
                {
                    await DisplayAlert("Ошибка!", "Проверьте соединение", "Ок");
                    return;
                }

            }
            else
            {
                _order = new Order()
                {
                    idOrder = Convert.ToInt32(enId.Text),
                    idClient = Convert.ToInt32(enIdCl.Text),
                    idEmployee = Convert.ToInt32(enIdEmp.Text),
                    idService = Convert.ToInt32(enIdServ.Text),
                    orderDate = Convert.ToDateTime(enDate.Text),
                    idOrderStatus = Convert.ToInt32(enIdStatus.Text),
                    orderDescription = enDesc.Text,
                };
                if (await PostService(_order)) Toast.MakeText(Android.App.Application.Context, "Добавление успешно!", ToastLength.Short).Show();
                else
                {
                    _order = null;
                    await DisplayAlert("error", "", "ok");
                    return;
                }
            }
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            

            Navigation.RemovePage(this);
        }
        private async Task<bool> PostService(Order or)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.43:5207/api/Orders/");
                string context = JsonConvert.SerializeObject(or);
                request.Content = new StringContent(context, Encoding.UTF8, "application/json");
                var response = await httpclient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
        private async Task<bool> PutService(string c, Order or)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Put, "http://192.168.1.43:5207/api/Orders/" + c);
                request.Content = new StringContent(JsonConvert.SerializeObject(or), Encoding.UTF8, "application/json");
                var response = await httpclient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
    }
}