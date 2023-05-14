using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Task_1
{
    class Customer
    {
        int _customerId;
        string _name;
        Address _address;

        public int ID
        {
            get { return _customerId; }
        }
        public string Name
        {
            get { return _name; }
        }
        public Address Address
        {
            get { return _address; }
        }
        public string DisplayAddress()
        {
            return _address.Address1 + (_address.Address2.Length == 0 ? " " + _address.Address2 : "") + " " + _address.City.Name + ", " + _address.City.Country.Name;
        }
        public Customer(int customerId)
        {
            //fill customer from database
            if (DatabaseInterface.CustomerRecordGet(customerId, out _name, out int addressId))
            {
                _customerId = customerId;
                _address = new Address(addressId);
            }
            else
            {
                MessageBox.Show(Language.LanguageFill("#internalerror #cannotread #customer ID " + customerId.ToString()));
            }
        }
        public Customer(string name, Address address)
        {
            //create customer in database
            _customerId = DatabaseInterface.CustomerRecordAdd(name, address.ID);
            _name = name;
            _address = address;
        }
        public Customer(int customerId, string name, Address address)
        {
            //full record exists, build record without database
            _customerId = customerId;
            _name = name;
            _address = address;
        }
        public bool UpdateAddress(Address address)
        {
            return UpdateAddress(address.ID);
        }
        public bool UpdateAddress(int addressId)
        {
            return UpdateCustomer(_name, addressId); //use existing name
        }
        public bool UpdateName(string name)
        {
            return UpdateCustomer(name, _address.ID); //use existing address ID
        }
        public bool UpdateCustomer(string name, Address address)
        {
            return UpdateCustomer(name, address.ID);
        }
        public bool UpdateCustomer(string name, int addressId)
        {
            if (DatabaseInterface.CustomerRecordUpdate(_customerId, name, addressId))
                return true;
            else
                MessageBox.Show("xxxx");
            return false;
        }
        public ListViewItem ToListViewItem(ListView list)
        {
            ListViewItem item = new ListViewItem();
            bool firstPass = true;
            foreach (ColumnHeader header in list.Columns)
            {
                string columnValue = "";
                if (header.Name == "#name") //Language.LanguageFill("#name"))
                {
                    columnValue = _name;
                }
                else if (header.Name == "#address") //Language.LanguageFill("#address"))
                {
                    columnValue = this.DisplayAddress();// DisplayAddress();
                }
                else if (header.Name == "#phone") // Language.LanguageFill("#phone"))
                {
                    columnValue = _address.Phone;
                } else {
                    MessageBox.Show(Language.LanguageFill("#internalerror (Invalid header found: " + header.Text + ")"));
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
            item.Tag = _customerId;
            return item;
        }
    }
    class CustomerListView : ListView
    {
        public CustomerListView()
        {
            this.View = View.Details;
            foreach (string columnText in (new string[] { "#name", "#address", "#phone" }))
            {
                this.Columns.Add(columnText, Language.LanguageFill(columnText));
            }
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            this.FullRowSelect = true;
            this.Resize += new System.EventHandler(UpdateColumns);
            this.MultiSelect = false;
        }

        public bool DeleteSelectedCustomer(out string errorMessage)
        {
            if (SelectedItems.Count == 1)
            {
                int selectedCustomer = (int)this.SelectedItems[0].Tag;
                if (DatabaseInterface.CustomerRecordDelete(selectedCustomer))
                {
                    this.Items.Remove(SelectedItems[0]);
                    errorMessage = "";
                    return true;
                }
                else
                {
                    errorMessage = Language.LanguageFill("#cannotdelete #customer");
                    return false;
                }
            }
            else
            {
                errorMessage = Language.LanguageFill("#mustselectrecord");
                return false;
            }
        }
        public Customer SelectedCustomer()
        {
            return new Customer((int)this.SelectedItems[0].Tag);
        }
        void UpdateColumns(object sender, EventArgs e)
        {
            foreach (ColumnHeader header in this.Columns)
            {
                header.Width = (this.Width - 1) / this.Columns.Count - 1;
            }
            //this.Columns[0].Width += this.Width % this.Columns.Count;
        }
        public void RefreshCustomers()
        {
            this.Items.Clear();

            foreach (Customer customer in DatabaseInterface.CustomerList())
            {
                this.Items.Add(customer.ToListViewItem(this));
            }
        }

    }
}
