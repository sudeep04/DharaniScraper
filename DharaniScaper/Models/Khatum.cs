using System;
using System.Collections.Generic;

#nullable disable

namespace DharaniScaper.Models
{
    public partial class Khatum
    {
        public int Id { get; set; }
        public int? KhataNumber { get; set; }
        public int? VillageId { get; set; }
        public string SurveyNumber { get; set; }
        public int? SurveyId { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int? MandalId { get; set; }
        public string MandalName { get; set; }
    }
}
