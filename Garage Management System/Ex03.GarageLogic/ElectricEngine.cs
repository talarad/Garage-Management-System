using System;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergyAmount, float i_CurrentEnergyAmount) : base(i_MaxEnergyAmount, i_CurrentEnergyAmount)
        {
        }

        public void Recharge(float i_NumOfHoursToCharge)
        {
            if (this.NumOfHoursLeftInBattery + i_NumOfHoursToCharge <= this.MaxHoursForFullyChargedBattery)
            {
                CurrentEnergyAmount += i_NumOfHoursToCharge;
            }
            else
            {
                throw new ValueOutOfRangeException(0, this.MaxEnergyAmount);
            }
        }

        public float NumOfHoursLeftInBattery
        {
            get { return CurrentEnergyAmount; }
        }

        public float MaxHoursForFullyChargedBattery
        {
            get { return MaxEnergyAmount; }
        }

        public override string ToString()
        {
            return string.Format("the battery has: {0} hours left{2}The maximal capacity of the battery: {1}", CurrentEnergyAmount, MaxEnergyAmount, Environment.NewLine);
        }
    }
}