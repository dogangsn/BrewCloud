using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using VetSystems.Shared.Contracts;
using VetSystems.Shared.Dtos;
using System.Net.Http.Headers;

namespace VetSystems.IdentityServer.Application.Services
{
    public class AccountDataService : IAccountDataService
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountDataService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
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
    }
}
