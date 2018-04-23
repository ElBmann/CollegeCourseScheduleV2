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
using CollegeCourseLibrary1;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace CourseSchedule
{

    public partial class MainWindow : Window
    {
        DataLayer DL = new DataLayer();
        CourseCollection CC = new CourseCollection();
        Course c = new Course();
        OpenFileDialog OFD = new OpenFileDialog();
        string path = @"C:\Users\Brian Recuero\Source\Repos\CollegeCourseScheduleV2\CourseSchedule\bin";

        public MainWindow()
        {
            InitializeComponent();
            _CourseFileNameTextBox.IsReadOnly = true;
            OFD.InitialDirectory = path;
            
        }//End Main Window

        private void _FindCourseIDButton_Click(object sender, RoutedEventArgs e)
        {
            string C_ID = "";
            string C_Num = "";
            string C_Title = "";
            string C_Credits = "";
            string C_Desc = "";
            string C_Desi = "";
            string searchID = "";

            _CourseIDDataTextBox.Clear();
            _NumberDataTextBox.Clear();
            _TitleDataTextBox.Clear();
            _CreditsDataTextBox.Clear();
            _DescriptionDataTextBox.Clear();
            _DesignatorDataTextBox.Clear();

            if (isEmptyFindCourseID())
            {
                MessageBox.Show("Please enter courseID");
            }
            else
            {
                
                string filename = "courses.json";

                      //Opens the file stream to read the JSON
                FileStream courseReader = new FileStream(filename, FileMode.Open, FileAccess.Read);

                //DataContractJsonSerializer Instance
                DataContractJsonSerializer inputCourseSerializer;
                inputCourseSerializer = new DataContractJsonSerializer(typeof(CourseCollection));

                //Populate the CourseCollection window member variable with data from the selected file (deserialize into that member variable). 
                CC = (CourseCollection)inputCourseSerializer.ReadObject(courseReader);

                //This can be used to populate fields or list views
                foreach (var listCourses in CC.CList)
                {
                    C_ID = listCourses.Id.ToString();
                    C_Num = listCourses.Number.ToString();
                    C_Title = listCourses.Title.ToString();
                    C_Credits = listCourses.Credits.ToString();
                    C_Desc = listCourses.Description.ToString();
                    C_Desi = listCourses.Designator.ToString();
                    //  _CourseIDDataTextBox.Text = listCourses.Id.ToString();

                  
                   
                    int credits = Int32.Parse(C_Credits);
                    int course_ID = Int32.Parse(C_ID);
                    c.Id = course_ID;
                    c.Number = C_Num;
                    c.Title = C_Title;
                    c.Credits = credits;
                    c.Description = C_Desc;
                    c.Designator = C_Desi;


                }


                CC.CList.Add(c);

                //Converts the sting into an int
                searchID = _FindCourseIDTextBox.Text;
                int searchCourseID = Int32.Parse(searchID);


                //If Id is null the course does not exsis
                if (CC.Find(searchCourseID) != null)
                {
                    // Grabing data from Course Collection List. And populating the TextBox in Course Data
                    _CourseIDDataTextBox.Text = CC.Find(searchCourseID).Id.ToString();
                    _DesignatorDataTextBox.Text = CC.Find(searchCourseID).Designator.ToString();
                    _NumberDataTextBox.Text = CC.Find(searchCourseID).Number.ToString();
                    _TitleDataTextBox.Text = CC.Find(searchCourseID).Title.ToString();
                    _CreditsDataTextBox.Text = CC.Find(searchCourseID).Credits.ToString();
                    _DescriptionDataTextBox.Text = CC.Find(searchCourseID).Description.ToString();


                   // MessageBox.Show(c.ToString());
                }
                else
                {
                    MessageBox.Show("Could Not Find");

                }
                courseReader.Close();
            }


        }//End _FindCourseIDButton_Click

        private void _OpenCrsCollectionbutton_Click(object sender, RoutedEventArgs e)
        {
            //Clear the text boxs Once Searching for the JSON file
            _FindCourseIDTextBox.Clear();
            _FindDesignatorTextBox.Clear();
            _FindNumberTextBox.Clear();
            _CourseIDDataTextBox.Clear();
            _CreditsDataTextBox.Clear();
            _DesignatorDataTextBox.Clear();
            _DescriptionDataTextBox.Clear();
            _FindCourseIDTextBox.Clear();
            _NumberDataTextBox.Clear();
            _TitleDataTextBox.Clear();



            //Setting up the default type once you open the file dialog 

            OFD.DefaultExt = ".json";
            OFD.Filter = "Json Document (.json)|*.json";
            OFD.Title = "Open Course Collection From JSON";
            if (OFD.ShowDialog() == true)
            {
                string filename = OFD.FileName;
                _CourseFileNameTextBox.Text = filename;

                //Opens the file stream to read the JSON
                FileStream courseReader = new FileStream(filename, FileMode.Open, FileAccess.Read);

                //DataContractJsonSerializer Instance
                DataContractJsonSerializer inputCourseSerializer;
                inputCourseSerializer = new DataContractJsonSerializer(typeof(CourseCollection));

                //Populate the CourseCollection window member variable with data from the selected file (deserialize into that member variable). 
                CC = (CourseCollection)inputCourseSerializer.ReadObject(courseReader);

                //This can be used to populate fields or list views
               /* foreach (var listCourses in CC.CList)
                {
                    _CourseIDDataTextBox.Text = listCourses.Id.ToString();
                }*/
                courseReader.Close();

            }

               Console.WriteLine("CourseCollection JSON Read \n");


        }//End _OpenCrsCollectionbutton_Click

        private void _FindDesignatorAndNumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (isEmptyFindDesAndNumber())
            {
                MessageBox.Show("Please enter Designator and Number");
            }
            else
            {
                string C_Des = "";
                string C_Num = "";
                C_Des = _FindDesignatorTextBox.Text;
                C_Num = _FindNumberTextBox.Text;
                c.Id = 23;
                c.Number = C_Num;
                c.Title = "Hello";
                c.Credits = 23;
                c.Description = "Hello there";
                c.Designator = C_Des;


                CC.CList.Add(c);

                if (CC.Find(C_Des, C_Num) != null)
                {
                    // Grabing data from Course Collection List. And populating the TextBox in Course Data
                    _CourseIDDataTextBox.Text = c.Id.ToString();
                    _DesignatorDataTextBox.Text = c.Designator.ToString();
                    _NumberDataTextBox.Text = c.Number.ToString();
                    _TitleDataTextBox.Text = c.Title.ToString();
                    _CreditsDataTextBox.Text = c.Credits.ToString();
                    _DescriptionDataTextBox.Text = c.Description.ToString();


                    MessageBox.Show(c.ToString());
                }
                else
                {
                    MessageBox.Show("Could Not Find");

                }
            }
        }// End _FindDesignatorAndNumberButton_Click

        public bool isEmptyFindCourseID()
        {
            if (_FindCourseIDTextBox.Text == "")
            {
                return true;
            }
            return false;
        }//End isEmptyFindCourseID 

        public bool isEmptyFindDesAndNumber()
        {
            if (_FindDesignatorTextBox.Text == "" || _FindNumberTextBox.Text == "")
            {
                return true;
            }
            return false;
        }//End isEmptyFindDesAndNumber

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled == true)
            {
                MessageBox.Show("Please enter numbers Only");
            }
        }//End NumberValidationTextBox

     

    }//End Main Window Class 
}
