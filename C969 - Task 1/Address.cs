using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Task_1
{
    class Address
    {
        int _addressId;
        string _address1;
        string _address2;
        City _city;
        string _postalCode;
        string _phone;
        public Address(int addressId)
        {
            if (addressId == 0)
            {
                MessageBox.Show("Invalid address ID passed to Address() function.");
            }
            //retrieve address from database
            if (DatabaseInterface.AddressGet(addressId, out _address1, out _address2, out int cityId, out _postalCode, out _phone))
            {
                _addressId = addressId;
                _city = new City(cityId);
            }
            else
            {
                MessageBox.Show("Unable to construct address object from primary key.");
            }

        }
        public Address(string address1, string address2, City city, string postalCode, string phone)
        {
            //create new address in database
            _addressId = DatabaseInterface.AddressAdd(address1, address2, city.ID, postalCode, phone);
            _address1 = address1;
            _address2 = address2;
            _city = city;
            _postalCode = postalCode;
            _phone = phone;
        }
        public Address(int addressId, string address1, string address2, City city, string postalCode, string phone)
        {
            //full address object available, no need to retrv from db.
            _addressId = addressId;
            _address1 = address1;
            _address2 = address2;
            _city = city;
            _postalCode = postalCode;
            _phone = phone;
        }
        public string DisplayAddress()
        {
            return _address1 + (_address2.Length > 0 ? " " : "") + _address2;
        }
        public string Address1
        {
            get { return _address1; }
        }
        public string Address2
        {
            get { return _address2; }
        }
        public City City
        {
            get { return _city; }
        }
        public string PostalCode
        {
            get { return _postalCode; }
        }
        public string Phone
        {
            get { return _phone; }
        }
        public int ID
        {
            get { return _addressId; }
        }
    }
    class City
    {
        int _cityId;
        string _cityName;
        Country _country;

        public int ID
        {
            get { return _cityId; }
        }
        public string Name
        {
            get { return _cityName; }
        }
        public Country Country
        {
            get { return _country; }
        }
        public City(int cityId, string cityName, Country country)
        {
            //full city object provided, no need to interact with the database.
            _cityId = cityId;
            _cityName = cityName;
            _country = country;
        }
        public City(int cityId)
        {
            if (DatabaseInterface.CityGet(cityId, out string cityName, out int countryId))
            {
                _country = new Country(countryId);
                _cityName = cityName;
                _cityId = cityId;
            }
            else
            {
                MessageBox.Show(Language.LanguageFill("#internalerror #city ID " + cityId.ToString() + " #notfound"));
            }
        }
        public City(string cityName, int countryId)
        {
            //add city to database
            _country = new Country(countryId);
            _cityName = cityName;
            _cityId = DatabaseInterface.CityAdd(this.Name, _country.ID);
        }
        public City(string cityName, Country country)
        {
            // retrieve city Id from database.
            _cityName = cityName;
            _country = country;
            _cityId = DatabaseInterface.CityAdd(this.Name, _country.ID);
        }
    }
    class Country
    {
        int _countryId;
        string _countryName;
        public int ID
        {
            get { return _countryId; }
        }
        public string Name
        {
            get { return _countryName; }
        }
        public Country(int countryId)
        {
            // retrieve country from database
            if (DatabaseInterface.CountryGet(countryId, out string countryName))
            {
                _countryName = countryName;
                _countryId = countryId;
            }
            else
            {
                MessageBox.Show(Language.LanguageFill("#internalerror #country ID " + countryId.ToString() + " #notfound"));
            }
}
        public Country(string countryName)
        {
            //create a new country in database
            _countryId = DatabaseInterface.CountryAdd(countryName);
            _countryName = countryName;
        }
        public Country(int countryId, string countryName)
        {
            //full object passed in, no need to interact w db.
            _countryId = countryId;
            _countryName = countryName;
        }
    }
}
