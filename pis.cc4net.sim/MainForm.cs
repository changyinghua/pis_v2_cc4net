using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pis.cc4net.sim
{
    public partial class MainForm : Form, ICCSubsystem
    {
        private CCConsole cc;

        private int a;

        private bool keyAltF4;

        private bool allowMove;

        public MainForm()
        {
            InitializeComponent();
            this.a = 0;
            this.cc = new CCConsole();
        }


        public void Login(string user, bool isEnglish, string staffGrade)
        {
            this.hcField.Invoke(new EventHandler(delegate
            {
                this.userField.Text = user;
                this.languageTextBox.Text = isEnglish ? "English" : "中文";
                this.staffGradeTextBox.Text = staffGrade;
            })); 
        }

        public void Logout()
        {
            System.Environment.Exit(0);
        }

        public void HealthCheck(int counter)
        {
           this.hcField.Invoke(new EventHandler(delegate
           {
               this.hcField.Text = "" + counter;
           }));  
        }

        private void OnLoad(object sender, EventArgs e)
        {
            this.cc.Config(this);
            this.cc.Connect();
            this.cc.Startup();
        }

        public void MoveToTop()
        {
            TopMost = true;
            TopMost = false;
        }

        public void ChangeUserId()
        {
            
        }

        public void AreaChange()
        {
           
        }

        public void ConfigPrivilege(string accessPrivilege)
        {
            this.acTextBox.Text = accessPrivilege;
        }

        public void Error(string errorMsgType, string errorMessage)
        {
            this.appErrTextBox.Text = errorMessage;
        }

        public void ChangePosition(int xPos, int yPos)
        {
            Location = new Point(xPos, yPos);    
        }

        public void ChangeDimension(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Input(string attributeType, bool attributeFlag)
        {
            if (attributeFlag)
            {
                switch (attributeType)
                {
                    case "00":
                        break;
                    case "01":

                        break;
                    case "02":
                        break;
                    case "03":
                        MinimizeBox = true;
                        break;
                    case "04":
                        MaximizeBox = true;
                        break;
                    case "05":
                        this.allowMove = false;
                        break;
                    case "06":
                        ControlBox = true;
                        break;
                }
            }
            else
            {
                this.keyAltF4 = false;
                ControlBox = false;
                this.allowMove = false;
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        if(!this.allowMove)
                        {
                            return;
                        }                     
                    }
                        
                    break;
            }
            base.WndProc(ref m);
        }

        private void OnIncomingButtonClick(object sender, EventArgs e)
        {
            CCResultCode i = this.cc.NewIncomingCall(
                DateTime.Now.ToString("MM/dd hh:mm:ss"),
                idTextBox.Text,
                aliasTextBox.Text,
                newIncomingCallCheckBox.Checked, 
                newIncomingHgihPcheckBox.Checked);
            richTextBox.Invoke(new EventHandler(delegate
            {
                richTextBox.AppendText("Incoming Call : "+i.ToString() +"\n");
            }));  
        }

        private void OnRmIncomingButtonClick(object sender, EventArgs e)
        {
            CCResultCode i = this.cc.RemoveIncomingCall(
                DateTime.Now.ToString("MM/dd hh:mm:ss"),
                rmCallIdTextBox.Text,
                rmCallAliasTextBox.Text,
                rmTypeCheckBox.Checked,
                rmHighCheckBox.Checked);
            richTextBox.Invoke(new EventHandler(delegate
            {
                richTextBox.AppendText("Remove Incoming Call : " + i.ToString() + "\n");
            }));  
        }

        private void OnNewOperationalMessageButtonClick(object sender, EventArgs e)
        {
            
            if(this.a>999999999)
            {
                a = 0;
            }

            string opId="3-"+a.ToString("0000000000");
            CCResultCode i = this.cc.NewOperationalMessage(
                DateTime.Now.ToString("MM/dd hh:mm:ss"),
                opId,
                descriptionTextBox.Text,
                opTypeCheckBox.Checked);
            richTextBox.Invoke(new EventHandler(delegate
            {
                richTextBox.AppendText("New Operatonal Message : " + i.ToString() + "\n");
                richTextBox.AppendText("Operatonal Message ID: " + a.ToString() + "\n");
            }));
            a++;
        }

        private void OnRmOpMsgButtonClick(object sender, EventArgs e)
        {
           /* int b =Convert.ToInt32(rmOpIdTextBox.Text);
            string opId = "3-" + b.ToString("0000000000");
            int i = this.cc.RemoveOperationalMessage(
                DateTime.Now.ToString("MM/dd hh:mm:ss"),
                opId,
                rmSubUnitTextBox.Text);
            richTextBox.Invoke(new EventHandler(delegate
            {
                richTextBox.AppendText("Remove Operatonal Message : " + i.ToString() + "\n");
            }));*/
        }

        private void OnClearButtonClick(object sender, EventArgs e)
        {
            richTextBox.Clear();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!this.keyAltF4)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }



        public void NewEmptyIncomingCallList()
        {          
            richTextBox.Invoke(new EventHandler(delegate
            {
                richTextBox.AppendText("NewEmptyIncomingCallList : " + this.cc.NewEmptyIncomingCallList().ToString() + "\n");
            }));
        }

        public void NewEmptyOperationalList()
        {        
            richTextBox.Invoke(new EventHandler(delegate
            {
                richTextBox.AppendText("NewEmptyOperationalList : " + this.cc.NewEmptyOperaionalList().ToString() + "\n");
            }));
        }
    }
}
/*
 richTextBox.Invoke(new EventHandler(delegate
                {
                    richTextBox.AppendText("Client> " + response + "                    " + DateTime.Now + "\n");
                }));  
*/