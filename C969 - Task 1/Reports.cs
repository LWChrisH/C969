using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Task_1
{
    class Report
    {
        string _name;
        string _sqlCode;
        string _subSqlCode;
        
        public Report(string name, string sqlCode)
        {
            Name = name;
            SqlCode = sqlCode;
        }
        public Report(string name, string sqlCode, string subSqlCode)
        {
            Name = name;
            SqlCode = sqlCode;
            SubSqlCode = subSqlCode;
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string SqlCode
        {
            get { return _sqlCode; }
            set { _sqlCode = value; }
        }
        public string SubSqlCode
        {
            get { return _subSqlCode; }
            set { _subSqlCode = value; }
        }
        public string Execute()
        {
            if (SubSqlCode == default(string))
            {
                return DatabaseInterface.AdHocQueryToHTMLTable(_sqlCode);
            }
            else
            {
                string ret = "";
                foreach (string s in DatabaseInterface.AdHocQueryToStringList(_sqlCode))
                {
                    try
                    {
                        ret += "<p>" + s + "</p>";
                        ret += DatabaseInterface.AdHocQueryToHTMLTable(_subSqlCode, s);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + " s=" + s + "; subcode: " + _subSqlCode);
                    }
                }
                return ret;
            }
        }
    }
    static class Reports
    {
        static List<Report> _reports = new List<Report>();
        static Reports()
        {
            _reports.Clear();
            _reports.Add(new Report(Language.LanguageFill("#numberofappointmenttypesbymonth")
                , "SELECT"
                    + " CONCAT(MONTHNAME(start), ' ', YEAR(start)) "
                    + " AS `" + Language.LanguageFill("#month") + "`"
                    + " , type "
                    + " as `" + Language.LanguageFill("#appointmenttype") + "`"
                    + " , COUNT(*) "
                    + " AS `" + Language.LanguageFill("#numberofappointments") + "` "
                    + " FROM appointment "
                    + " group by concat(monthname(start), ' ', year(start)), month(start), year(start), type "
                    + " order by year(start), month(start), type "));
            _reports.Add(new Report(Language.LanguageFill("#scheduleforeachconsultant")
                , "select u.username from user u inner join appointment a on a.userId = u.userId group by u.username"
                , "select "
                    + " c.customerName "
                    + " as `" + Language.LanguageFill("#customer") + "`"
                    + " , a.title "
                    + " as `" + Language.LanguageFill("#title") + "`"
                    + " , a.type "
                    + " as `" + Language.LanguageFill("#appointmenttype") + "`"
                    + " , a.start "
                    + " as `" + Language.LanguageFill("#start") + "`"
                    + " , timestampdiff(MINUTE, a.start,a.end) "
                    + " as `" + Language.LanguageFill("#duration") + "`"
                    + " from appointment a "
                    + " inner join user u on a.userId = u.userId "
                    + " inner join customer c on c.customerId = a.customerId "
                    + " where u.username = @p0 and a.start > utc_timestamp();"));
            _reports.Add(new Report(Language.LanguageFill("#myscheduledhourspermonth")
                , " select "
                    + " concat(monthname(start), ' ', year(start)) "
                    + " AS `" + Language.LanguageFill("#month") + "`"
                    + " , sum(timestampdiff(MINUTE, a.start, a.end)+1) / 60 "
                    + " as `" + Language.LanguageFill("#hours") + "`"
                    + " , COUNT(*) "
                    + " AS `" + Language.LanguageFill("#numberofappointments") + "` "
                    + " FROM appointment a "
                    + " inner join user u on a.userId = u.userId "
                    + " where u.userName = @username "
                    + " group by concat(monthname(start), ' ', year(start)), month(start), year(start) "
                    + " order by year(start), month(start) "));
        }
        public static List<Report> AllReports
        {
            get { return _reports; }
        }
    }
    class ReportsListView : ListView
    {
        public ReportsListView()
        {
            this.Clear();
            this.View = View.Details;
            foreach (string columnText in (new string[] { "#reportname" }))
            {
                this.Columns.Add(columnText, Language.LanguageFill(columnText));
            }
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            this.FullRowSelect = true;
            this.Resize += new System.EventHandler(UpdateColumns);
            this.MultiSelect = false;
            this.DoubleClick += ReportsListView_DoubleClick;
            RefreshReports();
        }

        private void ReportsListView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string reportName = this.SelectedItems[0].Text;
                string htmlCode = Reports.AllReports.Where(a => a.Name == reportName).First().Execute();
                Form form = new ReportViewer(htmlCode, reportName);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.LanguageFill("#errorrunningreport " + ex.Message));
            }
        }

        void UpdateColumns(object sender, EventArgs e)
        {
            foreach (ColumnHeader header in this.Columns)
            {
                header.Width = (this.Width - 1) / this.Columns.Count - 1;
            }
        }
        public void RefreshReports()
        {
            this.Items.Clear();
            foreach (Report report in Reports.AllReports)
            {
                this.Items.Add(report.Name) ;
            }
        }
    }
}
