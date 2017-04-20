using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Win32;
using Winbot.Entities;

namespace Winbot.Utils
{
    internal class ScenarioFileManager : IScenarioFileManager
    {
        private readonly SaveFileDialog _saveFileDialog;
        private readonly XmlSerializer _xmlSerializer;

        public ScenarioFileManager()
        {
            const string filter = "Winbot Files (WIB)|*.WIB";
            _saveFileDialog = new SaveFileDialog { Filter = filter };
            _xmlSerializer = new XmlSerializer(typeof(Scenario));
        }

        public void Save(Scenario scenario)
        {
            _saveFileDialog.FileName = GetFileName(scenario);
            var result = _saveFileDialog.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;

            using (var fileStream = File.Create(_saveFileDialog.FileName))
            {
                _xmlSerializer.Serialize(fileStream, scenario);
            }
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

            Scenario scenario;

            using (var fileStrem = File.OpenRead(filePath))
            {
                scenario = _xmlSerializer.Deserialize(fileStrem) as Scenario;
            }

            onLoad(scenario);
        }

        private string GetFileName(Scenario scenario)
        {
            return $"{Path.GetInvalidFileNameChars().Aggregate(scenario.Name, (current, invalidChar) => current.Replace(invalidChar, '_'))}.wib";
        }
    }
}
