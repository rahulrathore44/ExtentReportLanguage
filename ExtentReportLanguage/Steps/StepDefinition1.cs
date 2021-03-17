using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace ExtentReportLanguage.Steps
{
    [Binding]
    public sealed class StepDefinition1
    {
        [Given(@"le nombre est (.*)")]
        public void SoitTheNumberIs(int p0)
        {
            Console.WriteLine(p0);
        }

        [When(@"Le deuxième nombre est (.*)")]
        public void QuandLeDeuxiemeNombreEst(int p0)
        {
            Console.WriteLine(p0);
        }

        [Then(@"La somme est de (.*)")]
        public void AlorsLaSommeEstDe(int p0)
        {
            Console.WriteLine(p0);
        }


    }
}
