#The Roulette Game

In this exercise, you will create a Roulette Game. You will create fakes, most prominently stubs (configurable ones where applicable) and put them to use in the unit test of the system before you put the modules together and play the game.

---

A sketch of the system structure is given below. Note that the UML class diagram is only a sketch designed to introduce the concepts of the game.

![alt text](https://github.com/Priebe109/ProEngineers/blob/master/I4SWT-GR1/Fakes%201%20-%20The%20Roulette%20Game/class_diagram.png "Roulette Game - Class Diagram")

---

The Roulette game consists of various concepts as described below:

The Roulette itself, which can be spun and which can be queried for the most recent result. The roulette consists of 37 Fields, each with a number (0..36) and color (0 is green, 1..36 is red or black). The roulette can be spun which results in a random result, namely 1 of the defined 37 fields.

A Bet, which is identified by the name of the betting player, the amount to bet, and the kind and value of bet (e.g. “Pete Mitchell”, “1000”, “Red/black”, “Red”). Three different kinds of bets are allowed:

* Betting on individual numbers (0-36) – pays back 36 times the amount
* Betting on even/odd numbers - pays back 2 times the amount
* Betting on Red/black - pays back 2 times the amount

The Roulette Game, which controls the opening and closing for bets, and placing of bets, spinning the roulette, payment of wins to players etc. The roulette game should allow several bets to be placed on the roulette.

The roulette game runs in rounds. When a round starts, the bets are opened and bets can be placed. Sometime later, the bets are closed (“rien ne vas plus”) - after this time, placing a bet is considered an error. After the bets are closed, the roulette game will spin the roulette and subsequently check if any bets are won. If so, a notification of the player name, kind of bet and amount won should be made e.g. to the console. Initially, consider the game to have an unlimited amount of money.

You are provided with a working implementation of the game, but this code is neither tested nor designed for test. Your task, in your group, is to refactor and test, using fakes where applicable.

---

##Exercise 1
Discuss the work to be done:

* How should the system be refactored so that the design is more testable than the one implemented?

The roulette game is depending on the roulette to determine the winning bets. The roulette games dependency on the roulette needs to be loosened, by introducing a roulette interface (IRoulette).

* How will we use fakes (especially configurable stubs) in the unit test of the classes? Do we need a boundary
value analysis for any of them?

A stub class implementing the roulette interface can be created and configured to provide testable results. We need BVA of the bets since they have a limited range of allowed input (0-36).

* How will we communicate with the user? Using the console? Should we have some sort of “façade” for this
between the console and the Roulette game?

We introduce a new interface, the IRouletteDisplay, as a facade between the console and roulette game. ConsoleDisplay will implement the IRouletteDisplay. Another class (MockDisplay) will fake the IRouletteDisplay to make the interaction between the roulette game and display testable. 
