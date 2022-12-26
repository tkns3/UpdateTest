using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UpdateTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = $"MainWindow {Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion}";
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            string t = "";

            await Updater.FetchReleasesAsync();

            t += Updater.ExePath + "\r\n";
            t += Updater.NewExePath + "\r\n";
            t += Updater.OldExePath + "\r\n";

            foreach (var r in Updater.ReleasesCache)
            {
                t += r.tag_name + "\r\n";
                t += r.body + "\r\n----\r\n";
            }
            textBoxReleases.Text = t;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Updater.StartUpdate();
        }
    }
}
