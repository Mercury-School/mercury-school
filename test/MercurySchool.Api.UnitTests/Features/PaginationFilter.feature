Feature: PaginationFilter
	As a user
	I want lists to have pagination filters
	So that data is more easily navigated

@Unit
Scenario: Pagination filter defined
	Given I have a pagination filter
	And I want the first page
	Then the result should be 1
	