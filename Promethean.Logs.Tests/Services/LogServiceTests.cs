using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promethean.Logs.Services;

namespace Promethean.Logs.Tests.Services
{
	[TestClass]
	public class LogServiceTests
	{
		private ILogService _logService;

		[TestInitialize]
		public void Setup() => _logService = new LogService(LoggerFactory.Create(builder => { }).CreateLogger<LogService>());

		[TestMethod("Log something with a lower level than the minimum configured, should not add the log message")]
		public void LogLowerLevelThanMinimum()
		{
			_logService.SetMinimumLevel(LogLevel.Critical);

			_logService.Log<LogServiceTests>(Faker.Lorem.Sentence(), Faker.Lorem.Sentence(), new object(), LogLevel.Information);

			Assert.AreEqual(0, _logService.Count);
		}

		[TestMethod("Log something with same level than the minimum configured, should add the log message")]
		public void LogSameLevelThanMinimum()
		{
			_logService.SetMinimumLevel(LogLevel.Information);

			_logService.Log<LogServiceTests>(Faker.Lorem.Sentence(), Faker.Lorem.Sentence(), new object(), LogLevel.Information);

			Assert.AreEqual(1, _logService.Count);
		}

		[TestMethod("Log something with higher level than the minimum configured, should add the log message")]
		public void LogHigherLevelThanMinimum()
		{
			_logService.SetMinimumLevel(LogLevel.Information);

			_logService.Log<LogServiceTests>(Faker.Lorem.Sentence(), Faker.Lorem.Sentence(), new object(), LogLevel.Critical);

			Assert.AreEqual(1, _logService.Count);
		}

		[TestMethod("Log a created exception")]
		public void LogCreatedException()
		{
			_logService.Log<LogServiceTests>(new Exception(Faker.Lorem.Sentence()), new object());

			Assert.AreEqual(1, _logService.Count);
		}

		[TestMethod("Log a thrown exception")]
		public void LogThrownException()
		{
			try
			{
				Assert.IsTrue(false);
			}
			catch (Exception exception)
			{
				_logService.Log<LogServiceTests>(exception, new object());
			}

			Assert.AreEqual(1, _logService.Count);
		}
	}
}