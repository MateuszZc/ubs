using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocaramba.Extensions;
using Ocaramba.Types;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ubs.Helper
{
	public static class DriverExtensions
	{
		public static void WaitForElementToBeDisplayed(this IWebDriver driver, ElementLocator locator,
			double timeoutInSeconds)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
			try
			{
				wait.Until(e => e.GetElement(locator).Displayed);
			}
			catch (NoSuchElementException)
			{
			}
		}


		public static bool TryToClick(this IWebDriver driver, ElementLocator locator, double timeout = 0)
		{
			var start = DateTime.Now;

			while (true)
			{
				try
				{
					driver.GetElement(locator).Click();
					return true;
				}
				catch
				{
					// ignored
				}

				if (DateTime.Now - start > TimeSpan.FromSeconds(timeout))
				{
					return false;
				}
			}
		}
	}
}
