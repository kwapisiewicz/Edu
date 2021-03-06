﻿using Exams.Host.Security;
using Exams.Model;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace Exams.Host
{
    public class WebApiConfig
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MessageHandlers.Add(new AuthorizationHandler());
            RegisterOData(config);

            appBuilder.UseWebApi(config);
        }

        private static void RegisterOData(HttpConfiguration config)
        {
            // New code:
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Question>("Questions");
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Answer>("Answers");
            builder.EntitySet<Score>("Scores");
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
        }
    }
}
