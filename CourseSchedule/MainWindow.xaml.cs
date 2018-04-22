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
            //DataLayer DL;
        }

        private void _FindCourseIDButton_Click(object sender, RoutedEventArgs e)
        {


         
            if (isEmptyFindCourseID())
            {
                MessageBox.Show("Please enter courseID");
            }
            else
            {
                string C_ID = "";
                C_ID = _FindCourseIDTextBox.Text;
                int CourseID = Int32.Parse(C_ID);
                c.Id = CourseID;
                c.Number = "23";
                c.Title = "Hello";
                c.Credits = 23;
                c.Description = "Hello there";
                c.Designator = "Balls";




                CC.CList.Add(c);

                if (CC.Find(CourseID) != null)
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

           


        }

        private void _OpenCrsCollectionbutton_Click(object sender, RoutedEventArgs e)
        {
            //Clear the text boxs
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





            OFD.DefaultExt = ".json";
            OFD.Filter = "Json Document (.json)|*.json";
            if (OFD.ShowDialog() == true)
            {
                string filename = OFD.FileName;
                _CourseFileNameTextBox.Text = filename;

                //Opens the file stream to read the JSON
                FileStream courseReader = new FileStream(filename, FileMode.Open, FileAccess.Read);

                //DataContractJsonSerializer Instance
                DataContractJsonSerializer inputCourseSerializer;

                inputCourseSerializer = new DataContractJsonSerializer(typeof(CourseCollection));

                CC = (CourseCollection)inputCourseSerializer.ReadObject(courseReader);

                //This can be used to populate fields or list views
               /* foreach (var listCourses in CC.CList)
                {
                    _CourseIDDataTextBox.Text = listCourses.Id.ToString();
                }*/
                courseReader.Close();

            }

           

               Console.WriteLine("CourseCollection JSON Read \n");

             

            
        }//End

        public bool isEmptyFindCourseID()
        {
            if (_FindCourseIDTextBox.Text == "")
            {
                return true;
            }
            return false;
        }//End isEmpty 

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled == true)
            {
                MessageBox.Show("Please enter numbers Only");
            }
        }

      
    }
}
