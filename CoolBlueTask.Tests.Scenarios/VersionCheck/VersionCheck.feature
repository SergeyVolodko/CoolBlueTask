Feature: VersionCheck
	In order to manage the versions of API
	As a developer
	I want to be able to maintain and manage API versions

Scenario: Check api version
	When I check the API version
	Then I should see a valid response with the version number
