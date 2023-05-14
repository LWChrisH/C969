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
    partial class MainScreen : Form
    {
        bool _isUserInteraction;
        List<int> _appointmentReminders = new List<int>();
        public MainScreen()
        {
            InitializeComponent();
            customerListView.RefreshCustomers();
            customerListView.DoubleClick += CustomerListView_DoubleClick;
            RefreshAppointments();
            dayRadioButton.CheckedChanged += DayRadioButton_CheckedChanged;
            weekRadioButton.CheckedChanged += WeekRadioButton_CheckedChanged;
            monthRadioButton.CheckedChanged += MonthRadioButton_CheckedChanged;
            appointmentMonthCalendar.DateChanged += AppointmentMonthCalendar_DateChanged;
            meRadioButton.CheckedChanged += MeRadioButton_CheckedChanged;
            everyoneRadioButton.CheckedChanged += EveryoneRadioButton_CheckedChanged;
            appointmentListView.DoubleClick += AppointmentListView_DoubleClick;
            OrganizeTabIndex(this.Controls);
            reportsListView1.RefreshReports();
            //need to update X values because some calendars are wider depending on language selected.
            int calendarDifferential = appointmentMonthCalendar.Width - 173;
            userFilterRadioGroup.Location = new Point(userFilterRadioGroup.Location.X + calendarDifferential, userFilterRadioGroup.Location.Y);
            timeframeRadioGroup.Location = new Point(timeframeRadioGroup.Location.X + calendarDifferential, timeframeRadioGroup.Location.Y);
            appointmentAddButton.Location = new Point(appointmentAddButton.Location.X + calendarDifferential, appointmentAddButton.Location.Y);
            appointmentEditButton.Location = new Point(appointmentEditButton.Location.X + calendarDifferential, appointmentEditButton.Location.Y);
            appointmentDeleteButton.Location = new Point(appointmentDeleteButton.Location.X + calendarDifferential, appointmentDeleteButton.Location.Y);
            appointmentRefreshButton.Location = new Point(appointmentRefreshButton.Location.X + calendarDifferential, appointmentRefreshButton.Location.Y);
            reminderTimer.Tick += ReminderTimer_Tick;
            reminderTimer.Interval = 60000;
            reminderTimer.Start();
            reminderTimer.Enabled = true;
            AppointmentReminderCheck();
        }

        private void ReminderTimer_Tick(object sender, EventArgs e) => AppointmentReminderCheck();
        private void AppointmentReminderCheck()
        {
            List<Appointment> upcomingAppointments = DatabaseInterface.AppointmentList(new User(Session.GetVariable("username")).ID, DateTime.Now, DateTime.Now.AddMinutes(15));

            if (upcomingAppointments.Count() > 0)
            {
                bool showAlert = false;
                int firstAppointmentId = 0;
                string alertMessage = Language.LanguageFill("#upcomingappointmentalert")+"\n\n";
                foreach (Appointment appointment in upcomingAppointments)
                {
                    if (!_appointmentReminders.Contains(appointment.ID) && appointment.Start >= DateTime.Now)
                    {
                        if (!showAlert)
                        {
                            firstAppointmentId = appointment.ID;
                        }
                        _appointmentReminders.Add(appointment.ID);
                        alertMessage += appointment.Customer.Name + " @ " + appointment.Start.ToString("yyyy-MM-dd HH:mm") + "\n";
                        showAlert = true;
                    }
                }
                if (showAlert)
                {
                    alertMessage += "\n" + Language.LanguageFill("#edit #appointment ?");
                    DialogResult result = MessageBox.Show(alertMessage, Language.LanguageFill("#upcomingappointment"), MessageBoxButtons.YesNo, MessageBoxIcon.None);
                    if (result == DialogResult.Yes)
                    {
                        Form form = new AppointmentForm(new Appointment(firstAppointmentId));
                        Language.LanguageFill(ref form);
                        form.ShowDialog();
                        RefreshAppointments();
                    }
                }
            }
        }

        private void EveryoneRadioButton_CheckedChanged(object sender, EventArgs e) => RefreshAppointments();

        private void MeRadioButton_CheckedChanged(object sender, EventArgs e) => RefreshAppointments();

        void OrganizeTabIndex(Control.ControlCollection controls)
        {
            List<Control> controlList = controls.Cast<Control>().ToList();
            //lambda expression is used here for ordering controls by x, then y.
            //LINQ require the use of lambda expressions, so this lambda expression makes the program more efficient by enabling the use of LINQ order functions.
            //admittedly, this isn't really a requirement of the program, but makes it eaiser for keyboard users by assigning tab indexes in left->right, top-> bottom order.
            controlList = controlList.OrderBy(c => c.Location.Y).ThenBy(c => c.Location.X).ToList();
            {//code block to define scope of tabIndex.
                int tabIndex = 0;
                foreach (Control control in controlList)
                {
                    control.TabIndex = tabIndex;
                    tabIndex++;
                    if (control.HasChildren)
                    {
                        OrganizeTabIndex(control.Controls);
                    }
                }
            }
        }

        private void AppointmentMonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (_isUserInteraction)
                UpdateCalendar();
            else
                RefreshAppointments();
        }

        private void WeekRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                UpdateCalendar();
        }

        private void MonthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                UpdateCalendar();
        }

        private void DayRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                UpdateCalendar();
        }
        private void UpdateCalendar()
        {
            _isUserInteraction = false;
            int startOffset = 0; //day
            int endOffset = 0;
            if (dayRadioButton.Checked)
            {
                appointmentMonthCalendar.MaxSelectionCount = 1;
                startOffset = 0;
                endOffset = 0;
            } else if (weekRadioButton.Checked)
            {
                appointmentMonthCalendar.MaxSelectionCount = 7;
                startOffset = (int)appointmentMonthCalendar.SelectionStart.DayOfWeek;
                endOffset = 6;
            } else if (monthRadioButton.Checked)
            {
                startOffset = appointmentMonthCalendar.SelectionStart.Day - 1;
                //end offset: one less than the day number of the day before the beginning of the following month.
                endOffset = appointmentMonthCalendar.SelectionStart.AddDays(-1 * startOffset).AddMonths(1).AddDays(-1).Day-1;
                appointmentMonthCalendar.MaxSelectionCount = endOffset + 1;
            }
            DateTime startDate = appointmentMonthCalendar.SelectionStart.AddDays(-1 * startOffset);
            DateTime endDate = startDate.AddDays(endOffset);
            appointmentMonthCalendar.SetSelectionRange(startDate, endDate);
            _isUserInteraction = true;
        }
        private bool ShowEveryonesAppointments()
        {
            if (everyoneRadioButton.Checked)
                return true;
            return false;
        }
        private void RefreshAppointments()
        {
            try
            {
                appointmentListView.RefreshAppointments(
                    ShowEveryonesAppointments() ? null : (int?)(new User(Session.GetVariable("username"))).ID
                    , StartDate(), EndDate());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.InnerException);
            }
        }
        private DateTime StartDate()
        {
            return appointmentMonthCalendar.SelectionStart;
        }
        private DateTime EndDate()
        {
            return appointmentMonthCalendar.SelectionEnd.Date.AddDays(1).AddMilliseconds(-1);
        }
        private void CustomerListView_DoubleClick(object sender, EventArgs e) => SelectedCustomerEdit();

        void InitListView()
        {
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void customerRefreshButton_Click(object sender, EventArgs e) => customerListView.RefreshCustomers();
        private void customerEditButton_Click(object sender, EventArgs e) => SelectedCustomerEdit();
        private void SelectedCustomerEdit()
        {
            if (customerListView.SelectedItems.Count == 1)
            {
                Form customerForm = new CustomerForm(customerListView.SelectedCustomer());
                Language.LanguageFill(ref customerForm);
                customerForm.ShowDialog();
                customerListView.RefreshCustomers();
            }
            else
            {
                MessageBox.Show(Language.LanguageFill("#selectcustomer"));
            }
        }

        private void customerRemoveButton_Click(object sender, EventArgs e)
        {
            if(customerListView.SelectedItems.Count == 1)
            {
                DialogResult result = MessageBox.Show(
                    Language.LanguageFill("#confirmdelete #customer ?")
                    , Language.LanguageFill("#confirm")
                    , MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes && DatabaseInterface.CustomerRecordDelete((int)customerListView.SelectedItems[0].Tag))
                {
                    customerListView.Items.Remove(customerListView.SelectedItems[0]);
                }
            }
            else
            {
                MessageBox.Show(Language.LanguageFill("#selectcustomer"));
            }
        }

        private void customerAddButton_Click(object sender, EventArgs e)
        {
            Form customerForm = new CustomerForm();
            Language.LanguageFill(ref customerForm);
            customerForm.ShowDialog();
            customerForm.Dispose();
        }

        private void logffButton_Click(object sender, EventArgs e)
        {
            Session.Logoff();
            this.Close();
        }

        private void appointmentDeleteButton_Click(object sender, EventArgs e)
        {
            if (appointmentListView.SelectedItems.Count == 1)
            {
                DialogResult result = MessageBox.Show(
                    Language.LanguageFill("#confirmdelete #appointment ?")
                    , Language.LanguageFill("#confirm")
                    , MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes && DatabaseInterface.AppointmentRecordDelete((int)appointmentListView.SelectedItems[0].Tag))
                {
                    appointmentListView.Items.Remove(appointmentListView.SelectedItems[0]);
                }
            }
            else
            {
                MessageBox.Show(Language.LanguageFill("#selectcustomer"));
            }
        }

        private void appointmentAddButton_Click(object sender, EventArgs e)
        {
            Form form = new AppointmentForm(appointmentMonthCalendar.SelectionStart.Date);
            Language.LanguageFill(ref form);
            form.ShowDialog();
            form.Dispose();
            RefreshAppointments();
        }

        private void appointmentRefreshButton_Click(object sender, EventArgs e)
        {
            RefreshAppointments();
        }
        private void SelectedAppointmentEdit()
        {
            if (appointmentListView.SelectedItems.Count == 1)
            {
                Form form = new AppointmentForm(appointmentListView.SelectedAppointment());
                Language.LanguageFill(ref form);
                form.ShowDialog();
                RefreshAppointments();
            }
            else
            {
                MessageBox.Show(Language.LanguageFill("#selectappointment"));
            }
        }

        private void appointmentEditButton_Click(object sender, EventArgs e)
        {
            SelectedAppointmentEdit();
        }
        private void AppointmentListView_DoubleClick(object sender, EventArgs e)
        {
            SelectedAppointmentEdit();
        }

        private void logonAuditFileLabel_Click(object sender, EventArgs e)
        {
            Logging.LaunchLog();
        }
    }
}
