using System;
using System.Collections.Generic;
using System.Text;

namespace DentalClinicBLL.Models
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public int DentistId { get; set; }

        public int PatientId { get; set; }

        public int ProcedureId { get; set; }
    }
}
