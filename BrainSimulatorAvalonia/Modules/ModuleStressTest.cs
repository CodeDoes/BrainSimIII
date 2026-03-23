// Copyright (c) FutureAI. All rights reserved.  
// Contains confidential and proprietary information and programs which may not be distributed without a separate license
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UKS;

namespace BrainSimulatorAvalonia.Modules
{
    public class ModuleStressTest : ModuleBase
    {
        public static string Output = "";

        public ModuleStressTest()
        {
        }

        public override void Initialize()
        {
        }

        public override void SetUpAfterLoad()
        {
        }

        public static string AddManyTestItems(int count)
        {
            int maxCount = 100000;
            List<string> items = new List<string>();
            if (count <= 0)
            {
                return "Count less or equal to 0, cannot commence.";
            }
            else if (count > maxCount)
            {
                return $"Count greater than maxCount {maxCount}, cannot commence.";
            }
            for (int i = 0; i < count; i++)
            {
                items.Add(i.ToString());
            }
            int maxOuter = 100;
            for (int i = 0; i < maxOuter; i++)
            {
                Thing parent = MainWindow.theUKS.GetOrAddThing("A" + i.ToString());
                for (int j = 0; j < 100; j++)
                {
                    Thing parent0 = MainWindow.theUKS.GetOrAddThing("B" + i.ToString() + j.ToString(), parent);
                    for (int k = 0; k < 10; k++)
                    {
                        Thing parent1 = MainWindow.theUKS.GetOrAddThing("C" + i.ToString() + j.ToString() + k.ToString(), parent0);
                    }
                }
                Debug.WriteLine($"{i + 1}/{maxOuter} done.");
            }
            Debug.WriteLine("Done!");
            return "Items added successfully.";
        }

        public override void Fire()
        {
            Init();
            UpdateDialog();
        }
    }
}
