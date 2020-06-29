using NUnit.Framework;
using Ocaramba;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using ubs.PageObjects;

namespace ubs.Steps
{
	[Binding]
	public class DemoSteps
	{
		private readonly DemoPageObjects page;
		private readonly LoginPageObjects loginPage;
		private readonly ScenarioContext scenarioContext;

		public DemoSteps(ScenarioContext scenarioContext)
		{
			this.scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));

			var driverContext = this.scenarioContext["DriverContext"] as DriverContext;

			if (page != null) return;

			page = new DemoPageObjects(driverContext);
			loginPage = new LoginPageObjects(driverContext);

		}
			   
		[When(@"User navigate to Trial E-Banking demo page")]
		public void WhenUserNavigateToTrialE_BankingDemoPage()
		{
			loginPage.ClickOnButton("UBS logins");
			loginPage.SelectOption("UBS E-Banking Switzerland");
			loginPage.SelectOption("Trial E-Banking demo");
			page.ClickOkOnPopWindow();
		}


		[When(@"User clicks on New button")]
		public void WhenUserClicksOnNewButton()
		{
			page.ClickOnNewButton();
		}

		[When(@"User clicks on ""(.*)"" option")]
		public void WhenUserClicksOnOption(string option)
		{
			page.ClickOnOptionButton(option);
		}

		[Then(@"User should see Account tranfer page")]
		public void ThenUserShouldSeeAccountTranferPage()
		{
			Assert.AreEqual("Account transfer", page.VerifyIfTransferPageIsPresented(), "Problem with TransferPage presentation");
		}

		[When(@"User clicks on ""(.*)"" field")]
		public void WhenUserClicksOnField(string fieldOption)
		{
			page.ClickOnFieldOption(fieldOption);
		}

		[When(@"User select first element in ""(.*)""")]
		public void WhenUserSelectFirstElementIn(string fieldOption)
		{
			page.ClickFirstElementOnFieldOption(fieldOption);
		}

		[When(@"User enter ""(.*)"" in ""(.*)"" field")]
		public void WhenUserEnterInField(string value, string field)
		{
			scenarioContext.Add(field, value);
			page.EnterValueInProperField(value, field);
		}

		[When(@"User clicks on Transfer button")]
		public void WhenUserClicksOnTransferButton()
		{
			page.ClickOnTransferButton();
		}

		[Then(@"User should see proper message after account transfer")]
		public void ThenUserShouldSeeProperMessageAfterAccountTransfer()
		{
			Assert.IsTrue(page.VerifyIfMessageAfterTransfer(), "Message after transfer is incorrect.");
		}

	}
}
