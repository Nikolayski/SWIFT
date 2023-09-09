using SWIFT.Entities;
using SWIFT.Main;
using System;
using System.IO;

namespace SWIFT.Engine
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string mainDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
            Console.WriteLine(mainDirectory);
            var dirInfo = new DirectoryInfo(mainDirectory);
            var files = dirInfo.GetFiles("*.txt");
            foreach (var file in files)
            {
                var strResult = string.Empty;
                using (StreamReader streamReader = File.OpenText(file.ToString()))
                {
                    strResult = streamReader.ReadToEnd();
                    var parser = new Parser();
                    var messagesInfo = parser.SeparateTxtFile(strResult.Trim());
                    var textHeader = new TextHeader(messagesInfo["TextBlock"]);

                }
            }

        }
    }
}

