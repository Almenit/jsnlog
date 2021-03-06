﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JSNLog.LogHandling;
using JSNLog.Tests.Logic;
using System.Xml;
using JSNLog.Infrastructure;
using System.Text;
using JSNLog.Exceptions;

namespace JSNLog.Tests.UnitTests
{
    /// <summary>
    /// These tests ensure that errorneous config is handled with an exception, rather than outright crashing.
    /// </summary>

    [TestClass]
    public class ProcessRootTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidAttributeException))]
        public void InvalidLevel()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <logger name=""l2"" level=""xyz"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAttributeException))]
        public void InvalidLevel2()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <ajaxAppender name=""da1"" level=""abc"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAttributeException))]
        public void InvalidAppender()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <ajaxAppender name=""da1"" level=""2300"" />
    <logger name=""l2"" appenders=""da2"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAttributeException))]
        public void InvalidAppender2()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <logger name=""l2"" appenders=""da2"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingAttributeException))]
        public void NoAppenderName()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <ajaxAppender level=""2300"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAttributeException))]
        public void InvalidBatchSize()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <ajaxAppender name=""aa"" batchSize=""abc"" level=""2300"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAttributeException))]
        public void InvalidBufferSize()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <ajaxAppender name=""aa"" bufferSize=""abc"" sendWithBufferLevel=""3000"" level=""2300"" storeInBufferLevel=""2000"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(GeneralAppenderException))]
        public void MissingBufferParameter()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <ajaxAppender name=""aa"" bufferSize=""2"" level=""2300"" storeInBufferLevel=""2000"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(GeneralAppenderException))]
        public void MissingBufferParameter2()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <ajaxAppender name=""aa"" sendWithBufferLevel=""3000"" level=""2300"" storeInBufferLevel=""2000"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(GeneralAppenderException))]
        public void MissingBufferParameter3()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <ajaxAppender name=""aa"" bufferSize=""2"" level=""2300"" sendWithBufferLevel=""3000"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(GeneralAppenderException))]
        public void WrongBufferParameter()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <ajaxAppender name=""aa"" bufferSize=""2"" storeInBufferLevel=""2400"" level=""2300"" sendWithBufferLevel=""3000"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(GeneralAppenderException))]
        public void WrongBufferParameter2()
        {
            // Arrange

            string configXml = @"
                <jsnlog>
    <ajaxAppender name=""aa"" bufferSize=""2"" storeInBufferLevel=""2000"" level=""3300"" sendWithBufferLevel=""3000"" />
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAttributeException))]
        public void InvalidEnabled()
        {
            // Arrange

            string configXml = @"
                <jsnlog enabled=""1"">
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAttributeException))]
        public void InvalidMaxMessages()
        {
            // Arrange

            string configXml = @"
                <jsnlog maxMessages=""xyz"">
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAttributeException))]
        public void InvalidMaxMessages2()
        {
            // Arrange

            string configXml = @"
                <jsnlog maxMessages=""true"">
</jsnlog>
";

            // Act and Assert
            RunTest(configXml);
        }

        private void RunTest(string configXml)
        {
            var sb = new StringBuilder();
            XmlElement xe = JSNLog.Tests.Logic.Utils.ConfigToXe(configXml);

            var configProcessor = new ConfigProcessor();
            configProcessor.ProcessRootExec(xe, sb, s => s, "23.89.450.1", "req", true);
        }
    }
}

