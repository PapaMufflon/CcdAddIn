Feature: Restart
	In order to keep progress of my path to enlightenment
	As a clean code developer
	I want to continue from where I stopped last time

Scenario: Continue from red level
	Given I am at the red level
	When restarting the application
	Then I am still at the red level
