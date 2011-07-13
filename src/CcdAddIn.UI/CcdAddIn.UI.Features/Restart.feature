Feature: Restart
  In order to keep progress of my path to enlightenment
  As a clean code developer
  I want to continue from where I stopped last time

Scenario: Continue from red level
  Given I am at the red level
  When restarting the application
  Then I am still at the red level

Scenario: Keep past retrospectives
  Given I am at the red level
  And I have done nearly enough retrospectives to advance a level
  And doing the next to last retrospective for advancing a level
  When restarting the application
  And doing the last retrospective for advancing a level
  Then I should get an advice to advance to the next level