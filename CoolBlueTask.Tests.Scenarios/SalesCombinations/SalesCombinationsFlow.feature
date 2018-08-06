Feature: SalesCombinationsFlow
	In order to sell more goods
	As a shop owner
	Jeff should be able to combine related products into propositions for customers

Scenario: Get products sales combinations
	Given Jeff has related products in his store
	| product  |
	| Laptop   |
	| Mouse    |
	| Headset  |
	| Keyboard |
	When He defines combinations of these products
	| Main product | Related product 1 | Related product 2 |
	| Laptop       | Mouse             | Keyboard          |
	| Laptop       | Headset           |                   |
	And customer observes 'Laptop' product
	Then customer sees defined by Jeff products suggestions for 'Laptop'

Scenario Outline: Create invalid Sales Combination
	Given Jeff has 'Pen' and 'Paper' products in his store
	When he tries to create a sale combination by entering <Wrong combination input>
	Then Jeff should see corresponding errors

Examples: Validation violation cases
	| Wrong combination input                    |
	| Empty input                                |
	| Main product missed                        |
	| Not existing main product                  |
	| Related products missed                    |
	| One of the related products does not exist |