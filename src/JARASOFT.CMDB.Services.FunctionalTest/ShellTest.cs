using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace JARASOFT.CMDB.Services.FunctionalTest
{
    [SetUpFixture]
    public class ShellTest
    {
        [SetUp]
        public void RunOnStart()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
        }
    }
}
