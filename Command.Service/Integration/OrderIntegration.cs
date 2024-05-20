using Command.Domain.Dtos;
using Command.Domain.Interfaces.Integration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Command.Service.Integration
{
	public class OrderIntegration(IConfiguration configuration) : IOrderIntegration
	{
		private readonly HttpClient _httpClient = new();
		private readonly string _baseUrl = configuration.GetSection("OrderApi:UrlBase").Value ?? string.Empty;

		public async Task<IEnumerable<OrderDto>> GetOrdersByCommand(int commandId, string accessToken)
		{
			var url = @$"{_baseUrl}get-orders-command?CommandId={commandId}";
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await _httpClient.GetAsync(url);
			var jsonString = await response.Content.ReadAsStringAsync();
			var command = JsonConvert.DeserializeObject<CommandDto>(jsonString);
			return command.Orders;
		}
	}
}
