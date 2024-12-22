Feature: Update category

Scenario: User updates an existing category
Given the API is running
And category with the name Savings exists in the database
When the user sends a PUT request to /api/categories with the following data
    | Name        |
    | Investments |
Then the response status code should be 200
And name of the category is updated in database