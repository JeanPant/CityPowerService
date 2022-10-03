using CityPowerService;

//SourceCode References
using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System;
using System.Collections.Generic;

namespace K2CityPowerServiceService
{
    internal class K2CityPowerService : ServiceAssemblyBase
    {
        private const string _CityPowerBaseURL = "https://www.citypower.co.za/_api/web/lists/getByTitle('Loadshedding')/items";
        private const string _EiskomBaseURL = "https://loadshedding.eskom.co.za/LoadShedding/";
        private const string _SelectTop = "10000";
        private const string _SQLConnection = @"Server=SAF-4TYFLQ2\SQLEXPRESS;Database=Loadshedding;Trusted_Connection=True;";

        //Define the ServiceInstance Properties
        public override string GetConfigSection()
        {
            base.Service.ServiceConfiguration.Add("City Power Base URL", _CityPowerBaseURL);
            base.Service.ServiceConfiguration.Add("Eskom BaseURL", _EiskomBaseURL);
            base.Service.ServiceConfiguration.Add("Select Top", _SelectTop);
            base.Service.ServiceConfiguration.Add("SQL Connection String", _SQLConnection);

            return base.GetConfigSection();
        }

        //Define the properties of the ServiceInstance based on the values from the Resource File
        public override string DescribeSchema()
        {
            try
            {
                string strCityPowerBaseURL = string.Empty;
                string strEskomBaseURL = string.Empty;
                string strSelectTop = string.Empty;
                string strSQLConnection = string.Empty;

                GetStageMethod getStage = null;
                GetSuburbMethod getSuburb = null;
                GetLoadsheddingTimesMethod getTimes = null;
                GetProvinceMethod getProvince = null;
                GetMuniciplalitiesMethod getMuniciplalities = null;

                base.Service.Name = Resources.K2_Service_Name;
                base.Service.MetaData.DisplayName = Resources.K2_Service_DisplayName;
                base.Service.MetaData.Description = Resources.K2_Service_Description;

                strCityPowerBaseURL = base.Service.ServiceConfiguration["City Power Base URL"] + string.Empty;
                strEskomBaseURL = base.Service.ServiceConfiguration["Eskom BaseURL"] + string.Empty;
                strSelectTop = base.Service.ServiceConfiguration["Select Top"] + string.Empty;
                strSQLConnection = base.Service.ServiceConfiguration["SQL Connection String"] + string.Empty;

                getStage = new GetStageMethod(strCityPowerBaseURL, strSelectTop);
                base.Service.ServiceObjects.Create(getStage.DescribeServiceObject());
                getStage.Dispose();
                getStage = null;

                getSuburb = new GetSuburbMethod(strCityPowerBaseURL, strSelectTop, strSQLConnection);
                base.Service.ServiceObjects.Create(getSuburb.DescribeServiceObject());
                getSuburb.Dispose();
                getSuburb = null;

                getTimes = new GetLoadsheddingTimesMethod(strCityPowerBaseURL, strSelectTop, strSQLConnection);
                base.Service.ServiceObjects.Create(getTimes.DescribeServiceObject());
                getTimes.Dispose();
                getTimes = null;

                getProvince = new GetProvinceMethod(strEskomBaseURL, strSelectTop);
                base.Service.ServiceObjects.Create(getProvince.DescribeServiceObject());
                getProvince.Dispose();
                getProvince = null;

                getMuniciplalities = new GetMuniciplalitiesMethod(strEskomBaseURL, strSelectTop);
                base.Service.ServiceObjects.Create(getMuniciplalities.DescribeServiceObject());
                getMuniciplalities.Dispose();
                getMuniciplalities = null;

                return base.DescribeSchema();
            }
            catch (Exception ex)
            {
                throw new Exception("Error DescribeSchema: " + ex.ToString());
            }
        }

        public override void Extend()
        {
            throw new NotImplementedException();
        }

        //Set the Properties of the ServiceInstance based on the values entered.

        public override void Execute()
        {
            string strCityPowerBaseURL = string.Empty;
            string strEskomBaseURL = string.Empty;
            string strSelectTop = string.Empty;
            string strSQLConnection = string.Empty;

            Dictionary<string, object> dicProperties = new Dictionary<string, object>();
            Dictionary<string, object> dicParameters = new Dictionary<string, object>();

            GetStageMethod list = null;
            GetSuburbMethod getSuburb = null;
            GetLoadsheddingTimesMethod getTimes = null;
            GetProvinceMethod getProvince = null;
            GetMuniciplalitiesMethod getMuniciplalities = null;

            base.ServicePackage.ResultTable = null;

            try
            {
                strCityPowerBaseURL = base.Service.ServiceConfiguration["City Power Base URL"] + string.Empty;
                strEskomBaseURL = base.Service.ServiceConfiguration["Eskom BaseURL"] + string.Empty;
                strSelectTop = base.Service.ServiceConfiguration["Select Top"] + string.Empty;
                strSQLConnection = base.Service.ServiceConfiguration["SQL Connection String"] + string.Empty;

                foreach (ServiceObject serviceObject in base.Service.ServiceObjects)
                {
                    foreach (Method method in serviceObject.Methods)
                    {
                        foreach (Property property in serviceObject.Properties)
                        {
                            if ((property.Value != null) && (!string.IsNullOrEmpty(property.Value + string.Empty)))
                            {
                                dicProperties.Add(property.Name, property.Value);
                            }
                        }
                        foreach (MethodParameter methodParameter in method.MethodParameters)
                        {
                            if ((methodParameter.Value != null) && (!string.IsNullOrEmpty(methodParameter.Value + string.Empty)))
                            {
                                dicParameters.Add(methodParameter.Name, methodParameter.Value);
                            }
                        }

                        if (serviceObject.Name.Equals(Resources.K2_Search_ServiceObject_Name))
                        {
                            list = new GetStageMethod(strCityPowerBaseURL, strSelectTop);

                            if (method.Name.Equals(Resources.K2_Search_List_Method_Name))
                            {
                                base.ServicePackage.ResultTable = list.List(dicProperties, dicParameters);
                            }
                        }
                        else if (serviceObject.Name.Equals(Resources.K2_Suburb_ServiceObject_Name))
                        {
                            getSuburb = new GetSuburbMethod(strCityPowerBaseURL, strSelectTop, strSQLConnection);

                            if (method.Name.Equals(Resources.K2_Suburb_List_Method_Name))
                            {
                                base.ServicePackage.ResultTable = getSuburb.List(dicProperties, dicParameters);
                            }
                        }
                        else if (serviceObject.Name.Equals(Resources.K2_Times_ServiceObject_Name))
                        {
                            getTimes = new GetLoadsheddingTimesMethod(strCityPowerBaseURL, strSelectTop, strSQLConnection);

                            if (method.Name.Equals(Resources.K2_Times_List_Method_Name))
                            {
                                base.ServicePackage.ResultTable = getTimes.List(dicProperties, dicParameters);
                            }
                        }
                        else if (serviceObject.Name.Equals(Resources.K2_Province_ServiceObject_Name))
                        {
                            getProvince = new GetProvinceMethod(strEskomBaseURL, strSelectTop);

                            if (method.Name.Equals(Resources.K2_Province_List_Method_Name))
                            {
                                base.ServicePackage.ResultTable = getProvince.List(dicProperties, dicParameters);
                            }
                        }
                        else if (serviceObject.Name.Equals(Resources.K2_Municiplalities_ServiceObject_Name))
                        {
                            getMuniciplalities = new GetMuniciplalitiesMethod(strEskomBaseURL, strSelectTop);

                            if (method.Name.Equals(Resources.K2_Municiplalities_List_Method_Name))
                            {
                                base.ServicePackage.ResultTable = getMuniciplalities.List(dicProperties, dicParameters);
                            }
                        }
                    }
                }
                base.ServicePackage.IsSuccessful = true;
            }
            catch (Exception exp)
            {
                base.ServicePackage.IsSuccessful = false;
                base.ServicePackage.ServiceMessages.Add(new ServiceMessage(exp.Message, MessageSeverity.Error));

                throw new Exception("Error Execute: " + exp.Message);
            }
        }
    }
}