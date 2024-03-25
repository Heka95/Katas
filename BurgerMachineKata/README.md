# Burger Machine
***

### Introduction
This project is an exercice to pratice Test Driven Development.

In fact, you implemented more complicated stuff than this exercice but the target is to implement
these principes:
- Meaningful namings
- Test Driven Development
- Low cyclomatic complexity

**It's very important to read and apply the Kata step by step**
***

### The context
Each customer will use a machine to order. These machines are managed by a third party company named **YouOrder** and they send a message on
each finished order.
Your job is to create a project which transforms the code received by these machines to the kitchen.
***

### Sprint 1
The starting project contains an Interface **IOrder** and a concrete class **Order** 
with **string GetCode()** method. You will use this object as a replacement of the real customer machine.

At first, the restaurant would like to implement the restaurant's menus. Your client gives to you somes examples to 
helping you on this task. You do not have any information about the received code semantic,
only the implementation is expected by your client.

**Examples:**

* "B-F-C" : "Burger Menu"
* "C-F-C" : "CheeseBurger Menu"
* "F-F-C" : "FishBurger Menu"

*When the machine give an empty message or a code "N-N-N", you will skip and show nothing message to the kitchen*

> *"Don’t use a comment when you can use a function or a variable"* (Uncle Bob)
***

### Sprint 2
Your client informs you they have been finished to decide the format of messages. In fact, the code can be explained by 
**\<Sandwich>-\<Side>-\<Drink>**. 
* The side is actually always **F** for the **Fries**
* The drink **C** for the **Cola**.
* The sandwich **B** is a **Burger**, **C** is a CheeseBurger and **F** a **FishBurger**. 

Any missing part will be replaced by a **N**. A menu must contain only all parts in fact.

By this mean, the company decides to offer to customers the detailed order commodities and usually, he give you some 
examples to validate this new needs.

* "B-N-N" : "Burger"
* "N-N-C" : "Cola"
* "F-N-C" : "FishBurger and Cola"
* "N-F-C" : "Fries and Cola"
* "C-N-C" : "CheeseBurger and Cola"
