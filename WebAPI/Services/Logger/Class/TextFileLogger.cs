using WebAPI.Services.Logger.Interface;

namespace WebAPI.Services.Logger.Class
{
    public class TextFileLogger : ILoggerService
    {
        private readonly string _logFilePath;
        public TextFileLogger()
        {

            _logFilePath = Environment.CurrentDirectory + "\\log.txt";
            CreateTextFile();
        }
        public void Write(string message)
        {
            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine($"[TextFileLogger] - {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss -- ")} {message}");
            }
        }
        private void CreateTextFile()
        {

            if (!File.Exists(_logFilePath))
            {
                using (StreamWriter writer = File.CreateText(_logFilePath))
                {
                    writer.WriteLine("Log Dosyası Oluşturuldu: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "\n");

                }
            }
        }
    }
}
