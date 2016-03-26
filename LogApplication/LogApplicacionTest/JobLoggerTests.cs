using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogApplication.Tests
{
    [TestClass()]
    public class JobLoggerTests
    {
        [TestMethod()]
        public void LogMessageTest()
        {
            JobLogger logger = new JobLogger(LogType.TextFile | LogType.Database);
            logger.LogMessage("mensaje otro tipo test", MessageType.Warning | MessageType.Error);            
        }
    }
}
