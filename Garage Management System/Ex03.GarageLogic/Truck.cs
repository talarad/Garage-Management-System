using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_MaxAirPressure = 32;
        private const int k_NumOfWeels = 12;
        private const string k_IsCarryDangerousMaterialsDescription = "if there are dangerous materials (true/false)";
        private const string k_MaxCarryWeight = "maximal weight that the truck can carry";
        private const float k_FuleTankCapacity = 135f;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private bool m_IsCarryDangerousMaterials;
        private float m_MaxCarryWeight;

        public Truck(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            Engine = new FuelEngine(k_FuelType, k_FuleTankCapacity, 0);
            Wheels = new Wheels(k_MaxAirPressure, k_NumOfWeels);
        }

        public Truck(string i_ModelName, string i_LicenseNumber, bool i_IsCarryDangerousMaterials, float i_MaxCarryWeight) : base(i_ModelName, i_LicenseNumber)
        {
            m_IsCarryDangerousMaterials = i_IsCarryDangerousMaterials;
            m_MaxCarryWeight = i_MaxCarryWeight;
        }

        protected override void CreateMetadata()
        {
            base.CreateMetadata();
            MetaData.Add(k_IsCarryDangerousMaterialsDescription, typeof(bool));
            MetaData.Add(k_MaxCarryWeight, typeof(float));
        }

        public override void Init(Dictionary<string, object> i_ValuesToInit)
        {
            base.Init(i_ValuesToInit);
            m_IsCarryDangerousMaterials = (bool)i_ValuesToInit[k_IsCarryDangerousMaterialsDescription];
            m_MaxCarryWeight = (float)i_ValuesToInit[k_MaxCarryWeight];
        }

        public float MaxCarryWeight
        {
            get { return m_MaxCarryWeight; }
        }

        public bool CarriesDangerousMaterials
        {
            get { return m_IsCarryDangerousMaterials; }
        }

        public override string ToString()
        {
            string carrieDangerousMaterials = m_IsCarryDangerousMaterials ? "is" : "is not";
            return string.Format("{3}{0}{3}Maximum permissible weight: {1}{3}The truck {2} Carrying dangerous materials ", base.ToString(), m_MaxCarryWeight, carrieDangerousMaterials, Environment.NewLine);
        }
    }
}
