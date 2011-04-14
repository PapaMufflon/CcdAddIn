Feature: Retrospective
  In order to reflect my day
  As a clean code developer
  I want to do a retrospective

Scenario: Make a retrospective
  Given I am at a non-black level
  When I click on retrospective
  Then I should be able to evaluate the principles and practices
  And the retrospective-button should be invisible

Scenario: See a feedback
  Given I am at a non-black level
  When I finish my retrospective
  Then I should see an advice reflecting my performance

Scenario: No suggestion to go to the next level
  Given I am at a non-black level
  And I finish my retrospective with no suggestion to advance to the next level
  When I accept that
  Then I should stay at the current level
  And the retrospective-button should be visible again

Scenario: Finish a retrospective
  Given I am at a non-black level
  And I make a retrospective
  When I finish the retrospective
  Then I should not be able to evaluate the principles and practices

Scenario: Keep the current level
  Given I am at a non-black level
  And I finish my retrospective with a suggestion to advance to the next level
  When I deny to advance
  Then I should stay at the current level

Scenario: Advance to the next level
  Given I am at a non-black level
  And I finish my retrospective with a suggestion to advance to the next level
  When I accept to advance to the next level
  Then I should be at the next level
  
Scenario: Advance to the next level on my own
  Given I am at a non-black level
  And I finish my retrospective with no suggestion to advance to the next level
  When I actively wish to advance to the next level
  Then I should be at the next level