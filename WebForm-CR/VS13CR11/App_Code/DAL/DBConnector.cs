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
///	
using System.Configuration;
using System.Data.SqlClient;

namespace VS13CR11.App_Code.DAL
{
    public class DBConnector
    {
        private string connectionString = null;
        private SqlConnection sqlConn = null;
        private SqlCommand cmd = null;

        public DBConnector()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }

        public SqlCommand GetCommand()
        {
            cmd = new SqlCommand();
            cmd.Connection = sqlConn;
            return cmd;
        }

        public SqlConnection GetConn()
        {
            sqlConn = new SqlConnection(connectionString);
            return sqlConn;
        }
    }
}