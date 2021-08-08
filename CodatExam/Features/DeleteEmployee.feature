Feature: DeleteEmployee


Scenario: TC_Delete_01- Delete a row from employee timesheet
Given I am in Codat main page
When I delete the row "1" from the table
Then the employee details are not in the table
