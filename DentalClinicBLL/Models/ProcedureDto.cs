﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DentalClinicBLL.Models
{
    public class ProcedureDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }
    }
}