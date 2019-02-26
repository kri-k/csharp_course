using System.Windows;

namespace Lab3
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            WindowManager.Register(typeof(ContactViewModel), "add", typeof(AddcontactWindow));
            WindowManager.Register(typeof(ContactViewModel), "change", typeof(ChangecontactData));
        }
    }
}
