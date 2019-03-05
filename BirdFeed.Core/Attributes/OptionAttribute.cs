using System;

namespace BirdFeed.Core.Attributes
{
  public class OptionAttribute : Attribute
  {
    public OptionAttribute(string name, string defaultValue)
    {
      if (string.IsNullOrEmpty(name))
      {
        throw new ArgumentNullException("name");
      }

      Name = name;
      DefaultValue = defaultValue;
    }

    public OptionAttribute(string name)
      : this(name, null) { }

    public readonly string Name;

    public readonly string DefaultValue;
  }
}