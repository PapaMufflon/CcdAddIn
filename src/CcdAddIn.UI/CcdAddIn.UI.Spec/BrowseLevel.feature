Feature: Browsing CCD levels
	In order to know the different CCD levels
	As a clean code developer
	I want to browse through each of the CCD levels

Scenario: Browse to red level
	Given I am at the black level
	When I browse to the next one
	Then I should see the red level
