using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    private string begin;

    public GameService(string begin)
    {
      this.begin = begin;
    }

    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Playing { get; set; } = true;

    public void Setup()
    {
      //Rooms
      Room room1 = new Room("Room1", $"You wake up to your cell phone ringing in your pocket. You answer.  \n \n \"It's Bobby!  You boys sure got yourselves into a mess this time.  You idjits gotta get out of there NOW!\" \n \n You look next to you and see your brother unconcious on the floor. Someone starts jiggling the door handle. There is another door to the east.");
      Room room2 = new Room("Room2", "You run along side your brother out of the first room into another one.  This room is dark.  You both start feeling around the counters and find a flashlight.");
      Room room3 = new Room("Room3", "description");
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
      room2.Items.Add(flashlight);
      room3.Items.Add(key);
      room4.Items.Add(knife);

      CurrentRoom = room1;
    }

    public void Play()
    {
      Setup();
      CheckUserName();
      while (Playing)
      {
        GetUserInput();
      }
    }
    public void CheckUserName()
    {
      System.Console.WriteLine("Are you Sam or Dean?");
      string name = Console.ReadLine().ToLower();
      if (name == "sam" || name == "peaches" || name == "dean")
      {
        CurrentPlayer = new Player(name);
        Look();
      }
      else
      {
        System.Console.WriteLine("Invalid option.");
        CheckUserName();
      }
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
      System.Console.WriteLine("Your choices are Look, Quit, Use, Go, Take, and Reset.");
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
      Setup();
    }

    public void StartGame() //????
    {
      Playing = true;
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
      Item newItem = CurrentPlayer.Inventory.Find(item => item.Name == itemName);
      if (newItem == null)
      {
        System.Console.WriteLine("Invalid option.");
      }
      else
      {
        CurrentPlayer.Inventory.Add(newItem);
        CurrentRoom.Items.Remove(newItem);
        Inventory();
      }
    }
  }
}