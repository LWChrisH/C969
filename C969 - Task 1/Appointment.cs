using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Task_1
{
    class Appointment
    {
        //title, description, location, contact, type, url, start, end
        int _appointmentId;
        Customer _customer;
        int _customerId;
        User _user;
        int _userId;
        string _title; //varchar(255)
        string _description; //text
        string _location; //text
        string _contact; //text
        string _type; //text
        string _url; //varchar(255)
        DateTime _start;
        DateTime _end;

        //for both user & customer, if the field has not yet been set then we need to create a new corresponding object from the corresponding ID.
        public Customer Customer
        {
            get
            {
                if (_customer == default(Customer))
                {
                    _customer = new Customer(_customerId);
                }
                return _customer;
            }
        }
        public User User
        {
            get
            {
                if (_user == default(User))
                {
                    _user = new User(_userId);
                }
                return _user;
            }
        }
        public Appointment(int appointmentId)
        {
            //read all data from database.
            DatabaseInterface.AppointmentRecordGet(appointmentId, out _customerId, out _userId, out _title, out _description, out _location, out _contact, out _type, out _url, out _start, out _end);
            _appointmentId = appointmentId;
        }
        public Appointment(Customer customer, User user, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end)
        {
            //insert into database. get new appointment id.
            _appointmentId = DatabaseInterface.AppointmentRecordSet(null, customer.ID, user.ID, title, description, location, contact, type, url, start, end);

            _customer = customer;
            _customerId = customer.ID;
            _user = user;
            _userId = user.ID;
            _title = title;
            _description = description;
            _location = location;
            _contact = contact;
            _type = type;
            _url = url;
            _start = start;
            _end = end;
        }
        public Appointment(int appointmentId, Customer customer, User user, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end)
        {
            //all data available. no need to read from database.
            _appointmentId = appointmentId;
            _customer = customer;
            _customerId = customer.ID;
            _user = user;
            _userId = user.ID;
            _title = title;
            _description = description;
            _location = location;
            _contact = contact;
            _type = type;
            _url = url;
            _start = start;
            _end = end;
        }
        public Appointment(int appointmentId, int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end)
        {
            //all data available. no need to read from database. leave _customer and _user blank, they can be pulled later when needed.
            _appointmentId = appointmentId;
            _customerId = customerId;
            _userId = userId;
            _title = title;
            _description = description;
            _location = location;
            _contact = contact;
            _type = type;
            _url = url;
            _start = start;
            _end = end;
        }
        public void Update(Customer customer, User user, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end)
        {
            //insert into database. get new appointment id.
            DatabaseInterface.AppointmentRecordSet(_appointmentId, customer.ID, user.ID, title, description, location, contact, type, url, start, end);

            _customer = customer;
            _customerId = customer.ID;
            _user = user;
            _userId = user.ID;
            _title = title;
            _description = description;
            _location = location;
            _contact = contact;
            _type = type;
            _url = url;
            _start = start;
            _end = end;
        }
        public int ID
        {
            get { return this._appointmentId; }
        }
        public string Title
        {
            get { return this._title; }//varchar(255)
        }
        public string Description
        {
            get { return this._description; }
        }
        public string Location
        {
            get { return this._location; }
        }
        public string Contact
        {
            get { return this._contact; }
        }
        public string Type
        {
            get { return this._type; }
        }
        public string Url
        {
            get { return this._url; } //varchar(255)
        }
        public DateTime Start
        {
            get { return this._start; }
        }
        public DateTime End
        {
            get { return this._start; }
        }
        public TimeSpan Duration
        {
            get { return this._end - this._start; }
        }

        public ListViewItem ToListViewItem(ListView list)
        {
            ListViewItem item = new ListViewItem();
            bool firstPass = true;
            foreach (ColumnHeader header in list.Columns)
            {
                string columnValue = "";
                if (header.Name == "#customer")
                {
                    columnValue = _customer.Name;
                }
                else if (header.Name == "#consultant")
                {
                    columnValue = _user.DisplayName();
                }
                else if (header.Name == "#title")
                {
                    columnValue = _title;
                }
                else if (header.Name == "#type")
                {
                    columnValue = _type;
                }
                else if (header.Name == "#time")
                {
                    columnValue = _start.ToString("MM/dd/yy hh:mm - ") + ((int)Duration.TotalMinutes).ToString() + "min";
                }
                else
                {
                    throw new Exception (Language.LanguageFill("#internalerror (Invalid header found: " + header.Text + ")"));
                }
                if (firstPass)
                {
                    firstPass = false;
                    item = new ListViewItem(columnValue);
                }
                else
                {
                    item.SubItems.Add(columnValue);
                }
            }
            item.Tag = _appointmentId;
            return item;
        }
    }

    class AppointmentListView : ListView
    {
        public AppointmentListView()
        {
            this.View = View.Details;
            foreach (string columnText in (new string[] { "#customer", "#consultant", "#title", "#type", "#time" }))
            {
                this.Columns.Add(columnText, Language.LanguageFill(columnText));
            }
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            this.FullRowSelect = true;
            this.Resize += new System.EventHandler(UpdateColumns);
            this.MultiSelect = false;
        }
        /*
        public bool DeleteSelectedAppointment(out string errorMessage)
        {
            if (SelectedItems.Count == 1)
            {
                int selectedAppointmentId = (int)this.SelectedItems[0].Tag;
                if (DatabaseInterface.AppointmentRecordDelete(selectedAppointmentId))
                {
                    this.Items.Remove(SelectedItems[0]);
                    errorMessage = "";
                    return true;
                }
                else
                {
                    errorMessage = Language.LanguageFill("#cannotdelete #appointment");
                    return false;
                }
            }
            else
            {
                errorMessage = Language.LanguageFill("#mustselectrecord");
                return false;
            }
        }*/
        public Appointment SelectedAppointment()
        {
            return new Appointment((int)this.SelectedItems[0].Tag);
        }
        void UpdateColumns(object sender, EventArgs e)
        {
            foreach (ColumnHeader header in this.Columns)
            {
                header.Width = (this.Width - 1) / this.Columns.Count - 1;
            }
        }
        public void RefreshAppointments(int? userId, DateTime startDate, DateTime endDate)
        {
            this.Items.Clear();

            foreach (Appointment appointment in DatabaseInterface.AppointmentList(userId, startDate, endDate).OrderBy(x => x.Start).ThenBy(x => x.User.DisplayName()))
            {
                this.Items.Add(appointment.ToListViewItem(this));
            }
        }
    }
}
