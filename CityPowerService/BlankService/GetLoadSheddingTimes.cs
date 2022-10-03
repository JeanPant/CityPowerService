using CityPowerService;
using HtmlAgilityPack;

//SourceCode References
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace K2CityPowerServiceService
{
    internal class GetLoadsheddingTimesMethod : IDisposable
    {
        //Local Variables to match ServiceInstance Configurations
        private string _CityPowerBaseURL = string.Empty;

        private string _SelectTop = string.Empty;
        private string _SQLConnection = string.Empty;

        internal GetLoadsheddingTimesMethod()
        {
        }

        internal GetLoadsheddingTimesMethod(string strCityPowerBaseURL, string strSelectTop, string strConnection)
        {
            _CityPowerBaseURL = strCityPowerBaseURL;
            _SelectTop = strSelectTop;
            _SQLConnection = strConnection;
        }

        public void Dispose()
        {
            _CityPowerBaseURL = string.Empty;
            _SelectTop = string.Empty;
        }

        //Set properties of the ServiceObject based on the values from the Resource File
        internal ServiceObject DescribeServiceObject()
        {
            try
            {
                ServiceObject serviceObject = null;

                serviceObject = new ServiceObject();
                serviceObject.Name = Resources.K2_Times_ServiceObject_Name;
                serviceObject.MetaData.DisplayName = Resources.K2_Times_ServiceObject_DisplayName;
                serviceObject.MetaData.Description = Resources.K2_Times_ServiceObject_Description;
                serviceObject.Type = typeof(GetLoadsheddingTimesMethod).Name;
                serviceObject.Active = true;
                serviceObject.Properties = GetProperties(serviceObject);
                serviceObject.Methods = GetMethods(serviceObject);

                return serviceObject;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Execute: " + ex.ToString());
            }
        }

        //Define the SmartObjects Properties and the Property Types
        private Properties GetProperties(ServiceObject serviceObject)
        {
            try
            {
                Properties properties = new Properties();

                properties.Create(new Property("Stage", "System.String", SoType.Text, new MetaData("Stage", string.Empty)));
                properties.Create(new Property("SuburbID", "System.String", SoType.Text, new MetaData("SuburbID", string.Empty)));
                properties.Create(new Property("ProvinceID", "System.String", SoType.Text, new MetaData("ProvinceID", string.Empty)));
                properties.Create(new Property("MunicipalityTotal", "System.String", SoType.Text, new MetaData("MunicipalityTotal", string.Empty)));
                properties.Create(new Property("Provider", "System.String", SoType.Text, new MetaData("Provider", string.Empty)));
                properties.Create(new Property("Date", "System.String", SoType.Text, new MetaData("Date", string.Empty)));
                properties.Create(new Property("Start", "System.String", SoType.Text, new MetaData("Start", string.Empty)));
                properties.Create(new Property("End", "System.String", SoType.Text, new MetaData("End", string.Empty)));
                properties.Create(new Property("DayRange", "System.String", SoType.Text, new MetaData("DayRange", string.Empty)));

                return properties;
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetProperties: " + ex.ToString());
            }
        }

        //Define the method with all its properties based on the values from the Resource File
        private Methods GetMethods(ServiceObject serviceObject)
        {
            try
            {
                Methods methods = new Methods();

                methods.Create(new Method(Resources.K2_Times_List_Method_Name, MethodType.List, new MetaData(Resources.K2_Times_List_Method_DisplayName,
                       Resources.K2_Times_List_Method_Description),
                       GetRequiredProperties(serviceObject, Resources.K2_Times_List_Method_Name),
                       GetMethodParameters(serviceObject, Resources.K2_Times_List_Method_Name),
                       GetInputProperties(serviceObject, Resources.K2_Times_List_Method_Name),
                       GetReturnProperties(serviceObject, Resources.K2_Times_List_Method_Name)));

                return methods;
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetMethods: " + ex.ToString());
            }
        }

        //Define the Input Properties of the SmartObject Method
        private InputProperties GetInputProperties(ServiceObject serviceObject, string strMethodName)
        {
            try
            {
                InputProperties inputProperties = new InputProperties();

                if (strMethodName.Equals(Resources.K2_Times_List_Method_Name))
                {
                    inputProperties.Add(serviceObject.Properties["Stage"]);
                    inputProperties.Add(serviceObject.Properties["SuburbID"]);
                    inputProperties.Add(serviceObject.Properties["ProvinceID"]);
                    inputProperties.Add(serviceObject.Properties["MunicipalityTotal"]);
                    inputProperties.Add(serviceObject.Properties["Provider"]);
                    inputProperties.Add(serviceObject.Properties["DayRange"]);
                }

                return inputProperties;
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetInputProperties: " + ex.ToString());
            }
        }

        //Define the Method Parameters of the SmartObject Method
        private MethodParameters GetMethodParameters(ServiceObject serviceObject, string strMethodName)
        {
            try
            {
                MethodParameters methodParameters = new MethodParameters();

                return methodParameters;
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetMethodParameters: " + ex.ToString());
            }
        }

        //Set the Input Properties of the SmartObject Method

        private Validation GetRequiredProperties(ServiceObject serviceObject, string strMethodName)
        {
            try
            {
                Validation validation = new Validation();
                RequiredProperties requiredProperties = new RequiredProperties();

                if (strMethodName.Equals(Resources.K2_Times_List_Method_Name))
                {
                    requiredProperties.Add(serviceObject.Properties["Stage"]);
                    requiredProperties.Add(serviceObject.Properties["SuburbID"]);
                    requiredProperties.Add(serviceObject.Properties["ProvinceID"]);
                    requiredProperties.Add(serviceObject.Properties["Provider"]);
                    requiredProperties.Add(serviceObject.Properties["MunicipalityTotal"]);
                    requiredProperties.Add(serviceObject.Properties["DayRange"]);
                }

                validation.RequiredProperties = requiredProperties;

                return validation;
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetRequiredProperties: " + ex.ToString());
            }
        }

        //Define the Return Properties of the SmartObject Method
        private ReturnProperties GetReturnProperties(ServiceObject serviceObject, string strMethodName)
        {
            try
            {
                ReturnProperties returnProperties = new ReturnProperties();

                if (strMethodName.Equals(Resources.K2_Times_List_Method_Name))
                {
                    returnProperties.Add(serviceObject.Properties["Date"]);
                    returnProperties.Add(serviceObject.Properties["Start"]);
                    returnProperties.Add(serviceObject.Properties["End"]);
                }

                return returnProperties;
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetReturnProperties: " + ex.ToString());
            }
        }

        //The method that will execute the List and return a DataTable of results
        internal DataTable List(Dictionary<string, object> dicProperties, Dictionary<string, object> dicParameters)
        {
            DataTable dataTable = null;
            DataTable _tempTable1 = null;
            DataTable _tempTable2 = null;

            DataRow dataRow = null;
            DataRow _tempRow1 = null;
            DataRow _tempRow2 = null;

            string strSuburbInput = string.Empty;
            string strStageInput = string.Empty;
            string strProvinceInput = string.Empty;
            string strMunicipalInput = string.Empty;
            string strProviderInput = string.Empty;
            int intDayRangeInput = 0;

            try
            {
                string strData = string.Empty;
                string[] arr1 = new string[2];
                dataTable = GetDataTable();
                strSuburbInput = dicProperties["SuburbID"] + string.Empty;
                strStageInput = dicProperties["Stage"] + string.Empty;
                strProvinceInput = dicProperties["ProvinceID"] + string.Empty;
                strMunicipalInput = dicProperties["MunicipalityTotal"] + string.Empty;
                strProviderInput = dicProperties["Provider"] + string.Empty;

                intDayRangeInput = int.Parse(dicProperties["DayRange"] + string.Empty);

                //If Tshwane or Ekurhuleni or City Power

                if (strProviderInput.ToLower() == "city of tshwane" || strProviderInput.ToLower() == "ekurhuleni" || strProviderInput.ToLower() == "citypower")
                {
                    string[] dtRange = new string[intDayRangeInput];

                    for (int i = 0; i < intDayRangeInput; i++)
                    {
                        dtRange[i] = DateTime.Today.AddDays(i).ToShortDateString();
                    }

                    /*
                                        string[] dtRange = new string[3];
                                        dtRange[0] = DateTime.Today.ToShortDateString();
                                        dtRange[1] = DateTime.Today.AddDays(1).ToShortDateString();
                                        dtRange[2] = DateTime.Today.AddDays(2).ToShortDateString();*/

                    string strTable = string.Empty;

                    if (strProviderInput.ToLower() == "city of tshwane")
                        strTable = "tshwane";
                    else if (strProviderInput.ToLower() == "ekurhuleni")
                        strTable = "Ekurhuleni";
                    else if (strProviderInput.ToLower() == "citypower")
                        strTable = "CityPower";

                    _tempTable1 = GetDataTempTable1();
                    using (SqlConnection connection = new SqlConnection(_SQLConnection))
                    {
                        connection.Open();
                        string query = "SELECT * FROM [dbo].[" + strTable + "] WHERE[Stage] <= " + strStageInput + " AND '" + strSuburbInput + "' in (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    _tempRow1 = _tempTable1.NewRow();

                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        if (i < 2)
                                            _tempRow1[i] = reader.GetTimeSpan(i).ToString();
                                        else
                                            _tempRow1[i] = reader.GetInt32(i).ToString();
                                    }
                                    _tempTable1.Rows.Add(_tempRow1);
                                }
                            }
                        }
                    }

                    _tempTable2 = GetDataTempTable2();
                    foreach (DataRow row in _tempTable1.Rows)
                    {
                        int currentDay = DateTime.Now.Day;

                        for (int i = 3; i < _tempTable1.Columns.Count; i++)
                        {
                            int columnName = int.Parse(_tempTable1.Columns[i].ColumnName);

                            bool validDate = IsDateValid(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), int.Parse(_tempTable1.Columns[i].ColumnName));

                            if (validDate)
                            {
                                if (currentDay <= columnName)
                                {
                                    if (row[i].ToString() == strSuburbInput)
                                    {
                                        _tempRow2 = _tempTable2.NewRow();

                                        string strDate = DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/" + _tempTable1.Columns[i].ColumnName;

                                        DateTime date2 = Convert.ToDateTime(strDate, System.Globalization.CultureInfo.GetCultureInfo("en-ZA").DateTimeFormat);

                                        _tempRow2["Date"] = date2.ToString("ddd, dd MMM");

                                        _tempRow2["Start"] = row[0].ToString();

                                        _tempRow2["End"] = row[1].ToString();

                                        _tempTable2.Rows.Add(_tempRow2);

                                    }
                                }
                            }
                        }

                        for (int i = currentDay + 3; i > 3; i--)
                        {
                            int columnName = int.Parse(_tempTable1.Columns[i].ColumnName);

                            if (row[i].ToString() == strSuburbInput)
                            {
                                _tempRow2 = _tempTable2.NewRow();

                                string strDate = DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/" + _tempTable1.Columns[i].ColumnName;

                                DateTime date2 = Convert.ToDateTime(strDate, System.Globalization.CultureInfo.GetCultureInfo("en-ZA").DateTimeFormat);
                                date2 = date2.AddMonths(1);

                                _tempRow2["Date"] = date2.ToString("ddd, dd MMM");
                                _tempRow2["Start"] = row[0].ToString();
                                _tempRow2["End"] = row[1].ToString();
                                _tempTable2.Rows.Add(_tempRow2);
                            }
                        }
                    }

                    //Sorting the Table
                    DataView dv = _tempTable2.DefaultView;
                    dv.Sort = "Date";
                    DataTable sortedtable1 = dv.ToTable();

                    foreach (DataRow item in sortedtable1.Rows)
                    {
                        string itemDate = DateTime.Parse(item[0].ToString()).ToShortDateString();
                        if (dtRange.Contains(itemDate))
                        {
                            dataRow = dataTable.NewRow();
                            dataRow["Date"] = DateTime.Parse(item[0].ToString()).ToString("ddd, dd MMM");
                            dataRow["Start"] = item[1];
                            dataRow["End"] = item[2];
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }

                //Else Eskom
                else if (strProviderInput.ToLower() == "eskom")

                {

                    /*DateTime[] dtRange = new DateTime[3];
                    dtRange[0] = DateTime.Today;
                    dtRange[1] = DateTime.Today.AddDays(1);
                    dtRange[2] = DateTime.Today.AddDays(2);*/

                    DateTime[] dtRange = new DateTime[intDayRangeInput];

                    for (int i = 0; i < intDayRangeInput; i++)
                    {
                        dtRange[i] = DateTime.Today.AddDays(i);
                    }

                    strData = "https://loadshedding.eskom.co.za/LoadShedding/GetScheduleM/" + strSuburbInput + "/" + strStageInput + "/" + strProvinceInput + "/" + strMunicipalInput;
                    HtmlDocument doc = new HtmlWeb().Load(strData);

                    if (doc.Text.Contains("If you are a DIRECT MUNICIPAL CUSTOMER"))
                    {
                        //still figuring out
                    }
                    else
                    {
                        var divs = doc.DocumentNode.SelectNodes("//div[@class='scheduleDay']");

                        foreach (HtmlAgilityPack.HtmlNode node in divs)
                        {
                            string strDate = string.Empty;
                            HtmlNode dayMonth = node.SelectSingleNode(".//div[@class='dayMonth']");
                            strDate = dayMonth.InnerText.Trim();

                            foreach (HtmlNode subNode in node.SelectNodes(".//a"))
                            {
                                DateTime dtItem = DateTime.Parse(strDate);

                                if (dtRange.Contains(dtItem))
                                {

                                    dataRow = dataTable.NewRow();
                                    string[] strStartEnd = subNode.InnerText.Split('-');
                                    dataRow["Date"] = strDate;
                                    if (strStartEnd[0].Trim().Length == 5)
                                        dataRow["Start"] = strStartEnd[0].Trim() + ":00";
                                    else
                                        dataRow["Start"] = strStartEnd[0].Trim();


                                    if (strStartEnd[1].Trim().Length == 5)
                                        dataRow["End"] = strStartEnd[1].Trim() + ":00";
                                    else
                                        dataRow["End"] = strStartEnd[1].Trim();


                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error List: " + ex.ToString());
            }

            return dataTable;
        }

        //Define the DataTable with its Properties
        private DataTable GetDataTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("Date", typeof(String)));
            dataTable.Columns.Add(new DataColumn("Start", typeof(String)));
            dataTable.Columns.Add(new DataColumn("End", typeof(String)));

            return dataTable;
        }

        private static DataTable GetDataTempTable1()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("From", typeof(String)));
            dataTable.Columns.Add(new DataColumn("To", typeof(String)));
            dataTable.Columns.Add(new DataColumn("Stage", typeof(String)));
            dataTable.Columns.Add(new DataColumn("1", typeof(String)));
            dataTable.Columns.Add(new DataColumn("2", typeof(String)));
            dataTable.Columns.Add(new DataColumn("3", typeof(String)));
            dataTable.Columns.Add(new DataColumn("4", typeof(String)));
            dataTable.Columns.Add(new DataColumn("5", typeof(String)));
            dataTable.Columns.Add(new DataColumn("6", typeof(String)));
            dataTable.Columns.Add(new DataColumn("7", typeof(String)));
            dataTable.Columns.Add(new DataColumn("8", typeof(String)));
            dataTable.Columns.Add(new DataColumn("9", typeof(String)));
            dataTable.Columns.Add(new DataColumn("10", typeof(String)));
            dataTable.Columns.Add(new DataColumn("11", typeof(String)));
            dataTable.Columns.Add(new DataColumn("12", typeof(String)));
            dataTable.Columns.Add(new DataColumn("13", typeof(String)));
            dataTable.Columns.Add(new DataColumn("14", typeof(String)));
            dataTable.Columns.Add(new DataColumn("15", typeof(String)));
            dataTable.Columns.Add(new DataColumn("16", typeof(String)));
            dataTable.Columns.Add(new DataColumn("17", typeof(String)));
            dataTable.Columns.Add(new DataColumn("18", typeof(String)));
            dataTable.Columns.Add(new DataColumn("19", typeof(String)));
            dataTable.Columns.Add(new DataColumn("20", typeof(String)));
            dataTable.Columns.Add(new DataColumn("21", typeof(String)));
            dataTable.Columns.Add(new DataColumn("22", typeof(String)));
            dataTable.Columns.Add(new DataColumn("23", typeof(String)));
            dataTable.Columns.Add(new DataColumn("24", typeof(String)));
            dataTable.Columns.Add(new DataColumn("25", typeof(String)));
            dataTable.Columns.Add(new DataColumn("26", typeof(String)));
            dataTable.Columns.Add(new DataColumn("27", typeof(String)));
            dataTable.Columns.Add(new DataColumn("28", typeof(String)));
            dataTable.Columns.Add(new DataColumn("29", typeof(String)));
            dataTable.Columns.Add(new DataColumn("30", typeof(String)));
            dataTable.Columns.Add(new DataColumn("31", typeof(String)));

            return dataTable;
        }

        private static DataTable GetDataTempTable2()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("Date", typeof(DateTime)));
            dataTable.Columns.Add(new DataColumn("Start", typeof(String)));
            dataTable.Columns.Add(new DataColumn("End", typeof(String)));

            return dataTable;
        }

        private static string GetDataFromURL(string strURL)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData(strURL);

            string webData = System.Text.Encoding.UTF8.GetString(raw);

            return webData;
        }

        private List<LoadsheddingTimes> IterateXML(string strData)
        {
            List<LoadsheddingTimes> loadsheddingTimes = new List<LoadsheddingTimes>();
            string strDate = string.Empty;
            string strStart = string.Empty;
            string strEnd = string.Empty;
            bool bFound1 = false;
            bool bFound2 = false;
            DateTime[] dtRange = new DateTime[3];
            dtRange[0] = DateTime.Today;
            dtRange[1] = DateTime.Today.AddDays(1);
            dtRange[2] = DateTime.Today.AddDays(2);

            try
            {
                XmlReader rdr = XmlReader.Create(new System.IO.StringReader(strData));
                while (rdr.Read())
                {
                    if (rdr.NodeType == XmlNodeType.Element)
                    {
                        if (rdr.LocalName == "EventDate")
                        {
                            string strTemp = rdr.ReadInnerXml();
                            if (dtRange.Contains(DateTime.Parse(strTemp.Substring(0, 10))))
                            {
                                strStart = DateTime.Parse(strTemp).ToString();
                                bFound1 = true;
                            }
                        }
                        if (rdr.LocalName == "EndDate")
                        {
                            string strTemp = rdr.ReadInnerXml();
                            if (dtRange.Contains(DateTime.Parse(strTemp.Substring(0, 10))))
                            {
                                strEnd = DateTime.Parse(strTemp).ToString();
                                bFound2 = true;
                            }

                            if (bFound1 && bFound2)
                            {
                                loadsheddingTimes.Add(new LoadsheddingTimes() { Date = DateTime.Parse(strStart).ToString("ddd, dd MMM"), Start = DateTime.Parse(strStart).ToShortTimeString(), End = DateTime.Parse(strEnd).ToShortTimeString() });
                                bFound1 = false; bFound2 = false;
                            }
                        }
                    }
                }
                return loadsheddingTimes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error IterateXML: " + ex.ToString());
            }
        }

        private void WriteMessage(string message)
        {
            if (System.IO.Directory.Exists("c:\\temp"))
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText("c:\\temp\\CityPower_Log.txt"))
                {
                    w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " - " + message);
                    w.Flush();
                    w.Close();
                }
            }
        }

        public bool IsDateValid(int year, int month, int day)
        {
            return day <= DateTime.DaysInMonth(year, month);
        }
    }

    public class LoadsheddingTimes
    {
        public string Date
        {
            get; set;
        }

        public string Start
        {
            get; set;
        }

        public string End
        {
            get; set;
        }
    }
}