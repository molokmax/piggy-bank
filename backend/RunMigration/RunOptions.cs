using System;
using System.Collections.Generic;
using System.Text;

namespace RunMigration
{
    internal class RunOptions
    {
        public string EnvironmentName { get; set; }
        public string Profile { get; set; }
        public string[] Tags { get; set; }
    }
}
