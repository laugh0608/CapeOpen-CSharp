using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ComUnRegister
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 执行反注册逻辑
            EnsureAdminAndExecuteAsync().Wait();
        }

        // 提权并执行注册/取消注册逻辑
        public static bool IsAdministrator()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

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
            // 已经是管理员，让用户确认是否继续
            WaitForEnterAsync().Wait();
            // 执行反注册逻辑
            await UnregisterComponentsAsync();
        }

        private static async Task UnregisterComponentsAsync()
        {
            RegistrationServices regSvcs = new RegistrationServices();

            try
            {
                Console.WriteLine("开始卸载单元模块...");
                await Task.Delay(1000);

                // 取消注册第一个组件
                Assembly asm2 = Assembly.LoadFrom("MyMixerTest.dll");
                regSvcs.UnregisterAssembly(asm2);
                Console.WriteLine("单元模块取消注册成功...");

                // 异步等待
                Console.WriteLine("正在卸载环境依赖...");
                await Task.Delay(1000);

                // 取消注册第二个组件
                Assembly asm1 = Assembly.LoadFrom("CapeOpen.dll");
                regSvcs.UnregisterAssembly(asm1);
                Console.WriteLine("环境依赖取消注册成功");

                await Task.Delay(1000);
                Console.WriteLine("依赖环境卸载成功...");
                
                Console.WriteLine("等待 3 秒后自动退出...");
                // 异步等待
                await Task.Delay(3000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"取消注册失败: {ex.Message}");
            }
        }
        // 等待用户输入逻辑
        private static async Task WaitForEnterAsync()
        {
            Console.WriteLine("请确认已经关闭了所有的 PME 环境后按回车继续...");
            await Task.Run(() =>
            {
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
            });
            Console.WriteLine("执行单元模块卸载...");
        }
    }
}
