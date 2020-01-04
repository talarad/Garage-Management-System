using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private const string k_ModelNameDescription = "Model name";
        private Dictionary<string, Type> m_Metadata = new Dictionary<string, Type>();
        private string m_ModelName;
        private string m_LicenseNumber;
        private Engine m_Engine;
        private Wheels m_Wheels;
        private bool m_IsInitialise = false;

        public Vehicle(string i_LicenseNumber)
        {
            m_LicenseNumber = i_LicenseNumber;
            CreateMetadata();
        }

        public Vehicle(string i_ModelName, string i_LicenseNumber) : this(i_LicenseNumber)
        {
            m_ModelName = i_ModelName;
        }

        protected virtual void CreateMetadata()
        {
            m_Metadata.Add(k_ModelNameDescription, typeof(string));
        }

        public virtual void Init(Dictionary<string, object> i_ValuesToInit)
        {
            if (!m_IsInitialise)
            {
                m_ModelName = (string)i_ValuesToInit[k_ModelNameDescription];
                m_IsInitialise = true;
            }
        }

        public Dictionary<string, Type> MetaData
        {
            get { return m_Metadata; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
        }

        public Engine Engine
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        public string License
        {
            get { return m_LicenseNumber; }
        }

        public float EnergyLeftPrecentage
        {
            get { return this.Engine.EnergyLeftPrecentage; }
        }

        public Wheels Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public override string ToString()
        {
            return string.Format("License Number: {0}{2}Model name: {1}", m_LicenseNumber, m_ModelName, Environment.NewLine);
        }
    }
}
