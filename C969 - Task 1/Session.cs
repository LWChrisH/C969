using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace C969___Task_1
{
    static class Session
    {
        static bool _authenticated = false;
        static Dictionary<string, string> _appVariables = new Dictionary<string, string>();

        public static string GetVariable(string variableName)
        {
            if (_appVariables.ContainsKey(variableName.Replace("$","")))
            {
                return _appVariables[variableName.Replace("$", "")];
            }
            else
            {
                return "";
            }
        }
        static bool SetVariable(string variableName, string variableValue)
        {
            if (_appVariables.ContainsKey(variableName))
            {
                _appVariables[variableName] = variableValue;
            }
            else
            {
                _appVariables.Add(variableName, variableValue);
            }
            if (_appVariables.ContainsKey(variableName) && _appVariables[variableName] == variableValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //insert user (username,password,active,createdate,createdby,lastupdateby) values ('chris','abc123',true,now(),'system','system')
        public static MySqlConnection GetDBConnection()
        {
            if (!_authenticated)
            {
                Session.SetVariable("username", "_unauthenticated (" + System.Environment.MachineName + ")");
            }
            string connectionString = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        public static void AddParameters(ref MySqlCommand command)
        {
            command.Parameters.AddWithValue("@username", GetVariable("username"));
        }
        public static bool Logoff()
        {
            try
            {
                _authenticated = false;
                SetVariable("username", "");
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool Logon(string username, string password)
        {
            User user = new User(username);
            if (!user.VerifyPassword(password))
            {
                throw new Exception(Language.LanguageFill("#invalidpassword"));
            }
            else
            {
                if (SetVariable("username", username))
                {
                    _authenticated = true;
                    Logging.LogEntry(Language.LanguageFill("#userloggedin :") + username);
                    return true;
                }
                else
                {
                    throw new Exception(Language.LanguageFill("#internalerror #cannotset #username"));
                }
            }
            /*
            MySqlConnection connection = GetDBConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select password from user where userName = @username";
            command.Parameters.AddWithValue("@username", username);
            MySqlDataReader reader = command.ExecuteReader();
            
            if(reader.Read() && reader["password"].ToString() == password)
            {
                _authenticated = true;
                if (SetVariable("username", username))
                {
                    errorMessage = "";
                    return true;
                }
                else
                {
                    errorMessage = Language.LanguageFill("#internalerror SetVariable(username)");
                    return false;
                }
            }
            else
            {
                errorMessage = Language.LanguageFill("#invalidpassword");
                return false;
            }
            reader.Close();
            command.Dispose();
            connection.Dispose();
            */
        }

        
        public static bool IsAuthenticated()
        {
            return _authenticated;
        }
    }
}
