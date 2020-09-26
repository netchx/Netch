﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Netch.Models;
using Netch.Utils;

namespace Netch.ServerEx.VMess.Form
{
    public partial class VMessForm : System.Windows.Forms.Form
    {
        private static VMess _server;

        public VMessForm(Server server = default)
        {
            InitializeComponent();

            _server = (VMess) server ?? new VMess();
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

        private void VMess_Load(object sender, EventArgs e)
        {
            #region InitText

            ConfigurationGroupBox.Text = i18N.Translate(ConfigurationGroupBox.Text);
            RemarkLabel.Text = i18N.Translate(RemarkLabel.Text);
            AddressLabel.Text = i18N.Translate(AddressLabel.Text);
            UserIDLabel.Text = i18N.Translate(UserIDLabel.Text);
            AlterIDLabel.Text = i18N.Translate(AlterIDLabel.Text);
            EncryptMethodLabel.Text = i18N.Translate(EncryptMethodLabel.Text);
            TransferProtocolLabel.Text = i18N.Translate(TransferProtocolLabel.Text);
            FakeTypeLabel.Text = i18N.Translate(FakeTypeLabel.Text);
            HostLabel.Text = i18N.Translate(HostLabel.Text);
            PathLabel.Text = i18N.Translate(PathLabel.Text);
            QUICSecurityLabel.Text = i18N.Translate(QUICSecurityLabel.Text);
            QUICSecretLabel.Text = i18N.Translate(QUICSecretLabel.Text);
            TLSSecureCheckBox.Text = i18N.Translate(TLSSecureCheckBox.Text);
            UseMuxCheckBox.Text = i18N.Translate(UseMuxCheckBox.Text);
            ControlButton.Text = i18N.Translate(ControlButton.Text);

            EncryptMethodComboBox.Items.AddRange(VMessGlobal.EncryptMethods.ToArray());
            TransferProtocolComboBox.Items.AddRange(VMessGlobal.TransferProtocols.ToArray());
            FakeTypeComboBox.Items.AddRange(VMessGlobal.FakeTypes.ToArray());
            QUICSecurityComboBox.Items.AddRange(VMessGlobal.QUIC.ToArray());

            #endregion

            RemarkTextBox.Text = _server.Remark;
            AddressTextBox.Text = _server.Hostname;
            PortTextBox.Text = _server.Port.ToString();
            UserIDTextBox.Text = _server.UserID;
            AlterIDTextBox.Text = _server.AlterID.ToString();
            EncryptMethodComboBox.SelectedIndex = VMessGlobal.EncryptMethods.IndexOf(_server.EncryptMethod);
            TransferProtocolComboBox.SelectedIndex = VMessGlobal.TransferProtocols.IndexOf(_server.TransferProtocol);
            FakeTypeComboBox.SelectedIndex = VMessGlobal.FakeTypes.IndexOf(_server.FakeType);
            HostTextBox.Text = _server.Host;
            PathTextBox.Text = _server.Path;
            QUICSecurityComboBox.SelectedIndex = VMessGlobal.QUIC.IndexOf(_server.QUICSecure);
            QUICSecretTextBox.Text = _server.QUICSecret;
            TLSSecureCheckBox.Checked = _server.TLSSecure;
            UseMuxCheckBox.Checked = _server.UseMux;
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(PortTextBox.Text, out var port)) return;

            if (!int.TryParse(AlterIDTextBox.Text, out var alterId)) return;

            _server.Remark = RemarkTextBox.Text;
            _server.Type = "VMess";
            _server.Hostname = AddressTextBox.Text;
            _server.Port = port;
            _server.UserID = UserIDTextBox.Text;
            _server.AlterID = alterId;
            _server.EncryptMethod = EncryptMethodComboBox.Text;
            _server.TransferProtocol = TransferProtocolComboBox.Text;
            _server.FakeType = FakeTypeComboBox.Text;
            _server.Host = HostTextBox.Text;
            _server.Path = PathTextBox.Text;
            _server.QUICSecure = QUICSecurityComboBox.Text;
            _server.QUICSecret = QUICSecretTextBox.Text;
            _server.TLSSecure = TLSSecureCheckBox.Checked;
            _server.UseMux = UseMuxCheckBox.Checked;
            _server.Country = null;

            if (Global.Settings.Server.IndexOf(_server) == -1) Global.Settings.Server.Add(_server);

            MessageBoxX.Show(i18N.Translate("Saved"));
            Close();
        }
    }
}