
using System.IO;
using System.Text;

namespace ManialinkToActionscriptAutoUpdater
{
    /// <summary>
    /// Converts a Manialink file to an Actionscript file. Removes all # symbols and puts the manialink code into a string method.
    /// </summary>
    internal class ManialinkToActionscriptConverter
    {
        internal static void Convert(string path, string exportFolder)
        {
            if (!Directory.Exists(exportFolder))
                throw new DirectoryNotFoundException("The export folder does not exist.");

            if (!File.Exists(path))
                throw new FileNotFoundException("The manialink file does not exist.", Path.GetFileName(path));

            string manialinkName = Path.GetFileNameWithoutExtension(path);
            string exportFileName = Path.Combine(exportFolder, manialinkName + "Manialink.as");

            string manialinkContent = File.ReadAllText(path);
            manialinkContent = manialinkContent.Replace("#", "\"\"\" + hashtag + \"\"\"");

            StringBuilder sb = new();
            sb.AppendLine("// Manialink code for " + manialinkName + ".xml");
            sb.AppendLine("\r\nstring Get"+manialinkName+ "() {\r\n\t");
            sb.AppendLine("\t//fixing hastag in \"\"\"\r\n\tstring hashtag = \"\\u0023\"; \n");
            sb.AppendLine("return \"\"\"");
            sb.Append(manialinkContent);
            sb.AppendLine("\"\"\";\n}");

            File.WriteAllText(exportFileName, sb.ToString());
        }
    }
}