using System;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Ocaramba;
using Ocaramba.Extensions;
using Ocaramba.Types;

namespace ubs.PageObjects
{
	public class LoginPageObjects : ProjectPageBase
	{
		private readonly ElementLocator
			_buttonLocator = new ElementLocator(Locator.XPath, "//button[contains(span/text(),'{0}')]"),
			_optionOnDropdownLocator = new ElementLocator(Locator.XPath, "//li[contains(a/text(),'{0}')]"),
			_headerLocator = new ElementLocator(Locator.XPath, "//h1"),
			_subHeaderLocator = new ElementLocator(Locator.XPath, "//h2"),
			_agreeToAllButtonLocator = new ElementLocator(Locator.XPath, "//span[contains(text(),'Agree to all')]"),
			_contractNumberFieldLocator = new ElementLocator(Locator.XPath, "//input[@data-label='Contract Number']"),
			_continueButtonLocator = new ElementLocator(Locator.Id, "AuthGetContractNrDialog_submit"),
			_messageWarningLocator = new ElementLocator(Locator.XPath, "//div[@class='uwr-message-box uwr-message-box-type-warning ']//div[2]"),
			_dropdownHeaderTextLocator = new ElementLocator(Locator.XPath, "//h3[@class='headerLogin__hl']");
		public LoginPageObjects(DriverContext driverContext) : base(driverContext)
		{
		}
		public enum Page
		{
			Home
		}
		public void OpenUbsWebpage(Page page)
		{
			switch (page)
			{
				case Page.Home:
					Driver.NavigateTo(GetUrl());
					break;
				default:
					throw new Exception($"Page {page} does not exist");
			}
		}
		private static Uri GetUrl()
		{
			var url = BaseConfiguration.GetUrlValue;
			return new Uri(url);
		}

		public LoginPageObjects ClickOnButton(string buttonName)
		{
			
			Driver.GetElement(_buttonLocator.Format(buttonName), 5).Click();
			return this;
		}		
		
		public LoginPageObjects ClickOnAgreeToAllPrivacySettings()
		{
			Driver.SwitchTo()
				.Frame(Driver.GetElement(new ElementLocator(Locator.XPath, "//iframe[@class='cboxIframe']")));

			
			Driver.GetElement(_agreeToAllButtonLocator,5).Click();

			return this;
		}		
		
		public string GetHeaderTextOfDropdown()
		{
			return Driver.GetElement(_dropdownHeaderTextLocator,5).Text;
		}

		public LoginPageObjects SelectOption(string optionName)
		{
			Driver.GetElement(_optionOnDropdownLocator.Format(optionName),5).Click();
			return this;
		}		
		
		public bool VerifyIfHeaderEBankingLoginIsCorrect()
		{
			return Driver.GetElement(_headerLocator,5).Text.Contains("E-Banking login"); 
		}		
		
		public bool VerifyIfUserCanSeeCorrectSubheader()
		{
			return Driver.GetElement(_subHeaderLocator,5).Text.Contains("Account and custody account overview"); 
		}

		public LoginPageObjects EnterValueInContractNumberField(string value)
		{
			Driver.GetElement(_contractNumberFieldLocator, 5).SendKeys(value);
			return this;
		}

		public LoginPageObjects ClickOnContinueButton()
		{
			Driver.GetElement(_continueButtonLocator,5).Click();
			return this;
		}

		public bool VerifyIfMessageIsPresented()
		{

			return Driver.GetElement(_messageWarningLocator,5).Text.Contains("Unknown contract number, please repeat entry.");
		}
	}
}
