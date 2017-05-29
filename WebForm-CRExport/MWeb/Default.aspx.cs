using MWeb.App_Code.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MWeb
{
    public partial class Default : System.Web.UI.Page
    {
        string sRptName = String.Empty;
        ArrayList parameterArrayList = new ArrayList();


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


            this.ddlDesignation.DataSource = dtDesignation;
            this.ddlDesignation.DataTextField = "DesignationName";
            this.ddlDesignation.DataValueField = "Id";
            this.ddlDesignation.DataBind();
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            GetParameters();
            sRptName = "rpt_get_employeeinfo_by_designation_id.rpt";

            string expValue = this.ddlExpReport.SelectedValue;

            if (expValue == "1")
            {
                Response.Redirect("ReportViewerPDF.aspx?sRptName=" + sRptName);
            }
            else if (expValue == "2")
            {
                Response.Redirect("/ReportViewer.aspx?sRptName=" + sRptName);
            }
            else if (expValue == "3")
            {
                Response.Redirect("ReportViewerExcel.aspx?sRptName=" + sRptName);
            }

            else if (expValue == "4")
            {
                Response.Redirect("ReportViewerDoc.aspx?sRptName=" + sRptName);

            }
        }

        private void GetParameters()
        {
            parameterArrayList.Add(0);
            parameterArrayList.Add(ddlDesignation.SelectedValue);
            Session["parameterArrayList"] = parameterArrayList;
        }
    }
}