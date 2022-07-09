
# C# Wikipedia web scraper

## Learning summary

While learning the C# programming language, I have produced a simple web scraper, that scours Wikipedia web pages, as one of my projects. This project scrapes the links from each page so it can continue crawling, and the content of each page. The content is used as material to determine the letter prevalence of the letters of the English alphabet. These prevalence statistics are then displayed as a bar chart to the user in the console.

* C# programming language: I have learned the entry level syntax of C# as well as learning about the C# infrastructure such as Visual Studio and useful utility functionality provided by the .NET standard library. I also learned to leverage passing arguments by reference in C# (the `ref` keyword) within the design of my solution.

* Object-oriented programming: while working on this project, I used OOP to aid in the design and solution for this project. I used the OOP principles of inheritance and polymorphism in order to create the iterable data structure `LinkSet`. Directly *inheriting* the `IEnumerable` interface and *implementing* its `GetEnumerator()` method. Furthermore, I extensively used the object-oriented concept of composition within my solution. This is especially true of the data structures that I created — such as `KeyQueue`; instead of inheriting from a base class or interface and adding a functionality, I used and operated on instances of other classes to *compose* the desired outcome. I also embraced the idea of abstraction, where all the attributes of the class were private - only modifiable via the public methods.

* Multithreading: this app has many components which must all run simultaneously for it to work. For instance, the keyboard input being read from a user is execution blocking, so would require a separate execution thread; or for example the Selenium web scraper bot that is analysing all these letters will have to have its own thread to run in; which is, again, separate to thread that is running the main loop, in which the data is being displayed.

* Selenium web driver: one of this project's core components is web scraping. For this, I needed to learn the rudimentary elements of the Selenium API for C#. I needed to know how to set options, start a web driver instance, navigate to various pages, and query the DOM - with Selenium.

* HyperText Markup Language: as a part of the knowledge required for using Selenium effectively, I needed to know how to work with HTML. I learned about the tree-like structure of the DOM in HTML pages, the various tags that it is composed of, and the attributes that these tags can have (such as `id`) and what they mean, as well as properties like `innerText`. I used this knowledge to construct element queries for links by finding all `<a>` tags, `id` queries for the root node of the content on a Wikipedia page, and `xpath` queries for the child elements of the current element.

* Problem solving: this project was all about solving problems. I broke down larger problems into a series of smaller sub problems, until a workable solution could be found for each one. For example, I applied my knowledge of data structures and their algorithms to this project in order to solve one of the key problems I was facing. I used my knowledge that the HTML page structure is in the form of a tree, to implement breadth-first tree traversal, so I could retrieve the entirety of the content from the Wikipedia page. Another example of problem solving strategies was the use of data visualisation techniques to display the data in a more readable format to the user, than just a big spreadsheet. I used modular arithmetic and other mathematical tools to be able to construct a bar chart representation of the data at hand.

## How to operate this project

### How to run the project

1. Download the `.zip` file from [here](https://github.com/AndreiCravtov/csharp-web-scraper/releases/tag/Windows).

2. Extract the `.zip` file, and run `CSharpWebScraper.exe`.

### Application use

The application will run and display all the data automatically upon being run, and will run indefinitely. At any time, the keyboard button `Q` can be pressed to quit the application.

## Viewing and  modifying  the project

This repository is a Visual Studio solution. It can be opened and edited by cloning this repo and opening the appropriate `.sln` file in Visual Studio, like any other project. From there, the source code can be viewed and modified.
