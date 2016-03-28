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

            //JobLogger logger2 = new JobLogger(LogType.Database);
            //logger2.LogMessage(null, MessageType.Message);

            JobLogger logger3 = new JobLogger(LogType.Database);
            logger3.LogMessage("mensaje test 3", MessageType.Message);

            JobLogger logger4 = new JobLogger(LogType.TextFile);
            logger4.LogMessage("mensaje test 4", MessageType.Error | MessageType.Warning);

            //JobLogger logger5 = new JobLogger(LogType.Console | LogType.Database);
            //logger5.LogMessage("mensaje test 5", MessageType.Message | MessageType.Warning);   

            JobLogger logger6 = new JobLogger(LogType.TextFile | LogType.Database);
            logger6.LogMessage("mensaje test 6", MessageType.Error);

        }
    }
}
