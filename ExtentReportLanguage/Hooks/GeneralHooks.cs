using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace ExtentReportLanguage.Hooks
{
    [Binding]
    public sealed class GeneralHooks
    {
        private static ScenarioContext _scenarioContext;
        private static FeatureContext _featureContext;
        private static ExtentReports _extentReports;
        private static ExtentHtmlReporter _extentHtmlReporter;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extentHtmlReporter = new ExtentHtmlReporter(@"C:\Data\log\");
            _extentHtmlReporter.Config.ReportName = "testreport.html";
            _extentHtmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            //_extentHtmlReporter.Config.Encoding = UTF8Encoding.
            _extentReports = new ExtentReports
            {
                GherkinDialect = "fr",
            };
            _extentReports.AttachReporter(_extentHtmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeatureStart(FeatureContext featureContext)
        {
            if (null != featureContext)
            {
                _feature = _extentReports.CreateTest(new GherkinKeyword("Fonctionnalité"),featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
            }
        }

        [BeforeScenario]
        public static void BeforeScenarioStart(ScenarioContext scenarioContext)
        {
            if (null != scenarioContext)
            {
                _scenarioContext = scenarioContext;
                _scenario = _feature.CreateNode(new GherkinKeyword("Scénario"),scenarioContext.ScenarioInfo.Title, scenarioContext.ScenarioInfo.Description);
            }
        }

        [AfterStep]
        public void AfterEachStep()
        {
            // logic -> Given ,when or then
            // add the node

            ScenarioBlock scenarioBlock = _scenarioContext.CurrentScenarioBlock;

            //switch case

            switch (scenarioBlock)
            {
                case ScenarioBlock.Given:
                    /*  if (_scenarioContext.TestError != null)
                      {
                          _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message + "\n" + _scenarioContext.TestError.StackTrace);
                      }
                      else
                      {
                          _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
                      }*/
                    CreateNode<Given>();
                    break;
                case ScenarioBlock.When:
                    /*   if (_scenarioContext.TestError != null)
                       {
                           _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message + "\n" + _scenarioContext.TestError.StackTrace);
                       }
                       else
                       {
                           _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
                       }
   */
                    CreateNode<When>();
                    break;
                case ScenarioBlock.Then:
                    /* if (_scenarioContext.TestError != null)
                     {
                         _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message+ "\n" + _scenarioContext.TestError.StackTrace);
                     }
                     else
                     {
                         _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
                     }
 */
                    CreateNode<Then>();
                    break;
                default:
                    /*if (_scenarioContext.TestError != null)
                    {
                        _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message + "\n" + _scenarioContext.TestError.StackTrace);
                    }
                    else
                    {
                        _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
                    }*/
                    CreateNode<And>();

                    break;
            }

        }

        public void CreateNode<T>() where T : IGherkinFormatterModel
        {
            if (_scenarioContext.TestError != null)
            {
                string name = @"C:\Data\log\" + _scenarioContext.ScenarioInfo.Title.Replace(" ", "") + ".jpeg";
                _scenario.CreateNode(new GherkinKeyword("Etant donnée ") ,_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message + "\n" + _scenarioContext.TestError.StackTrace)
                    .AddScreenCaptureFromPath(name);
            }
            else
            {
                _scenario.CreateNode(new GherkinKeyword("Etant donnée "),_scenarioContext.StepContext.StepInfo.Text).Pass("");
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extentReports.Flush();
        }
    }
}
