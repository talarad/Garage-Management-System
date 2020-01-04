namespace Ex03.GarageLogic
{
    public class RegularCar : Car
    {
        private const float k_FuelTankCapacity = 42f;
        private const eFuelType k_FuelType = eFuelType.Octan98;

        public RegularCar(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            Engine = new FuelEngine(k_FuelType, k_FuelTankCapacity, 0);
        }
    }
}
