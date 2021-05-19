using System;
using System.Collections.Generic;

#nullable disable

namespace DharaniScaper.Models
{
    public partial class District
    {
        public int Id { get; set; }
        public int? DistrictId { get; set; }
        public string Name { get; set; }
    }
}
