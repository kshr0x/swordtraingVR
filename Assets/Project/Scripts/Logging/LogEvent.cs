public struct LogEvent
{
    public string type;      // "hit", "parry", "start", ...
    public float time;
    public string payload;   // JSON‑строка с параметрами
}