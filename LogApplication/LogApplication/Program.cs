using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------Start logs--------");

            JobLogger logger = new JobLogger(LogType.Database | LogType.Console);
            logger.LogMessage("Message log test", MessageType.Error | MessageType.Warning);
            
            Console.WriteLine("------Eng logs-------");          
        }
    }
  
}
