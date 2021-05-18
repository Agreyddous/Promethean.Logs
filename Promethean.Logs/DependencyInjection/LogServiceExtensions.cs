using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Promethean.Logs.Contracts;
using Promethean.Logs.Services;

namespace Promethean.Logs.DependencyInjection
{
	public static class LogServiceExtensions
	{
		public static IServiceCollection AddLogService(this IServiceCollection serviceCollection) => serviceCollection.AddLogService<LogService>();

		public static IServiceCollection AddLogService<TLogService>(this IServiceCollection serviceCollection, LogLevel minimumLogLevel = LogLevel.Error) where TLogService : class, ILogService, new() => serviceCollection.AddScoped<ILogService, TLogService>(services => new TLogService().SetMinimumLevel<TLogService>(minimumLogLevel).SetLogger<TLogService>(services.GetService<ILogger>()));
	}
}