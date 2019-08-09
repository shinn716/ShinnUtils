/// Author : shinn716
/// Only for windows.
/// https://docs.microsoft.com/zh-tw/dotnet/api/system.diagnostics.processwindowstyle?view=netframework-4.8

using System.Diagnostics;
using System.IO;

public static class WindowsEventAPI
{
    #region API
    /// <summary>
    /// WindowsStyle
    /// </summary>
    public enum WindowsStyle
    {
        Hidden,
        Maximized,
        Minimized,
        Normal
    }

    /// <summary>
    /// 開啟 Windows 檔案
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    public static void OpenExetnationFile(string path, string fileName)
    {
        string file = Path.Combine(path, fileName);
        Process.Start(file);
    }
    
    /// <summary>
    /// 搜尋目前正在執行的程式
    /// </summary>
    public static void SearchAllProgramsOnRunning()
    {
        Process[] p1 = Process.GetProcesses();
        foreach (Process pro in p1)
            UnityEngine.Debug.Log(pro.ProcessName);
    }

    /// <summary>
    /// 尋找目前正在運行的 processName 程式
    /// </summary>
    /// <param name="processName"></param>
    public static void SearchProgram(string processName)
    {
        Process[] p1 = Process.GetProcesses();
        foreach (Process pro in p1)
        {
            if (pro.ProcessName.ToUpper().Contains(processName))
            {
                UnityEngine.Debug.Log("Got it: " + processName);
            }
        }
    }

    /// <summary>
    /// 可自定 Windows 的動作, 像是 隱藏 一般 最大化 最小化, 此程式會開啟程式, 不須額外執行 Process.Start()
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <param name="windowStyle"></param>
    /// 
    /// Example:相對路徑
    /// string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\exe");
    /// WindowsEventAPI.SetWindowEvent(path, "Kinect.exe", ProcessWindowStyle.Minimized);
    public static void SetWindowEvent(string path, string fileName, WindowsStyle windowStyle = WindowsStyle.Normal)
    {
        string file = Path.Combine(path, fileName);
        Process myProcess = new Process();
        myProcess.StartInfo.UseShellExecute = true;
        myProcess.StartInfo.FileName = file;
        myProcess.StartInfo.CreateNoWindow = false;

        myProcess.StartInfo.WindowStyle = Select(windowStyle);
        myProcess.Start();
    }

    public static void SetWindowEvent(string file, WindowsStyle windowStyle = WindowsStyle.Normal)
    {
        Process myProcess = new Process();
        myProcess.StartInfo.UseShellExecute = true;
        myProcess.StartInfo.FileName = file;
        myProcess.StartInfo.CreateNoWindow = false;

        myProcess.StartInfo.WindowStyle = Select(windowStyle);
        myProcess.Start();
    }
    #endregion

    #region Private function
    private static ProcessWindowStyle Select(WindowsStyle style)
    {
        switch (style)
        {
            case WindowsStyle.Hidden:
                return ProcessWindowStyle.Hidden;
            case WindowsStyle.Maximized:
                return ProcessWindowStyle.Maximized;
            case WindowsStyle.Minimized:
                return ProcessWindowStyle.Minimized;
            default:
                return ProcessWindowStyle.Normal;
        }
    }
    #endregion
}
