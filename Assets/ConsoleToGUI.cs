using UnityEngine;
using System.Collections.Generic;

public class ConsoleToGUI : MonoBehaviour
{
    string myLog = "*begin log";
    string filename = "";
    bool doShow = true;
    int kChars = 700;

    // Store the last log message and its count
    string lastLogMessage = "";
    int lastLogCount = 0;

    void OnEnable() { Application.logMessageReceived += Log; }
    void OnDisable() { Application.logMessageReceived -= Log; }

    void Update() { if (Input.GetKeyDown(KeyCode.Space)) { doShow = !doShow; } }

    public void Log(string logString, string stackTrace, LogType type)
    {
        // Check if the current log message is the same as the last one
        if (logString == lastLogMessage)
        {
            lastLogCount++;
        }
        else
        {
            // If it's a different message, append the last one with its count and reset the counter
            if (lastLogCount > 1)
            {
                myLog = myLog + "\n(" + lastLogCount + " times) " + lastLogMessage;
            }
            // Reset log message and counter
            myLog = myLog + "\n" + logString;
            lastLogMessage = logString;
            lastLogCount = 1;
        }

        // Limit the length of the log to avoid excessive memory usage
        if (myLog.Length > kChars)
        {
            myLog = myLog.Substring(myLog.Length - kChars);
        }

        // Save to a file
        if (filename == "")
        {
            string d = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/YOUR_LOGS";
            System.IO.Directory.CreateDirectory(d);
            string r = Random.Range(1000, 9999).ToString();
            filename = d + "/log-" + r + ".txt";
        }

        try { System.IO.File.AppendAllText(filename, logString + "\n"); }
        catch { }
    }

    void OnGUI()
    {
        if (!doShow) { return; }

        // Scale the GUI for different screen sizes
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));

        // Display the log in the GUI
        GUI.TextArea(new Rect(10, 10, 540, 370), myLog);
    }
}
