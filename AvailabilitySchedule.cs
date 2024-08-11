using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment_2
{
    public class AvailabilitySchedule
    {
        public int CarId { get; set; }
        private List<(DateTime StartDate, DateTime EndDate)> TimePeriods { get; set; }

        public AvailabilitySchedule(int carId)
        {
            CarId = carId;
            TimePeriods = new List<(DateTime, DateTime)>();
        }

        public void AddTimePeriod(DateTime startDateTime, DateTime endDateTime)
        {
            // Normalize the DateTime to only consider the date part
            startDateTime = startDateTime.Date;
            endDateTime = endDateTime.Date;

            if (IsValidDate(startDateTime, endDateTime))
            {
                TimePeriods.Add((startDateTime, endDateTime));
                Console.WriteLine("Schedule added successfully.");
            }
            else
            {
                Console.WriteLine("Error: Invalid date. The start date must be before the end date, and both must be in the future.");
            }
        }

        public List<(DateTime StartDate, DateTime EndDate)> GetTimePeriods()
        {
            return TimePeriods;
        }

        private bool IsValidDate(DateTime startDateTime, DateTime endDateTime)
        {
            return startDateTime.Date < endDateTime.Date && startDateTime.Date >= DateTime.Now.Date && endDateTime.Date > DateTime.Now.Date;
        }

        public void UpdateSchedule(DateTime startDateTime, DateTime endDateTime)
        {
            AddTimePeriod(startDateTime, endDateTime);
        }
    }
}


