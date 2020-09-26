﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Netch.Utils;

namespace Netch.ServerEx.Shadowsocks.Form
{
    public partial class ShadowsocksForm : System.Windows.Forms.Form
    {
        private readonly Shadowsocks _server;

        public ShadowsocksForm(Shadowsocks server = default)
        {
            InitializeComponent();

            _server = server ?? new Shadowsocks();
        }

        private void Shadowsocks_Load(object sender, EventArgs e)
        {
            #region InitText

            ConfigurationGroupBox.Text = i18N.Translate(ConfigurationGroupBox.Text);
            RemarkLabel.Text = i18N.Translate(RemarkLabel.Text);
            AddressLabel.Text = i18N.Translate(AddressLabel.Text);
            PasswordLabel.Text = i18N.Translate(PasswordLabel.Text);
            EncryptMethodLabel.Text = i18N.Translate(EncryptMethodLabel.Text);
            PluginLabel.Text = i18N.Translate(PluginLabel.Text);
            PluginOptionsLabel.Text = i18N.Translate(PluginOptionsLabel.Text);
            ControlButton.Text = i18N.Translate(ControlButton.Text);

            EncryptMethodComboBox.Items.AddRange(SSGlobal.EncryptMethods.ToArray());

            #endregion

            RemarkTextBox.Text = _server.Remark;
            AddressTextBox.Text = _server.Hostname;
            PortTextBox.Text = _server.Port.ToString();
            PasswordTextBox.Text = _server.Password;
            EncryptMethodComboBox.SelectedIndex = SSGlobal.EncryptMethods.IndexOf(_server.EncryptMethod);
            PluginTextBox.Text = _server.Plugin;
            PluginOptionsTextBox.Text = _server.PluginOption;
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

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) brush = SystemBrushes.HighlightText as SolidBrush;

                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(PortTextBox.Text, out var port)) return;

            _server.Remark = RemarkTextBox.Text;
            _server.Type = "SS";
            _server.Hostname = AddressTextBox.Text;
            _server.Port = port;
            _server.Password = PasswordTextBox.Text;
            _server.EncryptMethod = EncryptMethodComboBox.Text;
            _server.Plugin = PluginTextBox.Text;
            _server.PluginOption = PluginOptionsTextBox.Text;
            _server.Country = null;

            if (Global.Settings.Server.IndexOf(_server) == -1)
                Global.Settings.Server.Add(_server);

            MessageBoxX.Show(i18N.Translate("Saved"));
            Close();
        }
    }
}