using System;
using Microsoft.Extensions.Logging;

namespace Promethean.Logs.Entities
{
	public class Log
	{
		public Log(Guid scopeId, string message, LogLevel level, string className, string method, string data)
		{
			Application = System.AppDomain.CurrentDomain.FriendlyName;
			Time = DateTime.UtcNow;

			ScopeId = scopeId;
			Message = message;
			Level = level;
			ClassName = className;
			Method = method;
			Data = data;
		}

		public string Application { get; private set; }
		public Guid ScopeId { get; private set; }
		public string Message { get; private set; }
		public LogLevel Level { get; private set; }
		public string ClassName { get; private set; }
		public string Method { get; private set; }
		public string Data { get; private set; }
		public DateTime Time { get; private set; }
	}
}