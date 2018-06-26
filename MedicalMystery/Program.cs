using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedicalMystery
{
    /// <summary>
    /// This is the starting point for the whole application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// MAinmethod which is used to initiate the application
        /// </summary>
        /// <param name="args">Arguments that are passed along</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builds a webHost
        /// </summary>
        /// <param name="args">Arguments that are passed along</param>
        /// <returns>IWebHost</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
