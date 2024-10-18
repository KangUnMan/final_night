using System;
using System.IO;
using UnityEngine;

public class LogFileManager : MonoBehaviour
{
    private static LogFileManager _instance;

    private string logFilePath;
    private StreamWriter logWriter;

    public static LogFileManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject logManager = new GameObject("LogFileManager");
                _instance = logManager.AddComponent<LogFileManager>();
                DontDestroyOnLoad(logManager);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // ���� ��ȯ�Ǿ LogFileManager�� �ı����� ����
            InitializeLogFile();
        }
        else if (_instance != this)
        {
            // �̹� �ν��Ͻ��� ������ ��� ���� ������ ������Ʈ�� �ı�
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �α� ���� �ʱ�ȭ �� ���� ����
    /// </summary>
    private void InitializeLogFile()
    {
        logFilePath = Path.Combine(Application.persistentDataPath, "game_log.txt");

        // �α� ������ ���� (������ ����)
        logWriter = new StreamWriter(logFilePath, true); // true�� append ���
        logWriter.AutoFlush = true; // �ڵ����� ���۸� ���

        // ����Ƽ �α� �޽����� ���Ͽ� ����ϴ� �ݹ� ���
        Application.logMessageReceived += HandleLog;

        Debug.Log("Log system initialized. Log file at: " + logFilePath);
    }

    /// <summary>
    /// ����Ƽ �α� �޽����� ���Ͽ� ����ϴ� �Լ�
    /// </summary>
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        string logEntry = $"{DateTime.Now}: [{type}] {logString}";

        if (type == LogType.Exception || type == LogType.Error)
        {
            logEntry += $"\nStackTrace: {stackTrace}";
        }

        // �α� ������ ���Ͽ� ���
        logWriter.WriteLine(logEntry);
    }

    private void OnDestroy()
    {
        // �α� �ݹ� ����
        Application.logMessageReceived -= HandleLog;

        // �α� ���� �ݱ�
        if (logWriter != null)
        {
            logWriter.Close();
            logWriter = null;
        }
    }

    private void OnApplicationQuit()
    {
        // ���� ���� �� �α� ������ ����
        if (logWriter != null)
        {
            logWriter.Close();
            logWriter = null;
        }
    }
}
