using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meadpanion.Models;
using Meadpanion.Services;
using Meadpanion.Util;
using Xamarin.Forms;

namespace Meadpanion.Events
{
    class StepFeeding
    {
        public IDataStore<Reading> ReadingDataStore => DependencyService.Get<IDataStore<Reading>>();

        public List<Reading> ReadingList { get; set; }


        public StepFeeding()
        {
            ReadingList = new List<Reading>();
        }

        public async Task Initialize(int meadID)
        {
            await StepFedReadings(meadID);
        }

        private async Task StepFedReadings(int MeadID)
        {
            var readings = await ReadingDataStore.GetItemsAsync(MeadID);
            var steps = readings.Where(b => b.StepFeeding == true);
            var originalGravity = readings.First(s => s.OriginalGravity == true).GravityReading;
            var abv = new List<float>();
            int i = 0;

            foreach (var item in readings)
            {
                if (item.StepFeeding)
                {
                    abv.Add(float.Parse(ReadingList.Last().ABV.Remove(ReadingList.Last().ABV.Length - 1)));
                    originalGravity = item.GravityReading;
                    i++;
                }

                if (i == 0)
                    ReadingList.Add(new Reading()
                    {
                        ID = item.ID,
                        Date = item.Date,
                        GravityReading = item.GravityReading,
                        ABV = ABVCalculator.StringCalculateABV(originalGravity, item.GravityReading),
                        Note = item.Note
                    });
                else
                {
                    ReadingList.Add(new Reading()
                    {
                        ID = item.ID,
                        Date = item.Date,
                        GravityReading = item.GravityReading,
                        ABV = (ABVCalculator.FloatCalculateABV(originalGravity, item.GravityReading) + abv.Last()).ToString("0.00") + "%",
                        Note = item.Note
                    });
                }
            }


        }
    }
}
