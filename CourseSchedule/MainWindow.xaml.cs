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

namespace CourseSchedule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataLayer DL = new DataLayer();
        CourseCollection CC = new CourseCollection();
        Course c = new Course();
        OpenFileDialog OFD = new OpenFileDialog();

        
        public MainWindow()
        {
            InitializeComponent();
            OFD.InitialDirectory = @"C:\Users\Brian Recuero\Source\Repos\CollegeCourseScheduleV2\CourseSchedule\bin";
            _CourseFileNameTextBox.IsReadOnly = true;
           

            //DataLayer DL;
        }

        private void _FindCourseIDButton_Click(object sender, RoutedEventArgs e)
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

            if (CC.Find(CourseID) !=null)
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

        private void _OpenCrsCollectionbutton_Click(object sender, RoutedEventArgs e)
        {

           
            OFD.DefaultExt = ".json";
            OFD.Filter = "Json Document (.json)|*.json";
            
            if (OFD.ShowDialog() == true)
            {
                string filename = OFD.FileName;
                _CourseFileNameTextBox.Text = filename;
            }
               
            {

            }
        }
    }
    }
