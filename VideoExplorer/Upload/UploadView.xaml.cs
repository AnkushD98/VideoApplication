using System.IO;
using System.Windows;
using System.Windows.Controls;
using Services;

namespace VideoExplorer.Upload
{
    /// <summary>
    /// Interaction logic for UploadView.xaml
    /// </summary>
    public partial class UploadView : UserControl
    {
        public UploadView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".mp4";
            dlg.Filter = "MP4 files (*.mp4)|*.mp4|TS Files (*.ts)|*.ts|MOV Files (*.mov)|*.mov|MKV Files (*.mkv)|*.mkv";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string fileName = dlg.FileName;
                selectedFileTextBox.Text = fileName;
                FileInfo fileInfo = new FileInfo(fileName); // Extract file info

                // Send the file stream to the upload service
                LocalFSUploaderService uploader = new LocalFSUploaderService();
                using (FileStream stream = File.OpenRead(fileName))
                {
                    uploadedVideoLinkTextBlock.Text = uploader.UploadVideo(stream, fileInfo.Name).ToString();
                }
            }
        }
    }
}
