using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, GarageVehicle> m_VehiclesInTheGarage;

        public Garage()
        {
            m_VehiclesInTheGarage = new Dictionary<string, GarageVehicle>();
        }

        public void AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            GarageVehicle newGarageVehicle = new GarageVehicle(i_OwnerName, i_OwnerPhoneNumber, i_Vehicle);
            bool isExist = m_VehiclesInTheGarage.ContainsKey(newGarageVehicle.Vehicle.License);
            if (!isExist)
            {
                m_VehiclesInTheGarage.Add(newGarageVehicle.Vehicle.License, newGarageVehicle);
            }
        }

        public List<string> LicesneNumbers(eGarageVehicleStatus? i_StatusToFind)
        {
            List<string> licenses = new List<string>();
            foreach (string license in m_VehiclesInTheGarage.Keys)
            {
                if (i_StatusToFind == null || m_VehiclesInTheGarage[license].CarStatus == i_StatusToFind)
                {
                    licenses.Add(license);
                }
            }

            return licenses;
        }

        public List<string> LicenseNumbers()
        {
            return LicesneNumbers(null);
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eGarageVehicleStatus i_NewStatus)
        {
            if (IsInTheGarage(i_LicenseNumber))
            {
                m_VehiclesInTheGarage[i_LicenseNumber].CarStatus = i_NewStatus;
            }
        }

        public void InflateWheelsToMax(string i_LicenseNumber)
        {
            if (IsInTheGarage(i_LicenseNumber))
            {
                m_VehiclesInTheGarage[i_LicenseNumber].Vehicle.Wheels.InflateAllToMax();
            }
        }

        public void FillGass(string i_LicenseNumber, eFuelType i_FuleType, float i_NumOfLitersToFill)
        {
            if (IsInTheGarage(i_LicenseNumber))
            {
                FuelEngine fuelEngine = m_VehiclesInTheGarage[i_LicenseNumber].Vehicle.Engine as FuelEngine;
                if (fuelEngine != null)
                {
                    fuelEngine.Refuel(i_NumOfLitersToFill, i_FuleType);
                }
                else
                {
                    throw new FormatException("Wrong engine type!");
                }
            }
        }

        public void ChargeElectricVehicle(string i_LicenseNumber, float i_NumOfMinutesToAdd)
        {
            if (IsInTheGarage(i_LicenseNumber))
            {
                ElectricEngine electricEngine = m_VehiclesInTheGarage[i_LicenseNumber].Vehicle.Engine as ElectricEngine;
                if (electricEngine != null)
                {
                    electricEngine.Recharge(i_NumOfMinutesToAdd / 60);
                }
                else
                {
                    throw new FormatException("Wrong engine type!");
                }
            }
        }

        public string VehicleData(string i_LicenseNumber)
        {
            return m_VehiclesInTheGarage[i_LicenseNumber].ToString();
        }

        public bool IsInTheGarage(string i_LicenseNumber)
        {
            return m_VehiclesInTheGarage.ContainsKey(i_LicenseNumber);
        }
    }
}
