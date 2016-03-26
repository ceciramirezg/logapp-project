using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace LogApplication
{
    public class JobLogger
    {
        private static LogType _logType;

        public JobLogger(LogType logType)
        {
             _logType = logType;          
        }

        public void LogMessage(string messageText, MessageType message)
        {          
            if (string.IsNullOrEmpty(messageText))
            {
                throw new Exception("Message text must be specified");
            } 

            string messageResult = string.Format("{0} type: {1}", DateTime.Now.ToShortDateString(), messageText);          

            StringBuilder builderMessage = new StringBuilder();

            if (message.HasFlag(MessageType.Error))
            {
                builderMessage.AppendLine(messageResult.Replace("type", MessageType.Error.ToString()));               
            }

            if (message.HasFlag(MessageType.Warning))
            {
                builderMessage.AppendLine(messageResult.Replace("type", MessageType.Warning.ToString()));              
            }

            if (message.HasFlag(MessageType.Message))
            {
                builderMessage.AppendLine(messageResult.Replace("type", MessageType.Message.ToString()));              
            }     

            if (_logType.HasFlag(LogType.Database))
            {
               // SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["LogDataBase"]);             
              //  connection.Open();
            
                //SqlCommand command = new SqlCommand("Insert into Log Values(" + string.Format("\"{0}\", {1}", messageText, "") + ")");
                //command.ExecuteNonQuery(); 

                try
                {                 
                    var connectionString = ConfigurationManager.ConnectionStrings["LogDataBase"].ToString(); 

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand();
                            command.Connection = connection;
                                      
                            if (message.HasFlag(MessageType.Error))
                            {
                                command.CommandText = "INSERT INTO Log VALUES(@param1, @param2)";
                                command.Parameters.AddWithValue("@param1", messageResult.Replace("type", MessageType.Error.ToString()));
                                command.Parameters.AddWithValue("@param2", MessageType.Error);
                                command.ExecuteNonQuery();                                
                            }

                            if (message.HasFlag(MessageType.Warning))
                            {
                                command.CommandText = "INSERT INTO Log VALUES(@param3, @param4)";
                                command.Parameters.AddWithValue("@param3", messageResult.Replace("type", MessageType.Warning.ToString()));
                                command.Parameters.AddWithValue("@param4", MessageType.Warning);
                                command.ExecuteNonQuery();
                            }

                            if (message.HasFlag(MessageType.Message))
                            {
                                command.CommandText = "INSERT INTO Log VALUES(@param5, @param6)";
                                command.Parameters.AddWithValue("@param5", messageResult.Replace("type", MessageType.Message.ToString()));
                                command.Parameters.AddWithValue("@param6", MessageType.Message);
                                command.ExecuteNonQuery();
                            }     

                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception(string.Format("{0} {1}", "Sql connection error. ", ex.Message));
                }                             

            }

            if (_logType.HasFlag(LogType.TextFile))
            {              

                //if (!File.Exists())
                //{
                //    File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                //}

                //File.WriteAllText(ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt", messageResult);

                try
                {
                    var pathFile = string.Format("{0}/LogFile{1}.txt", ConfigurationManager.AppSettings["LogFileDirectory"], DateTime.Now.ToString("yyyyMMdd"));
                    
                    using (StreamWriter sw = (File.Exists(pathFile)) ? File.AppendText(pathFile) : File.CreateText(pathFile))
                    {
                        sw.WriteLine(builderMessage);
                    } 
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("{0} {1}", "Write file error. ", ex.Message));
                }
             
            }

            if (_logType.HasFlag(LogType.Console))
            {
                if (message.HasFlag(MessageType.Error))
                {                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(messageResult.Replace("type", MessageType.Error.ToString()));
                }
                if (message.HasFlag(MessageType.Warning))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(messageResult.Replace("type", MessageType.Warning.ToString()));
                }
                if (message.HasFlag(MessageType.Message))
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(messageResult.Replace("type", MessageType.Message.ToString()));
                }
              //  Console.WriteLine(builderMessage);
                Console.ReadKey();
            }                
        }
    }

}
