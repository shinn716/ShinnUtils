// Date: 20200916
// Modified: Shinn 

using System.Collections.Generic;
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
        public KeyCode toggleKey = KeyCode.BackQuote;

        public bool Enable = true;
        public bool CustomSize = false;
        public Rect GUIRt = new Rect(0, 50, 300, 300);
        public bool Show { get; set; }

        RectTransform objRect;
        List<Log> logs = new List<Log>();
        Vector2 scrollPosition;
        bool collapse;

        // Visual elements:
        static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>()
    {
        { LogType.Assert, Color.white },
        { LogType.Error, Color.red },
        { LogType.Exception, Color.red },
        { LogType.Log, Color.white },
        { LogType.Warning, Color.yellow },
    };

        const int margin = 80;

        Rect windowRect;
        Rect titleBarRect = new Rect(0, 0, 10000, 20);
        GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
        GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");

        private void Start()
        {
            Show = Enable;

            objRect = GetComponent<RectTransform>();
            if (CustomSize)
            {
                GUIRt = new Rect(GUIRt.x, GUIRt.y, GUIRt.width, GUIRt.height);
            }
            else
            {
                GUIRt = new Rect(0, 0, Screen.width, Screen.height);
            }
        }

        void OnEnable()
        {
#pragma warning disable CS0618 // 類型或成員已經過時
            Application.RegisterLogCallback(HandleLog);
#pragma warning restore CS0618 // 類型或成員已經過時
        }

        void OnDisable()
        {
#pragma warning disable CS0618 // 類型或成員已經過時
            Application.RegisterLogCallback(null);
#pragma warning restore CS0618 // 類型或成員已經過時
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                Show = !Show;
                Enable = Show;
            }
        }

        void OnGUI()
        {
            if (!Show || !Enable)
                return;

            Rect windowRect = new Rect(GUIRt.x, GUIRt.y, GUIRt.width, GUIRt.height);
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
    }
}