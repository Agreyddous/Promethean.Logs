using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Promethean.Logs.Contracts;
using Promethean.Logs.Entities;
using Promethean.Logs.Extensions;

namespace Promethean.Logs.Services
{
	public class LogService : ILogService
	{
		protected readonly Guid ScopeId;
		private readonly IList<Log> _logs;

		protected LogLevel MinimumLevel;
		private ILogger _logger;

		public LogService()
		{
			ScopeId = Guid.NewGuid();
			_logs = new List<Log>();
			MinimumLevel = LogLevel.Error;
		}

		public int Count => _logs.Count;

		public TLogService SetMinimumLevel<TLogService>(LogLevel minimumLevel) where TLogService : class, ILogService
		{
			MinimumLevel = minimumLevel;

			return this as TLogService;
		}

		public TLogService SetLogger<TLogService>(ILogger logger) where TLogService : class, ILogService
		{
			_logger = logger;

			return this as TLogService;
		}

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