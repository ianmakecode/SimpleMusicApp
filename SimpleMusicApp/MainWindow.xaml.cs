using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Forms;

namespace SimpleMusicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MusicPlayerModel m;
        public MainWindow()
        {
            InitializeComponent();
            m = new MusicPlayerModel();
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m.ModelSoundPlayer.Play();
            }
            catch (InvalidOperationException xe)
            {
                (new ThreadExceptionDialog(xe)).ShowDialog();
            }
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            m.ModelSoundPlayer.Stop();
        }

        private void fileDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string wav_file = e.AddedItems[0] as string;
                m.LoadSong(wav_file);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            DialogResult result = dlg.ShowDialog();
            
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                fileDisplay.Items.Clear();
                m.Path = dlg.SelectedPath;
                selectedPath.Text = dlg.SelectedPath;
                
                foreach (string f in m.GetFiles() )
                {
                    fileDisplay.Items.Add(f);
                }
            }
            
        }
    }
}
