public class LoggingModule : IModule
{
    public SessionLogger Logger { get; private set; }

    public void Initialize()
    {
        Logger = new SessionLogger();
        Logger.BeginSession();
    }

    public void Tick() { /* Optional real‑time flush */ }

    public void Shutdown()
    {
        JsonLogWriter.Write(Logger.GetEvents());
    }

    // helper
    public void Log(string type, object payload = null) => Logger.Log(type, payload);
}