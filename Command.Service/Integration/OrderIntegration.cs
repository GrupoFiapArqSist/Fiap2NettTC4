using Command.Domain.Dtos;
using Command.Domain.Interfaces.Integration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Command.Service.Integration
{
	public class OrderIntegration(IConfiguration configuration) : IOrderIntegration
	{
		private readonly IConfiguration _configuration = configuration;
		private readonly HttpClient _httpClient = new HttpClient();

		public async Task<IEnumerable<OrderDto>> GetOrdersByCommand(int commandId, string accessToken)
		{
			var baseUrl = _configuration.GetSection("OrderApi:UrlBase").Value;
			var url = @$"{baseUrl}get-orders-command?CommandId={commandId}";
			_httpClient.BaseAddress = new Uri(url);
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await _httpClient.GetAsync(url);
			var jsonString = await response.Content.ReadAsStringAsync();
			var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(jsonString);
			return orders;
		}
	}
}
