using HandyControl.Controls;
using HandyControl.Tools;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextBox = System.Windows.Controls.TextBox;
using Window = System.Windows.Window;

// ID: 3005205
//Name: Jun Sumida
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
            TextBlock statusMS = StatusMS;
            statusMS.Text = "*Status Message";
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

                IncrementServiceTag(GetService_Tag());

                if (GetServicePrioriry() == 1)
                {
                    RegularService.Enqueue(addDrone);
                    DisplayRegularService();
                    StatusMS.Text ="*New Items added to Regular Queue";
                    
                }
                if(GetServicePrioriry() == 2)
                {
                //6.6	Before a new service item is added to the Express Queue the service cost must be increased by 15%.
                    addDrone.SetServiceCost(double.Parse(TextBoxServiceCost.Text) * 1.15);
                    ExpressService.Enqueue(addDrone);
                    DisplayExpressService();
                    StatusMS.Text = "*New Items added to Express Queue";
                }
                //Clear textboxes
                ClearTextBoxes();                
            } else
            {
                StatusMS.Text = "*Fill all items in the Textboxes";
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
        //6.10	Create a custom keypress method to ensure the Service Cost textbox can only
        //      accept a double value with one decimal point. (Regex: Regular Expressions)
        private void TextBoxServiceCost_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            //^:start, \d+:0-9, (\.\d{0,1}):only accept one decimal, ?:0or1, $:finish...
            Regex regular = new (@"^\d+(\.\d{0,1})?$");
            e.Handled = !regular.IsMatch(((TextBox)sender).Text.Insert(((TextBox)sender).SelectionStart, e.Text));
        }
        
        //6.11	Create a custom method to increment the service tag control,
        //      this method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        private void IncrementServiceTag(Xceed.Wpf.Toolkit.IntegerUpDown service_Tag)
        {
            int value = (int)service_Tag.Value;
            int currentTag = value;
            currentTag += 10;
            Service_Tag.Value = currentTag;
        }

        private Xceed.Wpf.Toolkit.IntegerUpDown GetService_Tag()
        {
            return Service_Tag;
        }

        //6.12	Create a mouse click method for the regular service ListView that
        //      will display the Client Name and Service Problem in the related textboxes.
        private void ListViewRegular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListViewRegular.SelectedIndex != -1)
            {
                int index = ListViewRegular.SelectedIndex;
                TextBoxClientName.Text = RegularService.ElementAt(index).GetClientName();
                TextBoxDroneModel.Text = RegularService.ElementAt(index).GetDroneModel();
                TextBoxServiceProblem.Text = RegularService.ElementAt(index).GetServiceProblem();
                TextBoxServiceCost.Text = RegularService.ElementAt(index).GetServiceCost().ToString();
            }
            else
            {
                ListViewRegular.UnselectAll();
            }
        }

        //6.13	Create a mouse click method for the express service ListView that
        //      will display the Client Name and Service Problem in the related textboxes.
        private void ListViewExpress_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListViewExpress.SelectedIndex != -1)
            {
                int index = ListViewExpress.SelectedIndex;
                TextBoxClientName.Text = ExpressService.ElementAt(index).GetClientName();
                TextBoxDroneModel.Text = ExpressService.ElementAt(index).GetDroneModel();
                TextBoxServiceProblem.Text = ExpressService.ElementAt(index).GetServiceProblem();
                TextBoxServiceCost.Text = ExpressService.ElementAt(index).GetServiceCost().ToString();                
            }
            else 
            {
                ListViewExpress.UnselectAll();
            }            
        }

        //6.14	Create a button click method that will remove a service item from the regular ListView and dequeue the regular service Queue<T> data structure.
        //      The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items.
        private void FinishedRegular_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (RegularService.Count > 0)
            {                
                FinishedList.Add(RegularService.Dequeue());
                DisplayRegularService();
                DisplayFinishedService();
                ClearTextBoxes();
                StatusMS.Text = "*Regular Service Removed from the Listview and Added to the Finished ListBox.";
            }

        }

        //6.15  Create a button click method that will remove a service item from the express ListView and deueue the express service Queue<T> data structure.
        //      The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items.
        private void FinishedExpress_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ExpressService.Count > 0)
            {
                FinishedList.Add(ExpressService.Dequeue());
                DisplayExpressService();
                DisplayFinishedService();
                ClearTextBoxes();
                StatusMS.Text = "*Express Service Removed from the Listview and Added to the Finished ListBox.";                
                
            }
        }

        private void DisplayFinishedService()
        {
            ListBoxFinishedService.Items.Clear();
            foreach (Drone drone in FinishedList)
            {
                ListBoxFinishedService.Items.Add(drone.DisplayFinishedService());
            }
        }

        //6.16  Create a double mouse click method that will delete a service item from the finished ListBox and
        //      and remove the same item from the List<T>.
        private void ListBoxFinishedService_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = ListBoxFinishedService.SelectedIndex;
            FinishedList.RemoveAt(index);
            DisplayFinishedService();
            StatusMS.Text = "*Finished Service Items Deleted.";
        }

        //6.17  Create a custom method that will clear all the textboxes after each service item has been added.
        private void ClearTextBoxes()
        {
            TextBoxClientName.Clear();
            TextBoxDroneModel.Clear();
            TextBoxServiceProblem.Clear();
            TextBoxServiceCost.Clear();
            
        }        
    }  
}
