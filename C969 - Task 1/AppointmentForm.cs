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
    partial class AppointmentForm : Form
    {
        Appointment _appointment;

        Customer _customer;
        User _user;
        int _duration;
        void IncreaseDuration()
        {
            IncreaseDuration(15);
        }
        void DecreaseDuration()
        {
            IncreaseDuration(-15);
        }
        void IncreaseDuration(int byMinutes)
        {
            if ((byMinutes + Duration) > 0 && (byMinutes + Duration) < (60*12)) //duration should be positive & less than 12 hours.
            {
                Duration += byMinutes;
            }
        }
        int Duration
        {
            get { return _duration; }
            set { _duration = value;
                this.durationDisplayTextBox.Text = _duration.ToString() + Language.LanguageFill(" #minutes");
                UpdateEndTimePicker();
            }
        }
        DateTime Start
        {
            get { return startDatePicker.Value.Date.AddHours(startTimePicker.Value.Hour).AddMinutes(startTimePicker.Value.Minute); }
            set { startDatePicker.Value = value.Date;
                startTimePicker.Value = value;
                ValidateForm(out string errorMessage);
            }
        }
        DateTime End
        {
            get { return Start.Date + endTimePicker.Value.TimeOfDay; }
        }
        void UpdateEndTimePicker()
        {
            this.endTimePicker.Value = this.startTimePicker.Value.AddMinutes(Duration);
            ValidateForm(out string errorMessage);
        }
        Customer Customer
        {
            get { return _customer; }
            set { _customer = value;
                this.customerSelectionButton.Text = _customer.Name + ", " + _customer.DisplayAddress();
                ValidateForm(out string errorMessage);
            }
        }
        User User
        {
            get { return _user; }
            set { _user = value;
                this.userSelectionButton.Text = _user.DisplayName();
            }
        }
        internal AppointmentForm(Appointment appointment)
        {
            ComboInitalize();
            this.Text = "#editappointment";
            _appointment = appointment;
            Duration = (int)appointment.Duration.TotalMinutes;
            Start = appointment.Start;
            User = appointment.User;
            Customer = appointment.Customer;
            titleTextBox.Text = appointment.Title;//255
            descriptionTextBox.Text = appointment.Description;
            locationTextBox.Text = appointment.Location;
            contactTextBox.Text = appointment.Contact;
            typeTextBox.Text = appointment.Type;
            urlTextBox.Text = appointment.Url;//255
        }
        internal AppointmentForm(DateTime suggestedStart)
        {
            ComboInitalize();
            this.Text = "#addappointment";
            Duration = 14;

            //startTimePicker.Value.AddSeconds(-1 * startTimePicker.Value.Second);
            if (startTimePicker.Value.Minute % 15 != 0)
                startTimePicker.Value = startTimePicker.Value.AddMinutes(15 - (startTimePicker.Value.Minute % 15));

            Start = NextAvailable(suggestedStart < Start ? Start : suggestedStart, Duration);
        }
        internal DateTime NextAvailable(DateTime time, int durationMinutes)
        {
            while (!CheckAppointmentTime(time, durationMinutes))
            {
                time = time.AddMinutes(15);
                //time = IsBusinessHours(time) ? time.AddMinutes(15) : time.Date.AddDays(1).AddHours(7);
            }
            return time;
        }
        internal bool IsBusinessHours(DateTime time)
        {
            if (time.Hour < 7 || time.Hour >= 19)
                return false;
            return true;
        }
        internal bool CheckAppointmentTime(DateTime start, int durationMinutes)
        {
            DateTime end = start.AddMinutes(durationMinutes);
            if (!IsBusinessHours(start))
            {
                return false;
            }
            if (!IsBusinessHours(end))
            {
                return false;
            }
            return DatabaseInterface.ValidateAppointmentTime(start, end, User.ID, _appointment == default(Appointment) ? null : (int?)_appointment.ID);
        }
        void ComboInitalize()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            OrganizeTabIndex(this.Controls);
            this.User = new User(Session.GetVariable("username"));
            this.userSelectionButton.FlatStyle = FlatStyle.Flat;
            this.increaseDurationButton.Click += IncreaseDurationButton_Click;
            this.decreaseDurationButton.Click += DecreaseDurationButton_Click;
            /*
            PopulateCountries();
            OrganizeTabIndex();
            this.nameTextBox.KeyUp += NameTextBox_KeyUp;
            this.address1TextBox.KeyUp += Address1TextBox_KeyUp;
            this.phoneTextBox.KeyUp += PhoneTextBox_KeyUp;
            this.postalCodeTextBox.KeyUp += PostalCodeTextBox_KeyUp;
            this.cityComboBox.KeyUp += CityComboBox_KeyUp;
            this.cityComboBox.SelectedIndexChanged += CityComboBox_SelectedIndexChanged;
            this.countryComboBox.KeyUp += CountryComboBox_KeyUp;
            this.countryComboBox.SelectedValueChanged += CountryComboBox_SelectedValueChanged;
            this.saveButton.Click += SaveButton_Click;
            ValidateForm(out string errorMessage);
            */
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).TextChanged += textBox_TextChanged;
                }
            }
            this.customerSelectionButton.Click += CustomerSelectionButton_Click;
            this.startTimePicker.ValueChanged += StartTimePicker_ValueChanged;
            this.startDatePicker.ValueChanged += StartDatePicker_ValueChanged;
            UpdateEndTimePicker();
        }

        private void StartDatePicker_ValueChanged(object sender, EventArgs e) => ValidateForm(out string errorMessage);

        private void textBox_TextChanged(object sender, EventArgs e) => ValidateForm(out string errorMessage);

        private void StartTimePicker_ValueChanged(object sender, EventArgs e) => UpdateEndTimePicker();

        private void DecreaseDurationButton_Click(object sender, EventArgs e) => DecreaseDuration();

        private void IncreaseDurationButton_Click(object sender, EventArgs e) => IncreaseDuration();

        private void CustomerSelectionButton_Click(object sender, EventArgs e)
        {
            CustomerListView customerList= new CustomerListView();
            this.Controls.Add(customerList);
            customerList.DoubleClick += CustomerListViewDoubleClick;
            customerList.Dock = DockStyle.Fill;
            //listview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            customerList.BringToFront();
            customerList.RefreshCustomers();
        }

        private void CustomerListViewDoubleClick(object sender, EventArgs e)
        {
            CustomerListView customerList = (CustomerListView)sender;
            this.Customer = customerList.SelectedCustomer();
            this.Controls.Remove((Control)sender);
            ((ListView)sender).Dispose();
            ValidateForm(out string errorMessage);
        }

        void OrganizeTabIndex(Control.ControlCollection controls)
        {
            List<Control> controlList = controls.Cast<Control>().ToList();
            //lambda expression is used here for ordering controls by x, then y.
            //LINQ require the use of lambda expressions, so this lambda expression makes the program more efficient by enabling the use of LINQ order functions.
            //admittedly, this isn't really a requirement of the program, but makes it eaiser for keyboard users by assigning tab indexes in logical left->right, top-> bottom order.
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
        bool ValidateTime(out string errorMessage)
        {
            try
            {
                if (!IsBusinessHours(startTimePicker.Value)
                    || !IsBusinessHours(endTimePicker.Value))
                {
                    errorMessage = "-" + Language.LanguageFill("#appointmentoutsidebusinesshours \n");
                    return false;
                }
                if (!DatabaseInterface.ValidateAppointmentTime(Start, End, User.ID, _appointment == default(Appointment) ? null : (int?)_appointment.ID))
                {
                    errorMessage = "-" + Language.LanguageFill("#appointmentconflict \n");
                    return false;
                }
                errorMessage = "";
                return true;
            }
            catch (Exception e)
            {
                errorMessage = "-" + Language.LanguageFill("#internalerror " + e.Message + "\n" + e.StackTrace + "\n\n" );
            }
            return false;
        }
        bool ValidateForm(out string errorMessage)
        {
            bool validates = true;
            if (ValidateTime(out errorMessage))
            {
                startLabel.BackColor = endLabel.BackColor = durationLabel.BackColor = DefaultBackColor;
                nextAvailableButton.Visible = false;
            }
            else
            {
                startLabel.BackColor = endLabel.BackColor = durationLabel.BackColor = Color.PaleVioletRed;
                nextAvailableButton.Visible = true;
                validates = false;
            }
            if (_customer == default(Customer))
            {
                errorMessage += "-" + Language.LanguageFill("#selectacustomer \n");
                customerSelectionButton.BackColor = Color.PaleVioletRed;
                validates = false;
            }
            else
            {
                customerSelectionButton.BackColor = Color.White;
            }
            if (_user == default(User))
            {
                errorMessage += "-" + Language.LanguageFill("#internalerror - #selectaconsultant \n");
                userSelectionButton.BackColor = Color.PaleVioletRed;
                validates = false;
            }
            else
            {
                userSelectionButton.BackColor = Color.White;
            }
            return validates;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm(out string errorMessage))
            {
                try
                {
                    if (_appointment == default(Appointment))
                    {
                        //new appointment
                        _appointment = new Appointment(Customer, User, titleTextBox.Text, descriptionTextBox.Text, locationTextBox.Text, contactTextBox.Text, typeTextBox.Text, urlTextBox.Text, Start, End);
                    }
                    else
                    {
                        _appointment.Update(Customer, User, titleTextBox.Text, descriptionTextBox.Text, locationTextBox.Text, contactTextBox.Text, typeTextBox.Text, urlTextBox.Text, Start, End);
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("-" + Language.LanguageFill("#internalerror " + ex.Message + "\n" + ex.StackTrace));
                }
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
        }

        private void nextAvailableButton_Click(object sender, EventArgs e)
        {
            Start = NextAvailable(Start, Duration);
        }
    }
}
