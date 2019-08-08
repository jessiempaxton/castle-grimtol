using System;
using System.Collections.Generic;
using System.Threading;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
    public Dictionary<string, IRoom> Exits { get; set; } = new Dictionary<string, IRoom>();

    //changing rooms- return a room if one is found at an exit
    public IRoom Go(string dir)
    {
      if (Exits.ContainsKey(dir))
      {
        return Exits[dir];
      }
      Console.WriteLine($@"[]
      
      You cant go that way");
      Thread.Sleep(1000);
      return this;
    }

    public void UseItem(string item)
    {
      if (Name == "Room 3" && item == "knife")
      {
        Console.Clear();
        Description = @"Room 3- The demon is dead on the floor - you can now go aorund it to the door to the east.";
      }
      else
      {
        Description = @"You walk in to face one of the demons that was chasing you. You see a door to the east but the demon is standing in the way.  He lunges at you- time to FIGHT!  *Enter help at any time to view your options*";
      }
      System.Console.WriteLine(Description);
    }
    public Room(string name, string description)
    {
      Name = name;
      Description = description;
    }













  }



}