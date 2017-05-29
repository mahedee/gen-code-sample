/// *************************************************************************************************
///	|| Creation History ||
///	-------------------------------------------------------------------------------------------------
///	Copyright		:	Copyright© MAHEDEE.NET. All rights reserved.
///	NameSpace		:	VS13CR11.App_Code.DAL
/// Class           :   DesignationDAL
/// Inherits        :   None
///	Author			:	Md. Mahedee Hasan
///	Purpose			:	
///	Creation Date	:	30/11/2015
/// ==================================================================================================
///  || Modification History ||
///  -------------------------------------------------------------------------------------------------
///  Sl No.	Date:		Author:			Ver:	Area of Change:

///	**************************************************************************************************

using System;
using System.Data;
using System.Data.SqlClient;

namespace VS13CR11.App_Code.DAL
{
    public class DesignationDAL
    {
        private SqlConnection sqlConn;
        private SqlCommand cmd;

        private readonly DBConnector objDBConnector;


        /// <summary>
        /// Constructor
        /// </summary>
        public DesignationDAL()
        {
            objDBConnector = new DBConnector();
            sqlConn = objDBConnector.GetConn();
            cmd = objDBConnector.GetCommand();
        }


        /// <summary>
        /// Get all designation information
        /// </summary>
        /// <returns></returns>
        public DataTable GetDesignationAll()
        {
            DataTable tblEmpInfo = new DataTable();
            SqlDataReader rdr = null;

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT [Id],[DesignationName] FROM [dbo].[Designation]";

            try
            {
                if (sqlConn.State == ConnectionState.Closed)
                    sqlConn.Open();

                rdr = cmd.ExecuteReader();
                tblEmpInfo.Load(rdr);
            }
            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
            return tblEmpInfo;
        }
    }
}