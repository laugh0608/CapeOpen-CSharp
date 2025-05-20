using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Windows.Forms;

namespace ComRegister
{
    [RunInstaller(true)]
    public partial class ComInstaller : Installer
    {
        public ComInstaller()
        {
            InitializeComponent();
        }

        // 覆盖 Commit 方法：安装成功完成时调用
        // 这是显示“安装完成”或测试信息的好地方
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState); // 务必调用基类的 Commit 方法

            // 在这里显示你的消息框
            MessageBox.Show("安装成功！", "安装完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 可选：覆盖 Install 方法：在安装过程中调用
        // 如果你想在安装过程中显示消息，可以在这里
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver); // 务必调用基类的 Install 方法

            MessageBox.Show("正在执行自定义安装操作...", "安装过程");
            // 这里可以添加其他安装逻辑
        }

        // 可选：覆盖 Rollback 方法：安装失败回滚时调用
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState); // 务必调用基类的 Rollback 方法

            MessageBox.Show("安装已回滚！", "安装失败回滚");
        }

        // 可选：覆盖 Uninstall 方法：卸载时调用
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState); // 务必调用基类的 Uninstall 方法

            MessageBox.Show("卸载完成！", "卸载信息");
        }

        // OnBeforeInstall 方法触发 BeforeInstall 事件
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);
            // 安装前，在此处执行任何附加操作

            MessageBox.Show("开始安装...");
        }
        // OnAfterInstall 方法触发 AfterInstall 事件
        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
            // 安装后，在此处执行任何附加操作

            MessageBox.Show("开始执行类库注册...");

            // 获取安装路径
            var setupDir = this.Context.Parameters["targetdir"];
            MessageBox.Show($"安装完毕，读取到安装路径: {setupDir}");
            // 检查路径是否为空（虽然 Installer Project 通常会提供，但防御性检查是好的）
            if (string.IsNullOrEmpty(setupDir))
            {
                // 在自定义操作中抛出异常会导致安装回滚
                // 如果是在 Commit 或 OnAfterInstall，可能不会导致回滚，但仍然不推荐
                // 更好的做法是记录错误或显示一个警告
                MessageBox.Show("错误：未能获取安装路径！", "安装错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // 退出方法
            }
            // 定义要拼接的文件名
            string coFileName = "CapeOpen.dll";
            string opFileName = "MyMixerTest.dll";
            // 使用 Path.Combine() 拼接目录和文件名
            string coPath = Path.Combine(setupDir, coFileName);
            string opPath = Path.Combine(setupDir, opFileName);
            MessageBox.Show($"安装路径: {setupDir}\nCapeOpen.dll的完整路径: {coPath}",
                            "安装信息",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            // 安装完毕后触发 AfterInstall 事件，传入 dll 文件完整路径，并执行提权和注册逻辑
            //WaitForEnterAsync().Wait();
            //RegisterComponentsWithDelay(coPath, opPath).Wait();
            //MessageBox.Show("类库注册完毕！");
        }
    }
}
