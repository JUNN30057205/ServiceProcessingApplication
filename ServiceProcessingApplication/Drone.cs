using System.Globalization;

namespace ServiceProcessingApplication
{
    class Drone
    {
        /* 6.1 Create a separate class file to hold the data items of the Drone
         *     Use separate getter and setter methods, ensure the attribute are private and the accessor methods are public 
         *     And a display method that returns a string for Client Name and Service Cost.
         *     And suitable code to the Client Name and Service Problem accessor methods so that data is formated as Title case or Sentence case.
         *     Save the class as "Drone.cs".
         */
        private string ClientName;
        private string DroneModel;
        private string ServiceProblem;
        private double ServiceCost;
        private int ServiceTag;

        public Drone() { }

        public string GetClientName() { return ClientName; }
        public string GetDroneModel() { return DroneModel; }
        public string GetServiceProblem() { return ServiceProblem; }
        public double GetServiceCost() { return ServiceCost; }
        public int GetServiceTag() { return ServiceTag; }

        public void SetClientName(string newClientName)
        {
            if (newClientName == null)
                ClientName = "Unknown";
            else
                ClientName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newClientName);
        }
        public void SetDroneModel(string newDroneModel)
        {
            DroneModel = newDroneModel;
        }
        public void SetServiceProblem(string newProblem)
        {
            ServiceProblem = newProblem;
        }
        public void SetServiceCost(double newServiceCost)
        {
            if (newServiceCost <= 0)
                ServiceCost = 0.99;
            else
                ServiceCost = newServiceCost;
        }
        public void SetServiceTag(int newTag)
        {
            ServiceTag = newTag;
        }
        public string DisplayFinishedService()
        {
            return GetClientName() + ": $" + GetServiceCost();
        }
    }
}
