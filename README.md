# HANGMAN: Save the Dinos

> "A superior alternative to Hangman."

## 👥 Authors

* **Herman Szabina** - *BOX48H*
* **Zsígó Zsombor** - *AYEC8P*

## 🦕 Overview
This is not your grandfather's morbid Hangman. In this C# Console Application, we are not executing criminals; we are attempting to prevent the Cretaceous-Paleogene extinction event. Every wrong guess brings an asteroid closer to a bewildered ASCII dinosaur.

**No pressure.**

## 🛠️ Technical Features
This project was constructed to meet rigorous software engineering standards, including but not limited to:

* **Game Loop:** Interactive console interface with robust input validation.
* **Error Handling:** Comprehensive `try-catch` blocks to manage I/O operations and runtime exceptions gracefully.
* **Persistent Storage:** Word banks and player high scores are serialized and stored via JSON (`System.Text.Json`).
* **Data Integrity:** Usage of C# `record` types for thread-safe, immutable score tracking.
* **Advanced Querying:** Utilization of LINQ for data aggregation, filtering, and sorting of scores.
* **Unit Testing:** Core game logic is verified using the NUnit framework.

## 📋 Prerequisites

* .NET SDK 8.0 or higher.
* A terminal supporting standard ASCII output.

## 🚀 How to Run

1.  **Configuration:** Ensure `words.json` and `scores.json` is present in the execution directory.
2.  **Command:**
    ```bash
    dotnet run
    ```
3.  **Play:** Follow the on-screen prompts to type your name, select a difficulty level and input your guesses. Enjoy the game!  

## 📐 Architecture

The application is structured into distinct layers to separate logic, data, and presentation:

### Core Logic
* **`Program.cs`**: The entry point. Handles the high-level game loop, dependency instantiation, and exception trapping.
* **`GameLogic.cs`**: Encapsulates the core rules, state management, and win/loss conditions.

### User Interface (UI)
* **`Display.cs`**: The high-level UI controller. Orchestrates screens (Menu, Game, Victory, Defeat).
* **`Rendering.cs`**: The low-level graphics engine. Handles console buffer management, text centering, and layout borders.
* **`Art.cs`**: A static repository containing the ASCII art assets for the gallows and dinosaurs.

### Data & Persistence
* **`ScoreSerialization.cs`**: Manages file I/O operations and JSON serialization and deserialization for player scores.
* **`WordProvider.cs`**: Handles the loading, validation, and caching of word datasets from configuration files.
* **`InputHandler.cs`**: Abstraction layer for user input validation (sanitizing key presses and strings).

### Domain Models & Types
* **`GameScore.cs`**: An immutable `record` type representing a completed game session.
* **`Difficulty.cs`**: Enumeration defining game difficulty levels.
* **`GuessResult.cs`**: Enumeration defining the outcome of a player's action (Success, Miss, Duplicate).

## 📊 Scoring System
Scores are automatically saved to scores.json upon game completion. It's calculated based on difficulty and efficiency. 

$$
\text{Score} = (\text{Difficulty} + 1) \times (10 - \text{Mistakes})
$$

*Note: Losing gives a score of 0. Mediocrity is not rewarded.*

## 🧪 Testing
To run the automated test suite and prove the logic is sound:

```bash
dotnet test
```