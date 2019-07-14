using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    private string begin;

    public GameService()  //what to use constructor for?
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
      Room room1 = new Room("Room1", $"You wake up to your cell phone ringing in your pocket. You answer.  \n \n \"{CurrentPlayer.PlayerName}, it's Bobby!  You boys sure got yourselves into a mess this time.  You idjits gotta get out of there NOW!\" \n \n You look next to you and see your brother unconscious on the floor. Someone starts jiggling the door handle. You remember you two had just gotten done fighting a pack of black eyed demons.  You are wounded.  There is another door to the east.  *Enter help at any time to view your options*");
      Room room2 = new Room("Room2", "You run along side your brother out of the first room into the next.  You find your demon killing knife on the floor.  The demon chasing you seems to have left.  There is another door to the east.  *Enter help at any time to view your options*");
      Room room3 = new Room("Room3", "You walk in to face one of the demons that was chasing you. You see a door to the east but the demon is standing in the way.  He lunges at you- time to FIGHT!  *Enter help at any time to view your options*");
      Room room4 = new Room("Room4", "description");

      //Items- ONE item that is both usable and takeable(take knife, use knife satifies this requirement)
      Item brother = new Item("Brother", "description");
      Item flashlight = new Item("Flashlight", "description");
      Item knife = new Item("Knife", "description");
      Item key = new Item("Key", "description");

      //Exits
      room1.Exits.Add("east", room2);
      room2.Exits.Add("west", room1);
      room2.Exits.Add("east", room3);
      room3.Exits.Add("west", room2);
      room3.Exits.Add("east", room4);
      room4.Exits.Add("west", room3);

      //Add items to rooms
      room1.Items.Add(brother);
      room2.Items.Add(knife);
      //   room3.Items.Add(flashlight);
      room4.Items.Add(key);

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
          // if(CurrentRoom.Name == "Room2" && !CurrentPlayer.Inventory.Contains("brother"))
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
        default:
          System.Console.WriteLine("Invalid entry.");
          break;
      }
    }

    public void Go(string direction)
    {
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        CurrentRoom = (Room)CurrentRoom.Go(direction);
      }
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
        System.Console.WriteLine($"{item.Name}");
      }
    }

    public void Look()
    {
      Console.Clear();
      Console.WriteLine($"{CurrentRoom.Name}- {CurrentRoom.Description}");
    }

    public void Quit()
    {
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
      System.Console.WriteLine($"use {itemName}");
      Item item = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName.ToLower());
      if (item == null)
      {
        System.Console.WriteLine("Invalid option.");
      }
      else if (CurrentRoom.Name == "Room3" && item.Name.ToLower() == "knife")
      {
        CurrentPlayer.Inventory.Remove(item);
        System.Console.WriteLine("You stab the demon with your demon killing knife and destroy it.");
      }
    }
  }
}