using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;
[assembly: WebActivator.PreApplicationStartMethod(typeof(AnhServicioWebOctano.Servidor.ServidorHost), "Start")]
namespace AnhServicioWebOctano.Servidor
{
    public class ServidorHost : AppHostBase
    {
        public ServidorHost()
            : base("Servicio Web Octano " + Assembly.GetExecutingAssembly().GetName().Version.ToString(), typeof(ServidorHost).Assembly) { }

        public override void Configure(Funq.Container container)
        {
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

            SetConfig(
                new EndpointHostConfig
                {
                    //ServiceStackHandlerFactoryPath = "WSOctano",
                    EnableFeatures = Feature.Json | Feature.Xml /*| Feature.Metadata*/,
                    GlobalResponseHeaders =
                            {
                                {"Access-Control-Allow-Origin", "*"},
                                {"Access-Control-Allow-Methods", "GET, POST, PUT, DELETE"},
                                {"Access-Control-Allow-Headers", "Content-Type"},
                                {"X-Powered-On", "AGENCIA NACIONAL DE HIDROCARBUROS"},
                                {
                                    "Cache-Control",
                                    "no-cache, private, must-revalidate, max-stale=0, post-check=0, pre-check=0, no-store"
                                },
                                {"Pragma", "no-cache"},
                                {"Expires", "0"},
                                {"Vary", "*"}
                            },
                    DefaultContentType = "application/json",
                    DebugMode = false
                });
            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof(ServidorHost).Assembly);
            //            Config.UseHttpsLinks = true;
        }

        public static void Start()
        {
            new ServidorHost().Init();
        }
    }
}