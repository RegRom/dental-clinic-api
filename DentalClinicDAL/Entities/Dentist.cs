using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinicDAL.Entities
{
    public class Dentist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Firstname { get; set; }

        [MaxLength(60)]
        public string Lastname { get; set; }

        [MaxLength(100)]
        public string Degree { get; set; }
    }
}
