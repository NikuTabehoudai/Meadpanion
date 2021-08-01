using System;
using System.Collections.Generic;
using System.Text;

namespace Meadpanion.Util
{
    public class ABVCalculator
    {
        public static string CalculateABV(float originalGravity, float currentGravity)
        {
            return ((originalGravity - currentGravity) * 131.25f).ToString("0.00") + "%";
        }


    }
}
