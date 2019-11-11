using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DentalClinicDAL.Entities
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Dentist Dentist { get; set; }

        [Required]
        public Patient Patient { get; set; }

        [Required]
        public Procedure Procedure { get; set; }
    }
}
