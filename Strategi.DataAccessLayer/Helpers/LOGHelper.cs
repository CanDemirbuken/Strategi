using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategi.DataAccessLayer.Helpers
{
    public class LOGHelper
    {
        public string logFolder = @"C:\Strategi\Log";
        public void LogError(string message)
        {
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            string logFile = Path.Combine(logFolder, "error_log.txt");

            using (StreamWriter writer = new StreamWriter(logFile))
            {
                writer.Write("Error: " + message);
            }
        }
    }
}
