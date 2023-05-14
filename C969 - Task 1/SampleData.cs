using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969___Task_1
{
    static class SampleData
    {
        static DateTime _lastAppointment;
        static Random _rnd = new Random();
        public static void GenerateSampleData()
        {
            _lastAppointment = DateTime.Now.Date.AddDays(1).AddHours(7); //start creating appointments tomorrow.
            User[] users = new User[]
                { new User("test", "test"),
                  new User("chris", "abc123")
                };
            foreach (string s in new string[] { "Bob", "Jake", "John", "Clyde","Joe", "Ezekiel" })
            {
                Customer customer = AddClient(s);
                AddAppointment(customer, users[_rnd.Next(0, users.Count())]);
                AddAppointment(customer, users[_rnd.Next(0, users.Count())]);
                AddAppointment(customer, users[_rnd.Next(0, users.Count())]);
            }

        }
        static Customer AddClient(string data)
        {
            Country country = new Country("Country of " + data);
            City city = new City(data + " City", country);
            Address address = new Address("123 " + data + " st", "Apt 5A", city, "90001", "555-555-5555");
            Customer customer = new Customer("Customer " + data, address);
            return customer;
        }
        static void AddAppointment(Customer customer, User user)
        {
            int duration = _rnd.Next(1, 8);
            while (!DatabaseInterface.ValidateAppointmentTime(_lastAppointment, _lastAppointment.AddMinutes(15 * 8 - 1), user.ID, null))
            {
                _lastAppointment = _lastAppointment.AddMinutes(15 * duration);
                if (_lastAppointment.Hour >= 15)
                {
                    _lastAppointment = _lastAppointment.Date.AddDays(1).AddHours(7);
                }
            }
            Appointment appointment = new Appointment(customer, user, "Test Appointment", "this is a test appointment.", "Virtual", "user@account.email", "follow up", "http://abc." + customer.Name.Replace(" ","").ToLower() + ".account.web", _lastAppointment, _lastAppointment.AddMinutes(15*duration-1));
            _lastAppointment = _lastAppointment.AddMinutes(15 * duration);
            if (_lastAppointment.Hour >= 15)
            {
                _lastAppointment = _lastAppointment.Date.AddDays(1).AddHours(7);
            }
        }
    }
}
