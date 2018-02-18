using System.IO;

namespace Adapter.Usql
{
    public class Writer
    {
        private readonly string _outputFolder;
        private readonly TableType _tableType;

        public Writer(string outputFolder, TableType tableType)
        {
            _outputFolder = outputFolder;
            _tableType = tableType;
        }

        public void Write()
        {

            Directory.CreateDirectory(_outputFolder);

            File.WriteAllText(
                Path.Combine(_outputFolder, _tableType.ScriptName),
                _tableType.Script);
        }

    }
}
