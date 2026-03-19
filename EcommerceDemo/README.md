# EcommerceDemo — Event-Driven Microservices with RabbitMQ

## 🧠 Purpose

This project demonstrates a **real-world microservices architecture** using RabbitMQ:

* Product Service → sends product data
* Cart Service → processes and builds cart
* Payment Service → final processing

It builds on the WhatsAppDemo and introduces:

* Multi-step processing (AP1, AP2, AP3)
* Message chaining between services
* Independent service architecture

---

## 🏗️ Architecture

```
ProductService 
   → (product-queue) 
CartService 
   → (payment-queue) 
PaymentService
```

---

## 🔁 End-to-End Flow

```
1. ProductService sends product
2. RabbitMQ stores message
3. CartService consumes message
4. AP1 → Validate
5. AP2 → Add to Cart
6. AP3 → Calculate Total
7. Cart sent to PaymentService
8. PaymentService processes payment
```

---

## 🧱 Services Explained

---

### 🟢 ProductService

Responsibility:

* Acts as **data producer**
* Sends product information

Example:

```json
{
  "productId": 1,
  "name": "Laptop",
  "price": 50000
}
```

---

### 🟡 CartService (Core Logic)

Handles business processing:

#### AP1 — Validation

* Ensures product data is valid

#### AP2 — Cart Creation

* Converts product → cart item
* Adds default quantity = 1

#### AP3 — Calculation

```csharp
Total = Price × Quantity
```

Example:

```
50000 × 1 = 50000
```

---

### 🔴 PaymentService

* Receives final cart
* Processes payment
* Logs result

---

## 🔄 Message Lifecycle (Same as WhatsApp)

```
READY → UNACK → ACK → REMOVED
```

---

## 🧪 How to Run

### 1. Start RabbitMQ

```
docker run -d -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

---

### 2. Run All Services

* ProductService
* CartService
* PaymentService

---

### 3. Send Product

POST:

```
http://localhost:{port}/api/product
```

Body:

```json
{
  "productId": 1,
  "name": "Laptop",
  "price": 50000
}
```

---

## 🖥️ Output

### ProductService

```
Sent Product: Laptop
```

---

### CartService

```
[UNACK START]
AP1: Validated
AP2: Adding to cart
AP3: Calculating total
[ACK SENT]
Cart sent to PaymentService
```

---

### PaymentService

```
Payment Done: 50000
```

---

## 🧠 Key Concepts

---

### ❗ No Shared Database

Each service:

* Has its own data
* Stores its own copy

---

### ❗ Data is NOT shared — it is COPIED

```
Product → sent → Cart → stored again
```

---

### ❗ RabbitMQ Role

```
Acts as message broker (middle layer)
```

It:

* Stores messages temporarily
* Delivers them reliably

---

## ⚠️ Important Design Notes

| Concept            | Explanation                                  |
| ------------------ | -------------------------------------------- |
| Loose Coupling     | Services don’t depend on each other          |
| Asynchronous       | No waiting between services                  |
| Idempotency Needed | Messages can be retried                      |
| Snapshot Data      | Cart stores product state at time of message |

---

## 🧠 Real-World Insight

If product price changes later:

* Cart still has old price (snapshot)
* Payment can revalidate if needed

---

## 🚀 Next Improvements

* Add quantity support
* Add retry mechanism
* Add Dead Letter Queue (DLQ)
* Add persistent queues
* Add database instead of in-memory

---

## 🎯 Final Understanding

```
Microservices do NOT share objects or DB
They communicate via messages (data snapshots)
```
