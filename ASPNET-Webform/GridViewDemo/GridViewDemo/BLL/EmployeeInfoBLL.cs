/// Class           :   EmployeeInfoBLL
///	Author			:	Md. Mahedee Hasan
///	Purpose			:	Business Logic Layer - Write business logic here
///	Creation Date	:	21/04/2012
/// ==================================================================================================
///  || Modification History ||
///  -------------------------------------------------------------------------------------------------
///  Sl No.	Date:		Author:			    Ver:	Area of Change:     
///  
///	**************************************************************************************************

using GridViewDemo.DAL;
using GridViewDemo.Models;
using System;
using System.Data;

namespace GridViewDemo.BLL
{
    public class EmployeeInfoBLL
    {
        public DataTable GetEmployeeInfoAll()
        {
            try
            {
                EmployeeInfoDAL objEmployeeInfoDAL = new EmployeeInfoDAL();
                return objEmployeeInfoDAL.GetEmployeeInfoAll();
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objEmployeeInfo"></param>
        /// <returns></returns>
        public string SaveEmployeeInfo(EmployeeInfo objEmployeeInfo)
        {
            try
            {
                EmployeeInfoDAL objEmployeeInfoDAL = new EmployeeInfoDAL();
                return objEmployeeInfoDAL.InsertEmployeeInfo(objEmployeeInfo);
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objEmployeeInfo"></param>
        /// <returns></returns>
        public string EditEmployeeInfo(EmployeeInfo objEmployeeInfo)
        {
            try
            {
                EmployeeInfoDAL objEmployeeInfoDAL = new EmployeeInfoDAL();
                return objEmployeeInfoDAL.UpdateEmployeeInfo(objEmployeeInfo);
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objEmployeeInfo"></param>
        /// <returns></returns>
        public string RemoveEmployeeInfo(int empGid)
        {
            try
            {
                EmployeeInfoDAL objEmployeeInfoDAL = new EmployeeInfoDAL();
                return objEmployeeInfoDAL.DeleteEmployeeInfo(empGid);
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
    }
}