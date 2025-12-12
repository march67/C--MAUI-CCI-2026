using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;
using Morpion;

namespace Morpion.test
{
    public class BoardTests
    {
        // faire les tests de manière progressive
        public class FakeBoardWithDiagonalWin : Board
        {
            public override bool CheckDiagonalWinCondition() => true;
        }

        public class FakeBoardWithColumnWin : Board
        {
            public override bool CheckColumnWinCondition() => true;
        }

        public class FakeBoardWithRowWin : Board
        {
            public override bool CheckRowWinCondition() => true;
        }


        [Fact]
        public void CheckDiagonalWinCondition_WithValidDiagonalWinCondition_ShouldReturnTrue()
        {
            // Arrange -
            var board = new Board(); // Tester si la grille est vide
            board.InputMoveOnBoard((1, 1, 'X')); // Commencer par tester cette méthode
            board.InputMoveOnBoard((2, 2, 'X'));
            board.InputMoveOnBoard((3, 3, 'X'));

            // Act
            var result = board.CheckDiagonalWinCondition();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckDiagonalWinCondition_WithEmptyGrid_ShouldReturnFalse()
        {
            // Arrange
            var board = new Board();

            // Act
            var result = board.CheckDiagonalWinCondition();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CheckColumnWinCondition_WithValidColumnWinCondition_ShouldReturnTrue()
        {
            // Arrange -
            var board = new Board();
            board.InputMoveOnBoard((1, 1, 'X'));
            board.InputMoveOnBoard((2, 1, 'X'));
            board.InputMoveOnBoard((3, 1, 'X'));

            // Act
            var result = board.CheckColumnWinCondition();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckColumnWinCondition_WithEmptyGrid_ShouldReturnFalse()
        {
            // Arrange
            var board = new Board();

            // Act
            var result = board.CheckColumnWinCondition();

            // Assert
            result.Should().BeFalse();
        }


        [Theory]
        [InlineData(1, 1, 1, 2, 1, 3, true)] // row 1, toutes les colonnes sont remplies
        [InlineData(1, 1, 2, 1, 3, 1, false)] // column 1, toutes les lignes sont remplies
        public void CheckRowWinCondition_ShouldReturnExpectedResult(
            int rowInput_1, int columnInput_1, int rowInput_2, int columnInput_2, int rowInput_3, int columnInput_3, bool expectedResult)
        {
            // Arrange
            Board board = new Board();
            board.InputMoveOnBoard((rowInput_1, columnInput_1, 'X'));
            board.InputMoveOnBoard((rowInput_2, columnInput_2, 'X'));
            board.InputMoveOnBoard((rowInput_3, columnInput_3, 'X'));

            // Act
            var result = board.CheckRowWinCondition();

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CheckWinCondition_WithTrueCheckDiagonalWinCondition_ShouldReturnTrue()
        {
            // Arrange
            var board = new FakeBoardWithDiagonalWin();
            string playerName = "David";

            // Act
            var result = board.CheckWinCondition(playerName);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckWinCondition_WithTrueCheckColumnWinCondition_ShouldReturnTrue()
        {
            // Arrange
            var board = new FakeBoardWithColumnWin();
            string playerName = "David";

            // Act
            var result = board.CheckWinCondition(playerName);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckWinCondition_WithTrueCheckRowWinCondition_ShouldReturnTrue()
        {
            // Arrange
            var board = new FakeBoardWithRowWin();
            string playerName = "David";

            // Act
            var result = board.CheckWinCondition(playerName);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckWinCondition_NotValidWinCondition_ShouldReturnFalse()
        {
            // Arrange
            var board = new Board();
            string playerName = "David";

            board.InputMoveOnBoard((1, 1, 'X'));
            board.InputMoveOnBoard((2, 2, 'X'));
            board.InputMoveOnBoard((3, 1, 'X'));

            // Act
            var result = board.CheckWinCondition(playerName);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CheckEndGame_WithGridFull_ShouldReturnTrue()
        {
            // Arrange
            var board = new Board();
            board.InputMoveOnBoard((1, 1, 'X'));
            board.InputMoveOnBoard((1, 2, 'X'));
            board.InputMoveOnBoard((1, 3, 'X'));
            board.InputMoveOnBoard((2, 1, 'X'));
            board.InputMoveOnBoard((2, 2, 'X'));
            board.InputMoveOnBoard((2, 3, 'X'));
            board.InputMoveOnBoard((3, 1, 'X'));
            board.InputMoveOnBoard((3, 2, 'X'));
            board.InputMoveOnBoard((3, 3, 'X'));

            // Act
            var result = board.CheckEndGame();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckEndGame_WithGridNotFull_ShouldReturnFalse()
        {
            // Arrange
            var board = new Board();
            board.InputMoveOnBoard((1, 1, 'X'));

            // Act
            var result = board.CheckEndGame();

            // Assert
            result.Should().BeFalse();
        }
    }
}