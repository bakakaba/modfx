namespace ModFx.Logging.File
{
    public class FileLoggingConfiguration
    {
        public bool Buffered { get; set; } = true;
        public string PathFormat { get; set; } = "logs/log-{Date}.txt";
        public long? MaxFileSize { get; set; }
        public int? MaxFileCount { get; set; }
    }
}