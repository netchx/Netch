﻿using System.Collections.Generic;

namespace Netch
{
    public static class Global
    {
        /// <summary>
        ///     主窗体
        /// </summary>
        public static Forms.MainForm MainForm;

        /// <summary>
        ///     杂项配置
        /// </summary>
        public static Dictionary<string, int> Settings = new Dictionary<string, int>();

        /// <summary>
        ///     服务器列表
        /// </summary>
        public static List<Objects.Server> Server = new List<Objects.Server>();

        /// <summary>
        ///     订阅链接列表
        /// </summary>
        public static List<Objects.SubscribeLink> SubscribeLink = new List<Objects.SubscribeLink>();

        /// <summary>
		///		SS/SSR 加密方式
		/// </summary>
		public static class EncryptMethods
        {
            /// <summary>
            ///		SS 加密列表
            /// </summary>
            public static List<string> SS = new List<string>()
            {
                "rc4-md5",
                "aes-128-gcm",
                "aes-192-gcm",
                "aes-256-gcm",
                "aes-128-cfb",
                "aes-192-cfb",
                "aes-256-cfb",
                "aes-128-ctr",
                "aes-192-ctr",
                "aes-256-ctr",
                "camellia-128-cfb",
                "camellia-192-cfb",
                "camellia-256-cfb",
                "bf-cfb",
                "chacha20-ietf-poly1305",
                "xchacha20-ietf-poly1305",
                "salsa20",
                "chacha20",
                "chacha20-ietf"
            };

            /// <summary>
            ///		SSR 加密列表
            /// </summary>
            public static List<string> SSR = new List<string>()
            {
                "none",
                "table",
                "rc4",
                "rc4-md5",
                "aes-128-cfb",
                "aes-192-cfb",
                "aes-256-cfb",
                "aes-128-ctr",
                "aes-192-ctr",
                "aes-256-ctr",
                "bf-cfb",
                "camellia-128-cfb",
                "camellia-192-cfb",
                "camellia-256-cfb",
                "cast5-cfb",
                "des-cfb",
                "idea-cfb",
                "rc2-cfb",
                "seed-cfb",
                "salsa20",
                "chacha20",
                "chacha20-ietf"
            };
        }

        /// <summary>
		///		SSR 协议列表
		/// </summary>
		public static List<string> Protocols = new List<string>()
        {
            "origin",
            "verify_deflate",
            "auth_sha1_v4",
            "auth_aes128_md5",
            "auth_aes128_sha1",
            "auth_chain_a"
        };

        /// <summary>
        ///		SSR 混淆列表
        /// </summary>
        public static List<string> OBFSs = new List<string>()
        {
            "plain",
            "http_simple",
            "http_post",
            "tls1.2_ticket_auth"
        };
    }
}
