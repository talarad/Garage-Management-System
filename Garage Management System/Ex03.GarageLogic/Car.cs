using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const int k_MaxAirPressure = 30;
        private const int k_NumOfWheels = 4;
        private const string k_CarColorDescription = "Color of the car";
        private const string k_NumOfDoorsDescription = "Number of doors";
        private eColor m_CarColor;
        private eNumberOfDoors m_NumOfDoors;

        public Car(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            Wheels = new Wheels(k_MaxAirPressure, k_NumOfWheels);
        }

        public Car(string i_ModelName, string i_LicenseNumber, eColor i_CarColor, eNumberOfDoors i_NumOfDoors) : base(i_ModelName, i_LicenseNumber)
        {
            m_CarColor = i_CarColor;
            m_NumOfDoors = i_NumOfDoors;
        }

        protected override void CreateMetadata()
        {
            base.CreateMetadata();
            MetaData.Add(k_CarColorDescription, typeof(eColor));
            MetaData.Add(k_NumOfDoorsDescription, typeof(eNumberOfDoors));
        }

        public override void Init(Dictionary<string, object> i_ValuesToInit)
        {
            base.Init(i_ValuesToInit);
            m_CarColor = (eColor)i_ValuesToInit[k_CarColorDescription];
            m_NumOfDoors = (eNumberOfDoors)i_ValuesToInit[k_NumOfDoorsDescription];
        }

        public eColor CarColor
        {
            get { return m_CarColor; }
        }

        public eNumberOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
        }

        public override string ToString()
        {
            return string.Format("{0}{3}car's color: {1}{3}number of doors: {2}", base.ToString(), m_CarColor.ToString(), m_NumOfDoors.ToString(), Environment.NewLine);
        }
    }

    public enum eColor
    {
        Yellow = 1,
        White = 2,
        Black = 3,
        Blue = 4
    }

    public enum eNumberOfDoors
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }
}
