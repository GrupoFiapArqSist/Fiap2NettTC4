using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using Command.Infra.Data.Context;
using Command.Infra.Data.Repositories;
using Command.Service.Integration;
using Command.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Command.API.Mapper.MappingConfig;

namespace Command.Test.IntegrationTests
{
	[TestClass]
	public class CommandIntegrationTests
	{
		private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

		private static IConfigurationRoot? _configuration;

		private static IConfigurationRoot Configuration
		{
			get
			{
				if (_configuration == null)
				{
					IConfigurationBuilder builder = new ConfigurationBuilder()
							.SetBasePath(Directory.GetCurrentDirectory())
							.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
							.AddEnvironmentVariables();
					_configuration = builder.Build();
				}
				return _configuration;
			}
		}

		public CommandIntegrationTests()
		{
			_dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;
		}

		[TestMethod]
		public async Task GetOpenCommandsAsync_ShouldReturnCommand_WhenCommandExists()
		{
			using var context = new ApplicationDbContext(_dbContextOptions);

			var commandService = new CommandService(
				new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())),
				new CommandRepository(context),
				new OrderIntegration(Configuration),
				new NotificationContext()
			);

			await commandService.OpenCommand(1, 1);

			var result = await commandService.GetOpenCommands();

			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Single().Number);
		}
	}
}
