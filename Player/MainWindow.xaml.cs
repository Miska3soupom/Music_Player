using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

namespace Player
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void timeSet_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Position = new TimeSpan(Convert.ToInt64(timeSet.Value));
        }

        private void media_opened(object sender, RoutedEventArgs e)
        {
            timeSet.Maximum = media.NaturalDuration.TimeSpan.Ticks;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog chmusic = new CommonOpenFileDialog { IsFolderPicker = true };
            CommonFileDialogResult result = chmusic.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                List<string> list = new List<string>();
                MList.ItemsSource = Directory.GetFiles(chmusic.FileName);
                foreach (string music in MList.Items)
                {
                    if (music.EndsWith(".mp3"))
                    {
                        list.Add(music);
                    }
                }
                MList.ItemsSource = list;
                media.Source = new Uri((string)MList.Items[0]);
                media.Play();
                media.Volume = 0.7;
                play_pause.Background = Brushes.DarkSeaGreen;
            }

        }

        private void play_pause_Click(object sender, RoutedEventArgs e)
        {
            if (play_pause.Background == Brushes.DarkSeaGreen)
            {
                media.Position = new TimeSpan(Convert.ToInt64(timeSet.Value));
                play_pause.LargeImageSource = new BitmapImage (new Uri("/Resources/play.png", UriKind.RelativeOrAbsolute));
                play_pause.Background = Brushes.Red;
                media.Stop();
            }
            else
            {
                play_pause.LargeImageSource = new BitmapImage(new Uri("/Resources/pause.png", UriKind.RelativeOrAbsolute));
                play_pause.Background = Brushes.DarkSeaGreen;
                media.Play();
            }
        }

        private void MList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MList.SelectedIndex > -1)
            {
                media.Source = new Uri((string)MList.SelectedItem);
                media.Play();
            }
        }
    }
}
