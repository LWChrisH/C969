using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace C969___Task_1
{
    static class Language
    {
        static Dictionary<string, Dictionary<string, string>> languageDictionary = new Dictionary<string, Dictionary<string, string>>();

        static Language()
        {
            Dictionary<string, string> appStrings = new Dictionary<string, string>();
            appStrings.Add("#languagetag", "Language: English");
            appStrings.Add("#please", "please");
            appStrings.Add("#logon", "log on");
            appStrings.Add("#username", "username");
            appStrings.Add("#consultant", "consultant");
            appStrings.Add("#password", "password");
            appStrings.Add("#invalidpassword", "password is invalid");
            appStrings.Add("#usernotfound", "username was not found");
            appStrings.Add("#usertag", "logged on user:");
            appStrings.Add("#calendartab", "calendar");
            appStrings.Add("#appointmentlisttab", "appointment list");
            appStrings.Add("#customertab", "customer Records");
            appStrings.Add("#mainappname", "main screen");
            appStrings.Add("#addappointment", "new appointment");
            appStrings.Add("#removeappointment", "remove appointment");
            appStrings.Add("#add", "add");
            appStrings.Add("#edit", "edit");
            appStrings.Add("#remove", "remove");
            appStrings.Add("#customer", "customer");
            appStrings.Add("#refresh", "refresh");
            appStrings.Add("#sampledata", "insert sample data");
            appStrings.Add("#day", "day");
            appStrings.Add("#week", "week");
            appStrings.Add("#month", "month");
            appStrings.Add("#name", "name");
            appStrings.Add("#address", "address");
            appStrings.Add("#address2", "address (continued)");
            appStrings.Add("#postalcode", "postal code");
            appStrings.Add("#phone", "phone");
            appStrings.Add("#save", "save");
            appStrings.Add("#cancel", "cancel");
            appStrings.Add("#city", "city");
            appStrings.Add("#country", "country");
            appStrings.Add("#notfound", "not found");
            appStrings.Add("#customerinformation", "customer information");
            appStrings.Add("#cannotdelete", "unable to delete from database:");
            appStrings.Add("#internalerror", "an internal error occurred:");
            appStrings.Add("#cannotread", "unable to read from database:");
            appStrings.Add("#cannotset", "unable to set");
            appStrings.Add("#entercustomername", "please enter the client name");
            appStrings.Add("#enteraddress", "please enter a valid address");
            appStrings.Add("#enterphone", "please enter a valid phone");
            appStrings.Add("#invalidphonecharacters", "phone number contains invalid characters");
            appStrings.Add("#enterpostalcode", "please enter a postal code");
            appStrings.Add("#entercity", "please select or enter a valid city");
            appStrings.Add("#entercountry", "please select or enter a valid country");
            appStrings.Add("#confirmdelete", "confirm deletion of this");
            appStrings.Add("#confirm", "please confirm");
            appStrings.Add("#selectcustomer", "select a customer");
            appStrings.Add("#creatingcustomer", "creating customer object");
            appStrings.Add("#logoff", "Log Off");
            appStrings.Add("#showby", "show by");
            appStrings.Add("#showfor", "show for");
            appStrings.Add("#me", "me");
            appStrings.Add("#everyone", "everyone");
            appStrings.Add("#title", "title");
            appStrings.Add("#type", "type");
            appStrings.Add("#start", "start");
            appStrings.Add("#end", "end");
            appStrings.Add("#selectacustomer", "select a customer");
            appStrings.Add("#appointment", "appointment");
            appStrings.Add("#selectaconsultant", "select a consultant");
            appStrings.Add("#description", "description");
            appStrings.Add("#location", "location");
            appStrings.Add("#appointmentconflict", "this appointment time conflicts with an existing appointment");
            appStrings.Add("#duration", "duration");
            appStrings.Add("#minutes", "minutes");
            appStrings.Add("#titletoolong", "title too long, must be 255 characters or less.");
            appStrings.Add("#appointmentoutsidebusinesshours", "you must schedule this appointment during business hours, between 7am and 7pm.");
            appStrings.Add("#contact", "contact");
            appStrings.Add("#selectappointment", "select an appointment");
            appStrings.Add("#time", "time");
            appStrings.Add("#reportname", "report name");
            appStrings.Add("#reportstab", "reports");
            appStrings.Add("#numberofappointmenttypesbymonth", "number of appointment types by month");
            appStrings.Add("#appointmenttype", "appointment Type");
            appStrings.Add("#numberofappointments", "number of appointments");
            appStrings.Add("#errorrunningreport", "error running report:");
            appStrings.Add("#toomanycolumns", "too many columns were included on input query");
            appStrings.Add("#nextavailable", "next available");
            appStrings.Add("#scheduleforeachconsultant", "the schedule for each consultant");
            appStrings.Add("#myscheduledhourspermonth", "my scheduled hours per month");
            appStrings.Add("#hours", "hours");
            appStrings.Add("#logfileerror", "An error was encountered writing to the log file. The application will shut down.");
            appStrings.Add("#usesampledatabutton", "hint: use the sample data button to create test and chris user accounts.");
            appStrings.Add("#sampledatapopulated", "sample data populated successfully.");
            appStrings.Add("#userloggedin", "user logged in");
            appStrings.Add("#launchauditfile", "launch audit file");
            appStrings.Add("#upcomingappointmentalert", "you have an upcoming appointment:");
            appStrings.Add("#upcomingappointment", "upcoming appointment");
            languageDictionary.Add("en", appStrings);

            appStrings = new Dictionary<string, string>();
            appStrings.Add("#languagetag", "Idioma: Español");
            appStrings.Add("#please", "por favor");
            appStrings.Add("#logon", "iniciar sesión");
            appStrings.Add("#username", "nombre de usuario");
            appStrings.Add("#consultant", "consultor");
            appStrings.Add("#password", "contraseña");
            appStrings.Add("#invalidpassword", "la contraseña es inválida");
            appStrings.Add("#usernotfound", "nombre de usuario no encontrado");
            appStrings.Add("#usertag", "usuario conectado:");
            appStrings.Add("#calendartab", "calendario");
            appStrings.Add("#appointmentlisttab", "lista de citas");
            appStrings.Add("#customertab", "registros de clientes");
            appStrings.Add("#mainappname", "pantalla principal");
            appStrings.Add("#addappointment", "nueva cita");
            appStrings.Add("#removeappointment", "quitar cita");
            appStrings.Add("#add", "añadir");
            appStrings.Add("#edit", "editar");
            appStrings.Add("#remove", "quitar");
            appStrings.Add("#customer", "cliente");
            appStrings.Add("#refresh", "actualizar");
            appStrings.Add("#sampledata", "insertar datos de muestra");
            appStrings.Add("#day", "día");
            appStrings.Add("#week", "semana");
            appStrings.Add("#month", "mes");
            appStrings.Add("#name", "nombre");
            appStrings.Add("#address", "dirección");
            appStrings.Add("#address2", "dirección (continuación)");
            appStrings.Add("#postalcode", "código postal");
            appStrings.Add("#phone", "teléfono");
            appStrings.Add("#save", "ahorrar");
            appStrings.Add("#cancel", "cancelar");
            appStrings.Add("#city", "ciudad");
            appStrings.Add("#country", "país");
            appStrings.Add("#notfound", "no encontrado");
            appStrings.Add("#customerinformation", "información al cliente");
            appStrings.Add("#cannotdelete", "no se puede eliminar de la base de datos:");
            appStrings.Add("#internalerror", "ha ocurrido un error interno");
            appStrings.Add("#cannotread", "no se puede leer de la base de datos:");
            appStrings.Add("#cannotset", "no se puede establecer");
            appStrings.Add("#entercustomername", "por favor ingrese el nombre del cliente");
            appStrings.Add("#enteraddress", "por favor ingrese una dirección válida");
            appStrings.Add("#enterphone", "por favor ingrese un teléfono válido");
            appStrings.Add("#invalidphonecharacters", "el número de teléfono contiene caracteres inválidos.");
            appStrings.Add("#enterpostalcode", "por favor ingrese un código postal");
            appStrings.Add("#entercity", "por favor seleccione o ingrese una ciudad válida");
            appStrings.Add("#entercountry", "por favor seleccione o ingrese un país válido");
            appStrings.Add("#confirmdelete", "confirmar eliminación de esto");
            appStrings.Add("#confirm", "por favor confirme");
            appStrings.Add("#selectcustomer", "seleccionar un cliente");
            appStrings.Add("#creatingcustomer", "creando objeto de cliente");
            appStrings.Add("#logoff", "cerrar sesión");
            appStrings.Add("#showby", "mostrar por");
            appStrings.Add("#showfor", "mostrar para");
            appStrings.Add("#me", "yo");
            appStrings.Add("#everyone", "todos");
            appStrings.Add("#title", "título");
            appStrings.Add("#type", "tipo");
            appStrings.Add("#start", "inicio");
            appStrings.Add("#end", "fin");
            appStrings.Add("#selectacustomer", "seleccionar un cliente");
            appStrings.Add("#appointment", "cita");
            appStrings.Add("#selectaconsultant", "seleccionar un consultor");
            appStrings.Add("#description", "descripción");
            appStrings.Add("#location", "ubicación");
            appStrings.Add("#appointmentconflict", "este horario de cita entra en conflicto con una cita existente");
            appStrings.Add("#duration", "duración");
            appStrings.Add("#minutes", "minutos");
            appStrings.Add("#titletoolong", "título demasiado largo, debe tener 255 caracteres o menos.");
            appStrings.Add("#appointmentoutsidebusinesshours", "debes programar esta cita durante el horario comercial, entre las 7 a.m. y las 7 p.m.");
            appStrings.Add("#contact", "contacto");
            appStrings.Add("#selectappointment", "seleccionar una cita");
            appStrings.Add("#time", "hora");
            appStrings.Add("#reportname", "nombre del informe");
            appStrings.Add("#reportstab", "informes");
            appStrings.Add("#numberofappointmenttypesbymonth", "cantidad de tipos de citas por mes");
            appStrings.Add("#appointmenttype", "tipo de Cita");
            appStrings.Add("#numberofappointments", "cantidad de citas");
            appStrings.Add("#errorrunningreport", "error al ejecutar el informe:");
            appStrings.Add("#toomanycolumns", "se incluyeron demasiadas columnas en la consulta de entrada");
            appStrings.Add("#nextavailable", "próximo disponible");
            appStrings.Add("#scheduleforeachconsultant", "el horario para cada consultor");
            appStrings.Add("#myscheduledhourspermonth", "mis horas programadas por mes");
            appStrings.Add("#hours", "horas");
            appStrings.Add("#logfileerror", "Se encontró un error al escribir en el archivo de registro. La aplicación se cerrará.");
            appStrings.Add("#usesampledatabutton", "pista: utiliza el botón de datos de muestra para crear usarios test y chris.");
            appStrings.Add("#sampledatapopulated", "datos de muestra insertados correctamente.");
            appStrings.Add("#userloggedin", "usuario conectado");
            appStrings.Add("#launchauditfile", "abrir archivo de auditoría");
            appStrings.Add("#upcomingappointmentalert", "tiene una cita próxima:");
            appStrings.Add("#upcomingappointment", "próxima cita");
            languageDictionary.Add("es", appStrings);

            CultureInfo ci = CultureInfo.CurrentUICulture;
            //MessageBox.Show(ci.TwoLetterISOLanguageName.ToLower());

            if (!(languageDictionary.ContainsKey(ci.TwoLetterISOLanguageName)))
            {
                string supportedLanguageString = "";

                foreach (string s in languageDictionary.Keys)
                {
                    supportedLanguageString += "\n" + (new CultureInfo(s).DisplayName);
                }

                MessageBox.Show(ci.DisplayName + " is not supported by this program. Dialogs will be shown in English.\n\nSupported Languages:" + supportedLanguageString);
            }
        }
        public static string LanguageFill(string s)
        {
            LanguageFill(ref s);
            return s;
        }
        public static void LanguageFill(ref string s)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            string languageName = ci.TwoLetterISOLanguageName;
            if (!(languageDictionary.ContainsKey(languageName)))
                languageName = "en";

            ReplaceStrings(ref s, languageName);
        }
        public static void LanguageFill(Control.ControlCollection controls)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            string languageName = ci.Name.Substring(0, 2);
            if (!(languageDictionary.ContainsKey(languageName)))
                languageName = "en";
            foreach (Control c in controls)
            {
                ProcessControl(c, languageName);
            }
        }
        public static void LanguageFill(ref Form f)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            string languageName = ci.Name.Substring(0, 2);
            if (!(languageDictionary.ContainsKey(languageName)))
                languageName = "en";
            string appString = f.Text;
            if (ReplaceStrings(ref appString, languageName))
            {
                f.Text = ci.TextInfo.ToTitleCase(appString);
            }
            foreach (Control c in f.Controls)
            {
                ProcessControl(c, languageName);
            }
        }
        static void ProcessControl(Control c, string languageName)
        {
            string appString = "";
            appString = c.Text;
            if (ReplaceStrings(ref appString, languageName))
            {
                    c.Text = appString;
            }
            if (c.HasChildren)
                foreach (Control child in c.Controls)
                    ProcessControl(child, languageName);
        }
        static bool ReplaceStrings(ref string appString,string languageName)
        {
            if (appString.Contains("#") || appString.Contains("$"))
            {
                string[] textSplit = appString.Split(' ');
                appString = "";
                foreach (string s in textSplit)
                {
                    if (s.StartsWith("$") && Session.GetVariable(s) != "")
                    {
                        appString += Session.GetVariable(s);
                    }
                    else if (s.StartsWith("#") && languageDictionary[languageName].ContainsKey(s.ToLower()))
                    {
                        appString += languageDictionary[languageName][s.ToLower()];
                    }
                    else
                    {
                        appString += s;
                    }
                    appString += " ";
                }
                appString = appString[0].ToString().ToUpper() + appString.Substring(1, appString.Length - 1);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
