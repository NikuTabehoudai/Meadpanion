using System;
using System.Collections.Generic;
using System.Text;

namespace Meadpanion.Util
{
    public class ABVCalculator
    {
        public static string StringCalculateABV(float originalGravity, float currentGravity)
        {
            return ((originalGravity - currentGravity) * 131.25f).ToString("0.00") + "%";
        }

        public static float FloatCalculateABV(float originalGravity, float currentGravity)
        {
            return ((originalGravity - currentGravity) * 131.25f);
        }

    }
}
