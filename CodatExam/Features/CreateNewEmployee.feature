Feature: CreateNewEmployee

Background: 
Given I am in Codat main page

Scenario: TC_New_01, TC_New_02- Empty fields validations for emplyee Id 
When I click on 'CreateNew'
And I enter " " in "HourlyRate"
And I click on 'AddNewRow'
And I click on 'Save'
Then I should see validations for fields
| key        | value                                                                           |
| EmployeeID | Please provide an employee ID. IDs are CDT followed by 4 digits (e.g. CTD9001). |
| HourlyRate | The field Hourly Rate ($) must be a number.                                     |
| Day        | The Day field is required.                                                      |
| Hours      | The Hours field is required.                                                    |
| Minutes    | The Minutes field is required.                                                  |

Scenario Outline: TC_New_03 Employee ID validation - Part 1
When I click on 'CreateNew'
And I enter "<value>" in "<FieldName>"
Then I should see validations for fields
| key        | Value                                                                           |
| EmployeeID | Please provide an employee ID. IDs are CDT followed by 4 digits (e.g. CTD9001). |
Examples: 
| FieldName  | value   |
| EmployeeId | DDT123  |
| EmployeeId | CDT#123 |
| EmployeeId | 123456  |

Scenario Outline: TC_New_03 Employee ID validation - Part 2
When I click on 'CreateNew'
And I enter "invalid" details for new employee creation
And I click on 'Save'
Then I should see validations for fields
| key        | Value                                                                           |
| EmployeeID | Please provide an employee ID. IDs are CDT followed by 4 digits (e.g. CTD9001). |
Examples: 
| FieldName  | value  |
| EmployeeId | DDT123 |


Scenario Outline: TC_New_04 Hourly Rate validation 
When I click on 'CreateNew'
And I enter "<value>" in "<FieldName>"
And I click on 'Save'
Then I should see validations for fields
| key        | Value                                       |
| HourlyRate | The field Hourly Rate ($) must be a number. |
Examples: 
| FieldName  | value |
| HourlyRate | abc   |
| HourlyRate | -234. |
| HourlyRate | ab234 |

Scenario: TC_New_06- New employee timesheet creation(End-end)
When I click on 'CreateNew'
And I enter "valid" details for new employee creation
And I click on 'Save'
Then I should be navigated to page with title containing "Details"
And verify employee details same as entered
When I click on 'BackToList'
Then new employee details displayed in the table

Scenario: TC_New_07- Duplicate employee Id
When I click on 'CreateNew'
And I enter "valid" details for new employee creation
And I click on 'Save'
Then I should be navigated to page with title containing "Details"
When I use same employee Id to create another payer
Then I should see validations for fields
| key        | Value                                 |
| EmployeeID | Please provide an unique employee Id. |