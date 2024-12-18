Feature: Add category

Scenario: User adds a valid category
Given the API is running
When the user sends a POST request to /api/categories with the following data
    | Name         |
    | TestCategory |
Then the response status code should be 200
#And the category is created in database