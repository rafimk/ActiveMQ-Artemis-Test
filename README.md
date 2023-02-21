# https://havret.io/activemq-artemis-address-model

To make the best use of Apache ActiveMQ Artemis you need to understand the messaging model of the broker. In this article I will explain the rules and principles that govern the Artemis address model and how they may affect your applications. To make things easier to follow I’ve created several code examples using ArtemisNetClient. You can find them, as always, on GitHub.

.NET Client for ActiveMQ Artemis
ArtemisNetClient is a lightweight library built on top of AmqpNetLite. It tries to fully leverage Apache ActiveMQ Artemis capabilities. It has a built-in configurable auto-recovery mechanism, transactions, asynchronous API, and supports ActiveMQ Artemis management API.

This last feature will be especially important for the purpose of this blog post, as we won’t need to dig into the broker internals (broker.xml) or tinker around with the Artemis management console to demonstrate more sophisticated examples.

Addresses, queues, and routing types
The messaging model that powers Apache ActiveMQ Artemis seems to be pretty straightforward if you consider that it is built on top of just three simple concepts addresses, queues, and routing types. These are the bits that bind producers and consumers together and allow messages to flow from one application to another.

The overarching idea is that producers never send messages directly to queues. Actually, a producer is unaware whether a message will be delivered to any queue at all. Instead, the producer can only send a message to an address. The address represents a message endpoint. It receives messages from producers and pushes them to queues. The address knows exactly what to do with a message it receives. Should it be appended to a single or many queues? Or maybe should it be discarded? The rules for that are defined by the routing type.

There are two routing types available in Apache ActiveMQ Artemis: Anycast and Multicast. The address can be created with either or both routing types.

When the address is created with Anycast routing type all the messages send to this address are evenly distributed1 among all the queues attached to it. With Multicast routing type every queue bound to the address receives its very own copy of a message. Had the message been pushed to the queue, it can be picked up by one of the consumers attached to this particular queue.
