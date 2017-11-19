using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using MWeb.App_Code.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MWeb
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        ReportDocument ObjReportClientDocument = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetReportDocument();
            }

            ViewReport();
        }


        private void ViewReport()
        {
            GetReportDocument();
            CRViewer.ReportSource = ObjReportClientDocument;

        }


        private void GetReportDocument()
        {
            ReportBase objclsReportBase = new ReportBase();
            string sRptFolder = Server.MapPath("~/Reports");
            string sRptName = string.Empty;

            ArrayList ParameterArrayList = new ArrayList();
            ParameterArrayList = (ArrayList)Session["parameterArrayList"];
            sRptName = Request.QueryString["sRptName"].Trim();

            ObjReportClientDocument = objclsReportBase.PFSubReportConnectionMainParameter(sRptName, ParameterArrayList, sRptFolder);

            foreach (Section oSection in ObjReportClientDocument.ReportDefinition.Sections)
            {
                foreach (ReportObject obj in oSection.ReportObjects)
                {
                    FieldObject field;
                    field = ObjReportClientDocument.ReportDefinition.ReportObjects[obj.Name] as FieldObject;



                    if (field != null)
                    {
                        Font oFon1 = new Font("Arial Narrow", field.Font.Size - 1F);
                        Font oFon2 = new Font("Arial", field.Font.Size - 1F);

                        if (oFon1 != null)
                        {
                            field.ApplyFont(oFon1);
                        }
                        else if (oFon2 != null)
                        {
                            field.ApplyFont(oFon2);
                        }
                    }
                }
            }
        }

        protected void CrystalReportViewer1_ReportRefresh(object source, ViewerEventArgs e)
        {
            OnInit(e);
            ViewReport();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (ObjReportClientDocument != null)
            {
                ObjReportClientDocument.Close();
                ObjReportClientDocument.Dispose();
                ObjReportClientDocument = null;
                // call GC to force collect the garbages -- not so sure whether this line is
                // necessary or not, but let us to be in the safe side.
                GC.Collect();
            }

            Response.Redirect("~/Default.aspx");
        }
    }
}