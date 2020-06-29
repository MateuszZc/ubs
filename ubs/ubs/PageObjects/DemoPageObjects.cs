using Ocaramba;
using Ocaramba.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ocaramba.Extensions;
using OpenQA.Selenium;
using ubs.Helper;

namespace ubs.PageObjects
{
	public class DemoPageObjects : ProjectPageBase
	{
		private readonly ElementLocator
			_okPopupWidnowButtonLocator = new ElementLocator(Locator.XPath, "//span[contains(text(), 'OK')]"),
			_newButtonLocator = new ElementLocator(Locator.XPath, "//a[contains(span/text(),'New')]"),
			_transactionButtonLocator = new ElementLocator(Locator.XPath, "//div[contains(a/div/header/h3/text(),'{0}')]"),
			_transactionFieldLocator = new ElementLocator(Locator.XPath, "//legend[contains(text(),'{0}')]//ancestor::fieldset//div[contains(@class,'AccountSelect__StyledAccountSelectWrapper')]"),
			_accountListLocator = new ElementLocator(Locator.XPath, "//h3[contains(text(),'{0}')]//ancestor::div[@data-cy='my-accounts-list-modal']//main//header"),
			_paymentFieldLocator = new ElementLocator(Locator.XPath, "//label[contains(text(),'{0}')]//preceding-sibling::input"),
			_transferButtonLocator = new ElementLocator(Locator.XPath, "//div[contains(@class,'Form__StyledFormFooter')]//button[contains(@class,'Button__StyledButton')]//div"),
			_messageAfterTransferLocator = new ElementLocator(Locator.XPath, "//div[@id='portal']//main//p"),
			_headersInMainSectionLocator = new ElementLocator(Locator.XPath, "//section[contains(@class,'Layout__StyledMainSection')]//header//h3");


		public DemoPageObjects(DriverContext driverContext) : base(driverContext)
		{
		}

		public DemoPageObjects ClickOkOnPopWindow()
		{
			Driver.GetElement(_okPopupWidnowButtonLocator, 5).Click();
			return this;
		}	
		
		public DemoPageObjects ClickOnNewButton()
		{
			Driver.GetElement(_newButtonLocator, 5).Click();
			return this;
		}

		public DemoPageObjects ClickOnOptionButton(string optionName)
		{
			Driver.GetElement(_transactionButtonLocator.Format(optionName), 5).Click();
			return this;
		}

		public string VerifyIfTransferPageIsPresented()
		{
			Driver.WaitForElementToBeDisplayed(_headersInMainSectionLocator, 5);
			return Driver.GetElement(_headersInMainSectionLocator, 5).Text;
		}

		public DemoPageObjects ClickOnFieldOption(string fieldOption)
		{
			Driver.GetElement(_transactionFieldLocator.Format(fieldOption),5).Click();
			return this;
		}

		public DemoPageObjects ClickFirstElementOnFieldOption(string fieldOption)
		{
			Driver.GetElement(_accountListLocator.Format(fieldOption),5).Click();
			return this;
		}

		public DemoPageObjects EnterValueInProperField(string value,string field)
		{

			switch (field)
			{
				case "Amount":
					((IJavaScriptExecutor)Driver).ExecuteScript($"document.querySelector('#gatsby-focus-wrapper > div > main > section > form > div > div > div > fieldset:nth-child(3) > div > div:nth-child(1) > div > input').value='{value}';");
					break;
				case "Booking text":
					((IJavaScriptExecutor)Driver).ExecuteScript($"document.querySelector('#gatsby-focus-wrapper > div > main > section > form > div > div > div > fieldset:nth-child(3) > div > div:nth-child(4) > div > input').value='{value}';");
					break;
			}
			return this;
		}
		
		public bool VerifyIfTransferButtonHasProperValue(string value)
		{
			return Driver.GetElement(_transferButtonLocator).Text.Contains(value);
		}

		public DemoPageObjects ClickOnTransferButton()
		{
			Driver.GetElement(_transferButtonLocator).Click();
			return this;
		}

		public bool VerifyIfMessageAfterTransfer()
		{
			return Driver.GetElement(_messageAfterTransferLocator).Text.Contains("Your order has been accepted. Provided sufficient funds are available, it will be executed on");
		}
	}
}
