Feature: ProductAdministration
	In order to manage products in the shop
	As a shop owner
	Jeff should be able to maintain shops product list

Scenario: Add new product
	Given Jeff has no products in his shop
	When he adds a new product 'The book' to the store
	Then he sees the new product in products list
	And product card has all provided details of 'The book'

Scenario: Edit product
	Given Jeff has product 'Teapot' in his shop
	When he changes all its details
	Then he sees the updated product card

Scenario Outline: Add invalid product
	When Jeff tries to add a new product with <Invalid inputs>
	Then he should see the list of respective errors

Examples: Validation violation cases
	| Invalid inputs                |
	| Empty input                   |
	| Invalid values for all fields |

Scenario Outline: Update product with invalid data
	Given Jeff has product 'CD-player' in his shop
	When Jeff tries to update this product with <Invalid inputs>
	Then he should see the list of respective errors

Examples: Validation violation cases
	| Invalid inputs                |
	| Empty input                   |
	| Invalid values for all fields |