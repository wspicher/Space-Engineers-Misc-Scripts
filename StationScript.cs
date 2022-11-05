using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;
using System.Diagnostics;

namespace IngameScript
{
    //This script controls the interfaces for the main text panel in the space station
    //It displays power, oxygen, and station pressure levels
    partial class Program : MyGridProgram
    {

        public Program()
        {
            // Configure this program to run the Main method every 1 update tick
            Runtime.UpdateFrequency = UpdateFrequency.Update1;
        }

        public void Main(string argument)
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update1;
            //Definitions
            IMyTextPanel panel = GridTerminalSystem.GetBlockWithName("Panel 1") as IMyTextPanel;
            IMyGasTank tank = GridTerminalSystem.GetBlockWithName("Oxygen Tank") as IMyGasTank;
            IMyAirVent vent = GridTerminalSystem.GetBlockWithName("Air Vent") as IMyAirVent;
            List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
            IMyBlockGroup batteries = GridTerminalSystem.GetBlockGroupWithName("Batteries");
            IMyDoor mainDoor = GridTerminalSystem.GetBlockWithName("Door 6") as IMyDoor;
            IMyShipConnector connector = GridTerminalSystem.GetBlockWithName("Connector") as IMyShipConnector;
            float stationPressure = (float)Math.Round(vent.GetOxygenLevel() * 100);
            float averageCharge = 0f;
            float storedOxygen = 0f;
            string head = "Ice Planet Approach Station Systems:";
           

            //Handle main door lock and pressure
            ventLock(mainDoor, vent.GetOxygenLevel());

            //Prepare blockgroups for looping
            batteries.GetBlocks(blocks);
            
            //Loop through batteries
            foreach (IMyBatteryBlock battery in blocks)
            {
                averageCharge += battery.CurrentStoredPower;
            }
            //Calculate average
            averageCharge = (float)Math.Round((averageCharge / 4) * 100);

            //Determine how much oxygen is stored
            storedOxygen = (float)Math.Round(tank.FilledRatio * 100);

            //Configure text panel format
            panel.ContentType = ContentType.TEXT_AND_IMAGE;
            panel.FontColor = Color.Aqua;
            panel.WriteText(head + "\nAverage Battery Charge: " + averageCharge + " MWh" + "\nStation Pressure: " + stationPressure + "%" + "\nOxygen Tank Levels: " + storedOxygen + "%" + "\nConnector Status: " + connectorStatus(connector));
                
            //Reset average for next cycle
            averageCharge = 0f;
        }
            
            

        //Method to control locking of main door due to station pressure
        void ventLock(IMyDoor mainDoor, float stationPressure)
        {
            //Check if station is pressurized
            if (stationPressure > 0)
            {
                //If pressurized
                mainDoor.ApplyAction("OnOff_Off");
            } else if (stationPressure <= 0)
            {
                //If depressurized
                mainDoor.ApplyAction("OnOff_On");
            }
        }

        //Method to output connection status of connector
        string connectorStatus(IMyShipConnector con)
        {
            string temp = "";
            if (con.Status.Equals(MyShipConnectorStatus.Connected))
            {
                temp = "Ship Locked";
            }
            else
            {
                temp = "No Ship/Unlocked";
            }

            return temp;
        }

    }
}
