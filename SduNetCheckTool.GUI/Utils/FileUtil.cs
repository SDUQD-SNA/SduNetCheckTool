using SduNetCheckTool.GUI.Common;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace SduNetCheckTool.GUI.Utils
{
    public static class FileUtil
    {
        public static string ExportPath = Directory.GetCurrentDirectory() + "\\报告";
        public static string ExportReport(ObservableCollection<DetectionTask> tasks)
        {
            var exportFilePath = ExportPath + "\\" + System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt";

            if (tasks.Any(i => i.TaskStatusEnum == TaskStatusEnum.Waiting))
                return "-1";

            if (!Directory.Exists(ExportPath))
                Directory.CreateDirectory(ExportPath);

            foreach (var detectionTask in tasks)
            {
                File.AppendAllText(exportFilePath, detectionTask.Tips + '\n' + '\n', Encoding.UTF8);
            }

            return exportFilePath;
        }
    }
}
