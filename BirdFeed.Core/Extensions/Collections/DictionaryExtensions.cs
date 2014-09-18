using BirdFeed.Core.Attributes;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace BirdFeed.Core.Extensions.Collections
{
  public static class DictionaryExtensions
  {
    public static NameValueCollection ToNameValueCollection(this IDictionary<string, string> dictionary)
    {
      if (dictionary == null || dictionary.Count == 0)
        return new NameValueCollection(0);

      NameValueCollection collection = new NameValueCollection(dictionary.Count);

      foreach (KeyValuePair<string, string> pair in dictionary)
      {
        collection.Add(pair.Key, pair.Value);
      }

      return collection;
    }

    public static IDictionary<string, string> PropertiesToDictionary<T>(this T obj)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>(0);

      if (obj == null)
        return dictionary;

      foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
      {
        OptionAttribute attr = property.GetCustomAttribute<OptionAttribute>();
        bool hasAttr = attr != null;
        string name = hasAttr ? attr.Name : property.Name;
        object propertyValue = property.GetValue(obj);

        if (propertyValue != null)
          dictionary.Add(name, propertyValue.ToString());
        else if (propertyValue == null && hasAttr && attr.DefaultValue != null)
          dictionary.Add(name, attr.DefaultValue);
      }

      return dictionary;
    }
  }
}