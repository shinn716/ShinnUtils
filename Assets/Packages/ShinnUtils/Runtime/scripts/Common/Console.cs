// Date: 20210115
// Modified: Shinn 
// Ver: 0.1.0
// (1) Add toggle function
// (2) Add export function

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Shinn
{
    /// <summary>
    /// A console to display Unity's debug logs in-game.
    /// </summary>
    public class Console : MonoBehaviour
    {
        struct Log
        {
            public string message;
            public string stackTrace;
            public LogType type;
        }

        /// <summary>
        /// The hotkey to show and hide the console window.
        /// </summary>
        public KeyCode enableToggle = KeyCode.BackQuote;

        List<Log> logs = new List<Log>();
        Vector2 scrollPosition;
        bool collapse;

        public Rect SizeOffset { get; set; }
        public bool ShowConsole { get; set; }

        // Visual elements:
        static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>()
        {
            { LogType.Assert, Color.white },
            { LogType.Error, Color.red },
            { LogType.Exception, Color.red },
            { LogType.Log, Color.white },
            { LogType.Warning, Color.yellow },
        };

        const int margin = 20;

        Rect windowRect;
        Rect titleBarRect = new Rect(0, 0, 10000, 20);
        GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
        GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");
        GUIContent exportLabel = new GUIContent("Export", "export messages.");

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void Update()
        {
            if (Input.GetKeyDown(enableToggle))
                ShowConsole = !ShowConsole;
        }

        void OnGUI()
        {
            if (!ShowConsole)
                return;

            Rect windowRect = new Rect(margin + SizeOffset.x, margin + SizeOffset.y, Screen.width - (margin * 2) + SizeOffset.width, Screen.height - (margin * 2) + SizeOffset.height);
            windowRect = GUILayout.Window(123456, windowRect, ConsoleWindow, "Console");
        }

        /// <summary>
        /// A window that displayss the recorded logs.
        /// </summary>
        /// <param name="windowID">Window ID.</param>
        void ConsoleWindow(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            // Iterate through the recorded logs.
            for (int i = 0; i < logs.Count; i++)
            {
                var log = logs[i];

                // Combine identical messages if collapse option is chosen.
                if (collapse)
                {
                    var messageSameAsPrevious = i > 0 && log.message == logs[i - 1].message;

                    if (messageSameAsPrevious)
                    {
                        continue;
                    }
                }

                GUI.contentColor = logTypeColors[log.type];
                GUILayout.Label(log.message);
            }

            GUILayout.EndScrollView();

            GUI.contentColor = Color.white;

            GUILayout.BeginHorizontal();

            if (GUILayout.Button(clearLabel))
            {
                logs.Clear();
            }

            // Shinn
            if (GUILayout.Button(exportLabel, GUILayout.Width(100)))
            {
                ExportMessages();
            }

            collapse = GUILayout.Toggle(collapse, collapseLabel, GUILayout.ExpandWidth(false));

            GUILayout.EndHorizontal();

            // Allow the window to be dragged by its title bar.
            GUI.DragWindow(titleBarRect);
        }

        /// <summary>
        /// Records a log from the log callback.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="stackTrace">Trace of where the message came from.</param>
        /// <param name="type">Type of message (error, exception, warning, assert).</param>
        void HandleLog(string message, string stackTrace, LogType type)
        {
            logs.Add(new Log()
            {
                message = message,
                stackTrace = stackTrace,
                type = type,
            });
        }

        /// <summary>
        /// Export messages
        /// </summary>
        void ExportMessages()
        {
            string full = Path.Combine(Application.streamingAssetsPath, $"log_{System.DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")}.txt");

            Debug.Log($"[Save to] {full}");

            using (StreamWriter outputFile = new StreamWriter(full, true))
            {
                StringBuilder sb = new StringBuilder();

                foreach (var i in logs)
                    sb.AppendLine($"[{i.type}]  {i.message}  {i.stackTrace}");

                outputFile.WriteLine(sb.ToString());
                outputFile.Close();
            }
        }
    }
}