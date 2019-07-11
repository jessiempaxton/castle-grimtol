using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      System.Console.WriteLine(@"Welcome to the game
   _____ _    _ _____  ______ _____  _   _       _______ _    _ _____            _      
  / ____| |  | |  __ \|  ____|  __ \| \ | |   /\|__   __| |  | |  __ \     /\   | |     
 | (___ | |  | | |__) | |__  | |__) |  \| |  /  \  | |  | |  | | |__) |   /  \  | |     
  \___ \| |  | |  ___/|  __| |  _  /| . ` | / /\ \ | |  | |  | |  _  /   / /\ \ | |     
  ____) | |__| | |    | |____| | \ \| |\  |/ ____ \| |  | |__| | | \ \  / ____ \| |____ 
 |_____/ \____/|_|    |______|_|  \_\_| \_/_/    \_\_|   \____/|_|  \_\/_/    \_\______|
                                                                                        
                                                                                        
      Please press any key to continue.");
      string begin = Console.ReadLine();
      GameService gameService = new GameService(begin);
      gameService.Play();

    }
  }
}
