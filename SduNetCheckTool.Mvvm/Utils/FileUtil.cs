using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using SduNetCheckTool.Mvvm.Common;

namespace SduNetCheckTool.Mvvm.Utils
{
    public static class FileUtil
    {
        public static string ExportPath = Directory.GetCurrentDirectory() + "\\报告";
        public static string ExportReport(ObservableCollection<DetectionTask> tasks)
        {
            var exportFilePath = ExportPath + "\\" + System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt";

            if (tasks.Any(i => i.TaskStatusEnum == TaskStatusEnum.Waiting))
                return "NoRecords";

            if (!Directory.Exists(ExportPath))
                Directory.CreateDirectory(ExportPath);

            foreach (var detectionTask in tasks)
            {
                File.AppendAllText(exportFilePath, detectionTask.Tips + '\n' + '\n', Encoding.UTF8);
            }

            return exportFilePath;
        }

        public static string ReadFile(string filePath)
        {
            if (!File.Exists(filePath)) return "FileNotExists";

            try
            {
                StreamReader sr = new StreamReader(filePath);
                string data = "", line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    data += line + "\n";
                }

                return data;
            }
            catch (Exception e)
            {
                //
            }
            return "-1";
        }
    }
}
