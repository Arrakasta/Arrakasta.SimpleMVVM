# How to Contribute to Arrakasta

We're excited that you want to help our project! Every contribution is valuable to us. This document provides a set of guidelines for contributors.

## Table of Contents
* [Code of Conduct](#code-of-conduct)
* [How Can I Contribute?](#how-can-i-contribute)
* [Reporting Bugs](#reporting-bugs)
* [Suggesting Enhancements](#suggesting-enhancements)
* [Your First Pull Request](#your-first-pull-request)
* [Setting Up Your Local Environment](#setting-up-your-local-environment)
* [Coding Style](#coding-style)

## Code of Conduct

This project and everyone participating in it is governed by the [Code of Conduct](./CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code.

## How Can I Contribute?

There are many ways to contribute:
* Improving documentation
* Reporting a bug
* Suggesting a new feature
* Writing code to fix a bug or implement a new feature

## Reporting Bugs

Before creating a bug report, please check the existing issues to see if someone has already reported it.

When creating a bug report, please include as many details as possible:
* The version of the project you are using.
* Clear and concise steps to reproduce the bug.
* What you expected to happen and what actually happened.
* Screenshots or logs, if applicable.

## Suggesting Enhancements

Describe the problem your proposal solves and how you suggest solving it. Explain why this enhancement would be useful to most users.

## Your First Pull Request

1.  **Fork the repository.**
2.  **Create a new branch** from `main`: `git checkout -b feature/your-enhancement`.
3.  **Make your changes.** Ensure that new functionality is covered by tests.
4.  **Ensure all tests pass** (`dotnet test`).
5.  **Commit your changes.** We adhere to the [Conventional Commits](https://www.conventionalcommits.org/) specification.
6.  **Push your changes to your fork** (`git push origin feature/your-enhancement`).
7.  **Create a Pull Request** to our repository. Give it a clear title and describe the changes you've made. If your PR fixes an existing issue, please reference its number.

## Setting Up Your Local Environment

1.  Clone your fork: `git clone https://github.com/Arrakasta/Arrakasta.SimpleMVVM.git`
2.  Navigate to the project directory: `cd Arrakasta.SimpleMVVM`
4.  Build the project: `dotnet build`
5.  Run the tests: `dotnet test`

## Coding Style

We follow the standard .NET coding conventions, defined in the `.editorconfig` file in the repository root. Please ensure your editor uses these settings.
