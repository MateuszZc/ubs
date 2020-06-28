using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ocaramba;
using TechTalk.SpecFlow;
using ubs.PageObjects;

namespace ubs.Steps
{
	[Binding]
	public class LoginSteps
	{
		private readonly LoginPageObjects page;
		private readonly ScenarioContext scenarioContext;
		public LoginSteps(ScenarioContext scenarioContext)
		{
			this.scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));

			var driverContext = this.scenarioContext["DriverContext"] as DriverContext;

			if (page != null) return;

			page = new LoginPageObjects(driverContext);
		}


		[Given(@"Open ubs webpage")]
		public void GivenOpenUbsWebpage()
		{
			page.OpenUbsWebpage(LoginPageObjects.Page.Home);
		}

		[When(@"User accept all Privacy Settings if its needed")]
		public void WhenUserAcceptAllPrivacySettingsIfItsNeeded()
		{
			page.ClickOnAgreeToAllPrivacySettings();
		}

		[Then(@"User should see dropdown ""(.*)""")]
		public void ThenUserShouldSeeDropdown(string headerText)
		{
			Assert.AreEqual(headerText, page.GetHeaderTextOfDropdown());
		}

		[When(@"User clicks on ""(.*)"" options in UBS logins dropdown")]
		public void WhenUserClicksOnOptionsInUBSLoginsDropdown(string optionName)
		{
			page.SelectOption(optionName);
		}

		[When(@"User click on ""(.*)"" button")]
		public void WhenUserClickOnButton(string buttonName)
		{
			page.ClickOnButton(buttonName);
		}

		[Then(@"User should see E-Banking login page")]
		public void ThenUserShouldSeeE_BankingLoginPage()
		{
			Assert.IsTrue(page.VerifyIfHeaderEBankingLoginIsCorrect(), "Header of E-Banking login is incorrect");
		}

		[When(@"User clicks on ""(.*)"" option on E-Banking login page")]
		public void WhenUserClicksOnOptionOnE_BankingLoginPage(string optionName)
		{
			page.SelectOption(optionName);
		}

		[Then(@"User should see Account and custody account overview page")]
		public void ThenUserShouldSeeAccountAndCustodyAccountOverviewPage()
		{
			Assert.IsTrue(page.VerifyIfUserCanSeeCorrectSubheader(), "Sub-header is incorrect.");
		}

		[When(@"User enter ""(.*)"" value in contract Number field")]
		public void WhenUserEnterValueInContractNumberField(string text)
		{
			page.EnterValueInContractNumberField(text);
		}

		[When(@"User clicks on Continue button")]
		public void WhenUserClicksOnContinueButton()
		{
			page.ClickOnContinueButton();
		}

		[Then(@"User should see warning message after incorrect try")]
		public void ThenUserShouldSeeWarningMessageAfterIncorrectTry()
		{
			Assert.IsTrue(page.VerifyIfMessageIsPresented());
		}


	}
}
