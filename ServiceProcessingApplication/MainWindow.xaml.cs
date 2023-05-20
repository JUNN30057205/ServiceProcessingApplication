using HandyControl.Controls;
using HandyControl.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using Window = System.Windows.Window;

// ID: 3005205
//Name:
//Asessment 3
//Version 1

namespace ServiceProcessingApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //6.2 Create a globel List<T> of type Drone called "FenishedList"
        List<Drone> FinishedList = new List<Drone>();
        //6.3 Create a global List<T> of type of Drone called "RegularService"
        Queue<Drone> RegularService = new Queue<Drone>();
        //6.4 Create a global List<T> of type of Drone called "ExpressService"
        Queue<Drone> ExpressService = new Queue<Drone>();

        //6.5 Create a button method called "AddNewItem" that will add a new service item to a Queue<> based on the priority
        //    Use Textboxes for the Client Name, Drone Model, Service Problem, and Service Cost.
        //    Use a numeric Up/Down control for the Service Tag.
        //    The new service item will be added to the appropriate Queue based on the Priority radio button.
        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxClientName.Text) &&
                !string.IsNullOrEmpty(TextBoxDroneModel.Text) &&
                !string.IsNullOrEmpty(TextBoxServiceProblem.Text) &&
                !string.IsNullOrEmpty(TextBoxServiceCost.Text))
            {
                Drone addDrone = new Drone();
                addDrone.SetClientName(TextBoxClientName.Text);
                addDrone.SetDroneModel(TextBoxDroneModel.Text);
                addDrone.SetServiceProblem(TextBoxServiceProblem.Text);
                addDrone.SetServiceCost(double.Parse(TextBoxServiceCost.Text));
                addDrone.SetServiceTag(int.Parse(Service_Tag.Text));

               
                if (GetServicePrioriry() == 1)
                {
                    RegularService.Enqueue(addDrone);
                    DisplayRegularService();
                    //MessageBox.Show("New Items added to Regular queue");
                    
                }
                if(GetServicePrioriry() == 2)
                {
                //6.6	Before a new service item is added to the Express Queue the service cost must be increased by 15%.
                    addDrone.SetServiceCost(double.Parse(TextBoxServiceCost.Text) * 1.15);
                    ExpressService.Enqueue(addDrone);
                    DisplayExpressService();
                    
                }
                //clear textboxes
                TextBoxClientName.Text = string.Empty;
                TextBoxDroneModel.Text = string.Empty;
                TextBoxServiceProblem.Text = string.Empty;
                TextBoxServiceCost.Text = string.Empty;
            }
        }
        //6.7	Create a custom method called “GetServicePriority” which returns the value of the priority radio group.
        //      This method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        private int GetServicePrioriry()
        {
            int prioriry = 0;
            if(RadioButtonRegular.IsChecked == true)
            {
                prioriry = 1;
            }
            if(RadioButtonExpress.IsChecked == true)
            {
                prioriry = 2;
            }

            return prioriry;
        }
        //6.8	Create a custom method that will display all the elements in the RegularService queue.
        //      The display must use a List View and with appropriate column headers.
        private void DisplayRegularService()
        {        
            ListViewRegular.Items.Clear();
            foreach(Drone value in RegularService)
            {
                ListViewRegular.Items.Add(new
                {
                    ClientName = value.GetClientName(),
                    DroneModel = value.GetDroneModel(),
                    ServiceProblem = value.GetServiceProblem(),
                    Cost = value.GetServiceCost(),
                    Tag = value.GetServiceTag(),
                });
            }
        }

        //6.9	Create a custom method that will display all the elements in the ExpressService queue.
        //      The display must use a List View and with appropriate column headers.
        private void DisplayExpressService()
        {
            ListViewExpress.Items.Clear();
            foreach(Drone value in ExpressService)
            {
                ListViewExpress.Items.Add(new
                {
                    ClientName = value.GetClientName(),
                    DroneModel = value.GetDroneModel(),
                    ServiceProblem = value.GetServiceProblem(),
                    Cost = value.GetServiceCost(),
                    Tag = value.GetServiceTag(),
                });
            }
        }
        //6.10	Create a custom keypress method to ensure the Service Cost textbox can only accept a double value with one decimal point.

    }
}
