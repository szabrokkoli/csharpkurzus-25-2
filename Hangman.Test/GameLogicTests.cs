using Hangman.Core;
using Hangman.Models;

namespace Hangman.Test;
using Hangman;

[TestFixture]
public class GameLogicTest
{
    [Test]
    public void Constructor_ShouldNormalizeWordToUpperCase()
    {
        var logic = new GameLogic("dino");

        Assert.That(logic.Word, Is.EqualTo("DINO"));
    }

    [Test]
    public void Guess_ValidLetter_ShouldReturnSuccess()
    {
        var logic = new GameLogic("DINO");

        var result = logic.Guess('d'); 

        Assert.That(result, Is.EqualTo(GuessResult.Success));
        Assert.That(logic.GuessedLetters, Does.Contain('D'));
    }

    [Test]
    public void Guess_InvalidLetter_ShouldReturnMissAndIncrementMistakes()
    {
        var logic = new GameLogic("DINO");

        var result = logic.Guess('a');

        Assert.That(result, Is.EqualTo(GuessResult.Miss));
        Assert.That(logic.CurrentMistakes, Is.EqualTo(1));
    }

    [Test]
    public void Guess_DuplicateLetter_ShouldReturnDuplicate()
    {
        var logic = new GameLogic("DINO");
        logic.Guess('n'); 

        var result = logic.Guess('n'); 

        Assert.That(result, Is.EqualTo(GuessResult.Duplicate));
        Assert.That(logic.CurrentMistakes, Is.EqualTo(0));
    }

    [Test]
    public void IsWin_AllLettersGuessed_ShouldReturnTrue()
    {
        var logic = new GameLogic("NO");

        logic.Guess('n');
        logic.Guess('o');

        Assert.That(logic.IsWin(), Is.True);
        Assert.That(logic.IsGameOver(), Is.True);
    }

    [Test]
    public void IsGameOver_MaxMistakesReached_ShouldReturnTrue()
    {
        var logic = new GameLogic("D", maxMistakes: 2);

        logic.Guess('x'); 
        logic.Guess('y'); 

        Assert.That(logic.CurrentMistakes, Is.EqualTo(2));
        Assert.That(logic.IsGameOver(), Is.True);
        Assert.That(logic.IsWin(), Is.False); 
    }
}