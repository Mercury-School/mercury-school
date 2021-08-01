using MercurySchool.Api.Models.Filters;
using System;
using TechTalk.SpecFlow;

namespace MercurySchool.APi.UnitTests.Steps
{
    [Binding]
    public class PaginationFilterSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly PaginationFilter _paginationFilter = new();

        public PaginationFilterSteps(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;

        [Given(@"I have a pagination filter")]
        public void GivenIHaveAPaginationFilter()
        {
            _scenarioContext.Pending();
        }
        
        [Given(@"I want the first page")]
        public void GivenIWantTheFirstPage()
        {
            _scenarioContext.Pending();
        }
        
        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int expectedPageValue)
        {
            _scenarioContext.Pending();
        }
    }
}
