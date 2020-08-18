﻿using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace webhook_server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.Run(  async (context) =>
            {
                context.Request.Headers.TryGetValue("Validation-Token", out StringValues validationToken);
                context.Response.Headers.Add("Validation-Token", validationToken);
                if (context.Request.Path == "/webhook" && context.Request.Method == "POST")
                {
                    using StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                    var str = await reader.ReadToEndAsync();
                    Console.WriteLine(str);
                }
            });
        }
    }
}
