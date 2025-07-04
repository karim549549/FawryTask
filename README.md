﻿# Fawry Quantum Internship Challenge – E-Commerce System

## Project Overview

This is a C# console-based e-commerce system developed as part of the **Fawry Rise Full Stack Internship Challenge**. It simulates a checkout process in an online shopping system, handling product stock, expiration, shipping logic, and balance validation, while ensuring thread safety.

---


## Design Patterns and Scalability Notes

This project is focused on clean, testable, object-oriented code with practical concurrency handling.

While the current system is kept intentionally simple to match the scope of the internship task, it naturally lends itself to the use of more advanced design patterns at a production level. For example:

- **Factory Pattern**: To encapsulate the creation of product types and avoid hard-coded `new` instantiations in the application layer.
- **Strategy Pattern**: To allow dynamic switching between different shipping calculation strategies (e.g., standard, express, free), or even for expiration policy logic.
- **Repository Pattern**: If this project were extended to use EF Core for full database interaction, repositories would help encapsulate persistence logic.

These patterns were not applied in this version to avoid overengineering the solution and to keep it aligned with the spirit of the challenge — focusing on clarity, correctness, and core logic.


## How to Run

### Prerequisites
- .NET 9 SDK installed
- Visual Studio 2022+ or any code editor with .NET support

### Run via Visual Studio
1. Open the solution (`fawryTask.sln`) in Visual Studio.
2. Set the startup project to the console app.
3. Press `Ctrl + F5` to run without debugging.

### Run via Terminal
```bash
dotnet build
dotnet run
```

---

## Features

- **Object-oriented design:** Abstraction, encapsulation, inheritance, and polymorphism.
- **Thread-safe operations** using `lock` in:
  - Product quantity (`TryReserveQuantity`)
  - Customer balance (`TryDeduct`)
- **Product types:**
  - Cheese, Biscuits: Perishable and shippable
  - TV: Shippable, not perishable
  - MobileScratchCard: Neither perishable nor shippable
- **IShippable interface** for shipping service
- **Checkout process** includes:
  - Expiry validation
  - Quantity availability check
  - Balance validation
  - Receipt and shipping output

---

## Test Cases and Outputs

### Example 1: Successful Checkout

**Setup:**
- Customer balance: 1000
- Cart: 2x Cheese (100), 1x Biscuits (150)
- Total: 350 + shipping 30 = 380

**Expected Output:**
```
** Shipment notice **
2x Cheese        400g
1x Biscuits      700g
Total package weight 1.1kg

** Checkout receipt **
2x Cheese        200
1x Biscuits      150
----------------------
Subtotal         350
Shipping         30
Amount           380
Remaining Balance: 620
```

---

### Example 2: Quantity Lock Under Concurrency

**Setup:**
- Product stock: 2x Cheese
- Two customers each try to buy 2x Cheese at the same time

**Expected Output (one will succeed, one will fail):**
```
Customer A checked out successfully.
Customer B failed: Product 'Cheese' is out of stock.
```

---

### Example 3: Balance Lock Under Concurrency

**Setup:**
- Customer balance: 1000
- Two checkouts attempt to buy 1x TV each at 600

**Expected Output:**
```
Cart C checked out successfully.
Cart D failed: Insufficient balance.
```

---

## Corner Cases Handled

| Case                              | Behavior                                         |
|------------------------------------|--------------------------------------------------|
| Cart is empty                     | Throws: Cart is empty.                           |
| Product is expired                | Throws: Product 'X' is expired.                  |
| Insufficient stock                | Checkout fails: Product 'X' is out of stock.     |
| Insufficient customer balance     | Checkout fails: Insufficient balance.            |
| Multiple concurrent checkouts     | Protected with locking on balance and quantity   |
| Product quantity/balance race     | Solved using synchronized access via `lock`      |
| Products with different behaviors | Handled via base class and interface polymorphism|

---
