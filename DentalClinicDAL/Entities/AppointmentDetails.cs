using System;
using System.Collections.Generic;
using System.Text;

namespace DentalClinicDAL.Entities
{
    public class AppointmentDetails
    {
        public Appointment Appointment { get; set; }

        public Procedure Procedure { get; set; }
    }
}
