using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Layout.Pattern;
using System.Management;

namespace Log4Net_CMTrace
{
    public class NumericLevelPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            int LoggingLevel = 1;
            switch (loggingEvent.Level.Value)
            {
                case 10000: LoggingLevel = 4; break;
                case 30000: LoggingLevel = 5; break;
                case 40000: LoggingLevel = 6; break;
                case 60000: LoggingLevel = 2; break;
                case 70000: LoggingLevel = 3; break;
                default: LoggingLevel = 1; break;

            }
            writer.Write(LoggingLevel);
        }
    }

    public class CMClientLogFolderPatternConverter : log4net.Util.PatternConverter
    {
        protected override void Convert(System.IO.TextWriter writer, object state)
        {
            string CMClientLogFolder = string.Empty;
            ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\ccm\\Policy\\Machine\\ActualConfig");
            ObjectQuery query = new ObjectQuery("SELECT LogDirectory FROM CCM_Logging_GlobalConfiguration");
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ManagementObject managementObject = (ManagementObject)enumerator.Current;
                    CMClientLogFolder = managementObject["LogDirectory"].ToString();
                }
            }
            writer.Write(CMClientLogFolder);
        }
    }
    public class CMAdminUILogFolderPatternConverter : log4net.Util.PatternConverter
    {
        override protected void Convert(System.IO.TextWriter writer, object state)
        {
            string CMAdminUILogFolder = string.Empty;
            try
            {
                CMAdminUILogFolder = Environment.GetEnvironmentVariable("SMS_ADMIN_UI_PATH");
            }
            catch
            {
                CMAdminUILogFolder = CMAdminUILogFolder = Environment.GetEnvironmentVariable("TEMP");
            }
            writer.Write(CMAdminUILogFolder + "\\..\\..\\AdminUILog");
        }
    }
    public class ProgramNamePatternConverter : log4net.Util.PatternConverter
    {
        override protected void Convert(System.IO.TextWriter writer, object state)
        {
            string ProgramName = (string.Empty);
            try
            {
                ProgramName = (System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            }
            catch
            {
                ProgramName = "programname";
            }
            writer.Write(ProgramName);
        }
    }
    
    public class UTCOffsetPatternConverter : log4net.Util.PatternConverter
    {
        override protected void Convert(System.IO.TextWriter writer, object state)
        {
            TimeSpan delta = (System.TimeSpan.Zero);
            try
            {
                System.TimeZone TimeZoneInfo = System.TimeZone.CurrentTimeZone;
                delta = TimeZoneInfo.GetUtcOffset(DateTime.Now);
            }
            catch
            {
                
            }
            writer.Write(delta.TotalMinutes.ToString());
        }
    }
    public class MyLock : log4net.Appender.FileAppender.MinimalLock
    {
        public override Stream AcquireLock()
        {
            if (CurrentAppender.Threshold == log4net.Core.Level.Off)
                return null;

            return base.AcquireLock();
        }
    }
}
