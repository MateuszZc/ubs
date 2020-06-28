using System;
using System.Net;
using NUnit.Framework;
using Ocaramba;
using Ocaramba.Logger;
using TechTalk.SpecFlow;

namespace ubs
{
	[Binding]
	public class ProjectTestBase : TestBase
	{
		private readonly ScenarioContext scenarioContext;
		private readonly DriverContext driverContext = new DriverContext();
		public ProjectTestBase(ScenarioContext scenarioContext)
		{
			this.scenarioContext = scenarioContext ?? throw new ArgumentNullException("scenarioContext");
		}
		public TestLogger LogTest
		{
			get
			{
				return DriverContext.LogTest;
			}

			set
			{
				DriverContext.LogTest = value;
			}
		}
		protected DriverContext DriverContext { get; } = new DriverContext();


		[BeforeFeature]
		public static void BeforeClass()
		{
		}

		[AfterFeature]
		public static void AfterClass()
		{
		}

		[Before]
		public void BeforeTest()
		{
			DriverContext.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
			DriverContext.TestTitle = scenarioContext.ScenarioInfo.Title;
			LogTest.LogTestStarting(DriverContext);
			DriverContext.Start();
			DriverContext.WindowMaximize();
			scenarioContext["DriverContext"] = DriverContext;

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

		}

		[After]
		public void AfterTest()
		{
			try
			{
				DriverContext.IsTestFailed = scenarioContext.TestError != null || !driverContext.VerifyMessages.Count.Equals(0);

				try
				{
					var filePaths = SaveTestDetailsIfTestFailed(driverContext);
					SaveAttachmentsToTestContext(filePaths);
				}
				catch (Exception ex)
				{
					LogTest.Error("Error saving test details");
					LogTest.LogError(ex);
				}

				var javaScriptErrors = DriverContext.LogJavaScriptErrors();

				LogTest.LogTestEnding(driverContext);


				if (IsVerifyFailedAndClearMessages(driverContext) && scenarioContext.TestError == null)
				{
					Assert.Fail();
				}

				if (javaScriptErrors)
				{
					Assert.Fail("JavaScript errors found. See the logs for details");
				}
			}
			finally
			{
				DriverContext.Stop();
			}
		}

		private void SaveAttachmentsToTestContext(string[] filePaths)
		{
			if (filePaths != null)
			{
				foreach (var filePath in filePaths)
				{
					LogTest.Info("Uploading file [{0}] to test context", filePath);
					try
					{
						TestContext.AddTestAttachment(filePath);
					}
					catch
					{
						LogTest.Error("Error uploading file [{0}] to test context", filePath);
						throw;
					}
				}
			}
		}
	}
}
