using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TerekhovAtelierApp
{
    internal class ConnectionClass
    {
        public async Task<List<Client>> GetDataClient()
        {
            using (HttpClient client = new HttpClient())
            { 
                return JsonConvert.DeserializeObject<List<Client>>( await client.GetStringAsync("http://192.168.27.12:5207/api/Clients/"));
               
            }
        }
        public async Task<List<Employees>> GetDataEmployee()
        {
            using (HttpClient client = new HttpClient())
            {
                var stringData = await client.GetStringAsync("http://192.168.27.12:5207/api/Employees/");
                return JsonConvert.DeserializeObject<List<Employees>>(stringData);
            }
        }
        public async Task<List<Services>> GetDataService()
        {
            using (HttpClient client = new HttpClient())
            {
                var stringData = await client.GetStringAsync("http://192.168.27.12:5207/api/Services/");
                return JsonConvert.DeserializeObject<List<Services>>(stringData);
            }
        }
    }
}
