using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;
using Morpion;

namespace Morpion.test
{
    public class BoardManagerTests
    {
        // faire les tests de mani�re progressive
        public class FakeBoardManagerWithDiagonalWin : BoardManager
        {
            public override bool CheckDiagonalWinCondition() => true;
        }

        public class FakeBoardManagerWithColumnWin : BoardManager
        {
            public override bool CheckColumnWinCondition() => true;
        }

        public class FakeBoardManagerWithRowWin : BoardManager
        {
            public override bool CheckRowWinCondition() => true;
        }


        [Fact]
        public void CheckDiagonalWinCondition_WithValidDiagonalWinCondition_ShouldReturnTrue()
        {
            // Arrange -
            var board = new BoardManager(); // Tester si la grille est vide
            board.InputMoveOnBoard((1, 1, 'X')); // Commencer par tester cette m�thode
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
            var board = new BoardManager();

            // Act
            var result = board.CheckDiagonalWinCondition();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CheckColumnWinCondition_WithValidColumnWinCondition_ShouldReturnTrue()
        {
            // Arrange -
            var board = new BoardManager();
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
            var board = new BoardManager();

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
            BoardManager boardManager = new BoardManager();
            boardManager.InputMoveOnBoard((rowInput_1, columnInput_1, 'X'));
            boardManager.InputMoveOnBoard((rowInput_2, columnInput_2, 'X'));
            boardManager.InputMoveOnBoard((rowInput_3, columnInput_3, 'X'));

            // Act
            var result = boardManager.CheckRowWinCondition();

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CheckWinCondition_WithTrueCheckDiagonalWinCondition_ShouldReturnTrue()
        {
            // Arrange
            var board = new FakeBoardManagerWithDiagonalWin();
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
            var board = new FakeBoardManagerWithColumnWin();
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
            var board = new FakeBoardManagerWithRowWin();
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
            var board = new BoardManager();
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
            var board = new BoardManager();
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
            var result = board.CheckDraw();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckEndGame_WithGridNotFull_ShouldReturnFalse()
        {
            // Arrange
            var board = new BoardManager();
            board.InputMoveOnBoard((1, 1, 'X'));

            // Act
            var result = board.CheckDraw();

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 1, 1, 2, true)] // Two inputs, in each different cell
        [InlineData(1, 1, 1, 1,  false)] // Two inputs, in the same cell
        public void CheckValidCellForInput_ShouldReturnExpectedResult(
            int rowInput_1, int columnInput_1, int rowInput_2, int columnInput_2, bool expectedResult)
        {
            // Arrange
            BoardManager boardManager = new BoardManager();
            HumanPlayerManager playerManager = new HumanPlayerManager();
            boardManager.InputMoveOnBoard((rowInput_1, columnInput_1, 'X'));

            // Act
                // - 1 car commence d�calage par rapport � InputMoveOnBoard qui est plus 'humainement logique' ligne 1 = 1 et pas 0
            var result = boardManager.CheckValidCellForInput(rowInput_2 - 1, columnInput_2 - 1, playerManager);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}