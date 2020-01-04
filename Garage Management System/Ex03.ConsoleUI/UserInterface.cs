using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private const string k_Menu = @"Please type a number from the menu, or press '0' to return to the menu: 
1. Insert your vehicle to our garage. 
2. Display lisence numbers list of the vehicles in the garage, filted by status (optional). 
3. Change a vihicle status. 
4. Inflate the wheels of a vehicle to maximum. 
5. Fill gas in a fuel type vehicle by a certain amount. 
6. Charge electric in an electric type vehicle by a certain amount. 
7. Display all the relevent data of a vehicle. 
8. Exit. ";
        private Garage m_Garage = new Garage();
        private string m_PossibleStatuses = generatePossibleEnumFields(typeof(eGarageVehicleStatus));

        private static string generatePossibleEnumFields(Type i_Fields)
        {
            StringBuilder possibleFields = new StringBuilder(Environment.NewLine);
            Array enumFields = Enum.GetValues(i_Fields);

            foreach (object field in enumFields)
            {
                possibleFields.Append(string.Format(" for {0} press '{1}'{2}", field.ToString(), (int)field, Environment.NewLine));
            }

            possibleFields.Remove(possibleFields.Length - 1, 1);

            return possibleFields.ToString();
        }

        public void PreMenu()
        {
            Console.WriteLine("Hello and welcome to our garage!");
            printMenu();
        }

        private void printMenu()
        {
            string tempNumberAsString;
            int userInput;

            Console.WriteLine(k_Menu);
            tempNumberAsString = Console.ReadLine();
            while (tempNumberAsString.Length != 1 || !int.TryParse(tempNumberAsString, out userInput) || userInput < 0 || userInput > 8)
            {
                Console.WriteLine("Wrong input, please enter a number from the menu, or 0 to return to the menu");
                tempNumberAsString = Console.ReadLine();
            }

            Console.Clear();
            menu(userInput);
        }

        private void menu(int i_chosenNumFromMenu)
        {
            bool quit = false;
            string licenseNum = string.Empty;
            bool isLicenseNumNeeded = !(i_chosenNumFromMenu == 0 || i_chosenNumFromMenu == 2 || i_chosenNumFromMenu == 8);

            if (isLicenseNumNeeded)
            {
                licenseNum = recieveLicenseNumFromUser();
                if (i_chosenNumFromMenu != 1 && !m_Garage.IsInTheGarage(licenseNum))
                {
                    Console.WriteLine("The provided license number is not in the garage.");
                    i_chosenNumFromMenu = -1;
                }
            }

            switch (i_chosenNumFromMenu)
            {
                case 0:
                    printMenu();
                    break;
                case 1:
                    if (!m_Garage.IsInTheGarage(licenseNum))
                    {
                        insertNewVehicle(licenseNum);
                    }
                    else
                    {
                        Console.WriteLine("This license number already exists in the garage! status changed to 'being repaired'");
                        m_Garage.ChangeVehicleStatus(licenseNum, eGarageVehicleStatus.InRepair);
                    }

                    break;
                case 2:
                    displayLicenseNumbers();
                    break;
                case 3:
                    changeVehicleStatus(licenseNum);
                    break;
                case 4:
                    m_Garage.InflateWheelsToMax(licenseNum);
                    break;
                case 5:
                    fillGass(licenseNum);
                    break;
                case 6:
                    chargeElectricVehicle(licenseNum);
                    break;
                case 7:
                    VehiclesData(licenseNum);
                    break;
                case 8:
                    quit = true;
                    break;
            }

            if (!quit)
            {
                backToTheMenuOrQuitHandler();
            }
        }

        private void backToTheMenuOrQuitHandler()
        {
            string menuOrQuit = string.Empty;

            Console.WriteLine("press 'm' to go back to the menu or 'q' to quit");
            menuOrQuit = Console.ReadLine();
            while (!menuOrQuit.Equals("q") && !menuOrQuit.Equals("m"))
            {
                Console.WriteLine("Wrong input. Input must be 'm' or 'q'");
                menuOrQuit = Console.ReadLine();
            }

            if (menuOrQuit == "m")
            {
                Console.Clear();
                printMenu();
            }
        }

        private string[] receiveContactDetails()
        {
            string[] nameAndPhone = new string[2];

            Console.WriteLine("Please enter your name: ");
            while ((nameAndPhone[0] = Console.ReadLine()) == string.Empty)
            {
                Console.WriteLine("Name must be atleast 1 letter");
            }

            Console.WriteLine("Please enter your phone number: ");
            while ((nameAndPhone[1] = Console.ReadLine()) == string.Empty)
            {
                Console.WriteLine("Phone must be atleast 1 letter");
            }

            return nameAndPhone;
        }

        private Vehicle createNewVehicle(string i_License)
        {
            int inputFieldAsNumber;
            string vehicleType;

            Console.WriteLine(string.Format("Please enter the type of the vehicle: {0}", generatePossibleEnumFields(typeof(eVehicles))));
            vehicleType = Console.ReadLine();

            while (!int.TryParse(vehicleType, out inputFieldAsNumber) || !Enum.IsDefined(typeof(eVehicles), inputFieldAsNumber))
            {
                Console.WriteLine("Input must be a number represnting the field, please try again.");
                vehicleType = Console.ReadLine();
            }

            return VehicleFactory.CreateVehicle((eVehicles)inputFieldAsNumber, i_License);
        }

        private object getAndConvertNumericStringToEnum(string i_InitialValueToTryParse, string i_RequestingMessage, string i_ErrorMessage, Type i_Type)
        {
            object convertedString = null;
            int inputFieldAsNumber;
            string userInput = i_InitialValueToTryParse;

            if (i_InitialValueToTryParse.Equals(string.Empty))
            {
                Console.WriteLine(i_RequestingMessage);
                userInput = Console.ReadLine();
            }

            while (!int.TryParse(userInput, out inputFieldAsNumber) || !Enum.IsDefined(i_Type, convertedString = Enum.ToObject(i_Type, inputFieldAsNumber)))
            {
                Console.WriteLine(i_ErrorMessage);
                userInput = Console.ReadLine();
            }

            return convertedString;
        }

        private object getAndConvertNumericStringToEnum(string i_RequestingMessage, string i_ErrorMessage, Type i_Type)
        {
            return getAndConvertNumericStringToEnum(string.Empty, i_RequestingMessage, i_ErrorMessage, i_Type);
        }

        private void initVehicleData(Vehicle i_Vehicle)
        {
            object fieldValue = null;
            Type fieldType;
            Dictionary<string, object> vehicleFields = new Dictionary<string, object>();
            string fieldMsg;

            foreach (string field in i_Vehicle.MetaData.Keys)
            {
                fieldType = i_Vehicle.MetaData[field];
                fieldMsg = string.Format("Please enter {0}{1}", field, Environment.NewLine);
                if (!fieldType.IsEnum)
                {
                    fieldValue = getAndConvertToGivenType(fieldMsg, string.Format("Wrong input to this field, {0}", fieldMsg), fieldType);
                }
                else
                {
                    fieldValue = getAndConvertNumericStringToEnum(string.Format("Please enter {0}{1}", field, generatePossibleEnumFields(fieldType)), "Input must be a number represnting the field, please try again.", fieldType);
                }

                vehicleFields.Add(field, fieldValue);
            }

            initVehicleCurrentEnergy(i_Vehicle);
            initVehicleWheels(i_Vehicle);
            i_Vehicle.Init(vehicleFields);
        }

        private object getAndConvertToGivenType(string i_RequestMassege, string i_ErrorMessage, Type i_TypeToConvartTo)
        {
            object convertedValue = null;
            bool isInputTypeCorrect = false;
            string userInput;
            Console.Write(i_RequestMassege);
            while (!isInputTypeCorrect)
            {
                userInput = Console.ReadLine();
                try
                {
                    convertedValue = Convert.ChangeType(userInput, i_TypeToConvartTo);
                    isInputTypeCorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine(string.Format("Wrong input to this field, {0}", i_ErrorMessage));
                }
            }

            return convertedValue;
        }

        private void initVehicleCurrentEnergy(Vehicle i_Vehicle)
        {
            string requestMsg = string.Empty;
            string errorMsg = string.Empty;
            string energyType = string.Empty;
            bool isEnergyAmountInRightRainge = false;
            float energyAmount;

            if (i_Vehicle.Engine is FuelEngine)
            {
                energyType = "fuel";
            }
            else
            {
                energyType = "electricity";
            }

            requestMsg = string.Format("please enter the current {0} amount:{1}", energyType, Environment.NewLine);
            errorMsg = string.Format("Wrong input. The current {0} amount must be a number.", energyType);
            while (!isEnergyAmountInRightRainge)
            {
                energyAmount = (float)getAndConvertToGivenType(requestMsg, errorMsg, typeof(float));
                try
                {
                    setVehicleEngineCurrentEnergyAmount(i_Vehicle, energyAmount);
                    isEnergyAmountInRightRainge = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine("Current {2} amount must be between {0} to {1}", e.MinValue, e.MaxValue, energyType);
                }
            }
        }

        private void setVehicleEngineCurrentEnergyAmount(Vehicle i_Vehicle, float i_CurrentEnergyAmount)
        {
            FuelEngine fuleEngine = i_Vehicle.Engine as FuelEngine;

            if (fuleEngine != null)
            {
                fuleEngine.Refuel(i_CurrentEnergyAmount, fuleEngine.FuelType);
            }
            else
            {
                (i_Vehicle.Engine as ElectricEngine).Recharge(i_CurrentEnergyAmount);
            }
        }

        private void initVehicleWheels(Vehicle i_Vehicle)
        {
            string manufacturerName;
            float currentAirPressure;
            bool isCurrentAirPressureInTheNeededRange = false;

            Console.WriteLine("Please enter the vehicles wheel's manufacturer name:");
            manufacturerName = Console.ReadLine();
            while (!isCurrentAirPressureInTheNeededRange)
            {
                Console.WriteLine("Please enter the vehicles wheel's current air pressure:");
                try
                {
                    while (!float.TryParse(Console.ReadLine(), out currentAirPressure))
                    {
                        Console.WriteLine("Air pressure must be a number, please try again.");
                    }

                    i_Vehicle.Wheels.InitAllWheels(manufacturerName, currentAirPressure);
                    isCurrentAirPressureInTheNeededRange = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine("Current air pressure must be between {0} to {1}", e.MinValue, e.MaxValue);
                }
            }
        }

        private void insertNewVehicle(string i_LicenseNum)
        {
            string[] nameAndPhone = receiveContactDetails();
            Vehicle vehicle = createNewVehicle(i_LicenseNum);

            initVehicleData(vehicle);
            m_Garage.AddVehicle(vehicle, nameAndPhone[0], nameAndPhone[1]);
            Console.WriteLine(string.Format("The car was successfully added to the garage. {0}", Environment.NewLine));
        }

        private void displayLicenseNumbers()
        {
            string requestMessage = string.Format("How do you want the license numbers to be filtered? {0}{1}{0}Or press 'none' for no filter", Environment.NewLine, m_PossibleStatuses);
            string errorMessage = string.Format("Wrong Input, input must be: {0} or none", m_PossibleStatuses);
            List<string> listOfLicenseNums;
            eGarageVehicleStatus parsedString;
            string filterBy = string.Empty;
            string chosenFilter = string.Empty;

            Console.WriteLine(requestMessage);
            filterBy = Console.ReadLine();
            if (filterBy != "none")
            {
                parsedString = (eGarageVehicleStatus)getAndConvertNumericStringToEnum(filterBy, requestMessage, errorMessage, typeof(eGarageVehicleStatus));
                listOfLicenseNums = m_Garage.LicesneNumbers(parsedString);
                chosenFilter = parsedString.ToString();
            }
            else
            {
                listOfLicenseNums = m_Garage.LicenseNumbers();
                chosenFilter = filterBy;
            }

            Console.WriteLine("The license numbers (filter: {0}): ", chosenFilter);
            foreach (string licenceNum in listOfLicenseNums)
            {
                Console.WriteLine("{0}", licenceNum);
            }
        }

        private string recieveLicenseNumFromUser()
        {
            string licenseNumber = string.Empty;
            Console.WriteLine("Please enter the license number of the vehicle: ");
            licenseNumber = Console.ReadLine();
            while (licenseNumber.Length < 1)
            {
                Console.WriteLine("License number can not be empty, please try again.");
                licenseNumber = Console.ReadLine();
            }

            return licenseNumber;
        }

        private void changeVehicleStatus(string i_LicenseNum)
        {
            string requestedMessege = string.Format("Which status do you want the vehicle to be changed to? {0}", m_PossibleStatuses);
            eGarageVehicleStatus newStatus;
            string errorMessage = string.Format("Wrong Input, input must be: {0}", m_PossibleStatuses);

            if (!m_Garage.IsInTheGarage(i_LicenseNum))
            {
                Console.WriteLine("The provided license number is not in the garage.");
            }
            else
            {
                newStatus = (eGarageVehicleStatus)getAndConvertNumericStringToEnum(requestedMessege, errorMessage, typeof(eGarageVehicleStatus));
                m_Garage.ChangeVehicleStatus(i_LicenseNum, newStatus);
            }

            Console.WriteLine("Vehicle status has been succefully changed!");
        }

        private float recieveMinutesOrLitersAmountToCharge()
        {
            string tempNumberAsString = Console.ReadLine();
            float amountOfMinutesOrLitersToCharge;

            while (!float.TryParse(tempNumberAsString, out amountOfMinutesOrLitersToCharge))
            {
                Console.WriteLine("The amount must be a possitive number! please enter currect amount");
                tempNumberAsString = Console.ReadLine();
            }

            return amountOfMinutesOrLitersToCharge;
        }

        private void fillGass(string i_LicenseNum)
        {
            string requestMessage = string.Format("Which kind of gass does your vehicle use? {0} ", generatePossibleEnumFields(typeof(eFuelType)));
            string errorMessage = "This is not the currect fuel for this type of vehicle! please enter currect type of fuel";
            eFuelType fuelType = (eFuelType)getAndConvertNumericStringToEnum(requestMessage, errorMessage, typeof(eFuelType));
            Console.WriteLine("Please enter amount of fuel to fill: ");
            float amountToFill = recieveMinutesOrLitersAmountToCharge();
            bool isCatchingExceptions = true;
            while (isCatchingExceptions)
            {
                try
                {
                    m_Garage.FillGass(i_LicenseNum, fuelType, amountToFill);
                    Console.WriteLine("Vehicle was successfuly fueled");
                    isCatchingExceptions = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Electric Car cannot be filled with gas! ");
                    break;
                }
                catch (ArgumentException)
                {
                    requestMessage = "Wrong type of fuel entered, which doesn't fit to this car, please enter the suitable fuel type: ";
                    fuelType = (eFuelType)getAndConvertNumericStringToEnum(requestMessage, errorMessage, typeof(eFuelType));
                }
                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Amount entered is too high and will exceed maximum, enter another amount");
                    amountToFill = recieveMinutesOrLitersAmountToCharge();
                }
            }
        }

        private void chargeElectricVehicle(string i_LicenseNum)
        {
            Console.WriteLine("Enter amount of minutes to charge:");
            float amountToCharge = recieveMinutesOrLitersAmountToCharge();
            bool isCatchingExceptions = true;
            while (isCatchingExceptions)
            {
                try
                {
                    m_Garage.ChargeElectricVehicle(i_LicenseNum, amountToCharge);
                    Console.WriteLine("Vehicle was successfuly charged");
                    isCatchingExceptions = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Fuel type Car cannot be charged with electric! ");
                    break;
                }
                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Amount entered is too high and will exceed maximum, enter another amount");
                    amountToCharge = recieveMinutesOrLitersAmountToCharge();
                }
            }
        }

        private void VehiclesData(string i_LicenseNum)
        {
            Console.Clear();
            Console.Write(m_Garage.VehicleData(i_LicenseNum));
        }
    }
}