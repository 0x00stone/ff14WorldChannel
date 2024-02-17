using Advanced_Combat_Tracker;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;

namespace FF14Chat.Common {
	public static class Log {
		private static string logFilePath;
		private static readonly ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
		private static readonly AutoResetEvent logSignal = new AutoResetEvent(false);
		private static bool isRunning = true;
		private static readonly Thread logThread;

		static Log() {
			logThread = new Thread(WriteLog) { IsBackground = true };
			logThread.Start();
		}

		public static void init() {
			logFilePath = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "ff14Chat\\Log\\" + DateTime.Now.ToString("yyyyMMdd") + ".log");

			string directoryPath = Path.GetDirectoryName(logFilePath);

			if(!Directory.Exists(directoryPath)) {
				Directory.CreateDirectory(directoryPath);
			}

			if(!File.Exists(logFilePath)) {
				FileStream fileStream = File.Create(logFilePath);
				fileStream.Close();
			}
		}


		public static void info(string message) {
			EnqueueLog($"INFO: {message}");
		}

		public static void error(string message) {
			EnqueueLog($"ERROR: {message}");
		}

		private static void EnqueueLog(string message) {
			logQueue.Enqueue($"{DateTime.Now}: {message}{Environment.NewLine}");
			logSignal.Set();
		}

		private static void WriteLog() {
			while(isRunning) {
				logSignal.WaitOne();
				while(logQueue.TryDequeue(out string logEntry)) {
					File.AppendAllText(logFilePath, logEntry);
					Debug.WriteLine(logEntry);
				}
			}
		}

		public static void Shutdown() {
			isRunning = false;
			logSignal.Set();
			logThread.Join();
		}
	}
}
