using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ComRegister
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 执行提权和注册逻辑
            EnsureAdminAndExecuteAsync().Wait();
        }

        // 提权逻辑
        public static bool IsAdministrator()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
        // 提权成功后注册逻辑
        public static async Task EnsureAdminAndExecuteAsync()
        {
            if (!IsAdministrator())
            {
                // 如果不是管理员，重新启动应用
                var exePath = Assembly.GetEntryAssembly().Location;
                var startInfo = new ProcessStartInfo(exePath)
                {
                    Verb = "runas",
                    UseShellExecute = true
                };

                Process.Start(startInfo);
                return;
            }
            // 已经是管理员，执行用户确认逻辑
            await WaitForEnterAsync();
            // 执行注册/取消注册逻辑
            await RegisterComponentsWithDelay();
        }
        // 组件注册逻辑
        private static async Task RegisterComponentsWithDelay()
        {
            Console.WriteLine("管理员权限获取成功，开始加载模块...");
            await Task.Delay(1000);

            RegistrationServices regSvcs = new RegistrationServices();

            Assembly asm1 = Assembly.LoadFrom("CapeOpen.dll");
            regSvcs.RegisterAssembly(asm1, AssemblyRegistrationFlags.SetCodeBase);

            Console.WriteLine("环境依赖注册成功...");
            await Task.Delay(1000);

            Assembly asm2 = Assembly.LoadFrom("MyMixerTest.dll");
            regSvcs.RegisterAssembly(asm2, AssemblyRegistrationFlags.SetCodeBase);

            Console.WriteLine("单元模块注册成功，3 秒后退出程序...");
            await Task.Delay(3000);
        }
        // 等待用户输入逻辑
        private static async Task WaitForEnterAsync()
        {
            Console.WriteLine("请确认已经关闭了所有的 PME 环境后按回车继续...");
            await Task.Run(() =>
            {
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
            });
            Console.WriteLine("执行单元模块注册...");
        }
    }
}
