﻿using Config.Net;
using log4net;
using log4net.Config;
using MHTriServer.Player;
using MHTriServer.Server;
using MHTriServer.Utils;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace MHTriServer
{
    public class MHTriServer
    {
        public static readonly ILog Log = LogManager.GetLogger(nameof(MHTriServer));

        public static void InitializeLogger()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var filepath = Path.Join(Path.GetDirectoryName(executingAssembly.Location), "log4net.xml");
            if (!File.Exists(filepath))
            {
                throw new ApplicationException("Unable to initialize the logging service\nPlease verify that `log4net.xml` exist at the root folder of the program binary");
            }

            var repo = LogManager.GetRepository(executingAssembly);
            XmlConfigurator.Configure(repo, new FileInfo(filepath));

            // Setup Thread Name
            Thread.CurrentThread.Name = nameof(MHTriServer);
        }

        public static IConfig InitializeConfig()
        {
            const string CONFIG_NAME = "MHTriServer.ini";
            var executingAssembly = Assembly.GetExecutingAssembly();
            var configPath = Path.Join(Path.GetDirectoryName(executingAssembly.Location), CONFIG_NAME);
            if (!File.Exists(configPath))
            {
                var defaultConfigBytes = ResourceUtils.GetResourceBytes(CONFIG_NAME);
                File.WriteAllBytes(configPath, defaultConfigBytes);
                Log.Warn("Default config has been written");
            }

            return new ConfigurationBuilder<IConfig>().UseIniFile(configPath).Build();
        }

        public static int Main(string[] args)
        {
            InitializeLogger();
            var config = InitializeConfig();

            var playerManager = new PlayerManager();

            X509Certificate2 opnServerCertificate;
            try
            {
                opnServerCertificate = new X509Certificate2(config.OpnServer.CertificatePath, config.OpnServer.CertificatePassphrase);
            }
            catch (CryptographicException e)
            {
                Log.FatalFormat("Unable to parse `{0}` certificate. {1}", config.OpnServer.CertificatePath, e.Message);
                return 1;
            }

            var opnServer = new OpnServer(playerManager, config.OpnServer.Address, config.OpnServer.Port, opnServerCertificate);
            var lmpServer = new LmpServer(playerManager);
            var fmpServer = new FmpServer(playerManager);

            opnServer.Start();
            lmpServer.Start();
            fmpServer.Start();

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q)
                {
                    break;
                }
            }

            fmpServer.Stop();
            lmpServer.Stop();
            opnServer.Stop();

            return 0;
        }
    }
}
