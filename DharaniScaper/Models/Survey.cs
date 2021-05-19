using System;
using System.Collections.Generic;

#nullable disable

namespace DharaniScaper.Models
{
    public partial class Survey
    {
        public int Id { get; set; }
        public string SurveyNumber { get; set; }
        public int? SurveyId { get; set; }
        public int? VillageId { get; set; }
    }
}
