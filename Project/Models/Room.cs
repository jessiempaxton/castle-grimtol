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
    // ??? 9. Create a similar method on the room for returning Items if the user used the 'take' action.
    // public IRoom ReturnItem(string itm)
    // {
    //   Item item = Items.Find(i => i.Name.ToLower() == itm.ToLower());
    //   if (item == null)
    //   {
    //     System.Console.WriteLine("Invalid option.");
    //   }
    //   else
    // }
    public Room(string name, string description)
    {
      Name = name;
      Description = description;
    }













  }



}