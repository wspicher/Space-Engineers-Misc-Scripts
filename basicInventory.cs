using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        public Program()
        {
            //Self run
            Runtime.UpdateFrequency = UpdateFrequency.Update10;
        }

        public void Main(string argument, UpdateType updateSource)
        {
            //Declarations
            IMyCargoContainer iceCar = GridTerminalSystem.GetBlockWithName("Ice Container") as IMyCargoContainer;
            IMyCargoContainer stoneCar = GridTerminalSystem.GetBlockWithName("Stones Container") as IMyCargoContainer;
            IMyCargoContainer dropOff = GridTerminalSystem.GetBlockWithName("Drop Off") as IMyCargoContainer;
            IMyTextPanel infoPanel = GridTerminalSystem.GetBlockWithName("Info Panel") as IMyTextPanel;
            IMyTextPanel iceInv = GridTerminalSystem.GetBlockWithName("Ice Panel") as IMyTextPanel;
            IMyTextPanel stoneInv = GridTerminalSystem.GetBlockWithName("Stone Panel") as IMyTextPanel;

            //Display cargo info to text panels
            writeInventory(iceInv, iceCar, Color.Teal, "Ice Container");
            writeInventory(infoPanel, dropOff, Color.Green, "Drop-off Container");
            writeInventory(stoneInv, stoneCar, Color.Orange, "Stone Contaioner");

            //Check the contents of dropoff
            for(int i = 0; i < dropOff.GetInventory(0).ItemCount; i++)
            {
                MyInventoryItem currentItem = dropOff.GetInventory(0).GetItemAt(i).Value;
                var curItemAmt = currentItem.Amount;

                if (currentItem.Type.SubtypeId.Equals("Ice"))
                {
                    //Move this item to the ice container
                    dropOff.GetInventory(0).TransferItemTo(iceCar.GetInventory(0), currentItem, curItemAmt); //dest, item, amount
                } else if (currentItem.Type.SubtypeId.Equals("Stone"))
                {
                    //Move this item to the stone container
                    dropOff.GetInventory(0).TransferItemTo(stoneCar.GetInventory(0), currentItem, curItemAmt); //dest, item, amount
                } 
            }
        }//End of main method


        //This method handles 
        public void writeInventory(IMyTextPanel infoPanel, IMyCargoContainer dropOff, Color textColor, string name)
        {
            string output = name + " Contents:\n";

            //Constantly check dropoff's contents
            //Display dropoff contents to info panel

            //configure info panel
            infoPanel.Alignment = TextAlignment.LEFT;
            infoPanel.FontColor = textColor;
            infoPanel.FontSize = 1.5f;
            infoPanel.ContentType = ContentType.TEXT_AND_IMAGE;

            for (int i = 0; i < dropOff.GetInventory(0).ItemCount; i++)
            {
                //Format the output to contain the contents of the dropoff container
                output += dropOff.GetInventory(0).GetItemAt(i).Value.Type.SubtypeId + ": " + dropOff.GetInventory(0).GetItemAt(i).Value.Amount + "\n";
            }

            infoPanel.WriteText(output);
        } //End of writeInventory method





    }
}
