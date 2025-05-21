using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JsonLogWriter
{
    public static void Write(IReadOnlyList<LogEvent> eventsList)
    {
        string dir = Path.Combine(Application.persistentDataPath, "Logs");
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        string file = Path.Combine(dir, $"log_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.json");
        File.WriteAllText(file, JsonUtility.ToJson(new Wrapper { events = eventsList }));
    }

    [System.Serializable] private class Wrapper { public IReadOnlyList<LogEvent> events; }
}
