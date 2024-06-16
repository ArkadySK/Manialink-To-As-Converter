using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManialinkToActionscriptAutoUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PathTextBox.Text = Properties.Settings.Default.LastSelectedFilePath;
            ExportFolderTextBox.Text = Properties.Settings.Default.LastExportFolderPath;
        }

        private void SetStatus(string message, SolidColorBrush brush)
        {
            StatusTextBlock.Background = null;
            var animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            StatusTextBlock.Background = brush;
            StatusTextBlock.BeginAnimation(OpacityProperty, animation);
            StatusTextBlock.Text = message;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            string path = PathTextBox.Text;
            string exportFolder = ExportFolderTextBox.Text;
            
            try
            {
                ManialinkToActionscriptConverter.Convert(path, exportFolder);
                SetStatus("The generation was successful!", new SolidColorBrush(Color.FromRgb(150, 250, 150)));
            }
            catch (Exception ex)
            {
                SetStatus("The generation failed: \n" + ex.Message, new SolidColorBrush(Color.FromRgb(250, 150, 150)));
            }
            Properties.Settings.Default.Save();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dg = new()
            {
                Filter = "Manialink files (*.xml)|*.xml"
            };
            var result = dg.ShowDialog();

            if (result != true)
                return;

            PathTextBox.Text = dg.FileName;
            Properties.Settings.Default.LastSelectedFilePath = PathTextBox.Text;
        }

        private void SelectExportFolderButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog dg = new();
            var result = dg.ShowDialog();

            if (result != true)
                return;

            ExportFolderTextBox.Text = dg.FolderName;
            Properties.Settings.Default.LastExportFolderPath = ExportFolderTextBox.Text;
        }
    }
}