# WhatsAppDemo — RabbitMQ Messaging (Foundations)

## 🧠 Purpose

This project demonstrates **how two services communicate using RabbitMQ**, similar to how a message is sent and received in WhatsApp.

It helps you understand:

* Message flow (Sender → Queue → Receiver)
* Serialization (Object → JSON → byte[])
* RabbitMQ lifecycle (Ready → Unacked → Ack)
* Difference between HTTP and Message-based communication

---

## 🏗️ Architecture

```
SenderService → RabbitMQ (chat-queue) → ReceiverService
```

---

## ⚙️ How It Works

### 1. SenderService (Producer)

* Accepts message via API (Swagger)
* Converts object → JSON → byte[]
* Sends message to RabbitMQ queue

### 2. RabbitMQ

* Stores message in queue
* Delivers message to consumer

### 3. ReceiverService (Consumer)

* Listens continuously (no API required)
* Receives message (byte[])
* Converts → object
* Prints message in console

---

## 🔁 Message Lifecycle

```
1. Message sent → READY
2. Consumer picks → UNACKED
3. Processing happens
4. ACK sent → message removed
```

---

## 🧪 How to Run

### 1. Start RabbitMQ

```
docker run -d -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

UI:

```
http://localhost:15672
guest / guest
```

---

### 2. Run Services

* Run SenderService
* Run ReceiverService

---

### 3. Send Message

POST:

```
http://localhost:{port}/api/chat
```

Body:

```json
{
  "text": "Hello bro 🔥"
}
```

---

## 🖥️ Output

Receiver console:

```
Received: Hello bro 🔥
```

---

## 🔍 Debug Concepts

| Concept | Meaning                       |
| ------- | ----------------------------- |
| READY   | Message waiting               |
| UNACKED | Message being processed       |
| ACK     | Message completed and removed |

---

## ⚠️ Important Notes

* No shared memory between services
* No direct API call between services
* Only **data transfer via queue**

---

## 🎯 Key Learning

```
RabbitMQ = Transport system for data
NOT database
NOT shared memory
```

---

## 🚀 Next Steps

* Add retry logic
* Handle failures
* Introduce Dead Letter Queue (DLQ)
* Convert to Worker Service
