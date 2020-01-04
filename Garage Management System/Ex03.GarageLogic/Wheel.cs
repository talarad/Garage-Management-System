using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const string k_ManufacturerNameDescription = "manufacturer name";
        private const string k_CurrentAirPressureDescription = "current air pressure";
        private string m_ManufacturerName;
        private float m_CurrentAirPressure = 0;
        private float m_MaxAirPressure;
       
        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_MaxAirPressure = i_MaxAirPressure;
            Inflation(i_CurrentAirPressure);
        }

        public void Inflation(float i_HowMuchAirToInflate)
        {
            if (i_HowMuchAirToInflate + m_CurrentAirPressure <= m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_HowMuchAirToInflate;
            }
            else
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressure);
            }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
        }

        public override string ToString()
        {
            return string.Format("Manufacturer name: {0}, cuurent air pressure: {1}, maximal air pressure: {2}", m_ManufacturerName, m_CurrentAirPressure, m_MaxAirPressure);
        }
    }
}
