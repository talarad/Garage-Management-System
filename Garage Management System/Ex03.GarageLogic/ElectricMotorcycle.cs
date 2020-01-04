namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_BatteryCapacity = 2.7f;

        public ElectricMotorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            Engine = new ElectricEngine(k_BatteryCapacity, 0);
        }
    }
}
