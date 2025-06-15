#  Library Application (UWP) with XML Database

This project was created as part of the "XML Technologies" course during second-cycle (Master’s level) studies.

##  Project Overview

This project is a modular application designed to manage a library system, implemented using C# and split into three main components:

- **Administrative Module** – A .NET Core console application for managing and generating XML data files.
- **Client Module (UWP)** – A graphical application built with Universal Windows Platform (UWP) that supports viewing, editing, and managing data (CRUD operations).
- **Data Model Library** – A .NET Standard 2.0 library containing data structures shared across both applications.

---


##  Project Structure

bibModelSalnik/ # Data model library (.NET Standard 2.0)
bibAdmSalnik/ # Administrative console module (.NET Core)
bibKliSalnik/ # UWP GUI client
README.md # This file


##  How to Use

1. **Run `bibAdmSalnik`** to generate sample XML data if not already present.
2. Check the following files in your **Documents** folder:
   - `autorzy.xml`
   - `wydawnictwa.xml`
   - `ksiazki.xml`
3. **Launch `bibKliSalnik` (UWP)**:
   - View authors, books, and publishers.
   - Add, delete, or edit records.
   - Changes are automatically saved when exiting the page.
