using FMS_UI_PROJECT.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS_UI_PROJECT.Report
{
    public partial class RPTViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viewReport();
        }

        public void viewReport()
        {
            List<FMS_Report> _list = Session["reports"] as List<FMS_Report>;
            string reportName = "FMS_Report";
            if (_list != null && _list.Count > 0)
            {
                ReportDataSource rdc = new ReportDataSource("DataSet1", _list);
                ReportViewer1.Enabled = true;
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Report/RPT_") + reportName + ".rdlc";
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.DataBind();
            }

            Session["reports"] = null;
        }
    }
}