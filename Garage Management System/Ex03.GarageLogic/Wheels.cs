using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheels
    {
        private readonly float r_MaxAirPressure;
        private Wheel[] m_Wheels;

        public Wheels(float i_MaxAirPressure, int i_NumOfWheels)
        {
            m_Wheels = new Wheel[i_NumOfWheels];
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void InitAllWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            for (int i = 0; i < m_Wheels.Length; i++)
            {
                m_Wheels[i] = new Wheel(i_ManufacturerName, i_CurrentAirPressure, r_MaxAirPressure);
            }
        }

        public void InflateAllToMax()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.Inflation(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public Wheel GetWheel(int i_WheelIndex)
        {
            return m_Wheels[i_WheelIndex];
        }

        public int Length
        {
            get { return m_Wheels.Length; }
        }

        public override string ToString()
        {
            StringBuilder garageVehicleToString = new StringBuilder();
            garageVehicleToString.Append(string.Format("Wheels data:{0}", Environment.NewLine));
            for (int i = 0; i < m_Wheels.Length; i++)
            {
                garageVehicleToString.Append(string.Format("Wheel number {0}:{2} {1}{2}", i + 1, m_Wheels[i].ToString(), Environment.NewLine));
            }

            return garageVehicleToString.ToString();
        }
    }
}
