using System;
using System.IO;

public class Logger
{
    public const string FolderName = "Log";
    public enum Branch { Info, Debug, Error }

    private static readonly object _locker = new object();
    private static readonly Lazy<Logger> _lazy = new Lazy<Logger>(() => new Logger(), true);

    public event Action<object> OnLogInfo;
    public event Action<object> OnLogDebug;
    public event Action<object> OnLogError;

    private Logger() { }

    public static Logger Instance => _lazy.Value;

    protected void WriteLog(object log, Branch branch)
    {
        lock (_locker)
        {
            var content = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]:{log}";

            Console.WriteLine(content);

            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderName, branch.ToString());
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var path = Path.Combine(folder, $"{DateTime.Today:yyyy-MM-dd}.log");
            File.AppendAllLines(path, new[] { content });
        }
    }

    public static void Info(object log)
    {
        Instance.WriteLog(log, Branch.Info);
        Instance.OnLogInfo?.Invoke(log);
    }

    public static void Debug(object log)
    {
        Instance.WriteLog(log, Branch.Debug);
        Instance.OnLogDebug?.Invoke(log);
    }

    public static void Error(object log)
    {
        Instance.WriteLog(log, Branch.Error);
        Instance.OnLogError?.Invoke(log);
    }
}
