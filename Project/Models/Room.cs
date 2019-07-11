using System;
using System.Collections.Generic;
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
      Console.WriteLine($"Wrong way- {Description}");

      return this;
    }

    //return item- double check this!
    // public IRoom ReturnItem(string item)
    // {
    //   Items.Add(item); 
    // }

    public Room(string name, string description)
    {
      Name = name;
      Description = description;
    }













  }



}