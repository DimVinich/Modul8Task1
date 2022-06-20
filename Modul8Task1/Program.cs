using System;
using System.IO;
using System.Linq;

namespace Modul8Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = @"D:\Temp";
            DateTime oldDate = DateTime.Now.AddMinutes(-30);

            Console.WriteLine("old DateTime = "+oldDate);
            PrintOldFiles(folder, oldDate);

            Console.ReadKey();
        }

        static void PrintOldFiles(string aFolder, DateTime aOldDate)  //  Вспомогательное для отработки лямбда по времени обращения
        {
            DirectoryInfo dirInfo = new DirectoryInfo(aFolder);
            FileInfo[] arrFile = dirInfo.GetFiles();
            
            foreach (FileInfo file in arrFile.Where( d => d.LastAccessTime < aOldDate))
            {
                Console.WriteLine(file.LastAccessTime);
            }

        }

        static void DeleteFolder(string aFolder)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(aFolder);
                DirectoryInfo[] arrDir = dirInfo.GetDirectories();
                FileInfo[] arrFile = dirInfo.GetFiles();

                foreach (FileInfo file in arrFile)
                {
                    file.Delete();
                }

                foreach (DirectoryInfo dir in arrDir)
                {
                    DeleteFolder(dir.FullName);
                    if (dir.GetDirectories().Length == 0 && dir.GetFiles().Length == 0)
                    {
                        dir.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ну удалось зачистить устаревшую информаци по причине " + ex.Message);
            }
        }

    }
}
