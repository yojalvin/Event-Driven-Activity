using System.Windows;
using System.IO;
using System;
using Microsoft.Win32;

namespace Reading_a_File
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOpen_OnClick(object sender, RoutedEventArgs e)
        {
            DisplayToList();
            
            //For The Challenge Part
            MessageBoxResult result = MessageBox.Show("Do You Want To Open FrmStudentRecord?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                FrmStudentRecord frmStudentRecord = new FrmStudentRecord();
                frmStudentRecord.Show();
                Close();
            }
        }

        private void DisplayToList()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.Title = "Browse Text Files";
            ofd.DefaultExt = "txt";
            ofd.Filter = "Txt Files (*.txt)|*.txt|All files (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {
                string path = ofd.FileName;

                try
                {
                    if (new FileInfo(path).Length == 0)
                    {
                        return;
                    }

                    LvShowText.Items.Clear();

                    using (StreamReader streamReader = File.OpenText(path))
                    {
                        string _GetText = "";
                        while ((_GetText = streamReader.ReadLine()) != null)
                        {
                            Console.WriteLine(_GetText);
                            LvShowText.Items.Add(_GetText);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }
    }
}