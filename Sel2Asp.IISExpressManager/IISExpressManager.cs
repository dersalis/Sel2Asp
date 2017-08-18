using System;
using System.Diagnostics;

namespace Sel2Asp.IISExpressManager
{
    /// <summary>
    /// Obsługuje server IIS Expres (dla programistów)
    /// </summary>
    public class IISExpressManager
    {
        private int _port; // Port IIS [1024]
        private string _applicationPath; // Scieżka katalogu Debug ["C:\PROJECTS\OnePage\OnePage.Web"]
        private Process _process; // Proces IIS

        private string _url; // Url do aplikacji
        /// <summary>
        /// Url do aplikacji
        /// </summary>
        public string Url
        {
            get
            {
                return String.Format($"http://localhost:{_port}");
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="applicationPath">Ścieżka do plików</param>
        /// <param name="port">Port IIS</param>
        public IISExpressManager(string applicationPath, int port = 1024)
        {
            _port = port;
            _applicationPath = applicationPath;
        }

        /// <summary>
        /// Uruchamia serwer IIS Express
        /// </summary>
        public void Start()
        {
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _process = new Process();
            _process.StartInfo.FileName = programFiles + @"\IIS Express\iisexpress.exe";
            _process.StartInfo.Arguments = string.Format($"/path:\"{_applicationPath}\" /port:{_port}");
            _process.Start();
        }

        /// <summary>
        /// Zatrzymuje serwer IIS Express
        /// </summary>
        public void Stop()
        {
            if (_process.HasExited == false)
                _process.Kill();
        }

        /// <summary>
        /// Zwraca bezwzględny adres url
        /// </summary>
        /// <param name="relativeUrl">Względny adres url</param>
        /// <returns>Bezwzględny adres url</returns>
        public string GetAbsoluteUrl(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
            {
                relativeUrl = "/" + relativeUrl;
            }
            return String.Format("http://localhost:{0}/{1}", _port, relativeUrl);
        }
    }
}
