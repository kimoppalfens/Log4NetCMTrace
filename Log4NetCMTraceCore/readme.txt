1) Create an instance of the log4net.ILog class by copy/pastin the line on line 3 at the beginning of your class or program

static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

2) load the logging.config file that configures the different logging options, more information in regards to the logging.config is specified below.
To do so copy/paste line 8 to be one of the first lines of the entry point of your program. 

log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("Logging.config"));

3) Start logging using the comments
log.Warn("Generated Warning message");
log.WarnFormat("Generated Warning message {0}","Using format string");
log.Info("Generated info message");
log.InfoFormat("Generated info message {0}","Using format string");
log.Error("Generated Error message");
log.ErrorFormat("Generated Error message {0}","Using format string");
log.Info("Generated info message");
log.InfoFormat("Generated info message {0}","Using format string");
log.Debug("Generated debug message");
log.DebugFormat("Generated debug message {0}","Using format string");

This Nuget package contains a sample logging.Config that logs in CMtrace format. There are a couple of variables you can use 
in the logging.config file regarding the location where the logfile will be saved. The location 

The conversion pattern line
      <conversionPattern value="%adminuilog\%programname.log" />
contains the location where the logfile will be generated.

This line accepts 3 variables that are prepended with the % character
%ccmlog = contains the folder where the SCCM client writes its logfiles, typically windows\ccm\logs, 
			but different on an mp, and potentially different if you chose to install the client elsewhere than windows\ccm
%adminuilog = folder containing smsadminui.log provided you have the Configuration Manager admin ui installed.

%programname = Name of your program to be used as the dynamic file name of the log


#Sample Program
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLog4NetCmtrace_Nuget
{
    class Program
    {
        static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("Logging.config"));
            log.Warn("Generated Warning message");
            log.WarnFormat("Generated Warning message {0}", "Using format string");
            log.Info("Generated info message");
            log.InfoFormat("Generated info message {0}", "Using format string");
            log.Error("Generated Error message");
            log.ErrorFormat("Generated Error message {0}", "Using format string");
            log.Debug("Generated debug message");
            log.DebugFormat("Generated debug message {0}", "Using format string");
        }
    }
}

#Console Output
WARN  Generated Warning message
WARN  Generated Warning message Using format string
ERROR Generated Error message
ERROR Generated Error message Using format string

#Logfile output
<![LOG[Generated Warning message]LOG]!><time="16:31:58.194-120" date="09-21-2016" component="Main" context="" type="2" thread="1" file="c:\users\kim\documents\visual studio 2015\Projects\TestLog4NetCmtrace-Nuget\TestLog4NetCmtrace-Nuget\Program.cs:15"> 
<![LOG[Generated Warning message Using format string]LOG]!><time="16:31:58.212-120" date="09-21-2016" component="Main" context="" type="2" thread="1" file="c:\users\kim\documents\visual studio 2015\Projects\TestLog4NetCmtrace-Nuget\TestLog4NetCmtrace-Nuget\Program.cs:16"> 
<![LOG[Generated info message]LOG]!><time="16:31:58.220-120" date="09-21-2016" component="Main" context="" type="6" thread="1" file="c:\users\kim\documents\visual studio 2015\Projects\TestLog4NetCmtrace-Nuget\TestLog4NetCmtrace-Nuget\Program.cs:17"> 
<![LOG[Generated info message Using format string]LOG]!><time="16:31:58.223-120" date="09-21-2016" component="Main" context="" type="6" thread="1" file="c:\users\kim\documents\visual studio 2015\Projects\TestLog4NetCmtrace-Nuget\TestLog4NetCmtrace-Nuget\Program.cs:18"> 
<![LOG[Generated Error message]LOG]!><time="16:31:58.227-120" date="09-21-2016" component="Main" context="" type="3" thread="1" file="c:\users\kim\documents\visual studio 2015\Projects\TestLog4NetCmtrace-Nuget\TestLog4NetCmtrace-Nuget\Program.cs:19"> 
<![LOG[Generated Error message Using format string]LOG]!><time="16:31:58.232-120" date="09-21-2016" component="Main" context="" type="3" thread="1" file="c:\users\kim\documents\visual studio 2015\Projects\TestLog4NetCmtrace-Nuget\TestLog4NetCmtrace-Nuget\Program.cs:20"> 
