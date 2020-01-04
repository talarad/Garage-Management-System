namespace Ex03.GarageLogic
{
    public class RegularMotorcycle : Motorcycle
    {
        private const float k_FuelTankCapacity = 5.5f;
        private const eFuelType k_FuelType = eFuelType.Octan95;

        public RegularMotorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            Engine = new FuelEngine(k_FuelType, k_FuelTankCapacity, 0);
        }
    }
}
