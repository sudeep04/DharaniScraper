using System;
using System.Collections.Generic;

#nullable disable

namespace DharaniScaper.Models
{
    public partial class SurveyInfo
    {
        public int Id { get; set; }
        public string District { get; set; }
        public string Mandal { get; set; }
        public string Village { get; set; }
        public string SurveyNumber { get; set; }
        public string PattadarName { get; set; }
        public string Father { get; set; }
        public double? TotalAc { get; set; }
        public string LandStatus { get; set; }
        public string LandType { get; set; }
        public string NatureLand { get; set; }
        public string Classification { get; set; }
        public string MarketValue { get; set; }
        public string TransactionStatus { get; set; }
        public string Ppbnumber { get; set; }
        public string HtmlInfo { get; set; }
    }
}
