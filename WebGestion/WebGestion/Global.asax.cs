using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using Microsoft.Practices.Unity.Configuration;
using MvcTables.Configuration;
using System.Web.Http;

// C:\Users\David\AppData\Local\Microsoft\VisualStudio\12.0\Extensions\vbcjsvqx.ymf\Templates\MvcScaffolder

namespace WebGestion
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureMvcTables.InTheSameAssembly.As<MvcApplication>();

        }

        // http://www.codeproject.com/Articles/34422/Detecting-a-mobile-browser-in-ASP-NET
        public static bool isMobileBrowser
        {
            get
            {
                //GETS THE CURRENT USER CONTEXT
                HttpContext context = HttpContext.Current;

                //FIRST TRY BUILT IN ASP.NT CHECK
                if (context.Request.Browser.IsMobileDevice)
                {
                    return true;
                }
                //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
                if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
                {
                    return true;
                }
                //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
                if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                    context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
                {
                    return true;
                }
                // http://stackoverflow.com/questions/19788159/how-to-know-if-the-asp-net-site-is-accessed-from-a-mobile-device-or-from-a-syste
                string userAgent = context.Request.UserAgent;
                if (userAgent.Contains("BlackBerry")
                  || (userAgent.Contains("iPhone") || (userAgent.Contains("Android"))))
                {
                    return true;
                }
                //AND FINALLY CHECK THE HTTP_USER_AGENT 
                //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
                if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
                {
                    //Create a list of all mobile types
                    string[] mobiles =
                        new[]
                            {
                        "midp", "j2me", "avant", "docomo",
                        "novarra", "palmos", "palmsource",
                        "240x320", "opwv", "chtml",
                        "pda", "windows ce", "mmp/",
                        "blackberry", "mib/", "symbian",
                        "wireless", "nokia", "hand", "mobi",
                        "phone", "cdm", "up.b", "audio",
                        "SIE-", "SEC-", "samsung", "HTC",
                        "mot-", "mitsu", "sagem", "sony"
                        , "alcatel", "lg", "eric", "vx",
                        "NEC", "philips", "mmm", "xx",
                        "panasonic", "sharp", "wap", "sch",
                        "rover", "pocket", "benq", "java",
                        "pt", "pg", "vox", "amoi",
                        "bird", "compal", "kg", "voda",
                        "sany", "kdd", "dbt", "sendo",
                        "sgh", "gradi", "jb", "dddi",
                        "moto", "iphone", "bq"
                            };

                    //Loop through each item in the list created above 
                    //and check if the header contains that text
                    var HTTP_USER_AGENT = context.Request.ServerVariables["HTTP_USER_AGENT"].ToLower();
                    foreach (string s in mobiles)
                    {
                        if (HTTP_USER_AGENT.Contains(s.ToLower()))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }
    }

}
