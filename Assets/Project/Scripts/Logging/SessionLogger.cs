using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionLogger
{
    private readonly List<LogEvent> _events = new();
    private float _startTime;

    public void BeginSession()
    {
        _events.Clear();
        _startTime = Time.time;
        Log("start", null);
    }

    public void Log(string type, object data)
    {
        _events.Add(new LogEvent
        {
            type = type,
            time = Time.time - _startTime,
            payload = data == null ? string.Empty : JsonUtility.ToJson(data)
        });
    }

    public IReadOnlyList<LogEvent> GetEvents() => _events;
}