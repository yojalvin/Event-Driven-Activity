using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Reading_a_File
{
    public partial class FrmRegistration : Window
    {
        public delegate string DelegateText(string txt);
        public delegate long DelegateNumber(long number);

        public static class StudentInfoClass
        {
            public static long StudentNo = 0;
            public static long Age = 0;
            public static DateTime Birthday = DateTime.MinValue;
            public static long ContactNo = 0;

            public static string LastName = "";
            public static string Program = "";
            public static string FirstName = "";
            public static string MiddleInitial = "";
            public static string Gender = "";

            public static long GetStudentNo(long studentNo) => studentNo;
            public static long GetAge(long age) => age;
            public static long GetContactNo(long contactNo) => contactNo;

            public static string GetLastName(string lastName) => lastName;
            public static string GetFirstName(string firstName) => firstName;
            public static string GetMiddleInitial(string middleName) => middleName;


        }

        public FrmRegistration()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] ListOfProgram = new string[]
            {
                "BS Information Technology",
                "BS Computer Science",
                "BS Information System",
                "BS in Accountancy",
                "BS in Hospitality Management",
                "BS in Tourism Management"
            };
            for (int i = 0; i < 6; i++)
            {
                cbProgram.Items.Add(ListOfProgram[i].ToString());
            }

            string[] ListOfGender = new string[]
            {
                "Male",
                "Female",
                "Rather not say"
            };
            for (int g = 0; g < 3; g++)
            {
                cbGender.Items.Add(ListOfGender[g].ToString());
            }
        }

        private void btnRegister_Click_1(object sender, RoutedEventArgs e)
        {

            //Cheks if the Student No is empty and consists of letterss
            if (string.IsNullOrWhiteSpace(txtStudent.Text) || !int.TryParse(txtStudent.Text, out int studentNo))
            {
                if (string.IsNullOrWhiteSpace(txtStudent.Text))
                {
                    MessageBox.Show("Student No cannot be empty");
                }
                else
                {
                    MessageBox.Show("Student No should be a numerical value");
                }
                return;
            }

            //Checks if the Last Name is empty and contais any numbers
            if (string.IsNullOrWhiteSpace(txtLastName.Text) || txtLastName.Text.Any(char.IsDigit))
            {
                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("Last Name cannot be empty");
                }
                else
                {
                    MessageBox.Show("Last Name cannot contain a number");
                }
                return;
            }

            //Cheks if the Age is empty and consists of letters
            if (string.IsNullOrWhiteSpace(txtAge.Text) || !int.TryParse(txtAge.Text, out int age))
            {
                if (string.IsNullOrWhiteSpace(txtAge.Text))
                {
                    MessageBox.Show("Age cannot be empty");
                }
                else
                {
                    MessageBox.Show("Age should be a numerical value");
                }
                return;
            }

            //Checking if the 'SelectedDate' property is 'null'.
            if (datePickerBirthday.SelectedDate == null)
            {
                MessageBox.Show("Please select a valid date");
                return;
            }

            //Checking if the selected item is null.
            if (cbProgram.SelectedItem == null)
            {
                MessageBox.Show("Please select a program");
                return;
            }

            //Checks if the Middle Initial is empty and contais any numbers
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || txtFirstName.Text.Any(char.IsDigit))
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    MessageBox.Show("First Name cannot be empty");
                }
                else
                {
                    MessageBox.Show("First Name cannot contain a number");
                }
                return;
            }

            //Checks if the middle initial is empty and consists of numbers
            if (string.IsNullOrWhiteSpace(txtMiddleInitial.Text) || txtMiddleInitial.Text.Any(char.IsDigit))
{
                if (string.IsNullOrWhiteSpace(txtMiddleInitial.Text))
                {
                    MessageBox.Show("Middle Initial cannot be empty");
                }
                else
                {
                    MessageBox.Show("Middle Initial cannot contain a number");
                }
                return;
            }

            //Checking if the selected item is null.
            if (cbGender.SelectedItem == null)
            {
                MessageBox.Show("Please select a gender");
                return;
            }

            //Cheks if the contact number is empty and consists of letters
            if (string.IsNullOrWhiteSpace(txtContact.Text) || !int.TryParse(txtContact.Text, out int Contact))
            {
                if (string.IsNullOrWhiteSpace(txtContact.Text))
                {
                    MessageBox.Show("Contact No cannot be empty");
                }
                else
                {
                    MessageBox.Show("Contact No should be a numerical value");
                }
                return;
            }

           


            StudentInfoClass.StudentNo = studentNo;
            StudentInfoClass.LastName = txtLastName.Text;
            StudentInfoClass.Age = age;
            StudentInfoClass.Birthday = datePickerBirthday.SelectedDate ?? DateTime.MinValue;
            StudentInfoClass.Program = cbProgram.Text;
            StudentInfoClass.FirstName = txtFirstName.Text;
            StudentInfoClass.MiddleInitial = txtMiddleInitial.Text;
            StudentInfoClass.Gender = (cbGender.Text);
            StudentInfoClass.ContactNo = Contact;

            string fileName = $"{StudentInfoClass.StudentNo}.txt";

            string[] studentInfo = {
                    $"Student No. : {StudentInfoClass.StudentNo}",
                    $"Full Name : {StudentInfoClass.LastName},{StudentInfoClass.FirstName},{StudentInfoClass.MiddleInitial}",
                    $"Birthday : {StudentInfoClass.Birthday.ToString("yyyy-MM-dd")}",
                    $"Program : {StudentInfoClass.Program}",
                    $"Gender: {StudentInfoClass.Gender}",
                    $"Age : {StudentInfoClass.Age}",
                    $"Contact No. : {StudentInfoClass.ContactNo}"
                };

            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string line in studentInfo)
                {
                    writer.WriteLine(line);
                }
            }

            MessageBox.Show("The student is Registered, and the text file is saved in your Documents Folder", "Save Path", MessageBoxButton.OK, MessageBoxImage.Information);

            txtLastName.Clear();
            txtFirstName.Clear();
            txtMiddleInitial.Clear();
            txtStudent.Clear();
            cbProgram.SelectedIndex = -1;
            cbGender.SelectedIndex = -1;
            txtContact.Clear();
            txtAge.Clear();
            datePickerBirthday.SelectedDate = null;
        }

        private void btnRecords_Click(object sender, RoutedEventArgs e)
        {
            FrmStudentRecord frmStudentRecord = new FrmStudentRecord();
            frmStudentRecord.Show();
            this.Close();
        }
    }

    public class StudentInformationClass
    {
        public long SetStudentNo = 0;
        public long SetContactNo = 0;
        public string SetProgram = " ";
        public string SetGender = " ";
        public string SetBirthday = " ";
        public string SetFullName = " ";
        public int SetAge = 0;
    }
}

