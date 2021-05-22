using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Promethean.Logs.Services;

namespace Promethean.Logs.DependencyInjection
{
	public static class LogServiceExtensions
	{
		public static IServiceCollection AddLogService(this IServiceCollection serviceCollection, LogLevel minimumLogLevel = LogLevel.Error) => serviceCollection.AddLogService<LogService>(minimumLogLevel);

		public static IServiceCollection AddLogService<TLogService>(this IServiceCollection serviceCollection, LogLevel minimumLogLevel = LogLevel.Error) where TLogService : class, ILogService
		{
			LogService.MinimumLevel = minimumLogLevel;
			return serviceCollection.AddScoped<ILogService, TLogService>();
		}
	}
}