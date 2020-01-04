using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public FuelEngine(eFuelType i_FuelType, float i_MaxEnergyAmount, float i_CurrentEnergyAmount) : base(i_MaxEnergyAmount, i_CurrentEnergyAmount)
        {
            m_FuelType = i_FuelType;
        }

        public void Refuel(float i_FuelAmountToFillInLiters, eFuelType i_FuelType)
        {
            if (i_FuelType != m_FuelType)
            {
                throw new ArgumentException("Wrong fuel type!");
            }
            else if (this.CurrentFuelLiters + i_FuelAmountToFillInLiters > this.MaxFuelLiters)
            {
                throw new ValueOutOfRangeException(0, this.MaxFuelLiters);
            }

            CurrentEnergyAmount += i_FuelAmountToFillInLiters;
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
        }

        public float CurrentFuelLiters
        {
            get { return CurrentEnergyAmount; }
        }

        public float MaxFuelLiters
        {
            get { return MaxEnergyAmount; }
        }

        public override string ToString()
        {
            return string.Format("the engine fuel type is: {0}{3}Amaunt of liters in the fuel tank: {1}{3}Fuel tank maximal capacity: {2} liters", m_FuelType.ToString(), CurrentEnergyAmount, MaxEnergyAmount, Environment.NewLine);
        }
    }

    public enum eFuelType
    {
        Soler = 1,
        Octan95 = 2,
        Octan96 = 3,
        Octan98 = 4
    }
}