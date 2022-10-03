using CityPowerService;

//SourceCode References
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System;
using System.Collections.Generic;
using System.Data;

namespace K2CityPowerServiceService
{
    internal class GetStageMethod : IDisposable
    {
        //Local Variables to match ServiceInstance Configurations
        private string _CityPowerBaseURL = string.Empty;

        private string _SelectTop = string.Empty;

        internal GetStageMethod()
        {
        }

        internal GetStageMethod(string strCityPowerBaseURL, string strSelectTop)
        {
            _CityPowerBaseURL = strCityPowerBaseURL;
            _SelectTop = strSelectTop;
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
                serviceObject.Name = Resources.K2_Search_ServiceObject_Name;
                serviceObject.MetaData.DisplayName = Resources.K2_Search_ServiceObject_DisplayName;
                serviceObject.MetaData.Description = Resources.K2_Search_ServiceObject_Description;
                serviceObject.Type = typeof(GetStageMethod).Name;
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

                methods.Create(new Method(Resources.K2_Search_List_Method_Name, MethodType.List, new MetaData(Resources.K2_Search_List_Method_DisplayName,
                       Resources.K2_Search_List_Method_Description),
                       GetRequiredProperties(serviceObject, Resources.K2_Search_List_Method_Name),
                       GetMethodParameters(serviceObject, Resources.K2_Search_List_Method_Name),
                       GetInputProperties(serviceObject, Resources.K2_Search_List_Method_Name),
                       GetReturnProperties(serviceObject, Resources.K2_Search_List_Method_Name)));

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

                /*if (strMethodName.Equals(Resources.K2_Search_List_Method_Name))
                {
                    inputProperties.Add(serviceObject.Properties["InputProperty1"]);
                }*/

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

                if (strMethodName.Equals(Resources.K2_Search_List_Method_Name))
                {
                    returnProperties.Add(serviceObject.Properties["Stage"]);
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

            try
            {
                int intStage = 0;
                string[] arr1 = new string[1];
                dataTable = GetDataTable();

                intStage = int.Parse(GetDataFromURL("https://loadshedding.eskom.co.za/LoadShedding/GetStatus")) - 1;
                if (intStage > 0)
                {
                    arr1[0] = intStage.ToString();
                }
                else
                {
                    arr1[0] = "0";
                }

                foreach (var item in arr1)
                {
                    dataRow = dataTable.NewRow();
                    dataRow["Stage"] = item;
                    dataTable.Rows.Add(dataRow);
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

            dataTable.Columns.Add(new DataColumn("Stage", typeof(String)));

            return dataTable;
        }

        private static string GetDataFromURL(string strURL)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData(strURL);

            string webData = System.Text.Encoding.UTF8.GetString(raw);

            return webData;
        }
    }
}