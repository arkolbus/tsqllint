﻿using System;
using System.IO;
using NUnit.Framework;
using TSQLLINT_CONSOLE.ConfigHandler;

namespace TSQLLINT_LIB_TESTS.UnitTests.CommandLineOptions
{
    public class CommandLineParserTest
    {
        private string _configFilePath;
        private string ConfigFilePath
        {
            get { return (string.IsNullOrWhiteSpace(_configFilePath) == false) ? _configFilePath : InitializeConfigFilePath(); }
        }

        private string InitializeConfigFilePath()
        {
            var testDirectoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);
            var result = testDirectoryInfo.Parent.Parent.FullName;
            _configFilePath = Path.Combine(result + @"\IntegrationTests\.tsqllintrc");

            return _configFilePath;
        }

        [Test]
        public void NoProblems()
        {
            // arrange
            var args = new[]
            {
                "-c", ConfigFilePath,
                "-f", @"c:\database\foo.sql"
            };

            // act
            var commandLineOptions = new CommandLineOptionParser(args);

            // assert
            Assert.AreEqual(ConfigFilePath, commandLineOptions.ConfigFile);
            Assert.AreEqual(@"c:\database\foo.sql", commandLineOptions.LintPath);
        }

        [Test]
        public void DefaultConfigFile()
        {
            // arrange
            var args = new string[0];

            // act
            var usersDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var commandLineParser = new CommandLineOptionParser(args);

            //assert
            Assert.AreEqual(Path.Combine(usersDirectory, @".tsqllintrc"), commandLineParser.ConfigFile);
        }

        [Test]
        public void GetUsage()
        {
            // arrange
            var args = new string[0];

            // act
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var commandLineParser = new CommandLineOptionParser(args);

            //assert
            Assert.IsTrue(commandLineParser.GetUsage().Contains("Usage: tsqllint [options]"));
        }
    }
}