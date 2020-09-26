﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Netch.Utils;

namespace Netch.ServerEx.ShadowsocksR.Form
{
    public partial class ShadowsocksRForm : System.Windows.Forms.Form
    {
        private readonly ShadowsocksR _server;

        public ShadowsocksRForm(Models.Server server = default)
        {
            InitializeComponent();

            _server = (ShadowsocksR) (server ?? new ShadowsocksR());
        }

        private void ShadowsocksR_Load(object sender, EventArgs e)
        {
            #region InitText

            ConfigurationGroupBox.Text = i18N.Translate(ConfigurationGroupBox.Text);
            RemarkLabel.Text = i18N.Translate(RemarkLabel.Text);
            AddressLabel.Text = i18N.Translate(AddressLabel.Text);
            PasswordLabel.Text = i18N.Translate(PasswordLabel.Text);
            EncryptMethodLabel.Text = i18N.Translate(EncryptMethodLabel.Text);
            ProtocolLabel.Text = i18N.Translate(ProtocolLabel.Text);
            ProtocolParamLabel.Text = i18N.Translate(ProtocolParamLabel.Text);
            OBFSLabel.Text = i18N.Translate(OBFSLabel.Text);
            OBFSParamLabel.Text = i18N.Translate(OBFSParamLabel.Text);
            ControlButton.Text = i18N.Translate(ControlButton.Text);

            EncryptMethodComboBox.Items.AddRange(SSRGlobal.EncryptMethods.ToArray());

            ProtocolComboBox.Items.AddRange(SSRGlobal.Protocols.ToArray());
            OBFSComboBox.Items.AddRange(SSRGlobal.OBFSs.ToArray());

            #endregion

            RemarkTextBox.Text = _server.Remark;
            AddressTextBox.Text = _server.Hostname;
            PortTextBox.Text = _server.Port.ToString();
            PasswordTextBox.Text = _server.Password;
            EncryptMethodComboBox.SelectedIndex = SSRGlobal.EncryptMethods.IndexOf(_server.EncryptMethod);
            ProtocolComboBox.SelectedIndex = SSRGlobal.Protocols.IndexOf(_server.Protocol);
            ProtocolParamTextBox.Text = _server.ProtocolParam;
            OBFSComboBox.SelectedIndex = SSRGlobal.OBFSs.IndexOf(_server.OBFS);
            OBFSOptionParamTextBox.Text = _server.OBFSParam;
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (sender is ComboBox cbx)
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    var sf = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    };

                    var brush = new SolidBrush(cbx.ForeColor);

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        brush = SystemBrushes.HighlightText as SolidBrush;
                    }

                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(PortTextBox.Text, out var port))
            {
                return;
            }

            _server.Remark = RemarkTextBox.Text;
            _server.Type = "SSR";
            _server.Hostname = AddressTextBox.Text;
            _server.Port = port;
            _server.Password = PasswordTextBox.Text;
            _server.EncryptMethod = EncryptMethodComboBox.Text;
            _server.Protocol = ProtocolComboBox.Text;
            _server.ProtocolParam = ProtocolParamTextBox.Text;
            _server.OBFS = OBFSComboBox.Text;
            _server.OBFSParam = OBFSOptionParamTextBox.Text;
            _server.Country = null;

            if (Global.Settings.Server.IndexOf(_server) == -1)
            {
                Global.Settings.Server.Add(_server);
            }

            MessageBoxX.Show(i18N.Translate("Saved"));
            Close();
        }
    }
}