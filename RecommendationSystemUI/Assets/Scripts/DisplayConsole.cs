using System;
using TMPro;
using UnityEngine;

public class DisplayConsole : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    private void Start()
    {
        if (MenuInteractions.Current.Database.isDebug)
        {
            Application.logMessageReceived += HandleLog;
            throw new Exception("test");
        }
    }
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        textBox.text += $"{logString}\n";
    }
}
