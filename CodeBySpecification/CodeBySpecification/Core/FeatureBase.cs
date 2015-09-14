using System;
using System.Collections.Generic;
using System.Configuration;
using CodeBySpecification.API;
using CodeBySpecification.API.Service.Api;
using Newtonsoft.Json.Linq;
using Report.Base.Service;
using ScreenRecorder.Base.Service;
using Selenium.Base.Service;
using TechTalk.SpecFlow;

namespace CodeBySpecification.Core
{
	[Binding]
	public class FeatureBase
	{
		private static IUiAutomationService UiAutomationService;
		private static readonly IDictionary<string, string> DataShare = new Dictionary<string, string>();
		private static string objectRepoResource;
		private static IScreenRecordService recorder;

        #region Core Step Definition Vocabulary

        [BeforeFeature("UIAutomationTest")]
		public static void BeforeSeleniumTestFeature()
		{
			var browserName = ConfigurationManager.AppSettings["UI.Tests.Target.Browser"];
			objectRepoResource = ConfigurationManager.AppSettings["UI.Tests.Object.Definitions.Path"];
            UiAutomationService = new SeleniumUIAutomationService();
            UiAutomationService.InitilizeTests(browserName, objectRepoResource);

            JObject currentFeature = new JObject();
            currentFeature["title"] = FeatureContext.Current.FeatureInfo.Title;
            currentFeature["description"] = FeatureContext.Current.FeatureInfo.Description;
            currentFeature["tags"] = new JArray(FeatureContext.Current.FeatureInfo.Tags);
            currentFeature["scenarios"] = new JArray();
            FeatureContext.Current["currentFeature"] = currentFeature;
        }

        [BeforeFeature("MobileUIAutomationTest")]
        public static void BeforeAppiumTestFeature()
        {
            var browserName = ConfigurationManager.AppSettings["UI.Tests.Appium.capability.platformName"];
            objectRepoResource = ConfigurationManager.AppSettings["UI.Tests.Object.Definitions.Path"];
            UiAutomationService = new AppiumUiAutomationServices();
            UiAutomationService.InitilizeTests(browserName, objectRepoResource);
		}
            
		[BeforeScenario("record")]
		public static void BeforeTestScenarioWithRecord()
		{
			recorder = new ExpressionEncoderRecorder();
			recorder.OutputFile = @"E:\" + ScenarioContext.Current.ScenarioInfo.Title + ".wmv";
			recorder.Start();
        }

        [BeforeScenario("UIAutomationTest")]
        public static void BeforeSeleniumTestScenario()
        {
            FeatureContext.Current["time"] = DateTime.UtcNow;
        }

        [AfterScenarioBlock]
        public static void AfterScenarioBlock()
        {
            var err = ScenarioContext.Current.TestError;
        }

        [AfterScenario("UIAutomationTest")]
        public static void AfterSeleniumTestScenario()
        {
            var time = DateTime.UtcNow;

			var currentFeature = (JObject) FeatureContext.Current["currentFeature"];
			var scenarios = (JArray) currentFeature["scenarios"];
            var scenario = new JObject();
            scenario["title"] = ScenarioContext.Current.ScenarioInfo.Title;
            scenario["startTime"] = FeatureContext.Current["time"].ToString();
            scenario["endTime"] = time.ToString();
            scenario["tags"] = new JArray(ScenarioContext.Current.ScenarioInfo.Tags);
            
            var err = ScenarioContext.Current.TestError;
			if (err != null)
			{
                var error = new JObject();
                error["message"] = ScenarioContext.Current.TestError.Message;
                error["stackTrace"] = ScenarioContext.Current.TestError.StackTrace;
                scenario["error"] = error;
                scenario["status"] = "error";
            }
            else {
                scenario["status"] = "ok";
            }
            scenarios.Add(scenario);
		}

		[AfterScenario("record")]
		public static void AfterTestScenarioWithRecord()
		{
			recorder.Stop();
        }

        [AfterFeature("UIAutomationTest")]
        public static void AfterSeleniumTestFeature()
        {
			var currentFeature = (JObject) FeatureContext.Current["currentFeature"];
            var outputJSON = currentFeature.ToString();

			IReportService report = new HtmlReportService();
			var ouput = report.Generate(currentFeature);
			System.IO.File.WriteAllText(@"E:\" + FeatureContext.Current.FeatureInfo.Title + ".json", outputJSON);
			System.IO.File.WriteAllText(@"E:\" + FeatureContext.Current.FeatureInfo.Title + ".html", ouput);
        }

        #region Read the content of <element>

        [Given(@"Read the content of ""(.*)""")]
		[When(@"Read the content of ""(.*)""")]
		[Then(@"Read the content of ""(.*)""")]
		public void ReadTheContentOf(string elementKey)
		{
			FeatureContext.Current[elementKey] = UiAutomationService.GetElementText(elementKey);
		}

		[Given(@"Get the content of ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[When(@"Get the content of ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[Then(@"Get the content of ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		public void ReadTheContentOfWithTheOf(string elementKey, string selectionType, string selection)
		{
			FeatureContext.Current[elementKey] = UiAutomationService.GetElementText(elementKey, selectionType, selection);
		}

		#endregion

		#region The content of <element> is equal to <expected value>

		[Given(@"Content of ""(.*)"" contains text ""(.*)""")]
		[When(@"Content of ""(.*)"" contains text ""(.*)""")]
		[Then(@"Content of ""(.*)"" contains text ""(.*)""")]
		[Given(@"The content of ""(.*)"" contains text ""(.*)""")]
		[When(@"The content of ""(.*)"" contains text ""(.*)""")]
		[Then(@"The content of ""(.*)"" contains text ""(.*)""")]
		public void TheConentOfIsEqualTo(string elementKey, string expectedContent)
		{
			UiAutomationService.IsElementContentEqual(elementKey, expectedContent);
		}

		[Given(@"The content of ""(.*)"" with the ""(.*)"" of ""(.*)"" has text ""(.*)""")]
		[When(@"The content of ""(.*)"" with the ""(.*)"" of ""(.*)"" has text ""(.*)""")]
		[Then(@"The content of ""(.*)"" with the ""(.*)"" of ""(.*)"" has text ""(.*)""")]
		[Given(@"Content of ""(.*)"" with the ""(.*)"" of ""(.*)"" has text ""(.*)""")]
		[When(@"Content of ""(.*)"" with the ""(.*)"" of ""(.*)"" has text ""(.*)""")]
		[Then(@"Content of ""(.*)"" with the ""(.*)"" of ""(.*)"" has text ""(.*)""")]
		public void TheConentOfWithTheOfIsEqualTo(string elementKey, string selectionMethod, string selection, string expectedContent)
		{
			UiAutomationService.IsElementContentEqual(elementKey, selectionMethod, selection, expectedContent);
		}

		#endregion

		#region The content of page/element contains text with pattern <text pattern>

		[Given(@"Page contains the text pattern ""(.*)""")]
		[When(@"Page contains the text pattern ""(.*)""")]
		[Then(@"Page contains the text pattern ""(.*)""")]
		[Given(@"The page contains text pattern ""(.*)""")]
		[When(@"The page contains text pattern ""(.*)""")]
		[Then(@"The page contains text pattern ""(.*)""")]
		[Given(@"The content of the page contains text pattern ""(.*)""")]
		[When(@"The content of the page contains text pattern ""(.*)""")]
		[Then(@"The content of the page contains text pattern ""(.*)""")]
		public void ThePageContainsTextPattern(string textPattern)
		{
			UiAutomationService.IsPageContainsTextPattern(textPattern);
		}

		[Given(@"""(.*)"" contains the text pattern ""(.*)""")]
		[When(@"""(.*)"" contains the text pattern ""(.*)""")]
		[Then(@"""(.*)"" contains the text pattern ""(.*)""")]
		[Given(@"The ""(.*)"" contains text pattern ""(.*)""")]
		[When(@"The ""(.*)"" contains text pattern ""(.*)""")]
		[Then(@"The ""(.*)"" contains text pattern ""(.*)""")]
		[Given(@"The content of the ""(.*)"" contains text pattern ""(.*)""")]
		[When(@"The content of the ""(.*)"" contains text pattern ""(.*)""")]
		[Then(@"The content of the ""(.*)"" contains text pattern ""(.*)""")]
		public void TheContainsTextPattern(string elementKey, string textPattern)
		{
			UiAutomationService.IsElementContainsTextPattern(elementKey, textPattern);
		}

		#endregion

		#region Click on <element>

		[Given(@"I click on ""(.*)""")]
		[When(@"I click on ""(.*)""")]
		[Then(@"I click on ""(.*)""")]
		[Given(@"Click on ""(.*)""")]
		[When(@"Click on ""(.*)""")]
		[Then(@"Click on ""(.*)""")]
		public void ClickOn(string elementKey)
		{
			UiAutomationService.ClickOn(elementKey);
		}

		[Given(@"I click on element ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[When(@"I click on element ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[Then(@"I click on element ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[Given(@"Click on element ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[When(@"Click on element ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[Then(@"Click on element ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		public void ClickOnWithTheOf(string elementKey, string selectionMethod, string selection)
		{
			UiAutomationService.ClickOn(elementKey, selectionMethod, selection);
		}

		[Given(@"I click on ""(.*)"" and wait ""(.*)"" seconds")]
		[When(@"I click on ""(.*)"" and wait ""(.*)"" seconds")]
		[Then(@"I click on ""(.*)"" and wait ""(.*)"" seconds")]
		[Given(@"Click on ""(.*)"" and wait ""(.*)"" seconds")]
		[When(@"Click on ""(.*)"" and wait ""(.*)"" seconds")]
		[Then(@"Click on ""(.*)"" and wait ""(.*)"" seconds")]
		public void ClickOnAndWaitSeconds(string elementKey, string waitTime)
		{
			int timeout;
			if (int.TryParse(waitTime, out timeout))
			{
				UiAutomationService.ClickOn(elementKey, timeout);
			}
			else
			{
				throw new Exception("\"" + waitTime + "\" is invalid.");
			}
		}

		[Given(@"I click on element ""(.*)"" with the ""(.*)"" of ""(.*)"" and wait ""(.*)"" seconds")]
		[When(@"I click on element ""(.*)"" with the ""(.*)"" of ""(.*)"" and wait ""(.*)"" seconds")]
		[Then(@"I click on element ""(.*)"" with the ""(.*)"" of ""(.*)"" and wait ""(.*)"" seconds")]
		[Given(@"Click on element ""(.*)"" with the ""(.*)"" of ""(.*)"" and wait ""(.*)"" seconds")]
		[When(@"Click on element ""(.*)"" with the ""(.*)"" of ""(.*)"" and wait ""(.*)"" seconds")]
		[Then(@"Click on element ""(.*)"" with the ""(.*)"" of ""(.*)"" and wait ""(.*)"" seconds")]
		public void ClickOnWithTheOfAndWaitSeconds(string elementKey, string selectionMethod, string selection, string waitTime)
		{
			int timeout;
			if (int.TryParse(waitTime, out timeout))
			{
				UiAutomationService.ClickOn(elementKey, timeout, selectionMethod, selection);
			}
			else
			{
				throw new Exception("\"" + waitTime + "\" is invalid.");
			}
		}

		#endregion

		#region Drag <element1> and drop on to <element2>

		[Given(@"I drag ""(.*)"" and drop on to ""(.*)""")]
		[When(@"I drag ""(.*)"" and drop on to ""(.*)""")]
		[Then(@"I drag ""(.*)"" and drop on to ""(.*)""")]
		[Given(@"drag ""(.*)"" and drop on to ""(.*)""")]
		[When(@"drag ""(.*)"" and drop on to ""(.*)""")]
		[Then(@"drag ""(.*)"" and drop on to ""(.*)""")]
		public void DragAndDropOnTo(string elementToDrag, string elementToDrop)
		{
			UiAutomationService.DragAndDrop(elementToDrag, elementToDrop);
		}

		[Given(@"I drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"I drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"I drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Given(@"drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[When(@"drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		[Then(@"drag ""(.*)"" \(with the ""(.*)"" of ""(.*)""\) and drop on to ""(.*)"" \(with the ""(.*)"" of ""(.*)""\)")]
		public void DragAndDropOnTo(string elementToDrag, string elementToDragSelectionMethod, string elementToDragSelection, string elementToDrop, string elementToDropSelectionMethod, string elementToDropSelection)
		{
			UiAutomationService.DragAndDrop(elementToDrag, elementToDrop);
		}

		#endregion

		#region Enter <value> to <element>

		[Given(@"I enter ""(.*)"" to the ""(.*)""")]
		[When(@"I enter ""(.*)"" to the ""(.*)""")]
		[Then(@"I enter ""(.*)"" to the ""(.*)""")]
		[Given(@"Enter ""(.*)"" to the ""(.*)""")]
		[When(@"Enter ""(.*)"" to the ""(.*)""")]
		[Then(@"Enter ""(.*)"" to the ""(.*)""")]
		public void EnterToThe(string value, string elementKey)
		{
			UiAutomationService.EnterTextTo(elementKey, value);
		}

		[Given(@"I enter value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[When(@"I enter value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[Then(@"I enter value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[Given(@"Enter value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[When(@"Enter value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[Then(@"Enter value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		public void EnterToTheWithTheOf(string value, string elementKey, string selectionMethod, string selection)
		{
			UiAutomationService.EnterTextTo(elementKey, value, selectionMethod, selection);
		}

		#endregion

		#region Navigate to SUT

        [Given(@"I navigate to System Under Test")]
        [When(@"I navigate to System Under Test")]
        [Then(@"I navigate to System Under Test")]
        [Given(@"I navigate to SUT")]
        [When(@"I navigate to SUT")]
        [Then(@"I navigate to SUT")]
        [Given(@"Navigate to System Under Test")]
        [When(@"Navigate to System Under Test")]
        [Then(@"Navigate to System Under Test")]
        [Given(@"Navigate to SUT")]
        [When(@"Navigate to SUT")]
        [Then(@"Navigate to SUT")]
        public void NavigateToSut()
		{
			UiAutomationService.GotoUrl(UiAutomationService.SutUrl);
		}

        #endregion

        #region Navigate to a sub link under the SUT

        [Given(@"I navigate to  ""(.*)"" of System Under Test")]
        [When(@"I navigate to ""(.*)"" of System Under Test")]
        [Then(@"I navigate to ""(.*)"" of System Under Test")]
        [Given(@"I navigate to ""(.*)"" of SUT")]
        [When(@"I navigate to ""(.*)"" of SUT")]
        [Then(@"I navigate to ""(.*)"" of SUT")]
        [Given(@"Navigate to ""(.*)"" of System Under Test")]
        [When(@"Navigate to ""(.*)"" of System Under Test")]
        [Then(@"Navigate to ""(.*)"" of System Under Test")]
        [Given(@"Navigate to ""(.*)"" of SUT")]
        [When(@"Navigate to ""(.*)"" of SUT")]
        [Then(@"Navigate to ""(.*)"" of SUT")]
        public void NavigateToSubLinkUnderSut(string subURL)
        {
			UiAutomationService.GotoUrl(UiAutomationService.SutUrl + "/" + subURL);
        }

        #endregion

        #region Navigate to <URL>

        [Given(@"I navigate to ""(.*)""")]
		[When(@"I navigate to ""(.*)""")]
		[Then(@"I navigate to ""(.*)""")]
		[Given(@"Navigate to ""(.*)""")]
		[When(@"Navigate to ""(.*)""")]
		[Then(@"Navigate to ""(.*)""")]
		public void NavigateTo(string url)
		{
			UiAutomationService.GotoUrl(url);
		}

		[Given(@"I navigate to URL stored in ""(.*)""")]
		[When(@"I navigate to URL stored in ""(.*)""")]
		[Then(@"I navigate to URL stored in ""(.*)""")]
		[Given(@"Navigate to URL stored in ""(.*)""")]
		[When(@"Navigate to URL stored in ""(.*)""")]
		[Then(@"Navigate to URL stored in ""(.*)""")]
		public void NavigateToUrlStoredIn(string veriable)
		{
			if (DataShare.ContainsKey(veriable.ToUpper()))
				UiAutomationService.GotoUrl(DataShare[veriable.ToUpper()]);
			else
			{
				throw new Exception("Veriable \"" + veriable + "\" not found.");
			}
		}

        #endregion

        #region Tabs and Window Manipulations

        [Given(@"I switch to tab ""(.*)""")]
		[When(@"I switch to tab ""(.*)""")]
		[Then(@"I switch to tab ""(.*)""")]
		[Given(@"Switch to tab ""(.*)""")]
		[When(@"Switch to tab ""(.*)""")]
		[Then(@"Switch to tab ""(.*)""")]
		[Given(@"I switch to window ""(.*)""")]
		[When(@"I switch to window ""(.*)""")]
		[Then(@"I switch to window ""(.*)""")]
		[Given(@"Switch to window ""(.*)""")]
		[When(@"Switch to window ""(.*)""")]
		[Then(@"Switch to window ""(.*)""")]
		public void GivenISwitchToTab(string tabNumber)
		{
			int tab;
			if (int.TryParse(tabNumber, out tab))
			{
				if (tab >= 0)
				{
					UiAutomationService.SwitchToWindow(tab);
				}
			}
			else
			{
				throw new Exception("Tab or window number provided isn't valid");
			}
		}

		[Given(@"I close tab ""(.*)""")]
		[When(@"I close tab ""(.*)""")]
		[Then(@"I close tab ""(.*)""")]
		[Given(@"Close tab ""(.*)""")]
		[When(@"Close tab ""(.*)""")]
		[Then(@"Close tab ""(.*)""")]
		[Given(@"I close window ""(.*)""")]
		[When(@"I close window ""(.*)""")]
		[Then(@"I close window ""(.*)""")]
		[Given(@"Close window ""(.*)""")]
		[When(@"Close window ""(.*)""")]
		[Then(@"Close window ""(.*)""")]
		public void GivenICloseTab(string tabNumber)
		{
			int tab;
			if (int.TryParse(tabNumber, out tab))
			{
				if (tab >= 0)
				{
					UiAutomationService.CloseWindow(tab);
				}
			}
			else
			{
				throw new Exception("Tab or window number provided isn't valid");
			}
		}

		#endregion

		#region Table Manipulations

		[Given(@"Table ""(.*)"" has ""(.*)"" of rows")]
		[When(@"Table ""(.*)"" has ""(.*)"" of rows")]
		[Then(@"Table ""(.*)"" has ""(.*)"" of rows")]
		public void GivenTableHasOfRows(string elementKey, string numberOfRows)
		{
			int rowCount;
			if (int.TryParse(numberOfRows, out rowCount))
			{
				UiAutomationService.TableHasRowCountOf(elementKey, rowCount);
			}
			else
			{
				throw new Exception("Invalid row number.");
			}
		}

		[Given(@"Row in table ""(.*)"" has ""(.*)"" coloums")]
		[When(@"Row in table ""(.*)"" has ""(.*)"" coloums")]
		[Then(@"Row in table ""(.*)"" has ""(.*)"" coloums")]
		public void GivenRowInTableHasColoums(string elementKey, string numberOfColumns)
		{
			int columnCount;
			if (int.TryParse(numberOfColumns, out columnCount))
			{
				UiAutomationService.TableHasColumnCountOf(elementKey, columnCount);
			}
			else
			{
				throw new Exception("Invalid column number.");
			}
		}

		[Given(@"""(.*)""st row, ""(.*)""st column of table ""(.*)"" contains value ""(.*)""")]
		[Given(@"""(.*)""nd row, ""(.*)""nd column of table ""(.*)"" contains value ""(.*)""")]
		[Given(@"""(.*)""rd row, ""(.*)""rd column of table ""(.*)"" contains value ""(.*)""")]
		[Given(@"""(.*)""th row, ""(.*)""th column of table ""(.*)"" contains value ""(.*)""")]
		[When(@"""(.*)""st row, ""(.*)""st column of table ""(.*)"" contains value ""(.*)""")]
		[When(@"""(.*)""nd row, ""(.*)""nd column of table ""(.*)"" contains value ""(.*)""")]
		[When(@"""(.*)""rd row, ""(.*)""rd column of table ""(.*)"" contains value ""(.*)""")]
		[When(@"""(.*)""th row, ""(.*)""th column of table ""(.*)"" contains value ""(.*)""")]
		[Then(@"""(.*)""st row, ""(.*)""st column of table ""(.*)"" contains value ""(.*)""")]
		[Then(@"""(.*)""nd row, ""(.*)""nd column of table ""(.*)"" contains value ""(.*)""")]
		[Then(@"""(.*)""rd row, ""(.*)""rd column of table ""(.*)"" contains value ""(.*)""")]
		[Then(@"""(.*)""th row, ""(.*)""th column of table ""(.*)"" contains value ""(.*)""")]
		public void GivenStRowStColumnOfTableContainsValue(string rowNumber, string columnNumber, string elementKey, string value)
		{
			int row, col;
			if (int.TryParse(rowNumber, out row) && int.TryParse(columnNumber, out col))
			{
				UiAutomationService.ValueOfTableRowColEqualTo(elementKey, row, col, value);
			}
			else
			{
				throw new Exception("Invalid row or column number.");
			}
		}

		#endregion

		#region The <Element> is Visible

		[Given(@"""(.*)"" is visible")]
		[Then(@"""(.*)"" is visible")]
		[When(@"""(.*)"" is visible")]
		[Given(@"The ""(.*)"" is visible")]
		[Then(@"The ""(.*)"" is visible")]
		[When(@"The ""(.*)"" is visible")]
		[Given(@"I wait for the ""(.*)"" to appear")]
		[When(@"I wait for the ""(.*)"" to appear")]
		[Then(@"I wait for the ""(.*)"" to appear")]
		[Given(@"Wait for the ""(.*)"" to appear")]
		[When(@"Wait for the ""(.*)"" to appear")]
		[Then(@"Wait for the ""(.*)"" to appear")]
		public void TheElementIsVisible(string elementKey)
		{
			UiAutomationService.IsElementVisible(elementKey);
		}

		[Given(@"The ""(.*)"" with the ""(.*)"" of ""(.*)"" is shown")]
		[Then(@"The ""(.*)"" with the ""(.*)"" of ""(.*)"" is shown")]
		[When(@"The ""(.*)"" with the ""(.*)"" of ""(.*)"" is shown")]
		[Given(@"I wait for the ""(.*)"" with the ""(.*)"" of ""(.*)"" to show")]
		[When(@"I wait for the ""(.*)"" with the ""(.*)"" of ""(.*)"" to show")]
		[Then(@"I wait for the ""(.*)"" with the ""(.*)"" of ""(.*)"" to show")]
		[Given(@"""(.*)"" with the ""(.*)"" of ""(.*)"" is shown")]
		[Then(@"""(.*)"" with the ""(.*)"" of ""(.*)"" is shown")]
		[When(@"""(.*)"" with the ""(.*)"" of ""(.*)"" is shown")]
		[Given(@"Wait for the ""(.*)"" with the ""(.*)"" of ""(.*)"" to show")]
		[When(@"Wait for the ""(.*)"" with the ""(.*)"" of ""(.*)"" to show")]
		[Then(@"Wait for the ""(.*)"" with the ""(.*)"" of ""(.*)"" to show")]
		public void TheElementWithTheOfIsVisible(string elementKey, string selectionMethod, string selection)
		{
			UiAutomationService.IsElementVisible(elementKey, selectionMethod, selection);
		}

		#endregion

		#region Accept the confirmation

		[Given(@"I accept the confirmation")]
		[Then(@"I accept the confirmation")]
		[When(@"I accept the confirmation")]
		[Given(@"I accept the confirmation alert")]
		[Then(@"I accept the confirmation alert")]
		[When(@"I accept the confirmation alert")]
		[Given(@"Accept the confirmation")]
		[Then(@"Accept the confirmation")]
		[When(@"Accept the confirmation")]
		[Given(@"Accept the confirmation alert")]
		[Then(@"Accept the confirmation alert")]
		[When(@"Accept the confirmation alert")]
		public void AcceptTheConfirmation()
		{
			UiAutomationService.AcceptTheConfirmation();
		}

		#endregion

		#region Read URL/<element> and store in data share

		[Given(@"I read the URL and store in ""(.*)"" variable")]
		[When(@"I read the URL and store in ""(.*)"" variable")]
		[Then(@"I read the URL and store in ""(.*)"" variable")]
		[Given(@"Read the URL and store in ""(.*)"" variable")]
		[When(@"Read the URL and store in ""(.*)"" variable")]
		[Then(@"Read the URL and store in ""(.*)"" variable")]
		public void ReadTheUrlAndStoreIn(string veriable)
		{
			if (DataShare.ContainsKey(veriable.ToUpper()))
				DataShare[veriable.ToUpper()] = UiAutomationService.ReadUrl();
			else
				DataShare.Add(veriable.ToUpper(), UiAutomationService.ReadUrl());
		}

		[Given(@"I read the content of element ""(.*)"" and store in variable ""(.*)""")]
		[When(@"I read the content of element ""(.*)"" and store in variable ""(.*)""")]
		[Then(@"I read the content of element ""(.*)"" and store in variable ""(.*)""")]
		[Given(@"Read the content of element ""(.*)"" and store in variable ""(.*)""")]
		[When(@"Read the content of element ""(.*)"" and store in variable ""(.*)""")]
		[Then(@"Read the content of element ""(.*)"" and store in variable ""(.*)""")]
		public void ReadTheContentOfElementAndStoreInVeriable(string elementKey, string veriable)
		{
			var elementContent = UiAutomationService.GetElementText(elementKey);
			if (DataShare.ContainsKey(veriable.ToUpper()))
				DataShare[veriable.ToUpper()] = elementContent;
			else
				DataShare.Add(veriable.ToUpper(), elementContent);
		}

		[Given(@"I read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[When(@"I read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[Then(@"I read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[Given(@"Read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[When(@"Read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[Then(@"Read the ""(.*)""st element of the URL path and store in ""(.*)"" variable")]
		[Given(@"I read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[When(@"I read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[Then(@"I read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[Given(@"Read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[When(@"Read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[Then(@"Read the ""(.*)""nd element of the URL path and store in ""(.*)"" variable")]
		[Given(@"I read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[When(@"I read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[Then(@"I read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[Given(@"Read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[When(@"Read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[Then(@"Read the ""(.*)""rd element of the URL path and store in ""(.*)"" variable")]
		[Given(@"I read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		[When(@"I read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		[Then(@"I read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		[Given(@"Read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		[When(@"Read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		[Then(@"Read the ""(.*)""th element of the URL path and store in ""(.*)"" variable")]
		public void ReadTheElementOfTheUrlPathAndStoreInVeriable(string index, string veriable)
		{
			var urlTosplit = UiAutomationService.ReadUrl().Replace("http://", "");
			var urlArray = urlTosplit.Split('/');
			int elementInex;
			if (!int.TryParse(index, out elementInex)) throw new Exception("\"" + index + "\" is not a valid integer.");
			var element = urlArray[elementInex];
			if (DataShare.ContainsKey(veriable.ToUpper()))
				DataShare[veriable.ToUpper()] = element;
			else
				DataShare.Add(veriable.ToUpper(), element);
		}

        #endregion

        #region variable manipulation

        [Given(@"I enter value of variable ""(.*)"" to the ""(.*)""")]
		[When(@"I enter value of variable ""(.*)"" to the ""(.*)""")]
		[Then(@"I enter value of variable ""(.*)"" to the ""(.*)""")]
		[Given(@"Enter value of variable ""(.*)"" to the ""(.*)""")]
		[When(@"Enter value of variable ""(.*)"" to the ""(.*)""")]
		[Then(@"Enter value of variable ""(.*)"" to the ""(.*)""")]
		public void EnterContentOfVeriableToThe(string veriable, string elementKey)
		{
			if (!DataShare.ContainsKey(veriable.ToUpper())) throw new Exception("Veriable \"" + veriable + "\" is not defined.");
			UiAutomationService.EnterTextTo(elementKey, DataShare[veriable.ToUpper()]);
		}

		[Given(@"I enter variable value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[When(@"I enter variable value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[Then(@"I enter variable value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[Given(@"Enter variable value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[When(@"Enter variable value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		[Then(@"Enter variable value ""(.*)"" to the ""(.*)"" with the ""(.*)"" of ""(.*)""")]
		public void EnterContentOfVeriableToTheWithTheOf(string veriable, string elementKey, string selectionMethod, string selection)
		{
			if (!DataShare.ContainsKey(veriable.ToUpper())) throw new Exception("Veriable \"" + veriable + "\" is not defined.");
			UiAutomationService.EnterTextTo(elementKey, DataShare[veriable.ToUpper()], selectionMethod, selection);
		}

		[Given(@"The value of variable ""(.*)"" is equal to ""(.*)""")]
		[When(@"The value of variable ""(.*)"" is equal to ""(.*)""")]
		[Then(@"The value of variable ""(.*)"" is equal to ""(.*)""")]
		[Given(@"Value of variable ""(.*)"" is equal to ""(.*)""")]
		[When(@"Value of variable ""(.*)"" is equal to ""(.*)""")]
		[Then(@"Value of variable ""(.*)"" is equal to ""(.*)""")]
		public void ValueOfVeriableIsEqualTo(string veriable, string value)
		{
			if (!DataShare.ContainsKey(veriable.ToUpper())) throw new Exception("Veriable \"" + veriable + "\" is not defined.");
			UiAutomationService.AreValuesEqual(DataShare[veriable.ToUpper()], value);
		}

        #endregion

        #region Frame manipulation 

        [Given(@"I switched to iframe with the ""(.*)"" of ""(.*)""")]
        [When(@"I switched to iframe with the ""(.*)"" of ""(.*)""")]
        [Then(@"I switched to iframe with the ""(.*)"" of ""(.*)""")]
        [Given(@"switched to iframe with the ""(.*)"" of ""(.*)""")]
        [When(@"switched to iframe with the ""(.*)"" of ""(.*)""")]
        [Then(@"switched to iframe with the ""(.*)"" of ""(.*)""")]
        [Given(@"I switched to frame with the ""(.*)"" of ""(.*)""")]
        [When(@"I switched to frame with the ""(.*)"" of ""(.*)""")]
        [Then(@"I switched to frame with the ""(.*)"" of ""(.*)""")]
        [Given(@"switched to frame with the ""(.*)"" of ""(.*)""")]
        [When(@"switched to frame with the ""(.*)"" of ""(.*)""")]
        [Then(@"switched to frame with the ""(.*)"" of ""(.*)""")]
        public void switchedToFrame(string selectionMethod, string selection)
        {
            UiAutomationService.switchToFrame(selectionMethod, selection);
        }

        [Given(@"I switched to default content")]
        [When(@"I switched to default content")]
        [Then(@"I switched to default content")]
        [Given(@"switched to default content")]
        [When(@"switched to default content")]
        [Then(@"switched to default content")]
        public void SwitchedToDefaultContent()
        {
            UiAutomationService.switchToDefaultContent();
        }

        #endregion

        #endregion
    }
}
