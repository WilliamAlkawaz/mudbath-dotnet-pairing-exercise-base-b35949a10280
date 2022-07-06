# Mudbath Mocha

[Interviewer Project Setup]

## Run Commands

`dotnet run`

Have included `.vscode` files as part of solution to enable local running and execution.

## Background

We like coffee.

So we built an app to fetch coffee for people from our favourite barista.

The app keeps track of coffee ordered; what the balance is for each user; what users have paid for already; and what is still owed.

## Data

We've got the following data

- `data/prices.json` - provided by our barista. Has details of what beverages are available, and what their prices are.
- `data/orders.json` - list of beverages ordered by users of the app.
- `data/payments.json` - list of payments made by users paying for items they have purchased.

## Requirements

### Requirement 1

In `ProcessService.cs` print all orders to the console.

### Requirement 2

Please count by user how many orders they have had.  Expected output:
```JSON
    [
        {
            "name": "Ellis",
            "count": 5
        }
    ]
```

### Requirement 3

Use the data from `orders.json` and `prices.json` to reconcile a total cost.  Expected output:

```JSON
    [
        {
            "name": "Ellis",
            "count": 5,
            "balance": 80.50
        }
    ]
```

### Requirement 4

Write a unit test to confirm the output from Requirement 3
- This step can be skipped if user is unfamilar with testing.

### Requirement 5

Reconcile the `payments.json` file with the output from Requirement 3.  Expected output:
```JSON
    [
        {
            "user": "coach",
            "orderTotal": 8.00,
            "paymentTotal": 2.50,
            "balance": 5.50
        }
    ]
```

Organize this functionality as you best see fit.

### Requirement 6

Modify `orders.json` so that the there is an unsupported order type.
- Modify the code to support this change
- What would be the best way to handle this in production
- Discuss

This can be easily handled by throwing an exception and wrapping the code in try and catch blocks. We may add logging as well if required. 
