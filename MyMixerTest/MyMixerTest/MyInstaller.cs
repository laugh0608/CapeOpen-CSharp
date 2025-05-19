using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace MyMixerTest
{
    [RunInstaller(true)]
    public partial class MyInstaller : Installer
    {
        public MyInstaller()
        {
            InitializeComponent();
        }
        // Override the 'Install' method.
        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
        }
        // Override the 'Commit' method.
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }
        // Override the 'Rollback' method.
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }
        // Override the 'Uninstall' method.
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
        // 衍生方法：
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
        }
        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
        }
    }
}
