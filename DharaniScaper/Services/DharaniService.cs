using DharaniScaper.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DharaniScaper.Services
{
    public class DharaniService
    {
        public DharaniScaper.Models.DharaniXContext _db;
        public DharaniService(DharaniScaper.Models.DharaniXContext db)
        {
            _db = db;
        }
         public  async Task<bool> GenerateMandals()
        {
            HttpClient client = new HttpClient();
            var count = 1;
            foreach (var district in _db.Districts.ToList())
            {
                HttpResponseMessage response = await client.GetAsync("https://dharani.telangana.gov.in/getMandalFromDivisionCitizenPortal?district="+ district.DistrictId +"&type=12");

                if (response.IsSuccessStatusCode)
                {
                    var pageContents = await response.Content.ReadAsStringAsync();
                    HtmlDocument pageDocument = new HtmlDocument();
                    pageDocument.LoadHtml(pageContents);

                    var programmerLinks = pageDocument.DocumentNode.Descendants("option").ToList();
                   
                    foreach (var lnk in programmerLinks)
                    {
                        var name = lnk.InnerText;
                        if (name == "Please Select")
                        {
                            continue;
                        }
                        var attr = lnk.Attributes[0];

                        var mandal = new Mandal();
                        mandal.MandalId = int.Parse(attr.Value);
                        mandal.Name = name;
                        mandal.DistrictId = district.DistrictId;
                        mandal.Id = count;
                        _db.Mandals.Add(mandal);
                        _db.SaveChanges();

                        count++;

                    }

                }
            }
            return true;
        }

        public async Task<bool> GenerateVillages()
        {
            HttpClient client = new HttpClient();
            var count = 1;
            foreach (var mandal in _db.Mandals.ToList())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://dharani.telangana.gov.in/getVillageFromMandalCitizenPortal?mandalId=" + mandal.MandalId + "&type=13");

                    if (response.IsSuccessStatusCode)
                    {
                        var pageContents = await response.Content.ReadAsStringAsync();
                        HtmlDocument pageDocument = new HtmlDocument();
                        pageDocument.LoadHtml(pageContents);

                        var programmerLinks = pageDocument.DocumentNode.Descendants("option").ToList();

                        foreach (var lnk in programmerLinks)
                        {
                            var name = lnk.InnerText;
                            if (name == "Please Select")
                            {
                                continue;
                            }
                            var attr = lnk.Attributes[0];

                            var village = new Village();
                            village.VillageId = int.Parse(attr.Value);
                            village.Name = name;
                            village.MandalId = mandal.MandalId;
                            village.Id = count;

                            _db.Villages.Add(village);
                            _db.SaveChanges();

                            count++;

                        }

                    }
                    else{
                       
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }

        public async Task<bool> GenerateSurveyNumbers()
        {
            HttpClient client = new HttpClient();
            var count = 1727566;
            var allvillages = _db.Villages.Where(x => x.VillageId >= 1373).ToList();
            foreach (var villageX in _db.Villages.ToList())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://dharani.telangana.gov.in/getSurveyCitizen?villId=" + villageX.VillageId + "&flag=survey");

                    if (response.IsSuccessStatusCode)
                    {
                        var pageContents = await response.Content.ReadAsStringAsync();
                        HtmlDocument pageDocument = new HtmlDocument();
                        pageDocument.LoadHtml(pageContents);

                        var programmerLinks = pageDocument.DocumentNode.Descendants("option").ToList();

                        foreach (var lnk in programmerLinks)
                        {
                            var name = lnk.InnerText;
                            if (name == "-- Select --")
                            {
                                continue;
                            }
                            var attr = lnk.Attributes[0];

                            var survey = new Survey();
                            survey.VillageId = villageX.VillageId;
                            survey.Id = count;
                            survey.SurveyNumber = name;


                            _db.Surveys.Add(survey);


                            count++;

                        }
                        _db.SaveChanges();
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }

        public async Task<bool> GenerateSurveyNumbersForLemoor()
        {
            HttpClient client = new HttpClient();
            int count = 6527;
            var dasarlapalle = _db.Villages.First(x => x.VillageId == 1206004);
            var villageX = dasarlapalle;
            //foreach (var villageX in _db.Villages.ToList())
            //{
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://dharani.telangana.gov.in/getSurveyCitizen?villId=" + villageX.VillageId + "&flag=survey");

                    if (response.IsSuccessStatusCode)
                    {
                        var pageContents = await response.Content.ReadAsStringAsync();
                        HtmlDocument pageDocument = new HtmlDocument();
                        pageDocument.LoadHtml(pageContents);

                        var programmerLinks = pageDocument.DocumentNode.Descendants("option").ToList();

                        foreach (var lnk in programmerLinks)
                        {
                            var name = lnk.InnerText;
                            if (name == "-- Select --")
                            {
                                continue;
                            }
                            var attr = lnk.Attributes[0];

                            var survey = new Survey();
                            survey.VillageId = villageX.VillageId;
                            survey.Id = count;
                            survey.SurveyNumber = name;


                            _db.Surveys.Add(survey);


                            count++;

                        }
                        _db.SaveChanges();
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            //}
            return true;
        }

        public async Task<bool> GenerateKhataNumbersForLemoor()
        {
            HttpClient client = new HttpClient();
            int count = 6534;
            var dasarlapalle = _db.Villages.First(x => x.VillageId == 1206004);
            var villageX = dasarlapalle;
            var mandal = _db.Mandals.First(x => x.MandalId == villageX.MandalId);
            var dasarlapalleDistrict = _db.Districts.First(x => x.DistrictId == mandal.DistrictId);
            foreach (var survey in _db.Surveys.Where(x => x.VillageId == 1206004).ToList())
            {
                try
            {
                HttpResponseMessage response = await client.GetAsync("https://dharani.telangana.gov.in/getKhataNoCitizen?villId=" + villageX.VillageId + "&flag=khatanos&surveyNo="+survey.SurveyNumber);

                if (response.IsSuccessStatusCode)
                {
                    var pageContents = await response.Content.ReadAsStringAsync();
                    HtmlDocument pageDocument = new HtmlDocument();
                    pageDocument.LoadHtml(pageContents);

                    var programmerLinks = pageDocument.DocumentNode.Descendants("option").ToList();

                    foreach (var lnk in programmerLinks)
                    {
                        var name = lnk.InnerText;
                        if (name == "-- Select --")
                        {
                            continue;
                        }
                        var attr = lnk.Attributes[0];

                            var kno = new Khatum();
                            kno.Id = count;
                            kno.KhataNumber = int.Parse(name);
                            kno.MandalId = villageX.MandalId;
                            kno.VillageId = villageX.VillageId;
                            kno.MandalName = mandal.Name;
                            kno.DistrictId = dasarlapalleDistrict.DistrictId;
                            kno.DistrictName = dasarlapalleDistrict.Name;
                            kno.SurveyNumber = survey.SurveyNumber;
                            _db.Khata.Add(kno);

                            //var survey = new Survey();
                            //survey.VillageId = villageX.VillageId;
                            //survey.Id = count;
                            //survey.SurveyNumber = name;


                            //_db.Surveys.Add(survey);


                            count++;

                    }
                    _db.SaveChanges();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            }
            return true;
        }

        public async Task<bool> GenerateSurveyInfoForLemoor()
        {
            HttpClient client = new HttpClient();
            int count = 6534;
            var dasarlapalle = _db.Villages.First(x => x.VillageId == 1206004);
            var villageX = dasarlapalle;
            var mandal = _db.Mandals.First(x => x.MandalId == villageX.MandalId);
            var dasarDistrict = _db.Districts.First(x => x.DistrictId == mandal.DistrictId);
            foreach (var kno in _db.Khata.Where(x => x.VillageId == 1206004).ToList())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://dharani.telangana.gov.in/getPublicDataInfo?villageId="+ villageX.VillageId + "&flagToSearch=surveynumber&searchData="+ kno.SurveyNumber + "&flagval=district&district="+kno.DistrictId+ "&mandal="+kno.MandalId +"&divi=&khataNoIdselect="+ kno.KhataNumber);

                    if (response.IsSuccessStatusCode)
                    {
                        var pageContents = await response.Content.ReadAsStringAsync();
                        //var pageContents = "< !DOCTYPE html >\r\n < html >\r\n < head >\r\n < meta http - equiv =\"Content-Security-Policy\"\r\n\tcontent=\"upgrade-insecure-requests\">\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=ISO-8859-1\">\r\n\r\n\r\n</head>\r\n\r\n\r\n\r\n<body>\r\n\r\n\t<form method=\"POST\" name=\"requestForm\" id=\"requestForm\">\r\n\t\t<div class=\"my-2 compress\">\r\n\t\t\t<div class=\"card-header\">\r\n\t\t\t\t<div class=\"col-md-6 txt-green\">\r\n\t\t\t\t\t<!-- <img src=\"img/collapse-icon.png\" border=\"0\" alt=\"collapse\"\r\n\t\t\t\t\t\tdata-toggle=\"collapse\" data-target=\"#land_detail\"\r\n\t\t\t\t\t\taria-expanded=\"true\" aria-controls=\"land_detail\"\r\n\t\t\t\t\t\tclass=\"direction-up\" /> -->\r\n\t\t\t\t\t<strong>Land Details</strong>\r\n\t\t\t\t</div>\r\n\r\n\t\t\t</div>\r\n\t\t\t<div class=\"card-body pt-0 pb-0 collapse show\" id=\"land_detail\">\r\n\t\t\t\t<div class=\"col-12 col-sm-12 my-3 text-right\">\r\n\t\t\t\t\t</div>\r\n\t\t\t\t<div class=\"row p-3\">\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>District</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\tRangareddy | రంగా రెడ్డి</div>\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Mandal</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\tKandukur | కందుకూరు</div>\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Village</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\tLemoor | లేమూర్</div>\r\n\t\t\t\t\t\t\t\t\t\r\n\r\n\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t\t<div class=\"row p-3\">\r\n\r\n                                   <div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Survey No./ Sub-Division No.</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\t1/అ/1</div>\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Pattadar Name</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\tజపాల యాదమ్మ</div>\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Father / Husband's Name</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\tజపాల రామయ్య లేట్</div>\r\n\r\n\r\n\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t\t<div class=\"row p-3\">\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Total Extent (Ac. Gts)</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\t0.1200</div>\r\n\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Land Status</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\tMarked as NALA </div>\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Land Type</strong> <br> NALA Land</div>\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\r\n\r\n\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t\t<div class=\"row p-3\">\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Nature of Land</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\tHomes</div>\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Classification of Land</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\tMetta/ Dry</div>\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Market value of Survey no.(in INR)</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\t1200000</div>\r\n\t\t\t\t\t\t\t\t\r\n\r\n\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t\t<div class=\"row p-3\">\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-8\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>Transaction Status</strong> <br> --<a\r\n\t\t\t\t\t\t\t\t\t\t\t\thref=\"\"\r\n\t\t\t\t\t\t\t\t\t\t\t\ttarget=\"_blank\"></a>\r\n\t\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>eKYC Status</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\tAadhaar available & e-KYC completed</div>\r\n\t\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t<div class=\"row p-3\">\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t<div class=\"col-12 col-sm-4\">\r\n\t\t\t\t\t\t\t\t\t\t<strong>PPB Number</strong> <br>\r\n\t\t\t\t\t\t\t\t\t\tT05110******</div>\r\n\t\t\t\t\t\t\t\t\t</div>\r\n\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t</div>\r\n\t\t</div>\r\n\t</form>\r\n\r\n\r\n\t<script>\r\n\t\t\r\n\t</script>\r\n\r\n</body>\r\n</html>";
                        HtmlDocument pageDocument = new HtmlDocument();
                        pageDocument.LoadHtml(pageContents);

                        var dname = string.Empty;
                        var mandalName = string.Empty;
                        var villageName = string.Empty;

                        var pattadarName = string.Empty;
                        var fatherName = string.Empty;
                        double landac = 0; 
                        var landStatus = string.Empty;
                        var landType = string.Empty;
                        var natureLand = string.Empty;
                        var classificationLand = string.Empty;
                        var marketValue = string.Empty;
                        var ppbnumber = string.Empty;
                        var transactionStatus = string.Empty;

                        var programmerNodes = pageDocument.DocumentNode.SelectNodes("//div[@class='col-12 col-sm-4']").ToList();
                        var tNode = pageDocument.DocumentNode.SelectNodes("//div[@class='col-12 col-sm-8']").ToList();

                        foreach (var lnk in programmerNodes)
                        {
                            var name = lnk.InnerText;
                            var strongText = lnk.ChildNodes["Strong"];
                            if (strongText.InnerText == "Survey No./ Sub-Division No." || strongText.InnerText == "eKYC Status")
                            {
                                continue;
                            }
                            else if (strongText.InnerText == "District")
                            {
                                dname = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            }
                            else if (strongText.InnerText == "Mandal")
                            {
                                mandalName = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            }
                            else if (strongText.InnerText == "Village")
                            {
                                villageName = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            }

                            else if (strongText.InnerText == "Pattadar Name")
                            {
                                pattadarName = lnk.ChildNodes[4].InnerText.Trim('\r', '\n','\t');
                            }
                            else if( strongText.InnerText == "Father / Husband's Name")
                            {
                                fatherName = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            } else if ( strongText.InnerText == "Total Extent (Ac. Gts)")
                            {
                                var temp = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                                landac = double.Parse(temp);
                            }
                            else if (strongText.InnerText == "Land Status")
                            {
                                landStatus = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            }
                            else if (strongText.InnerText == "Land Type")
                            {
                                landType = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            }
                            else if (strongText.InnerText == "Nature of Land")
                            {
                                natureLand = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            }
                            else if (strongText.InnerText == "Classification of Land")
                            {
                                classificationLand = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            }
                            else if (strongText.InnerText == "Market value of Survey no.(in INR)")
                            {
                                marketValue = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            }
                            else if (strongText.InnerText == "PPB Number")
                            {
                                ppbnumber = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            }

                          

                        }
                        foreach (var lnk in tNode)
                        {
                            var name = lnk.InnerText;
                            var strongText = lnk.ChildNodes["Strong"];
                            if (strongText.InnerText == "Transaction Status" )
                            {
                                transactionStatus = lnk.ChildNodes[4].InnerText.Trim('\r', '\n', '\t');
                            }
                        }
                        var sInfo = new SurveyInfo();
                        sInfo.Id = count;
                        sInfo.District = dname;
                        sInfo.Mandal = mandalName;
                        sInfo.Village = villageName;
                        sInfo.SurveyNumber = kno.SurveyNumber;
                        sInfo.PattadarName = pattadarName;
                        sInfo.Father = fatherName;
                        sInfo.TotalAc = landac;
                        sInfo.LandStatus = landStatus;
                        sInfo.LandType = landType;
                        sInfo.NatureLand = natureLand;
                        sInfo.Classification = classificationLand;
                        sInfo.MarketValue = marketValue;
                        sInfo.TransactionStatus = transactionStatus;
                        sInfo.Ppbnumber = ppbnumber;
                        sInfo.HtmlInfo = programmerNodes.Count != 14 ? pageContents : string.Empty;
                        _db.SurveyInfos.Add(sInfo);
                        _db.SaveChanges();
                        count++;
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }

        public bool GenerateResults()
        {
            List<SurveyInfo> result = _db.SurveyInfos.Where(x => x.Id >= 6534).ToList();
            var test = CalculateAcres(1.32, 1.32);
            Dictionary<string, ResultsInfo> dict = new Dictionary<string, ResultsInfo>();

            foreach(var surv in result)
            {
                if (dict.ContainsKey(surv.PattadarName))
                {
                    var x = dict[surv.PattadarName];
                    x.TotalAc = CalculateAcres((double)x.TotalAc, (double)surv.TotalAc);
                    x.SurveyNumber = x.SurveyNumber + ", " + surv.SurveyNumber;
                    x.LandStatus = x.LandStatus + ", " + surv.LandStatus;
                    x.LandType = x.LandType + ", " + surv.LandType;
                    x.TransactionStatus = x.TransactionStatus + ", " + surv.TransactionStatus;

                }
                else
                {
                    var rInfo = new ResultsInfo();
                    rInfo.Id = surv.Id;
                    rInfo.District = surv.District;
                    rInfo.Mandal = surv.Mandal;
                    rInfo.Village = surv.Village;
                    rInfo.SurveyNumber = surv.SurveyNumber;
                    rInfo.PattadarName = surv.PattadarName;
                    rInfo.Father = surv.Father;
                    rInfo.TotalAc = surv.TotalAc;
                    rInfo.LandStatus = surv.LandStatus;
                    rInfo.LandType = surv.LandType;
                    rInfo.NatureLand = surv.NatureLand;
                    rInfo.Classification = surv.Classification;
                    rInfo.MarketValue = surv.MarketValue;
                    rInfo.TransactionStatus = surv.TransactionStatus;
                    rInfo.Ppbnumber = surv.Ppbnumber;
                    rInfo.HtmlInfo = string.Empty;
                    dict.Add(surv.PattadarName, rInfo);
                }
            }

            var resultsFinal = new List<ResultsInfo>();
            foreach( var xx in dict)
            {
                resultsFinal.Add(xx.Value);
            }

            var resiltsxx = resultsFinal.OrderByDescending(o => o.TotalAc).ToList();
            //double totalVal = 0;
            //var sudeepLand = CalculateAcres(CalculateAcres(CalculateAcres(CalculateAcres(55.13, 60.1), 52.16), 43.21), 68.37);
            //foreach (var xx in resultsFinal)
            //{
            //    totalVal = CalculateAcres(totalVal, (double)xx.TotalAc);
            //}
            var count = 1;
            foreach (var xx in resultsFinal)
            {
                _db.ResultsInfos.Add(xx);
                _db.SaveChanges();

            }

            return true;
        }

        public double CalculateAcres(double valA, double valB)
        {
            var fracA = (valA % 1) * 100;
            var fracB = (valB % 1) * 100;
            valA = Math.Floor(valA);
            valB = Math.Floor(valB);

            var fracRem = ((fracA + fracB) % 40)/100;
            int fracQueotien = (int)((fracA + fracB) / 40);

            var finalSum = valA + valB + fracQueotien + fracRem;
            return Math.Round(finalSum,2);
        }
    }
}
