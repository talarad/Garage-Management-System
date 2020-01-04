using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private const int k_MaxAirPressure = 33;
        private const int k_NumOfWheels = 2;
        private const string k_LicenseType = "license type";
        private const string k_EngineVolume = "engine volume";
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            Wheels = new Wheels(k_MaxAirPressure, k_NumOfWheels);
        }

        public Motorcycle(string i_ModelName, string i_LicenseNumber, eLicenseType i_LicenseType, int i_EngineVolume) : base(i_ModelName, i_LicenseNumber)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
        }

        protected override void CreateMetadata()
        {
            base.CreateMetadata();
            MetaData.Add(k_LicenseType, typeof(eLicenseType));
            MetaData.Add(k_EngineVolume, typeof(int));
        }

        public override void Init(Dictionary<string, object> i_valuesToInit)
        {
            base.Init(i_valuesToInit);
            m_LicenseType = (eLicenseType)i_valuesToInit[k_LicenseType];
            m_EngineVolume = (int)i_valuesToInit[k_EngineVolume];
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
            set { m_EngineVolume = value; }
        }

        public eLicenseType LicenceType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public override string ToString()
        {
            return string.Format("{3}{0}{3}licence type: {1}{3}engine volume: {2}", base.ToString(), m_LicenseType.ToString(), m_EngineVolume, Environment.NewLine);
        }
    }

    public enum eLicenseType
    {
        A = 1,
        AB = 2,
        A2 = 3,
        B1 = 4
    }
}