//******************************************************
// File: MainWindow.cs
//
// Purpose: Using College Course Libarary1 DLL to
// Read Json files and displaying them in List views
// and texts views
//  
//
// Written By: Brian J. Recuero 
//
// Compiler: Visual Studio 2015
//
//******************************************************


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
        #region creating Instance variables
        DataLayer DL = new DataLayer();
        CourseCollection CC = new CourseCollection();
        ProfessorCollection PC = new ProfessorCollection();
        Course c = new Course();
        OpenFileDialog OFD = new OpenFileDialog();
        string path = @"C:\Users\Brian Recuero\Source\Repos\CollegeCourseScheduleV2\CourseSchedule\bin";
        #endregion

        #region Methods
        //****************************************************
        // Method: MainWindow()
        // Sets all TextBoxes to read only at run time. And
        // Sets the initialDirectory Path for open file dialog 
        //
        // Purpose: To show the heading for screen output.
        //****************************************************
        public MainWindow()
        {
            InitializeComponent();
            _CourseFileNameTextBox.IsReadOnly = true;
            _ProfessorsFileNameTextBox.IsReadOnly = true;
            _CourseIDDataTextBox.IsReadOnly = true;
            _CreditsDataTextBox.IsReadOnly = true;
            _DescriptionDataTextBox.IsReadOnly = true;
            _DesignatorDataTextBox.IsReadOnly = true;
            _NumberDataTextBox.IsReadOnly = true;
            _TitleDataTextBox.IsReadOnly = true;
            OFD.InitialDirectory = path;
            
        }//End Main Window


        //****************************************************
        // Method: FindCourseIDButton_Click()
        //
        // Purpose: When the button is pressed. Course collection file is desirialized(Read).
        // The user can choose to search by Id from the read file.
        // If the id is found then its displayed to the Textbox(ReadOnly)
        // else a pop message is displayed telling the usr that the ID was not found.
        //****************************************************
        private void _FindCourseIDButton_Click(object sender, RoutedEventArgs e)
        {
            //Initializing Variables
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


                   
                }
                else
                {
                    MessageBox.Show("Could Not Find");

                }
                courseReader.Close();
            }


        }//End _FindCourseIDButton_Click

        //****************************************************
        // Method: _OpenCrsCollectionbutton_Click()
        //
        // Purpose: When the button is pressed. Course collection file is desirialized(Read)
        // The file name is then displayed in the Text Box
        //****************************************************

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

               


        }//End _OpenCrsCollectionbutton_Click

        //****************************************************
        // Method: _FindDesignatorAndNumberButton_Click()
        // Purpose: When the button is pressed. Course collection file is desirialized(Read).
        // The user can search by Designator and Number from the read file.
        // If both Disignator and Number are found then its displayed to the Textbox(ReadOnly)
        // else a pop message is displayed telling the usr that the ID was not found.
        //****************************************************
        private void _FindDesignatorAndNumberButton_Click(object sender, RoutedEventArgs e)
        {
            string C_ID = "";
            string C_Num = "";
            string C_Title = "";
            string C_Credits = "";
            string C_Desc = "";
            string C_Desi = "";
          

            _CourseIDDataTextBox.Clear();
            _NumberDataTextBox.Clear();
            _TitleDataTextBox.Clear();
            _CreditsDataTextBox.Clear();
            _DescriptionDataTextBox.Clear();
            _DesignatorDataTextBox.Clear();

            if (isEmptyFindDesAndNumber())
            {
                MessageBox.Show("Please enter Designator or Number");
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



                //If Id is null the course does not exsis
                if (CC.Find(_FindDesignatorTextBox.Text, _FindNumberTextBox.Text) != null)
                {
                    // Grabing data from Course Collection List. And populating the TextBox in Course Data
                    _CourseIDDataTextBox.Text = CC.Find(_FindDesignatorTextBox.Text, _FindNumberTextBox.Text).Id.ToString();
                    _DesignatorDataTextBox.Text = CC.Find(_FindDesignatorTextBox.Text, _FindNumberTextBox.Text).Designator.ToString();
                    _NumberDataTextBox.Text = CC.Find(_FindDesignatorTextBox.Text, _FindNumberTextBox.Text).Number.ToString();
                    _TitleDataTextBox.Text = CC.Find(_FindDesignatorTextBox.Text, _FindNumberTextBox.Text).Title.ToString();
                    _CreditsDataTextBox.Text = CC.Find(_FindDesignatorTextBox.Text, _FindNumberTextBox.Text).Credits.ToString();
                    _DescriptionDataTextBox.Text = CC.Find(_FindDesignatorTextBox.Text, _FindNumberTextBox.Text).Description.ToString();


                   
                }
                else
                {
                    MessageBox.Show("Could Not Find");

                }
                courseReader.Close();
            }
        }// End _FindDesignatorAndNumberButton_Click

        //****************************************************
        // Method: _ProfessorCollectionButton_Click()
        //
        // Purpose: When the button is pressed. Course collection file is desirialized(Read)
        // The file name is then displayed in the Text Box and all the data is then 
        // Displayed in the list view.
        //****************************************************

        private void _ProfessorCollectionButton_Click(object sender, RoutedEventArgs e)
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
                _ProfessorsFileNameTextBox.Text = filename;

                //Opens the file stream to read the JSON
                FileStream professorReader = new FileStream(filename, FileMode.Open, FileAccess.Read);

                //DataContractJsonSerializer Instance
                DataContractJsonSerializer inputProfessorSerializer;
                inputProfessorSerializer = new DataContractJsonSerializer(typeof(ProfessorCollection));

                //Populate the ProfessorCollection window member variable with data from the selected file (deserialize into that member variable). 
                PC = (ProfessorCollection)inputProfessorSerializer.ReadObject(professorReader);

                //This can be used to populate fields or list views
                foreach (var PList in PC.PList)
                {
                    listView.Items.Add(PList);

                }
                professorReader.Close();

            }


        }

        //****************************************************
        // Method: isEmptyFindCourseID()
        // Purpose: If the Find Course ID textBox is empty 
        // return true. 
        //****************************************************
        public bool isEmptyFindCourseID()
        {
            if (_FindCourseIDTextBox.Text == "")
            {
                return true;
            }
            return false;
        }//End isEmptyFindCourseID 

        //****************************************************
        // Method: isEmptyFindDesAndNumber()
        // Purpose: If the Find Course Des and Num textBox is empty 
        // return true. 
        //****************************************************
        public bool isEmptyFindDesAndNumber()
        {
            if (_FindDesignatorTextBox.Text == "" || _FindNumberTextBox.Text == "")
            {
                return true;
            }
            return false;
        }//End isEmptyFindDesAndNumber

        //****************************************************
        // Method: NumberValidationTextBox()
        // Purpose: Using Regexs checks if the data being inputted
        // is a number. Else a message is displayed telling user
        // that to only input numbers.
        // 
        //****************************************************
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
    #endregion
}
