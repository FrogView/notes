    using log4net;
    using log4net.Config;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public static class Log
    {
        private static readonly bool isInit = false;

        static Log()
        {
            if (!isInit)
            {
                InitLog4Net();
                isInit = true;
            }
        }
        private static void InitLog4Net()
        {
            Console.OutputEncoding = Encoding.UTF8;

            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }

        public static void Error(string message, Exception ex = null)
        {
            var logger = GetLogger(message);
            logger.Error(message, ex);
        }

        public static void Info(string message, Exception ex = null)
        {
            var logger = GetLogger(message);
            logger.Info(message, ex);
        }

        private static ILog GetLogger(string message)
        {
            MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            StackTrace trace = new StackTrace();
            if (trace.FrameCount >= 3)
            {
                StackFrame frame = trace.GetFrame(2);
                method = frame.GetMethod();
            }

            var fullName = string.Format("{0}.{1}({2})", method.ReflectedType.FullName, method.Name, string.Join(",", method.GetParameters().Select(o => string.Format("{0} {1}", o.ParameterType, o.Name)).ToArray()));
            var logger = LogManager.GetLogger(fullName);
            return logger;
        }
    }
