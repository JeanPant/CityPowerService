using CityPowerService;
using Newtonsoft.Json;

//SourceCode References
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace K2CityPowerServiceService
{
    internal class GetSuburbMethod : IDisposable
    {
        //Local Variables to match ServiceInstance Configurations
        private string _CityPowerBaseURL = string.Empty;

        private string _SelectTop = string.Empty;
        private string _SQLConnection = string.Empty;

        internal GetSuburbMethod()
        {
        }

        internal GetSuburbMethod(string strCityPowerBaseURL, string strSelectTop, string strSQLConnection)
        {
            _CityPowerBaseURL = strCityPowerBaseURL;
            _SelectTop = strSelectTop;
            _SQLConnection = strSQLConnection;
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
                serviceObject.Name = Resources.K2_Suburb_ServiceObject_Name;
                serviceObject.MetaData.DisplayName = Resources.K2_Suburb_ServiceObject_DisplayName;
                serviceObject.MetaData.Description = Resources.K2_Suburb_ServiceObject_Description;
                serviceObject.Type = typeof(GetSuburbMethod).Name;
                serviceObject.Active = true;
                serviceObject.Properties = GetProperties(serviceObject);
                serviceObject.Methods = GetMethods(serviceObject);

                return serviceObject;
            }
            catch (Exception ex)
            {
                throw new Exception("Error DescribeServiceObject: " + ex.ToString());
            }
        }

        //Define the SmartObjects Properties and the Property Types
        private Properties GetProperties(ServiceObject serviceObject)
        {
            try
            {
                Properties properties = new Properties();

                properties.Create(new Property("Suburb", "System.String", SoType.Text, new MetaData("Suburb", string.Empty)));
                properties.Create(new Property("ID", "System.String", SoType.Text, new MetaData("ID", string.Empty)));
                properties.Create(new Property("Suburb Name", "System.String", SoType.Text, new MetaData("Suburb Name", string.Empty)));
                properties.Create(new Property("Municiplality", "System.String", SoType.Text, new MetaData("Municiplality", string.Empty)));
                properties.Create(new Property("Provider", "System.String", SoType.Text, new MetaData("Provider", string.Empty)));

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

                methods.Create(new Method(Resources.K2_Suburb_List_Method_Name, MethodType.List, new MetaData(Resources.K2_Suburb_List_Method_DisplayName,
                       Resources.K2_Suburb_List_Method_Description),
                       GetRequiredProperties(serviceObject, Resources.K2_Suburb_List_Method_Name),
                       GetMethodParameters(serviceObject, Resources.K2_Suburb_List_Method_Name),
                       GetInputProperties(serviceObject, Resources.K2_Suburb_List_Method_Name),
                       GetReturnProperties(serviceObject, Resources.K2_Suburb_List_Method_Name)));

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

                if (strMethodName.Equals(Resources.K2_Suburb_List_Method_Name))
                {
                    inputProperties.Add(serviceObject.Properties["Suburb"]);
                    inputProperties.Add(serviceObject.Properties["Municiplality"]);
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

                /*if (strMethodName.Equals(Resources.K2_Search_List_Method_Name))
                {
                    requiredProperties.Add(serviceObject.Properties["InputProperty1"]);
                }*/

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

                if (strMethodName.Equals(Resources.K2_Suburb_List_Method_Name))
                {
                    returnProperties.Add(serviceObject.Properties["ID"]);
                    returnProperties.Add(serviceObject.Properties["Suburb Name"]);
                    returnProperties.Add(serviceObject.Properties["Provider"]);
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
            DataRow dataRow = null;
            string strSuburbInput = "";
            string strMunInput = "";

            try
            {
                string strData = string.Empty;
                string[] arr1 = new string[2];
                dataTable = GetDataTable();

                strSuburbInput = dicProperties["Suburb"] + string.Empty;

                strMunInput = dicProperties["Municiplality"] + string.Empty;

                //https://www.citypower.co.za/_api/web/lists/getByTitle('Suburb%20&%20Block')/items?$select=*&$filter=startswith(Suburb_x0020_Name,%27W%27)&$top=1000

                if (strSuburbInput == "unknown")
                {
                    strData = GetDataFromURL("https://www.citypower.co.za/_api/web/lists/getByTitle('Suburb%20&%20Block')/items");
                }
                else
                {
                    strData = GetDataFromURL("https://www.citypower.co.za/_api/web/lists/getByTitle('Suburb%20&%20Block')/items?$select=*&$filter=startswith(Suburb_x0020_Name,%27" + strSuburbInput + "%27)&$top=1000");
                }
                List<Suburbs> suburbs = IterateXML(strData);

                //If CityPower

                for (int i = 0; i <= suburbs.Count - 1; i++)
                {
                    dataRow = dataTable.NewRow();
                    dataRow["ID"] = suburbs[i].SuburbID;
                    dataRow["Suburb Name"] = suburbs[i].SuburbName;
                    dataRow["Provider"] = "Citypower";
                    dataTable.Rows.Add(dataRow);
                }
                //If Tshwane
                using (SqlConnection connection = new SqlConnection(_SQLConnection))
                {
                    connection.Open();
                    string query = "SELECT * FROM [dbo].[TshwaneSuburb] WHERE [Suburb] like '%" + strSuburbInput + "%'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataRow = dataTable.NewRow();

                                dataRow["ID"] = reader.GetInt32(1);
                                dataRow["Suburb Name"] = reader.GetString(0);
                                dataRow["Provider"] = "City of Tshwane";

                                dataTable.Rows.Add(dataRow);
                            }
                        }
                    }
                }

                //if Ekurhuleni
                using (SqlConnection connection = new SqlConnection(_SQLConnection))
                {
                    connection.Open();
                    string query = "SELECT * FROM [dbo].[EkuruleniSuburb] WHERE [Suburb] like '%" + strSuburbInput + "%'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataRow = dataTable.NewRow();

                                dataRow["ID"] = reader.GetInt32(0);
                                dataRow["Suburb Name"] = reader.GetString(1);
                                dataRow["Provider"] = "Ekurhuleni";

                                dataTable.Rows.Add(dataRow);
                            }
                        }
                    }
                }
                //Else If Eskom
                strData = GetDataFromURL("http://loadshedding.eskom.co.za/LoadShedding/GetSurburbData/?pageSize=10000&pageNum=1&id=" + strMunInput);

                int index1 = strData.IndexOf('[');
                strData = strData.Remove(0, index1);
                int index2 = strData.LastIndexOf(']');
                strData = strData.Substring(0, index2 + 1);

                var suburbData = JsonConvert.DeserializeObject<List<SuburbData>>(strData);

                foreach (var item in suburbData)
                {
                    if (item.text.Contains(strSuburbInput))
                    {
                        dataRow = dataTable.NewRow();
                        dataRow["ID"] = item.id;
                        dataRow["Suburb Name"] = item.text;
                        dataRow["Provider"] = "Eskom";
                        dataTable.Rows.Add(dataRow);
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
            try
            {
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add(new DataColumn("ID", typeof(String)));
                dataTable.Columns.Add(new DataColumn("Suburb Name", typeof(String)));
                dataTable.Columns.Add(new DataColumn("Provider", typeof(String)));

                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetDataTable: " + ex.ToString());
            }
        }

        private static string GetDataFromURL(string strURL)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData(strURL);

            string webData = System.Text.Encoding.UTF8.GetString(raw);

            return webData;
        }

        private List<Suburbs> IterateXML(string strData)
        {
            List<Suburbs> suburbs = new List<Suburbs>();

            string strSuburbID = string.Empty;
            string strSububName = string.Empty;
            bool bFound = false;

            try
            {
                XmlReader rdr = XmlReader.Create(new System.IO.StringReader(strData));
                while (rdr.Read())
                {
                    if (rdr.NodeType == XmlNodeType.Element)
                    {
                        if (rdr.LocalName == "Title")
                        {
                            strSuburbID = rdr.ReadInnerXml();
                            bFound = true;
                        }
                        if (rdr.LocalName == "Suburb_x0020_Name")
                        {
                            strSububName = rdr.ReadInnerXml();

                            suburbs.Add(new Suburbs() { SuburbID = strSuburbID, SuburbName = strSububName });
                        }
                    }
                }
                return suburbs;
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
    }

    public class Suburbs
    {
        public string SuburbID
        {
            get; set;
        }

        public string SuburbName
        {
            get; set;
        }

        public string Provider
        {
            get; set;
        }
    }

    public class SuburbData
    {
        public string id { get; set; }
        public string text { get; set; }
        public string tot { get; set; }
    }
}