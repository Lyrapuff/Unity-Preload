using System;

namespace SmallTail.Preload.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PreloadedAttribute : Attribute
    {
        public string Name;

        public PreloadedAttribute()
        {
            
        }

        public PreloadedAttribute(string name)
        {
            Name = name;
        }
    }
}