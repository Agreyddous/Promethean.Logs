using System;
using Microsoft.Extensions.Logging;

namespace Promethean.Logs.Services
{
	public interface ILogService : IDisposable
	{
		int Count { get; }

		void SetMinimumLevel(LogLevel minimumLevel);

		void Log<TInvoker>(string message, string method, object data, LogLevel level = LogLevel.Information);
		void Log<TInvoker>(Exception exception, object data, LogLevel level = LogLevel.Critical);
	}
}