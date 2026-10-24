using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace CSLab09_LINQ_Report
{
    public partial class frmReport : Form
    {
        public string HoTenKH { get; set; }
        public List<HangChonMua> DSMua { get; set; }

        public frmReport(string hoten, List<HangChonMua> dsmua)
        {
            InitializeComponent();
            HoTenKH = hoten;
            DSMua = dsmua;
        }

        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            ReportParameter[] param = new ReportParameter[1];
            param[0] = new ReportParameter("txtHoTenKH", HoTenKH);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsHangChonMua", DSMua));
            reportViewer1.LocalReport.SetParameters(param);
            this.reportViewer1.RefreshReport();
        }
    }
}