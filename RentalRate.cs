using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SWAD_Assignment_2
{
    public class RentalRate
    {
        public int CarId { get; private set; } // Link RentalRate to Car
        public double Rate { get; private set; }

        public RentalRate(int carId, double rate)
        {
            CarId = carId;

            if (ValidateRate(rate))
            {
                Rate = rate;
            }
            else
            {
                throw new ArgumentException("Invalid rate. Rate must be greater than 0.");
            }
        }

        public bool ValidateRate(double rate)
        {
            return rate > 0;
        }

        public void UpdateRate(double rate)
        {
            if (ValidateRate(rate))
            {
                Rate = rate;
            }
            else
            {
                throw new ArgumentException("Invalid rate. Rate must be greater than 0.");
            }
        }
    }
}


