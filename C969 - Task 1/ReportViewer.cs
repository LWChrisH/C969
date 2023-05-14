using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Task_1
{
    public partial class ReportViewer : Form
    {
        public ReportViewer(string htmlCode, string reportName)
        {
            InitializeComponent();
            webBrowser1.DocumentText = htmlCode;
            webBrowser1.AccessibleDescription = htmlCode;
            this.Text = reportName;
        }
    }
}
