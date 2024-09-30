using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertInCSV.Class
{
    internal class ReadCSVfile
    {
        private static string path;
        private static string innerDirectory = "InnerFile";
        private static string outDirectory = "OutFile";
        private static string innerFile = "2024-08-02.log";
        private readonly int number;

        private static string outDirectoryFile;
        public string FileName { get; set; }

        public List<ObjectCSVmode> ObjectCSVList { get; set; }

        static ReadCSVfile()
        {
            path = AppDomain.CurrentDomain.BaseDirectory;
            CreateDirectory();
            outDirectoryFile = CreateDirectoryOutFile(innerFile);
        }

        public ReadCSVfile(int number)
        {
            this.number = number;
            FileName = $@"Клеть N{number}.csv";
            ObjectCSVList = ReadCSVdataMode();
            WriteCSVdata();

        }

        private List<ObjectCSV> ReadCSVdata()
        {
            string pathFull = $@"{path}\{innerDirectory}\{innerFile}";

            List<ObjectCSV> objectCSVs = new List<ObjectCSV>();

            using (StreamReader streamReader = new StreamReader(pathFull))
            {
                string? line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Replace("|", "");
                    string[] strArray = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (strArray.Length > 3 && strArray[3] == "Номер")
                    {
                        if (int.Parse(strArray[5]) == number)
                        {
                            objectCSVs.Add(new ObjectCSV(strArray[2], strArray[12]));
                        }
                    }                                  
                }
            }

            return objectCSVs;
        }

        private List<ObjectCSVmode> ReadCSVdataMode()
        {
            string pathFull = $@"{path}\{innerDirectory}\{innerFile}";

            List<ObjectCSVmode> objectCSVs = new List<ObjectCSVmode>();

            using (StreamReader streamReader = new StreamReader(pathFull))
            {
                string? line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Replace("|", "");
                    string[] strArray = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (strArray.Length > 3 && strArray[3] == "Номер")
                    {
                        if (int.Parse(strArray[5]) == number && (strArray[8] == Mode.IDLING || strArray[8] == Mode.ROLLING))
                        {
                            objectCSVs.Add(new ObjectCSVmode(strArray[2], strArray[12], strArray[8]));
                        }
                    }
                }
            }

            return objectCSVs;
        }


        private void WriteCSVdata()
        {
            string pathFull = $@"{path}{outDirectory}\{outDirectoryFile}\{FileName}";

            using (StreamWriter streamWriter = new StreamWriter(pathFull, false, System.Text.Encoding.UTF8))
            {
                foreach (var csv in ObjectCSVList)
                {
                    streamWriter.WriteLine(csv.ToString());
                }
            }
        }

        private static void CreateDirectory()
        {
            string[] pathTemp = new string[] { $@"{path}\{innerDirectory}", $@"{path}\{outDirectory}" };

            foreach (string path in pathTemp)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        private static string CreateDirectoryOutFile(string nameFile)
        {
            string[] strArray = nameFile.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            string pathTemp = $@"{path}\{outDirectory}\{strArray[0]}";

            if (!Directory.Exists(pathTemp))
            {
                Directory.CreateDirectory(pathTemp);
            }

            return strArray[0];
        }
    }
}
