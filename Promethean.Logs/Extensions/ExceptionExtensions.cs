using System;

namespace Promethean.Logs.Extensions
{
	public static class ExceptionExtensions
	{
		public static string GetMethod(this Exception exception) => exception?.TargetSite != null ? $"{exception.TargetSite.DeclaringType} - {exception.TargetSite.Name}" : string.Empty;

		public static string GetCompleteMessage(this Exception exception)
		{
			string message = exception.Message;

			if (exception.InnerException != null)
				message += $"\n{exception.GetCompleteMessage()}";

			return message;
		}
	}
}