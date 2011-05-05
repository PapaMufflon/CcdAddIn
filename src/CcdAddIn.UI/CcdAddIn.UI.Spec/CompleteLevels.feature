Feature: All levels
  In order to learn everything
  As a clean code developer
  I want to reach all levels

Scenario: Browse through the colored levels
  Given I start at the red level
  When I browse through all colored levels
  Then I will end at the blue level

Scenario: Reach the white level
  Given I start at the red level
  When I browse through all colored levels
  And I advance to the next level
  Then I should be at the white level
  And I should not have the possibility to do a retrospective
  And I should have the possibility to begin again with the red level

Scenario: Make a grand cycle
  Given I start at the red level
  When I browse through all the levels
  Then I should end at the red level again