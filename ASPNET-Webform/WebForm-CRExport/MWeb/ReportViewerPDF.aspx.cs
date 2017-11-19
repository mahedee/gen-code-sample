using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MWeb
{
    public partial class ReportViewerPDF : System.Web.UI.Page
    {
        string sRptFolder = string.Empty;
        string sRptName = string.Empty;

        ReportDocument ObjReportClientDocument = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            sRptName = Request.QueryString["sRptName"];
            if (!Page.IsPostBack)
            {
                hdn_URL.Value = this.Page.Request.UrlReferrer.AbsoluteUri.ToString();
                this.frame_view_report.Attributes["src"] = "ReportHolderPDF.aspx?sRptName=" + sRptName;
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (ObjReportClientDocument != null)
            {
                ObjReportClientDocument.Close();
                ObjReportClientDocument.Dispose();
                ObjReportClientDocument = null;
                GC.Collect();
            }
            Response.Redirect(hdn_URL.Value);
        }
    }
}