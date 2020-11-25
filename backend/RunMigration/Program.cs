using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace RunMigration
{
    internal class Program
    {
        private static readonly NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();

        private static int Main(string[] args)
        {
            logger.Info("Migration. Started");
            try
            {
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

                RunOptions opts = ParseArguments(args);
                logger.Info("Migration. Profile = '{0}', Tags = [{1}]", opts.Profile, opts.Tags == null ? "" : String.Join(", ", opts.Tags));

                var serviceProvider = CreateServices(opts);

                // Put the database update into a scope to ensure
                // that all resources will be disposed.
                using (var scope = serviceProvider.CreateScope())
                {
                    UpdateDatabase(scope.ServiceProvider);
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw;
            }
            finally
            {
                logger.Info("Migration. Finished");
            }
            return 0;
        }

        private static RunOptions ParseArguments(string[] args)
        {
            string tagsValue = GetArgValue(args, "tag");
            string profileValue = GetArgValue(args, "profile");
            string environmentName = GetArgValue(args, "environment");
            if (String.IsNullOrEmpty(environmentName))
            {
                environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            }
            string[] tags = String.IsNullOrEmpty(tagsValue) ? null : tagsValue.Split(new char[] { ',', ';' });
            return new RunOptions()
            {
                EnvironmentName = environmentName,
                Profile = profileValue,
                Tags = tags
            };
        }

        private static string GetArgValue(string[] args, string name)
        {
            int index = Array.IndexOf(args, $"--{name}");
            int valueIndex = index + 1;
            if (index >= 0)
            {
                if (valueIndex >= args.Length)
                {
                    throw new ApplicationException($"Value of argument '--{name}' is not found");
                }
                return args[valueIndex];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices(RunOptions runOpts)
        {
            Assembly[] assemblies = RegisterMigrations.GetMigrationAssemblies();

            string environmentName = runOpts.EnvironmentName;

            logger.Info("Migration. Environment = '{0}'", environmentName);

            IConfiguration configuration = GetConfiguration(environmentName);
            string dbConnectionString = configuration.GetConnectionString("DbContext");
            if (String.IsNullOrEmpty(dbConnectionString))
            {
                throw new ApplicationException("There is no database connection string");
            }

            IServiceCollection services = new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add MSSQL support to FluentMigrator
                    .AddSqlServer2016()
                    // Set the connection string
                    .WithGlobalConnectionString(dbConnectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(assemblies).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // .AddContextSupport(configuration)
                .AddSingleton(configuration)
                // Start of type filter configuration
                .Configure<RunnerOptions>(opt =>
                {
                    // Configure migration runner
                    if (!String.IsNullOrEmpty(runOpts.Profile))
                    {
                        opt.Profile = runOpts.Profile;
                    }
                    if (runOpts.Tags != null && runOpts.Tags.Length > 0)
                    {
                        opt.Tags = runOpts.Tags;
                    }
                });


            bool enableFileLog = configuration.GetValue("MigrationLogFileEnable", true);
            if (enableFileLog)
            {
                string migrationLogPath = GetLogPath();
                services
                    .AddSingleton<ILoggerProvider, LogFileFluentMigratorLoggerProvider>()
                    .Configure<LogFileFluentMigratorLoggerOptions>(opt =>
                    {
                        opt.OutputFileName = migrationLogPath;
                        opt.ShowSql = true;
                    });
            }

            // Build the service provider
            return services.BuildServiceProvider(false);
        }

        private static string GetLogPath()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = String.Format("MigrationLog_{0}.txt", DateTimeOffset.Now.ToString("yyyyMMdd_HHmmss"));
            return Path.Combine(path, fileName);
        }

        private static IConfiguration GetConfiguration(string envName)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!String.IsNullOrEmpty(envName))
            {
                string cfgName = $"appsettings.{envName}.json";
                builder.AddJsonFile(cfgName, optional: true, reloadOnChange: true);
            }
            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                { "EnvironmentName", envName }
            });

            return builder.Build();
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
