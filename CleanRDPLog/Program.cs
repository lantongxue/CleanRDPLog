using Microsoft.Win32;
using System;
using System.IO;

/// <summary>
/// RDP日志清除工具
/// </summary>
namespace CleanRDPLog
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "RDP日志清除工具 by 猎艳 QQ：863448246";

            Console.WriteLine("开始清除RDP连接记录......");

            RegistryKey currentUser = Registry.CurrentUser;

            RegistryKey TSC = currentUser.OpenSubKey(@"Software\Microsoft\Terminal Server Client", true);

            RegistryKey TSC_Default = TSC.OpenSubKey("Default", true);

            RegistryKey TSC_Servers = TSC.OpenSubKey("Servers", true);

            Console.WriteLine("开始清理注册表......");

            foreach (string name in TSC_Default.GetValueNames())
            {
                Console.Write("["+TSC_Default.GetValue(name)+"]");

                TSC_Default.DeleteValue(name);

                Console.WriteLine("已清除");
            }

            foreach (string server_name in TSC_Servers.GetSubKeyNames())
            {
                TSC_Servers.DeleteSubKey(server_name);
            }

            TSC_Servers.Close();
            TSC_Default.Close();
            TSC.Close();
            currentUser.Close();

            Console.WriteLine("开始清理缓存配置文件......");

            string Default_rdp_file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            File.Delete(Default_rdp_file + @"\Default.rdp");

            Console.WriteLine("清理完毕！ 请按任意键继续....");

            Console.ReadKey();

            
        }
    }
}
