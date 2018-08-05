Feature: ProductBrowsing
	In order to buy more things from web shop
	As a consumer
	I should find products easily

Scenario: Search product by name
	Given Jeff has following products in his store
	| product |
	| Laptop  |
	| Tablet  |
	| Table   |
	When I search for 'Table'
	Then I see following products
	| product |
	| Tablet  |
	| Table   |