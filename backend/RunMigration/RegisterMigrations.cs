using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RunMigration
{
    internal class RegisterMigrations
    {
        public static Assembly[] GetMigrationAssemblies()
        {
            return new Assembly[]
            {
                typeof(RegisterMigrations).Assembly
            };
        }
    }
}
