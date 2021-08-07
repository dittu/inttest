Feature: CreateNewEmployee

Background: 
Given I'm in Codat main page

Scenario: TC_New_01- Empty fields validations for emplyee Id 
When I click on 'Create New'
And I enter " " in "HourlyRate"
And I click on 'Add New Row'
And I click on 'Save'
Then I should see validations for fields
| key        | value                                                                           |
| EmployeeID | Please provide an employee ID. IDs are CDT followed by 4 digits (e.g. CTD9001). |
| HourlyRate | The field Hourly Rate ($) must be a number.                                     |
| Day        | The Day field is required.                                                      |
| Hours      | The Hours field is required.                                                    |
| Minutes    | The Minutes field is required.                                                  |


Scenario: TC_New_06- New employee timesheet creation
When I click on 'Create New'
And I enter valid details for new employee creation
And I click on 'Save'
Then I should be navigated to page with title containing "Details"
And created employee details should be same as entered
When I click on 'Back To List'
Then new employee should be seen in the table