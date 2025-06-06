using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    [Serializable]
    public class SettingsSave
    {
        public bool ShowCode { get; set; }
        public bool ShowScene { get; set; }
        public string Path { get; set; }
        public string ProjectType { get; set; }
        public SettingsSave(bool showCode, bool showScene, string path, string projectType)
        {
            ShowCode = showCode;
            ShowScene = showScene;
            Path = path;
            ProjectType = projectType;
        }
    }
}
