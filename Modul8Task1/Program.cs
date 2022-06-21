using System;
using System.IO;
using System.Linq;

namespace Modul8Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime oldDate = DateTime.Now.AddMinutes(-30);

            // Ввод и провека папки на существование
            Console.WriteLine("Введите директорию для зачистки :");
            string folder = Console.ReadLine();

            if ( !Directory.Exists(folder))
            {
                Console.WriteLine(" Вы ввели не сущестdующую директорию. Программа прекращает свою работу.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Начата зачистка директории {0} от файлов и директорий старше {1} минут", folder, oldDate);

            DeleteFolder(folder, oldDate);

            Console.WriteLine("Зачистка завершена");

            Console.ReadKey();
        }


        static void DeleteFolder(string aFolder, DateTime aoldDate)   // Зачистка директории от устаревшей информации
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(aFolder);
                DirectoryInfo[] arrDir = dirInfo.GetDirectories();
                FileInfo[] arrFile = dirInfo.GetFiles();

                foreach (FileInfo file in arrFile.Where(d => d.LastAccessTime < aoldDate))
                {
                    file.Delete();
                }

                foreach (DirectoryInfo dir in arrDir)
                {
                    DeleteFolder(dir.FullName, aoldDate);
                    if (dir.GetDirectories().Length == 0 && dir.GetFiles().Length == 0 && dir.LastAccessTime < aoldDate )
                    {
                        dir.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось зачистить устаревшую информаци по причине " + ex.Message);
            }
        }

        static void PrintOldFiles(string aFolder, DateTime aOldDate)  //  Вспомогательное для отработки лямбда по времени обращения
        {
            DirectoryInfo dirInfo = new DirectoryInfo(aFolder);
            FileInfo[] arrFile = dirInfo.GetFiles();

            foreach (FileInfo file in arrFile.Where(d => d.LastAccessTime < aOldDate))
            {
                Console.WriteLine(file.FullName +"    "+ file.LastAccessTime);
            }

        }

    }
}
