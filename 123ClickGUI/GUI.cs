using _123ClickGUI.Properties;
using SimWinInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Forms;

namespace _123ClickGUI
{
    public partial class GUI : Form
    {
        #region Variables
        private int countDownRemaining = -1;

        private Communication communication;
        private ClickLocations clickLocations;
        private KeyboardListener keyboardListener;

        private int? x, y;

        public List<string> backLog = new List<string>();
        #endregion

        #region Setup
        public GUI()
        {
            
            InitializeComponent();
            FileManager.setup();
            tbName.Text = FileManager.name;
            clickLocations = new ClickLocations();
            keyboardListener = new KeyboardListener();
            setupClickLocations();
            subscribeToEvents();
            updateUpdater();
        }

        private void updateUpdater()
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "tempUpdater.exe"))
                {
                    Process cmd = new Process();
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.StartInfo.Verb = "runas";
                    cmd.Start();
                    cmd.StandardInput.WriteLine("del 123Updater.exe");
                    cmd.StandardInput.WriteLine("rename tempUpdater.exe 123Updater.exe");
                    cmd.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void subscribeToEvents()
        {
            Communication.onConnected += Communication_onConnected;
            Communication.onDisconnect += Communication_onDisconnect;
            Communication.onCountDownStart += Communication_onCountDownStart;
            Communication.onUsersChanged += Communication_onUsersChanged;
            Communication.onCountDownCancel += Communication_onCountDownCancel;
            Communication.onUpdateRequest += Communication_onUpdateRequest;
            Communication.onRewind += Communication_onRewind;
            Communication.onForward += Communication_onForward;
            clickLocations.onRecordsChanged += ClickLocations_onRecordsChanged;
            keyboardListener.onCountDownToggle += KeyboardListener_onCountDownToggle;
            keyboardListener.onRewind += KeyboardListener_onRewind;
            keyboardListener.onForward += KeyboardListener_onForward;
        }

        private void KeyboardListener_onForward(object sender, EventArgs e)
        {
            if (communication.isConnected())
                communication.sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Rewind, FileManager.name));
        }

        public void setupClickLocations()
        {
            lvClickLocations.Items.Clear();
            foreach (var item in clickLocations.getAllClickLocations())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item;
                lvi.SubItems.Add(clickLocations.getLocationX(item).ToString());
                lvi.SubItems.Add(clickLocations.getLocationY(item).ToString());
                lvClickLocations.Items.Add(lvi);
            }
            if (lvClickLocations.Items.Count > 0)
            {
                ListViewItem lvi = lvClickLocations.FindItemWithText(clickLocations.getLastLocation());
                if (lvi != null)
                    lvi.Selected = true;
            }
        }
        #endregion

        #region Outside Events
        private void ClickLocations_onRecordsChanged()
        {
            setupClickLocations();
        }
        private void Communication_onUsersChanged()
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                string userName = FileManager.name;
                lvOnline.Items.Clear();
                foreach (var item in Communication.usersOnline)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = item;
                    if (item == userName)
                        lvi.ForeColor = Color.DarkBlue;
                    else
                        lvi.ForeColor = Color.Teal;
                    lvOnline.Items.Add(lvi);
                }
            }));
        }

        private void Communication_onCountDownStart()
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                logUpdate();
                countDownRemaining = 2;
                countDownTimer.Start();
                Console.Beep(600, 250);
                btnCountDown.Text = "❚❚";
            }));
        }
        private void Communication_onCountDownCancel()
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                logUpdate();
                countDownRemaining = -1;
                countDownTimer.Stop();
                Console.Beep(100, 750);
                btnCountDown.Text = "▶";
            }));
        }

        private void Communication_onDisconnect()
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                btnCountDown.Enabled = false;
                btnRewind.Enabled = false;
                btnForward.Enabled = false;
                lblConnected.Text = "Disconnected";
                lblConnected.ForeColor = Color.Red;
                logUpdate("Lost connection");
                lvOnline.Items.Clear();
            }));
        }

        private void Communication_onConnected()
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                btnCountDown.Enabled = true;
                btnRewind.Enabled = true;
                btnForward.Enabled = true;
                lblConnected.Text = "Connected";
                lblConnected.ForeColor = Color.Green;
                logUpdate("Connected");
            }));
        }

        private void Communication_onUpdateRequest()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "123Updater.exe"))
            {
                MessageBox.Show("A new version was found, close this popup to update");
                Process.Start("123Updater.exe");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Could not find 123Updater.exe");
            }
        }

        private void Communication_onRewind()
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                logUpdate();
                int beforeX = Cursor.Position.X;
                int beforeY = Cursor.Position.Y;
                SimMouse.Click(MouseButtons.Left, x.Value, y.Value);
                Console.Beep(700, 100);
                Console.Beep(700, 100);
                SimMouse.Click(MouseButtons.Left, x.Value, y.Value);
                SendKeys.Send("{LEFT}");
                Cursor.Position = new Point(beforeX, beforeY);
            }));
        }

        private void Communication_onForward()
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                logUpdate();
                int beforeX = Cursor.Position.X;
                int beforeY = Cursor.Position.Y;
                SimMouse.Click(MouseButtons.Left, x.Value, y.Value);
                Console.Beep(400, 100);
                Console.Beep(400, 100);
                SimMouse.Click(MouseButtons.Left, x.Value, y.Value);
                SendKeys.Send("{RIGHT}");
                Cursor.Position = new Point(beforeX, beforeY);
            }));
        }

        private void KeyboardListener_onRewind(object sender, EventArgs e)
        {
            if(communication.isConnected())
                communication.sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Rewind, FileManager.name));
        }

        private void KeyboardListener_onCountDownToggle(object sender, EventArgs e)
        {
            if(communication.isConnected())
            {
                if (countDownRemaining > -1)
                {
                    communication.sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.CancelCountDown, FileManager.name));
                }
                else
                {
                    communication.sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.StartCountDown, FileManager.name));
                }
            }
        }

        #endregion

        #region Events
        private void MouseHover_ToolTip(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            if (control.Tag != null)
                new ToolTip().SetToolTip(control, control.Tag.ToString());
        }
        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            communication.sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Disconnect, ""));
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (FileManager.name != tbName.Text && communication.changeName(tbName.Text))
                {
                    logUpdate("Changed name to: " + tbName.Text);
                }
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            ClickLocationEditor clickLocationEditor = new ClickLocationEditor(clickLocations);
            clickLocationEditor.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lvClickLocations.SelectedItems[0];
            ClickLocationEditor clickLocationEditor = new ClickLocationEditor(lvi.Text, int.Parse(lvi.SubItems[1].Text), int.Parse(lvi.SubItems[2].Text), clickLocations);
            clickLocationEditor.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvClickLocations.SelectedItems.Count > 0)
            {
                clickLocations.saveLastLocation(lvClickLocations.SelectedItems[0].Text);
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                ListViewItem lvi = lvClickLocations.SelectedItems[0];
                x = int.Parse(lvi.SubItems[1].Text);
                y = int.Parse(lvi.SubItems[2].Text);
            }
            else
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                x = null;
                y = null;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lvClickLocations.SelectedItems[0];
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove " + lvi.Text + "?", "Remove " + lvi.Text, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                clickLocations.removeRecord(lvi.Text);
                if (lvClickLocations.Items.Count > 0)
                    lvClickLocations.Items[0].Selected = true;
            }
        }

        private void btnCountDown_Click(object sender, EventArgs e)
        {
            if(countDownRemaining > -1)
            {
                communication.sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.CancelCountDown, FileManager.name));
            }
            else
            {
                communication.sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.StartCountDown, FileManager.name));
            }
            
        }

        private void GUI_Shown(object sender, EventArgs e)
        {
            communication = new Communication(this);
        }

        private void btnRewind_Click(object sender, EventArgs e)
        {
            communication.sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Rewind, FileManager.name));
        }
        #endregion

        #region Log
        public void logUpdate(string message)
        {
            rbLog.AppendText("[" + DateTime.Now.ToLongTimeString() + "] " + message + Environment.NewLine);
            rbLog.ScrollToCaret();
        }
        public void logUpdate()
        {
            while (backLog.Count > 0)
            {
                rbLog.AppendText("[" + DateTime.Now.ToLongTimeString() + "] " + backLog.First() + Environment.NewLine);
                backLog.RemoveAt(0);
                rbLog.ScrollToCaret();
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            communication.sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Forward, FileManager.name));
        }
        #endregion

        #region Countdown
        private void countDownTimer_Tick(object sender, EventArgs e)
        {
            if (countDownRemaining > 0)
                Console.Beep(600, 250);
            countDownRemaining--;
            if (countDownRemaining == -1)
            {
                int beforeX = Cursor.Position.X;
                int beforeY = Cursor.Position.Y;
                Console.Beep(300, 750);
                if (x != null && y != null)
                    SimMouse.Click(MouseButtons.Left, x.Value, y.Value);
                else
                    SimMouse.Click(MouseButtons.Left, Cursor.Position.X, Cursor.Position.Y);

                countDownTimer.Enabled = false;
                Cursor.Position = new Point(beforeX, beforeY);
                btnCountDown.Text = "▶";
            }
        }
        #endregion

    }
}
