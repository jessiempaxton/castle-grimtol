using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //   Console.BackgroundColor = ConsoleColor.DarkBlue;
      //   Console.ForegroundColor = ConsoleColor.Black;
      GameService gameService = new GameService();
      gameService.StartGame();
      //   GameService gameService = new GameService();
      //   gameService.Play();

    }
  }
}
