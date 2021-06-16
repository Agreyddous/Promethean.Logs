using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Promethean.Logs.Entities;
using Promethean.Logs.Extensions;
using Promethean.Logs.Services.Contracts;

namespace Promethean.Logs.Services
{
	public class LogService : ILogService
	{
		protected readonly Guid ScopeId;
		private readonly IList<Log> _logs;

		private ILogger<LogService> _logger;

		public static LogLevel MinimumLevel { get; set; }

		public LogService(ILogger<LogService> logger)
		{
			ScopeId = Guid.NewGuid();
			_logs = new List<Log>();

			_logger = logger;
		}

		public int Count => _logs.Count;

		public void SetMinimumLevel(LogLevel minimumLevel) => MinimumLevel = minimumLevel;

		public void Log<TInvoker>(string message, string method, object data, LogLevel level = LogLevel.Information) => AddLog(new Log(ScopeId, message, level, typeof(TInvoker).FullName, method, JsonSerializer.Serialize(data)));

		public void Log<TInvoker>(Exception exception, object data, LogLevel level = LogLevel.Critical) => AddLog(new Log(ScopeId, exception.GetCompleteMessage(), level, typeof(TInvoker).FullName, exception.GetMethod(), _handleExceptionData(exception, data)));

		public void Dispose() => RegisterLogs(_logs, _logger);

		private string _handleExceptionData(Exception exception, object data) => JsonSerializer.Serialize(new
		{
			StackTrace = exception.StackTrace,
			Data = data
		});

		protected virtual void AddLog(Log log)
		{
			if (log.Level >= MinimumLevel && log.Level != LogLevel.None)
				_logs.Add(log);
		}

		protected virtual void RegisterLogs(IList<Log> logs, ILogger logger)
		{
			foreach (Log log in logs)
				logger.Log(log.Level, log.Message, log);
		}
	}
}