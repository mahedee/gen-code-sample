/// *************************************************************************************************
///	|| Creation History ||
///	-------------------------------------------------------------------------------------------------
///	Copyright		:	Copyright© MAHEDEE.NET. All rights reserved.
///	NameSpace		:	MWeb.App_Code.Utility
/// Class           :   ReportBase
/// Inherits        :   None
///	Author			:	Md. Mahedee Hasan
///	Purpose			:	Contains common validator
///	Creation Date	:	30/11/2015
/// ==================================================================================================
///  || Modification History ||
///  -------------------------------------------------------------------------------------------------
///  Sl No.	Date:		Author:			Ver:	Area of Change:

///	**************************************************************************************************
///	

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWeb.App_Code.Utility
{
    public class ReportBase
    {
        
    string ServerName = System.Configuration.ConfigurationManager.AppSettings["ServerName"];
    string DataBaseName = System.Configuration.ConfigurationManager.AppSettings["DBName"];
    string UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"];
    string Password = System.Configuration.ConfigurationManager.AppSettings["Password"];


    ReportDocument crReportDocument = new ReportDocument();



    public ReportBase()
    {
    }
    public ReportDocument PFSubReportConnectionMainParameter(string ReportName, ArrayList ParamArraylist, string ReportFolder) //As ReportDocument
    {
        int ObjArrayA, ObjArrayB, Paraindex;
        string Paravalue;
        ObjArrayB = 0;

        string path = ReportFolder + @"\" + @ReportName;

        try
        {

            crReportDocument.Load(path);
        }
        catch (Exception ex)
        {

            string msg = "The report file " + path +
                        " can not be loaded, ensure that the report exists or the path is correct." +
                        "\nException:\n" + ex.Message +
                        "\nSource:" + ex.Source +
                        "\nStacktrace:" + ex.StackTrace;
            //LM_BaseClass.LM_LogError(msg);
            throw new Exception(msg);
        }

        //        Catch Err As Exception
        //            I = 1 ''//Report Application Server Not Found
        //            ErrStr = Err.Message
        //            Return Nothing
        //        End Try

        //        ' Call the Logon function passing in our ReportDocument
        //        ' and connectivity values.
        //        Try
        if (!Logon(crReportDocument, ServerName, DataBaseName, UserID, Password))
        {
            // login failed
            //CrystalDecisions.CrystalReports.Engine.ReportDocument.
            string msg = "Can not login to Report Server " +
                        "\nDatabase Server: " + ServerName +
                        "\nDatabase:\n" + DataBaseName +
                        "\nDBUser:" + UserID +
                        "\nDBPassword:" + Password;
            //LM_BaseClass.LM_LogError(msg);
            throw new Exception(msg);
        }
        Logon(crReportDocument, ServerName, DataBaseName, UserID, Password);
        //        Catch Err As Exception
        //            I = 2
        //            ErrStr = Err.Message
        //            Return Nothing
        //        End Try

        //        Try
        //To Check Parameter Feild Array have the Same Amount of Parameter Feild in the Report
        int ParamArrayCount, ParameterFieldCount;
        //Getting Value from the Array

        if (ParamArraylist.Count != 0)
        {
            ParamArrayCount = (ParamArraylist.Count / 2);

            //Getting Value From the Report (Parameter and Formula Feild)
            ParameterFieldCount = crReportDocument.DataDefinition.ParameterFields.Count;

            //Parameter on The Report and Array List Parameter Amount is not the same
            ParamArrayCount = ParameterFieldCount;

            //        Catch Err As Exception
            //            I = 8 ''//Report is using Crystal Report Engin Version 9
            //            ErrStr = Err.Message
            //            Return Nothing
            //        End Try


            //        Try
            for (ObjArrayA = 0; ObjArrayA < ((ParamArraylist.Count / 2)); ObjArrayA++)
            {
                Paraindex = (int)ParamArraylist[ObjArrayB];
                Paravalue = (string)ParamArraylist[ObjArrayB + 1];
                PassParameter(Paraindex, Paravalue);
                ObjArrayB = ObjArrayB + 2;
            }
        }

        //        Catch Err As Exception
        //            I = 9 ''//Problem inserting Formula and Parameter Feild
        //            ErrStr = Err.Message
        //            Return Nothing
        //        End Try

        return crReportDocument;

    }
    public void PassParameter(int ParameterIndex, string ParameterValue)
    {
        //        '
        //        ' Declare the parameter related objects.
        //        '
        ParameterDiscreteValue crParameterDiscreteValue;
        ParameterFieldDefinitions crParameterFieldDefinitions;
        ParameterFieldDefinition crParameterFieldLocation;
        ParameterValues crParameterValues;

        //        '
        //        ' Get the report's parameters collection.
        //        '
        //string msg = "Improper parameter list, Parameter defined at report do not match with parameter passed to report " +
        //            "\nReport Path = " + ReportPath +
        //            "\n" + originalParamList +
        //            "\n" + passedParameterList;

        //LM_BaseClass.LM_LogError(msg);
        //throw new Exception(msg);

        crParameterFieldDefinitions = crReportDocument.DataDefinition.ParameterFields;
        crParameterFieldLocation = (ParameterFieldDefinition)crParameterFieldDefinitions[ParameterIndex];
        crParameterValues = crParameterFieldLocation.CurrentValues;
        crParameterDiscreteValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
        crParameterDiscreteValue.Value = System.Convert.ToString(ParameterValue);
        crParameterValues.Add(crParameterDiscreteValue);
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues);
    }

    private bool Logon(CrystalDecisions.CrystalReports.Engine.ReportDocument cr, string server, string database, string user_id, string password)
    {
        // Declare and instantiate a new connection info object.
        CrystalDecisions.Shared.ConnectionInfo ci;
        ci = new CrystalDecisions.Shared.ConnectionInfo();

        ci.ServerName = server;
        ci.DatabaseName = database;
        ci.UserID = user_id;
        ci.Password = password;//password;
        //  ci.IntegratedSecurity = false;

        // If the ApplyLogon function fails then return a false for this function.
        // We are applying logon information to the main report at this stage.
        if (!ApplyLogon(cr, ci))
        {
            return false;
        }

        // Declare a subreport object.
        CrystalDecisions.CrystalReports.Engine.SubreportObject subobj;

        // Loop through all the report objects and locate subreports.
        // If a subreport is found then apply logon information to
        // the subreport.
        foreach (CrystalDecisions.CrystalReports.Engine.ReportObject obj in cr.ReportDefinition.ReportObjects)
        {
            if (obj.Kind == CrystalDecisions.Shared.ReportObjectKind.SubreportObject)
            {
                subobj = (CrystalDecisions.CrystalReports.Engine.SubreportObject)obj;
                if (!ApplyLogon(cr.OpenSubreport(subobj.SubreportName), ci))
                {
                    return false;
                }
            }
        }

        // Return True if the code runs to this stage.
        return true;

    }

    private bool ApplyLogon(CrystalDecisions.CrystalReports.Engine.ReportDocument cr, CrystalDecisions.Shared.ConnectionInfo ci)
    {
        // This function is called by the "Logon" function
        // It loops through the report tables and applies
        // the connection information to each table.

        // Declare the TableLogOnInfo object and a table object for use later.
        CrystalDecisions.Shared.TableLogOnInfo li;
        // For each table apply connection info.

        foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in cr.Database.Tables)
        {

            li = tbl.LogOnInfo;
            li.ConnectionInfo.ServerName = ci.ServerName;
            li.ConnectionInfo.DatabaseName = ci.DatabaseName;
            li.ConnectionInfo.UserID = ci.UserID;
            li.ConnectionInfo.Password = ci.Password;
            tbl.ApplyLogOnInfo(li);
            tbl.Location = ci.DatabaseName + ".dbo." + tbl.Name;

            // Verify that the logon was successful.
            // If TestConnectivity returns false, correct table locations.
            if (!tbl.TestConnectivity())
            {
                return false;
            }
        }
        return true;
    }
    }
}