using Morpion;
ConsoleWrapper console = new ConsoleWrapper();

Game game = new Game(console);

await game.StartGame();

Console.ReadLine();