using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Contracts;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Dtos.MailKit;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Mail;

namespace BrewCloud.Vet.Application.Services.Mails
{
    public class MailService : IMailService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<MailService> _logger;
        private readonly IIdentityRepository _identityRepository;
        private HttpClient _client;

        public MailService(IHttpClientFactory clientFactory, ILogger<MailService> logger, IIdentityRepository identityRepository)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _client = _clientFactory.CreateClient("mail");
            _identityRepository = identityRepository;
        }


        public async Task<Response<bool>> SendMail(string path, ISendMailRequestEvent request)
        {
            var client = _clientFactory.CreateClient("mail");

            var result = new Response<bool>();

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, path);
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

        private StringContent CreateJsonObjectContent<T>(T model) where T : class
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(model, new Newtonsoft.Json.JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            var stringContent = new StringContent(json);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return stringContent;

        }
    }
}
