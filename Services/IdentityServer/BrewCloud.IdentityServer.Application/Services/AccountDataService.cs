using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using BrewCloud.Shared.Contracts;
using BrewCloud.Shared.Dtos;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BrewCloud.IdentityServer.Application.Models;

namespace BrewCloud.IdentityServer.Application.Services
{
    public class AccountDataService : IAccountDataService
    {
        private HttpClient _client;
        private readonly IHttpClientFactory _clientFactory;

        public AccountDataService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient("account");
        }

        public async System.Threading.Tasks.Task<Response<bool>> SendAsync(string path, ICreateSubscriptionRequestEvent model)
        {
            var client = _clientFactory.CreateClient("account");

            var result = new Response<bool>();

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, path);
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("VB-Tenant", model.RecId.ToString());
            requestMessage.Headers.Add("VB-Db", model.ConnectionString);
            requestMessage.Content = CreateJsonObjectContent(new { ConnectionString = model.ConnectionString });


            var responseMessage = await client.SendAsync(requestMessage);
            //  var responseMessage = client.PostAsync("account/account/ComplateSubscription", objectContent).Result;

            if (responseMessage.StatusCode == HttpStatusCode.OK || responseMessage.StatusCode == HttpStatusCode.NoContent)
            {

                string content = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Response<bool>>(content);
            }
            else
            {
                result.StatusCode = 500;
            }

            return result;
        }

        private StringContent CreateJsonObjectContent<T>(T model) where T : class
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(model, new Newtonsoft.Json.JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            var stringContent = new StringContent(json);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return stringContent;

        }

        public async Task<Response<bool>> SeedData(ICreateSubscriptionRequestEvent model)
        {
            var client = _clientFactory.CreateClient("account");
            var result = new Response<bool>();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "account/account/ComplateSubscription");
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("VB-Tenant", model.RecId.ToString());
            requestMessage.Headers.Add("VB-Db", model.ConnectionString);
            requestMessage.Content = CreateJsonObjectContent(model);
            var responseMessage = await client.SendAsync(requestMessage);
            //  var responseMessage = client.PostAsync("account/account/ComplateSubscription", objectContent).Result;
            if (responseMessage.StatusCode == HttpStatusCode.OK || responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Response<bool>>(content);
            }
            else
            {
                result.StatusCode = 500;
            }

            return result;
        }

        public async Task<Response<bool>> ImportExchangeRate(ExchangeRequestDto model)
        {
            var client = _clientFactory.CreateClient("account");

            var result = new Response<bool>();

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "integration/integration/CreateExchangeRate");
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("VB-Tenant", model.PropertyId.ToString());
            requestMessage.Headers.Add("VB-Db", model.ConnectionString);
            requestMessage.Content = CreateJsonObjectContent(model);


            var responseMessage = await client.SendAsync(requestMessage);
            //  var responseMessage = client.PostAsync("account/account/ComplateSubscription", objectContent).Result;

            if (responseMessage.StatusCode == HttpStatusCode.OK || responseMessage.StatusCode == HttpStatusCode.NoContent)
            {

                string content = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Response<bool>>(content);
            }
            else
            {
                result.StatusCode = 500;
            }

            return result;
        }

        public async Task<Response<bool>> CreateDatabse(ICreateSubscriptionRequestEvent model)
        {
            var result = new Response<bool>();

            var client = _clientFactory.CreateClient("account");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "account/account/CreateSubscription");
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("VB-Tenant", model.RecId.ToString());
            requestMessage.Headers.Add("VB-Db", model.ConnectionString);
            requestMessage.Content = CreateJsonObjectContent(model);

            var responseMessage = await client.SendAsync(requestMessage);
            //  var responseMessage = client.PostAsync("account/account/ComplateSubscription", objectContent).Result;

            if (responseMessage.StatusCode == HttpStatusCode.OK || responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Response<bool>>(content);
            }
            else
            {
                result.StatusCode = 500;
            }
            //var url = $"api/account/CreateSubscription";
            //_client = new HttpClient();
            //_client.BaseAddress = new Uri("http://localhost:5012/");

            ////var json = JsonConvert.SerializeObject(request);
            ////var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            //var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            //requestMessage.Headers.Add("Accept", "application/json");
            //requestMessage.Headers.Add("VB-Tenant", model.RecId.ToString());
            //requestMessage.Headers.Add("VB-Db", model.ConnectionString);
            //requestMessage.Content = CreateJsonObjectContent(model);

            //var httpResponseMessage = _client.SendAsync(requestMessage).Result;

            //var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
            //if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            //     result = JsonConvert.DeserializeObject<Response<bool>>(response);
            //return result;

            return result;
        }
    }
}
