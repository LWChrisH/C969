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
    partial class LogonForm : Form
    {
        public LogonForm()
        {
            InitializeComponent();
        }

        private void logonButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session.Logon(usernameTextBox.Text, passwordTextBox.Text))
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Language.LanguageFill("#internalerror #cannotset #username"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException + "\n(" + Language.LanguageFill("#usesampledatabutton")+")");
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usernameTextBox.Text = "test";
            passwordTextBox.Text = "test";
            logonButton.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            usernameTextBox.Text = "chris";
            passwordTextBox.Text = "abc123";
            logonButton.PerformClick();
        }

        private void sampleDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                SampleData.GenerateSampleData();
                MessageBox.Show(Language.LanguageFill("#sampledatapopulated"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.LanguageFill("#internalerror " + ex.Message + "\n\n" + ex.StackTrace));
            }
        }
    }
}
