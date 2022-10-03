using CityPowerService;

//SourceCode References
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System;
using System.Collections.Generic;
using System.Data;

namespace K2CityPowerServiceService
{
    internal class GetProvinceMethod : IDisposable
    {
        //Local Variables to match ServiceInstance Configurations
        private string _CityPowerBaseURL = string.Empty;

        private string _SelectTop = string.Empty;

        internal GetProvinceMethod()
        {
        }

        internal GetProvinceMethod(string strCityPowerBaseURL, string strSelectTop)
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
                serviceObject.Name = Resources.K2_Province_ServiceObject_Name;
                serviceObject.MetaData.DisplayName = Resources.K2_Province_ServiceObject_DisplayName;
                serviceObject.MetaData.Description = Resources.K2_Province_ServiceObject_Description;
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

                properties.Create(new Property("ID", "System.String", SoType.Text, new MetaData("ID", string.Empty)));
                properties.Create(new Property("Province", "System.String", SoType.Text, new MetaData("Province", string.Empty)));

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

                methods.Create(new Method(Resources.K2_Province_List_Method_Name, MethodType.List, new MetaData(Resources.K2_Province_List_Method_DisplayName,
                       Resources.K2_Province_List_Method_Description),
                       GetRequiredProperties(serviceObject, Resources.K2_Province_List_Method_Name),
                       GetMethodParameters(serviceObject, Resources.K2_Province_List_Method_Name),
                       GetInputProperties(serviceObject, Resources.K2_Province_List_Method_Name),
                       GetReturnProperties(serviceObject, Resources.K2_Province_List_Method_Name)));

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

                if (strMethodName.Equals(Resources.K2_Province_List_Method_Name))
                {
                    returnProperties.Add(serviceObject.Properties["ID"]);
                    returnProperties.Add(serviceObject.Properties["Province"]);
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
                dataTable = GetDataTable();

                dataRow = dataTable.NewRow();
                dataRow["ID"] = "1";
                dataRow["Province"] = "Eastern Cape";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["ID"] = "2";
                dataRow["Province"] = "Free State";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["ID"] = "3";
                dataRow["Province"] = "Gauteng";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["ID"] = "4";
                dataRow["Province"] = "KwaZulu-Natal";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["ID"] = "5";
                dataRow["Province"] = "Limpopo";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["ID"] = "6";
                dataRow["Province"] = "Mpumalanga";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["ID"] = "7";
                dataRow["Province"] = "North West";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["ID"] = "8";
                dataRow["Province"] = "Northern Cape";
                dataTable.Rows.Add(dataRow);

                dataRow = dataTable.NewRow();
                dataRow["ID"] = "9";
                dataRow["Province"] = "Western Cape";
                dataTable.Rows.Add(dataRow);
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
                dataTable.Columns.Add(new DataColumn("Province", typeof(String)));

                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetDataTable: " + ex.ToString());
            }
        }
    }

    public class Province
    {
        public string ProvinceID
        {
            get; set;
        }

        public string ProvincebName
        {
            get; set;
        }
    }
}