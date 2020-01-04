namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_BatteryCapacity = 2.7f;

        public ElectricCar(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            Engine = new ElectricEngine(k_BatteryCapacity, 0);
        }
    }
}