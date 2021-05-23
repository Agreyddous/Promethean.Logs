using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promethean.Logs.DependencyInjection;
using Promethean.Logs.Services;

namespace Promethean.Logs.Tests.DependencyInjection
{
	[TestClass]
	public class LogServiceExtensionsTests
	{
		private readonly ServiceProvider _serviceProvider;

		private IServiceScope _serviceScope;

		public LogServiceExtensionsTests()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddLogService();
			services.AddLogging(configure => configure.AddConsole());

			_serviceProvider = services.BuildServiceProvider();
		}

		[TestInitialize]
		public void Setup() => _serviceScope = _serviceProvider.CreateScope();

		[TestMethod("Log a Critical message, should add the log message")]
		public void HandleValidRegisteredCommandAndResult()
		{
			ILogService logService = _getService<ILogService>();

			logService.Log<LogServiceExtensionsTests>(Faker.Lorem.Sentence(), nameof(HandleValidRegisteredCommandAndResult), new { }, LogLevel.Critical);

			Assert.AreEqual(1, logService.Count);
		}

		[TestCleanup]
		public void Cleanup() => _serviceProvider.Dispose();

		private TService _getService<TService>() => _serviceScope.ServiceProvider.GetService<TService>();
	}
}