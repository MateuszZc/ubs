using System;
using Ocaramba;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ubs
{
	public partial class ProjectPageBase
	{
		public ProjectPageBase(DriverContext driverContext)
		{
			this.DriverContext = driverContext;
			this.Driver = driverContext.Driver;
			Wait = new WebDriverWait(DriverContext.Driver, new TimeSpan(0, 0, 3));
		}
		protected IWebDriver Driver { get; set; }

		protected DriverContext DriverContext { get; set; }

		protected WebDriverWait Wait { get; set; }
	}
}
