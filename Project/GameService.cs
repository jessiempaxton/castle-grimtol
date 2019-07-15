using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    private string begin;

    public GameService()
    {

    }

    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Playing { get; set; } = true;

    public void StartGame()
    {
      Console.Clear();
      System.Console.WriteLine(@"Welcome to the game
   _____ _    _ _____  ______ _____  _   _       _______ _    _ _____            _      
  / ____| |  | |  __ \|  ____|  __ \| \ | |   /\|__   __| |  | |  __ \     /\   | |     
 | (___ | |  | | |__) | |__  | |__) |  \| |  /  \  | |  | |  | | |__) |   /  \  | |     
  \___ \| |  | |  ___/|  __| |  _  /| . ` | / /\ \ | |  | |  | |  _  /   / /\ \ | |     
  ____) | |__| | |    | |____| | \ \| |\  |/ ____ \| |  | |__| | | \ \  / ____ \| |____ 
 |_____/ \____/|_|    |______|_|  \_\_| \_/_/    \_\_|   \____/|_|  \_\/_/    \_\______|
                                                                                        
                                                                                        
      Please press enter to continue.");
      string begin = Console.ReadLine();
      if (begin != null)
      {
        CheckUserName();
        Setup();
        Look();
      }
      while (Playing)
      {
        GetUserInput();
      }
    }
    public void CheckUserName()
    {
      System.Console.WriteLine("Are you Sam or Dean?");
      string name = Console.ReadLine().ToLower();
      if (name == "sam" || name == "dean" || name == "peaches")
      {
        name = name[0].ToString().ToUpper() + name.Substring(1);
        CurrentPlayer = new Player(name);
      }
      else
      {
        System.Console.WriteLine("Invalid option.");
        CheckUserName();
      }
    }

    public void Setup()
    {
      //Rooms
      Room room1 = new Room("Room 1", $"You wake up to your cell phone ringing in your pocket. You answer.  \n \n \"{CurrentPlayer.PlayerName}, it's Bobby!  You boys sure got yourselves into a mess this time.  You idjits gotta get out of there NOW!\" \n \n You look next to you and see your brother unconscious on the floor. You remember you two had just been fighting a group of black eyed demons.  You are wounded.  Someone starts jiggling the door handle. There is another door to the east.  *Enter help at any time to view your options*");
      Room room2 = new Room("Room 2", "You run along side your brother out of the first room into the next.  You find your demon killing knife on the floor.  The demons chasing you seem to have left.  There is another door to the east. *Enter help at any time to view your options*");
      Room room3 = new Room("Room 3", "You walk in to face one of the demons that was chasing you. You see a door to the east but the demon is standing in the way.  He lunges at you- time to FIGHT!  *Enter help at any time to view your options*");
      Room room4 = new Room("Room 4", "Running around the dead demon, you two enter the next room only to find TWO more demons waiting for you ready to fight. You see Baby’s keys laying on the floor next to a door to the east. There’s no way you can fight off both demons alone… *Enter help at any time to view your options*");
      Room room5 = new Room("Outside", "This door takes you outside to where Baby is parked.  There's one final demon to destroy. *Enter help at any time to view your options*");

      //Items- ONE item that is both usable and takeable(take knife, use knife satifies this requirement)
      Item brother = new Item("Brother", "You guys fight all the time and it's super annoying but you probably need him.");
      Item knife = new Item("Knife", "Demon killing blade.");
      Item keys = new Item("Keys", "Baby's keys.");

      //Exits
      room1.Exits.Add("east", room2);
      room2.Exits.Add("west", room1);
      room2.Exits.Add("east", room3);
      room3.Exits.Add("west", room2);
      room3.Exits.Add("east", room4);
      room4.Exits.Add("west", room3);
      room4.Exits.Add("east", room5);
      room5.Exits.Add("west", room4);

      //Add items to rooms
      room1.Items.Add(brother);
      room2.Items.Add(knife);
      room3.Items.Add(knife);
      room4.Items.Add(keys);
      room4.Items.Add(brother);

      CurrentRoom = room1;
    }

    public void GetUserInput()
    {
      System.Console.WriteLine($"What do you choose to do {CurrentPlayer.PlayerName}?");
      string[] input = Console.ReadLine().Split(" ");
      string command = input[0];
      string option = "";
      if (input.Length > 1)
      {
        option = input[1];
      }
      switch (command)
      {
        case "look":
          Look();
          break;
        case "quit":
          Quit();
          break;
        case "use":
          UseItem(option);
          break;
        case "go":
          // if(CurrentRoom.Name == "Room 2" && CurrentPlayer.Inventory
          // {
          //     Lose();
          //   }
          //   else
          Go(option);
          Look();
          break;
        case "take":
          TakeItem(option);
          break;
        case "reset":
          Reset();
          break;
        case "inventory":
          Inventory();
          break;
        case "help":
          Help();
          break;
        case "yes":
          StartGame();
          break;
        case "no":
          Quit();
          break;
        default:
          System.Console.WriteLine("Invalid entry.");
          break;
      }
    }

    public void Go(string direction)
    {
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        CurrentRoom = (Room)CurrentRoom.Go(direction); //casting
        // if(CurrentRoom.Name == "Room 2" && CurrentPlayer.Inventory(!= brother)
      }
      // else if (CurrentRoom.Name == "Room 2" && CurrentPlayer.Inventory.Contains("brother"))
      // {
      //   Lose();
      // }
      else
      {
        System.Console.WriteLine("Invalid option.");
      }
    }

    public void Help()
    {
      System.Console.WriteLine("Your choices are Look, Use, Go, Take, Reset, Quit, and Inventory.");
      GetUserInput();
    }

    public void Inventory()
    {
      System.Console.WriteLine("You Have:");
      foreach (var item in CurrentPlayer.Inventory)
      {
        System.Console.WriteLine($"{item.Name} - {item.Description}");
      }
    }

    public void Look()
    {
      Console.Clear();
      Console.WriteLine($"{CurrentRoom.Name}- {CurrentRoom.Description}");
    }

    public void Quit()
    {
      Console.Clear();
      Playing = false;
      Console.WriteLine("Goodbye, ya' idjit.");
    }

    public void Reset()
    {
      Console.Clear();
      StartGame();
    }

    public void TakeItem(string itemName)
    {
      // if (CurrentRoom.Items.Contains(itemName)){ }
      Item item = CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName.ToLower());
      if (item == null)
      {
        System.Console.WriteLine("Invalid option.");
      }
      else
      {
        CurrentRoom.Items.Remove(item);
        CurrentPlayer.Inventory.Add(item);
        System.Console.WriteLine($"{itemName} has been added to your inventory.");
      }
    }
    public void UseItem(string itemName)
    {
      Item item = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName.ToLower());
      if (item == null)
      {
        System.Console.WriteLine("Invalid option.");
      }
      else if (CurrentRoom.Name == "Room 3" && item.Name.ToLower() == "knife")
      {
        // CurrentPlayer.Inventory.Remove(item);
        CurrentRoom.UseItem(itemName);
        // System.Console.WriteLine("You stab the demon with your demon killing knife and destroy it.  Don't forget to take your knife with you again.");
      }
      else if (CurrentRoom.Name == "Room 4" && item.Name.ToLower() == "brother")
      {
        CurrentPlayer.Inventory.Remove(item);
        // CurrentPlayer.Inventory.Remove("knife");
        System.Console.WriteLine("You pass your brother the knife and run for the keys.  He fights off and kills the demons so you can get the keys.  Don't leave him behind!");
      }
      else if (CurrentRoom.Name == "Outside" && item.Name.ToLower() == "knife")
      {
        CurrentPlayer.Inventory.Remove(item);
        System.Console.WriteLine("You stab the demon with your demon killing knife and destroy it.  You both run to the car to make your escape!");
      }
      else if (CurrentRoom.Name == "Outside" && item.Name.ToLower() == "keys")
      {
        CurrentPlayer.Inventory.Remove(item);
        Win();
      }
      else
      {
        Lose();
      }
    }
    public void Win()
    {
      Console.Clear();
      Playing = false;
      System.Console.WriteLine(@"YOU WIN!
      	
                        __________________
                      _/ ||                ~-_
                    ,/   //       /~-       /  ~-_  ____________
  -----------------/-----------------\---------------------------//     ~~~~~~~
 O--------------  /               ~~^ |                         | ~|    ~~~~~~~~~~
 }======{--------\____________________|_________________________|  |   ~~~~~~~
 \===== / /~~~\ \ \                   |         ________________|-~      ~~~~~~~~~
  \----|  \___/ ||--------------------'----------|  \____/ //
 ______`.______.'________________________________`._______.'____________
      ");
    }

    public void Lose()
    {
      Console.Clear();
      System.Console.WriteLine(@"
 __     ______  _    _   _____ _____ ______ _____  
 \ \   / / __ \| |  | | |  __ \_   _|  ____|  __ \ 
  \ \_/ / |  | | |  | | | |  | || | | |__  | |  | |
   \   /| |  | | |  | | | |  | || | |  __| | |  | |
    | | | |__| | |__| | | |__| || |_| |____| |__| |
    |_|__\____/ \____/__|_____/_____|______|_____/ 
    |__   __| |  | |  ____| |  ____| \ | |  __ \   
       | |  | |__| | |__    | |__  |  \| | |  | |  
       | |  |  __  |  __|   |  __| | . ` | |  | |  
       | |  | |  | | |____  | |____| |\  | |__| |  
  _____|_|  |_|  |_|______| |______|_|_\_|_____/   
 |__   __|                           (_)    |__ \  
    | |_ __ _   _    __ _  __ _  __ _ _ _ __   ) | 
    | | '__| | | |  / _` |/ _` |/ _` | | '_ \ / /  
    | | |  | |_| | | (_| | (_| | (_| | | | | |_|   
    |_|_|   \__, |  \__,_|\__, |\__,_|_|_| |_(_)   
             __/ |         __/ |                   
            |___/         |___/                    
*Please enter yes or no*");
      GetUserInput();
    }
  }
}