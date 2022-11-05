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
            //Run script every tick
            Runtime.UpdateFrequency = UpdateFrequency.Update1;
        }

        public void Main(string argument, UpdateType updateSource)
        {
            IMyMotorStator rotor = GridTerminalSystem.GetBlockWithName("Rotor") as IMyMotorStator;
            IMySensorBlock sensor = GridTerminalSystem.GetBlockWithName("Sensor") as IMySensorBlock;
            IMyTextSurface disp1 = Me.GetSurface(0);
            IMyTextSurface disp2 = Me.GetSurface(1);
            double rotorAngle = Math.Round(rotor.Angle * (180 / Math.PI));

            //Configure displays
            disp1.ContentType = ContentType.TEXT_AND_IMAGE;
            disp1.FontColor = Color.Red;
            disp1.Alignment = TextAlignment.CENTER;
            disp2.ContentType = ContentType.TEXT_AND_IMAGE;

            disp1.FontSize = 3;
            disp2.FontSize = 3;

            //Bottom screen shows rotor degree
            disp2.WriteText("Rotor Angle:\n" + rotorAngle);

            //Display sensor contacts 
            if(sensor.IsActive)
             {
                //Display on UI where radar contact is
                 disp1.WriteText(radarHud(rotorAngle));
             }
            
        }//End main

        public String radarHud(double sensRot)
        {
            string radarScreenBase = "*    *    *    *    *\n*    *    *    *    *\n*    *    x    *    *\n*    *    *    *    *\n*    *    *    *    *";

            //Interpret rotation info
            //Display on hud screen
            


            return radarScreenBase;
        }

    }
}
