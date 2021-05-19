using System;
using System.Collections.Generic;

#nullable disable

namespace DharaniScaper.Models
{
    public partial class Village
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? VillageId { get; set; }
        public int? MandalId { get; set; }
    }
}
