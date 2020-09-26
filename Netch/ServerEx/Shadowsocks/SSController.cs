using System.Runtime.InteropServices;
using System.Text;
using Netch.Models;
using Netch.Utils;

namespace Netch.ServerEx.Shadowsocks
{
    public class SSController : ServerController
    {
        public SSController()
        {
            Name = "Shadowsocks";
        }

        public override bool Start(Server s, Mode mode)
        {
            bool DllFlag()
            {
                return Global.Settings.BootShadowsocksFromDLL && (mode.Type == 0 || mode.Type == 1 || mode.Type == 2);
            }

            var server = (Shadowsocks) s;
            //从DLL启动Shaowsocks
            if (DllFlag())
            {
                State = State.Starting;
                var client = Encoding.UTF8.GetBytes($"{LocalAddress}:{Socks5LocalPort}");
                var remote = Encoding.UTF8.GetBytes($"{server.Hostname}:{server.Port}");
                var passwd = Encoding.UTF8.GetBytes($"{server.Password}");
                var method = Encoding.UTF8.GetBytes($"{server.EncryptMethod}");
                if (!ShadowsocksDLL.Info(client, remote, passwd, method))
                {
                    State = State.Stopped;
                    Logging.Error("DLL SS INFO 设置失败！");
                    return false;
                }

                Logging.Info("DLL SS INFO 设置成功！");

                if (!ShadowsocksDLL.Start())
                {
                    State = State.Stopped;
                    Logging.Error("DLL SS 启动失败！");
                    return false;
                }

                Logging.Info("DLL SS 启动成功！");
                State = State.Started;
                return true;
            }

            #region Argument

            var argument = new StringBuilder();
            argument.Append(
                $"-s {server.Hostname} " +
                $"-p {server.Port} " +
                $"-b {LocalAddress} " +
                $"-l {Socks5LocalPort} " +
                $"-m {server.EncryptMethod} " +
                $"-k \"{server.Password}\" " +
                "-u ");
            if (!string.IsNullOrWhiteSpace(server.Plugin) && !string.IsNullOrWhiteSpace(server.PluginOption))
                argument.Append($"--plugin {server.Plugin} " +
                                $"--plugin-opts \"{server.PluginOption}\"");
            if (mode.BypassChina)
                argument.Append(" --acl default.acl");

            #endregion

            return StartInstanceAuto(argument.ToString());
        }

        public override void Stop()
        {
            if (Instance == null)
                ShadowsocksDLL.Stop();
            else
                StopInstance();
        }


        private class ShadowsocksDLL
        {
            [DllImport("shadowsocks-windows-dynamic", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool Info(byte[] client, byte[] remote, byte[] passwd, byte[] method);

            [DllImport("shadowsocks-windows-dynamic", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool Start();

            [DllImport("shadowsocks-windows-dynamic", CallingConvention = CallingConvention.Cdecl)]
            public static extern void Stop();
        }
    }
}