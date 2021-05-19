using System;
using System.Collections.Generic;

#nullable disable

namespace DharaniScaper.Models
{
    public partial class Mandal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MandalId { get; set; }
        public int? DistrictId { get; set; }
    }
}
