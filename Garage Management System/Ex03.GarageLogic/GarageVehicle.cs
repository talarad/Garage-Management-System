using System;

namespace Ex03.GarageLogic
{
    internal class GarageVehicle
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private Vehicle m_Vehicle;
        private eGarageVehicleStatus m_VehicalStatus;

        public GarageVehicle(string i_OwnersName, string i_OwnersPhoneNumber, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnersName;
            m_OwnerPhoneNumber = i_OwnersPhoneNumber;
            m_Vehicle = i_Vehicle;
            m_VehicalStatus = eGarageVehicleStatus.InRepair;
        }

        internal string OwnerName
        {
            get { return m_OwnerName; }
        }

        internal string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
        }

        internal Vehicle Vehicle
        {
            get { return m_Vehicle; }
        }

        internal eGarageVehicleStatus CarStatus
        {
            get { return m_VehicalStatus; }
            set { m_VehicalStatus = value; }
        }

        public override string ToString()
        {
            ///            string garageVehicleToString = string.Format(@"owner's name: {0}
            ///owner phone Number: {1}

            ///vehicle data: 
            ///{2}

            ///Engine data:
            ///{3}

            ///{4}
            ///", m_OwnerName, m_OwnerPhoneNumber, m_Vehicle.ToString(), m_Vehicle.Engine.ToString(), m_Vehicle.Wheels.ToString());
            string garageVehicleToString = string.Format("owner's name: {0}{5}owner phone Number: {1}{5}{5}vehicle data:{5}{2}{5}{5}Engine data:{5}{3}{5}{5}{4}", m_OwnerName, m_OwnerPhoneNumber, m_Vehicle.ToString(), m_Vehicle.Engine.ToString(), m_Vehicle.Wheels.ToString(), Environment.NewLine);

            return garageVehicleToString;
        }
    }

    public enum eGarageVehicleStatus
    {
        InRepair = 1,
        Fixed = 2,
        paid = 3
    }
}