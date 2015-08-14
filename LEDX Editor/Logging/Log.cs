using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Config;

namespace LEDX.Logging {

	public enum LogLevel {
		Debug = 0,
		Error = 1,
		Fatal = 2,
		Info = 3,
		Warning = 4
	}
	/// <summary>
	/// Write out messages using the logging provider.
	/// </summary>
	public static class Log {
		#region Members
		private static readonly ILog Logger = LogManager.GetLogger(typeof(Log));
		private static readonly Dictionary<LogLevel, Action<string>> Actions;
		#endregion

		/// <summary>
		/// Static instance of the log manager.
		/// </summary>
		static Log() {
			XmlConfigurator.Configure();
			Actions = new Dictionary<LogLevel, Action<string>> {
				{LogLevel.Debug, WriteDebug},
				{LogLevel.Error, WriteError},
				{LogLevel.Fatal, WriteFatal},
				{LogLevel.Info, WriteInfo},
				{LogLevel.Warning, WriteWarning}
			};
		}

		/// <summary>
		/// Get the <see cref="NotifyAppender"/> log.
		/// </summary>
		/// <returns>The instance of the <see cref="NotifyAppender"/> log, if configured.
		/// Null otherwise.</returns>
		public static NotifyAppender Appender {
			get { return LogManager.GetCurrentLoggers().SelectMany(log => log.Logger.Repository.GetAppenders()).OfType<NotifyAppender>().Select(appender => appender).FirstOrDefault(); }
		}

		/// <summary>
		/// Write the message to the appropriate log based on the relevant log level.
		/// </summary>
		/// <param name="level">The log level to be used.</param>
		/// <param name="message">The message to be written.</param>
		/// <exception cref="ArgumentNullException">Thrown if the message is empty.</exception>
		public static void Write(string message, LogLevel level = LogLevel.Info) {
			if (!string.IsNullOrEmpty(message)) {
				if (level > LogLevel.Warning || level < LogLevel.Debug)
					throw new ArgumentOutOfRangeException("level");
				// Now call the appropriate log level message.
				Actions[level](message);
			}
		}

		#region Action methods
		private static void WriteDebug(string message) {
			if (Logger.IsDebugEnabled)
				Logger.Debug(message);
		}

		private static void WriteError(string message) {
			if (Logger.IsErrorEnabled)
				Logger.Error(message);
		}

		private static void WriteFatal(string message) {
			if (Logger.IsFatalEnabled)
				Logger.Fatal(message);
		}

		private static void WriteInfo(string message) {
			if (Logger.IsInfoEnabled)
				Logger.Info(message);
		}

		private static void WriteWarning(string message) {
			if (Logger.IsWarnEnabled)
				Logger.Warn(message);
		}
		#endregion
	}
}
