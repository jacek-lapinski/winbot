using System;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;
using Winbot.Entities;

namespace Winbot.Utils
{
    internal class ScenarioFileManager : IScenarioFileManager
    {
        private readonly SaveFileDialog _saveFileDialog;

        public ScenarioFileManager()
        {
            const string filter = "Winbot Files (WIB)|*.WIB";
            _saveFileDialog = new SaveFileDialog { Filter = filter };
        }

        public void Save(Scenario scenario)
        {
            var result = _saveFileDialog.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;

            var content = JsonConvert.SerializeObject(scenario);
            File.WriteAllText(_saveFileDialog.FileName, content);
        }

        public void Load(string filePath, Action<Scenario> onLoad)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            var extension = Path.GetExtension(filePath);
            if (extension != ".wib")
            {
                throw new FileFormatException();
            }

            var content = File.ReadAllText(filePath);
            var scenario = JsonConvert.DeserializeObject<Scenario>(content);
            onLoad(scenario);
        }
    }
}
