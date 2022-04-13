using ADORM.Domain;
using ADORM.Domain.Objects.Generator;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ADORM
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            if (args.Any())
            {
                if (args.First().Trim() == "?")
                {
                    Console.WriteLine(Const.Cli.HelpText);
                }
                else
                {
                    Generate(args);
                }
            }
            else
            {
                ConsoleHelper.HideConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CodeGeneratorForm());
            }
        }

        private static void Generate(string[] args)
        {
            var option = new GeneratorOption(args);
            var generator = new AdoCodeGenerator(option);
            generator.Ready().Go();
        }

        /// <summary>
        /// 全局异常捕获
        /// 注意：该事件无法阻止程序停止运行，仅用于记录未预见的异常，可预见的异常请自行捕获。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UnhandledException");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string path = Path.Combine(folder, $"{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            File.WriteAllText(path, e.ExceptionObject.ToString());
        }
    }
}
