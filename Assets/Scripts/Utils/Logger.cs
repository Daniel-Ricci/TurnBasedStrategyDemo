using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Utils
{
    public static class Logger
    {
        public const bool DebugMode = true;

        /// <summary>
        /// Logs a message if debugMode is enabled.
        /// Includes class, method and line number, with color-coded output.
        /// </summary>
        public static void Log(string message, LogType logType = LogType.Log)
        {
            if (!DebugMode) return;

            // Get stack info
            StackFrame frame = new StackFrame(1, true);
            var method = frame.GetMethod();

            string className = method.DeclaringType != null ? method.DeclaringType.Name : "UnknownClass";
            string methodName = method.Name;
            int lineNumber = frame.GetFileLineNumber();

            string formattedBase =
                $"{className}.{methodName} (Line {lineNumber}) - {logType.ToString().ToUpper()}: {message}";

            string coloredText = GetColoredMessage(formattedBase, logType);

            // Log using correct Unity log function
            switch (logType)
            {
                case LogType.Log:
                    Debug.Log(coloredText);
                    break;

                case LogType.Warning:
                    Debug.LogWarning(coloredText);
                    break;

                case LogType.Assert:
                    Debug.LogAssertion(coloredText);
                    break;

                case LogType.Error:
                case LogType.Exception:
                    Debug.LogError(coloredText);
                    break;
            }
        }

        private static string GetColoredMessage(string message, LogType type)
        {
            switch (type)
            {
                case LogType.Log:
                    return $"<color=#FFFFFF>{message}</color>"; // white

                case LogType.Warning:
                    return $"<color=#E6C700>{message}</color>"; // yellow

                case LogType.Assert:
                    return $"<color=#6FD3FF>{message}</color>"; // light blue

                case LogType.Error:
                case LogType.Exception:
                    return $"<color=#FF5555>{message}</color>"; // red
            }

            return message;
        }
    }
}
