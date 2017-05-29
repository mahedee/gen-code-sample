using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using VS13CR11.App_Code.BLL;


namespace VS13CR11
{
    public partial class _Default : Page
    {
        //string sRptFolder = String.Empty;
        string sRptName = String.Empty;
        ArrayList parameterArrayList = new ArrayList();

        public _Default()
        {
            //sRptFolder = Request.PhysicalApplicationPath.ToString() + "Reports";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Load_ddlDesignation();
            }
        }

        private void Load_ddlDesignation()
        {
            DesignationBLL objDesignationBLL = new DesignationBLL();
            DataTable dtDesignation = new DataTable();
            dtDesignation = objDesignationBLL.GetDesignationAll();

            DataRow newDefaultRow = dtDesignation.NewRow();

            newDefaultRow["Id"] = "0";
            newDefaultRow["DesignationName"] = "--Select a Designation--";
            dtDesignation.Rows.InsertAt(newDefaultRow, 0);

            //dataSet1.Tables["Customers"].Rows.Add(newCustomersRow);


            this.ddlDesignation.DataSource = dtDesignation;
            this.ddlDesignation.DataTextField = "DesignationName";
            this.ddlDesignation.DataValueField = "Id";
            this.ddlDesignation.DataBind();
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            GetParameters();
            sRptName = "rpt_get_employeeinfo_by_designation_id.rpt";
            Response.Redirect("/ReportViewer.aspx?sRptName=" + sRptName);
            //Response.Redirect("/About.aspx?sRptName=" + sRptName);
        }

        private void GetParameters()
        {
            parameterArrayList.Add(0);
            parameterArrayList.Add(ddlDesignation.SelectedValue);
            Session["parameterArrayList"] = parameterArrayList;
        }
    }
}