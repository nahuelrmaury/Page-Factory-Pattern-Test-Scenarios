# Page Factory Pattern Test Scenarios

In this task, we will implement three test scenarios using the Page Factory design pattern. The Page Factory pattern helps in creating a more maintainable and readable test automation framework by separating the page objects from the test code.

## Table of Contents
- [Introduction](#introduction)
- [Scenario 1](#scenario-1)
- [Scenario 2](#scenario-2)
- [Scenario 3](#scenario-3)
- [Conclusion](#conclusion)

## Introduction

We will automate interactions with a web application by implementing the following scenarios using the Page Factory pattern. The application involves customer login, product selection, registration, and order placement.

## Scenario 1

### Steps:
1. Open the main page.
2. Open the Customer Login page.
3. Login with valid user credentials.
4. Open the 'Watches' subcategory using the navigation menu.
5. Add the 'Dash Digital Watch' to the cart using the 'Add to Cart Button.'
6. Proceed to Checkout.
7. Fill in a new address with specific data (as shown in the picture).
   - The phone number should be a random number.
   - Create a new address even if another one already exists.
8. Place the order.
9. Save information about the order number and click 'Continue Shopping.'
10. Open the 'My Account' page.
11. Open 'My Orders.'
12. Find the created order by the saved order number and open it.
13. Check that the highlighted data is correct.

## Scenario 2

### Steps:
1. Open the main page.
2. Open the Registration page.
3. Fill in all fields except 'Email.'
4. Press 'Create an Account.'
5. Check that an error message 'This is a required field' appears.

## Scenario 3

### Steps:
1. Open the main page.
2. Open the Customer Login page.
3. Login with valid user credentials.
4. Press the 'Gear' category button.
5. Open the 'Bags' category.
6. Add the first two products to the cart using the 'Add to Cart' button.
7. Open the third product.
8. Press 'Add to Cart.'
9. Check that the cart icon displays the correct number.

## Conclusion

This README outlines the three test scenarios that will be implemented using the Page Factory pattern. Each scenario involves interacting with the web application's pages and validating specific actions and results. The use of the Page Factory pattern ensures maintainable and organized test automation code.
