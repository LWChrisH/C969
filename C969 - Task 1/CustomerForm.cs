using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace C969___Task_1
{
    partial class CustomerForm : Form
    {
        Customer _customer;
        public CustomerForm()
        {
            ComboInitalize();
            this.Text = "#add #customerinformation";
        }
        public CustomerForm(Customer customer)
        {
            ComboInitalize();
            this.Text = "#edit #customerinformation";
            this.nameTextBox.Text = customer.Name;
            this.address1TextBox.Text = customer.Address.Address1;
            this.address2TextBox.Text = customer.Address.Address2;
            this.phoneTextBox.Text = customer.Address.Phone;
            this.postalCodeTextBox.Text = customer.Address.PostalCode;
            this.countryComboBox.Text = customer.Address.City.Country.Name;
            this.cityComboBox.Text = customer.Address.City.Name;
            _customer = customer;
        }
        void ComboInitalize()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            PopulateCountries();
            OrganizeTabIndex();
            /*this.nameTextBox.KeyPress += NameTextBox_KeyPress;
            this.address1TextBox.KeyPress += Address1TextBox_KeyPress;
            this.phoneTextBox.KeyPress += PhoneTextBox_KeyPress;
            this.postalCodeTextBox.KeyPress += PostalCodeTextBox_KeyPress;
            this.countryComboBox.KeyPress += CountryComboBox_KeyPress;*/
            this.nameTextBox.KeyUp += NameTextBox_KeyUp;
            this.address1TextBox.KeyUp += Address1TextBox_KeyUp;
            this.phoneTextBox.KeyUp += PhoneTextBox_KeyUp;
            this.postalCodeTextBox.KeyUp += PostalCodeTextBox_KeyUp;
            //this.cityComboBox.KeyUp += CityComboBox_KeyUp;
            this.cityComboBox.TextChanged += CityComboBox_TextChanged;
            this.cityComboBox.SelectedIndexChanged += CityComboBox_SelectedIndexChanged;
            this.countryComboBox.KeyUp += CountryComboBox_KeyUp;
            this.countryComboBox.SelectedValueChanged += CountryComboBox_SelectedValueChanged;
            this.saveButton.Click += SaveButton_Click;
            ValidateForm(out string errorMessage);
        }


        void OrganizeTabIndex()
        {
            OrganizeTabIndex(this.Controls);
        }
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

        //lambda operators are used here for each event handler so that all can be directed to ValidateForm(). This saves us 2 lines x5 handlers = 12 lines of { or }
        private void CityComboBox_TextChanged(object sender, EventArgs e) => ValidateForm(out string errorMessage);
        private void CityComboBox_SelectedIndexChanged(object sender, EventArgs e) => ValidateForm(out string errorMessage);
        //private void CityComboBox_KeyUp(object sender, KeyEventArgs e) => ValidateForm(out string errorMessage);
        private void Address1TextBox_KeyUp(object sender, KeyEventArgs e) => ValidateForm(out string errorMessage);
        private void PostalCodeTextBox_KeyUp(object sender, KeyEventArgs e) => ValidateForm(out string errorMessage);
        private void PhoneTextBox_KeyUp(object sender, KeyEventArgs e) => ValidateForm(out string errorMessage);
        private void NameTextBox_KeyUp(object sender, KeyEventArgs e) => ValidateForm(out string errorMessage);
        //these have to validate the form and also populate the cities:
        private void CountryComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            ValidateForm(out string errorMessage);
            PopulateCities();
        }
        private void CountryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ValidateForm(out string errorMessage);
            PopulateCities();
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateForm(out string errorMessage))
            {
                MessageBox.Show(errorMessage);
            }
            else
            {
                if (!CreateCustomer())
                {
                    MessageBox.Show(Language.LanguageFill("#internalerror #creatingcustomer"));
                }
                else
                {
                    this.Close();
                }
            }
        }
        private bool CreateCustomer()
        {
            if (_customer == default(Customer))
            {
                //_customer = new Customer();
                try
                {
                    this._customer = new Customer(this.nameTextBox.Text, ConstructAddress());
                }
                catch (Exception e)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return _customer.UpdateCustomer(this.nameTextBox.Text, ConstructAddress());
            }
        }
        private Address ConstructAddress()
        {
            Country country = new Country(this.countryComboBox.Text);
            City city = new City(this.cityComboBox.Text, country);
            Address address = new Address(this.address1TextBox.Text, this.address2TextBox.Text, city, this.postalCodeTextBox.Text, this.phoneTextBox.Text);
            return address;
        }
        /*
        private void CountryComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateForm(out string errorMessage);
            PopulateCities();
        }
        private void PostalCodeTextBox_KeyPress(object sender, KeyPressEventArgs e) => ValidateForm(out string errorMessage);
        private void PhoneTextBox_KeyPress(object sender, KeyPressEventArgs e) => ValidateForm(out string errorMessage);
        private void Address1TextBox_KeyPress(object sender, KeyPressEventArgs e) => ValidateForm(out string errorMessage);
        private void NameTextBox_KeyPress(object sender, KeyPressEventArgs e) => ValidateForm(out string errorMessage);
        */
        bool ValidateForm(out string errorMessage)
        {
            errorMessage = "";
            if (this.nameTextBox.Text.Length < 3)
            {
                this.nameTextBox.BackColor = Color.PaleVioletRed;
                errorMessage += " #entercustomername \n";
            } else {
                this.nameTextBox.BackColor = Color.White;
            }

            if (Int64.TryParse(this.address1TextBox.Text.Split(' ')[0], out Int64 addrNum) //validate first #
                    && this.address1TextBox.Text.Contains(" ") //contains space
                    && this.address1TextBox.Text.Split(new char[] { ' ' },2)[1].Length > 2) //second word must be at least 3 characters
            {
                this.address1TextBox.BackColor = Color.White;
            } else {
                this.address1TextBox.BackColor = Color.PaleVioletRed;
                errorMessage += " #enteraddress \n";
            }

            if (phoneTextBox.Text.Length < 10)
            {
                this.phoneTextBox.BackColor = Color.PaleVioletRed;
                errorMessage += " #enterphone \n";
            } else if (!Regex.IsMatch(phoneTextBox.Text, @"^[0-9\-\(\)]+$"))
            {
                this.phoneTextBox.BackColor = Color.PaleVioletRed;
                errorMessage += " #invalidphonecharacters \n";
            } else {
                this.phoneTextBox.BackColor = Color.White;
            }

            if (countryComboBox.Text.Length > 1)
            {
                countryComboBox.BackColor = Color.White;
                cityComboBox.Enabled = true;
            } else {
                countryComboBox.BackColor = Color.PaleVioletRed;
                cityComboBox.Enabled = false;
                errorMessage += " #entercountry \n";
            }

            if (cityComboBox.Text.Length > 1)
            {
                cityComboBox.BackColor = Color.White;
            } else {
                cityComboBox.BackColor = Color.PaleVioletRed;
                errorMessage += " #entercity \n";
            }

            if (postalCodeTextBox.Text.Length < 2)
            {
                postalCodeTextBox.BackColor = Color.PaleVioletRed;
                errorMessage += " #enterpostalcode \n";
            } else {
                postalCodeTextBox.BackColor = Color.White;
            }
            if (errorMessage == "")
            {
                return true;
            } else {
                errorMessage = Language.LanguageFill(errorMessage);
                return false;
            }
        }
        void PopulateCountries()
        {
            List<string> countryList = DatabaseInterface.CountryList();
            countryList.Insert(0, "");

            this.countryComboBox.DataSource = countryList;
            countryComboBox.Text = "";
        }
        void PopulateCities()
        {
            string country = countryComboBox.Text;
            if (country.Length > 0)
            {
                List<string> cityList = DatabaseInterface.CityList(country);
                cityList.Insert(0, "");
                this.cityComboBox.DataSource = cityList;
            }
            else
            {
                cityComboBox.DataSource = null;
            }
        }
    }
}
