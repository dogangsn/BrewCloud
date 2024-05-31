using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Contracts;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.Vet.Application.Services.Message.Sms
{
    public class SmsService : ISmsService
    {

        private readonly IHttpClientFactory _clientFactory; 
        private readonly IIdentityRepository _identityRepository;
        private HttpClient _client;

        public SmsService(IHttpClientFactory clientFactory, IIdentityRepository identityRepository, HttpClient client)
        {
            _clientFactory = clientFactory;
            _identityRepository = identityRepository;
            _client = client;
        }

        public async Task<Response<bool>> SendSms(ISendMessageRequest request)
        {
            var client = _clientFactory.CreateClient("mail");
            var result = new Response<bool>();

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Integrations/sms/SendSmsTransactions");
            requestMessage.Headers.Add("Authorization", $"Bearer {_identityRepository.Token}");

            var json = JsonConvert.SerializeObject(request);
            var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            requestMessage.Content = content;
            var responseMessage = await client.SendAsync(requestMessage);
            if (responseMessage.StatusCode == HttpStatusCode.OK || responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                //string content = await responseMessage.Content.ReadAsStringAsync();
                //result = JsonConvert.DeserializeObject<Response<bool>>(content);
            }
            else
            {
                result.StatusCode = 500;
            }

            return result;
        }
    }
}
