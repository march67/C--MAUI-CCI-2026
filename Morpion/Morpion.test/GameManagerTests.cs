using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpion.test
{
    public class FakeConsoleWrapper : IConsoleWrapper
    {
        private readonly Queue<string> _inputs;
        public List<string> WrittenMessages { get; } = new();

        public FakeConsoleWrapper(params string[] inputs)
        {
            _inputs = new Queue<string>(inputs);
        }

        public void Write(string message)
        {
            WrittenMessages.Add(message);
        }

        public string? ReadLine()
        {
            // Retourne les valeurs de la Queue une par une dans l'ordre d'ajout
            return _inputs.Count > 0 ? _inputs.Dequeue() : null;
        }
    }

    public class GameManagerTests
    {
        [Fact]
        public void InitializeTwoHumanPlayers_WithValidInputs_ShouldCreateTwoPlayers()
        {
            // Arrange
            var fakeConsole = new FakeConsoleWrapper(
                "David",    // joueur 1
                "X",

                "Alice",    // joueur 2
                "O"
            );
            var game = new GameManager(fakeConsole);

            // Act
            game.InitializeTwoHumanPlayers();

            // Assert
            game.PlayerList[0].GetPlayerName().Should().Be("David");
            game.PlayerList[0].GetPlayerSymbol().Should().Be('X');
            game.PlayerList[1].GetPlayerName().Should().Be("Alice");
            game.PlayerList[1].GetPlayerSymbol().Should().Be('O');
        }
    

    [Fact]
        public void InitializeTwoHumanPlayers_ShouldDisplayCorrectPrompts()
        {
            // Arrange
            var fakeConsole = new FakeConsoleWrapper("David", "X", "Alice", "O"); // Queue les inputs
                // à chaque appel de _console.ReadLine() va dequeue et fournit la valeur dans l'ordre
                // premier readline => David
                // deuxième readline => X
                // etc.
            var game = new GameManager(fakeConsole);

            // Act
            game.InitializeTwoHumanPlayers();

            // Assert
            fakeConsole.WrittenMessages.Should().Contain("Joueur 1, veuillez saisir votre prénom\n");
            fakeConsole.WrittenMessages.Should().Contain("Joueur 2, veuillez saisir votre symbole de jeu\n");
        }
    }
}
