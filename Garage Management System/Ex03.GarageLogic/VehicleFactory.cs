namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public static Vehicle CreateVehicle(eVehicles i_TypeOfCarToCreate, string i_License)
        {
            Vehicle createdVehicle = null;

            switch (i_TypeOfCarToCreate)
            {
                case eVehicles.RegularCar:
                    createdVehicle = new RegularCar(i_License);
                    break;
                case eVehicles.ElectricCar:
                    createdVehicle = new ElectricCar(i_License);
                    break;
                case eVehicles.RegularMotorcycle:
                    createdVehicle = new RegularMotorcycle(i_License);
                    break;
                case eVehicles.ElectricMotorcycle:
                    createdVehicle = new ElectricMotorcycle(i_License);
                    break;
                case eVehicles.Truck:
                    createdVehicle = new Truck(i_License);
                    break;
            }

            return createdVehicle;
        }
    }

    public enum eVehicles
    {
        RegularCar = 1,
        ElectricCar = 2,
        RegularMotorcycle = 3,
        ElectricMotorcycle = 4,
        Truck = 5
    }
}
