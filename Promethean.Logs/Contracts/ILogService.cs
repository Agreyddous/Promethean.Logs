using System;
using Microsoft.Extensions.Logging;

namespace Promethean.Logs.Contracts
{
	public interface ILogService : IDisposable
	{
		TLogService SetMinimumLevel<TLogService>(LogLevel minimumLevel) where TLogService : class, ILogService;
		TLogService SetLogger<TLogService>(ILogger logger) where TLogService : class, ILogService;

		void Log<TInvoker>(string message, string method, object data, LogLevel level = LogLevel.Information);
		void Log<TInvoker>(Exception exception, object data, LogLevel level = LogLevel.Critical);
	}
}