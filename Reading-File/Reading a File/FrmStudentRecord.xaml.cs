using System;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace Reading_a_File;

public partial class FrmStudentRecord : Window
{
    public FrmStudentRecord()
    {
        InitializeComponent();
    }

    private void BtnRegister_OnClick(object sender, RoutedEventArgs e)
    {
        FrmRegistration frmRegistration = new FrmRegistration();
        frmRegistration.Show();
        Close();
    }

    private void BtnFind_OnClick(object sender, RoutedEventArgs e)
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

                LvViewRecord.Items.Clear();

                using (StreamReader streamReader = File.OpenText(path))
                {
                    string _GetText = "";
                    while ((_GetText = streamReader.ReadLine()) != null)
                    {
                        Console.WriteLine(_GetText);
                        LvViewRecord.Items.Add(_GetText);
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }

    private void BtnUpload_OnClick(object sender, RoutedEventArgs e)
    {
        if (LvViewRecord.Items.Count == 0)
        {
            MessageBox.Show("There Is No Txt To Save.", "Empty Content", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        SaveFileDialog sfd = new SaveFileDialog();
        sfd.InitialDirectory = @"C:\";
        sfd.Title = "Choose Save Location";
        sfd.DefaultExt = "txt";
        sfd.Filter = "Txt Files (*.txt)|*.txt|All files (*.*)|*.*";

        if (sfd.ShowDialog() == true)
        {
            string savePath = sfd.FileName;
           

            StringBuilder content = new StringBuilder();
            foreach (var item in LvViewRecord.Items)
            {
                content.AppendLine(item.ToString());
            }

            File.WriteAllText(savePath, content.ToString());
            MessageBox.Show($"Your Txt File has been uploaded to {savePath}", "Save Path", MessageBoxButton.OK, MessageBoxImage.Information);

            LvViewRecord.Items.Clear();
        }
    }
}