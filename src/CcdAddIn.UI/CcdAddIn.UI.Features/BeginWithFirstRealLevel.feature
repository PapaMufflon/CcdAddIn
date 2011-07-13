Feature: Begin with first real level
  In order to begin the path of enlightenment
  As a clean code developer
  I want to master the black level

Scenario: Correct start
  Given I start the addin for the first time
  When I read the main text
  Then it should be a warm welcome message

Scenario: No retrospective at black level
  Given I am at the black level
  When I search for a retro-button
  Then I should not find one

Scenario: Browse to red level
  Given I am at the black level
  When I browse to the next one
  Then I should see the red level
