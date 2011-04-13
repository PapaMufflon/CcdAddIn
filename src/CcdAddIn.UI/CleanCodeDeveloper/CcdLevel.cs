using System.Collections.Generic;
using System.Linq;

namespace CcdAddIn.UI.CleanCodeDeveloper
{
    public class CcdLevel
    {
        public Level Level { get; private set; }
        public List<Item> Principles { get; private set; }
        public List<Item> Practices { get; private set; }
        
        public CcdLevel(Level level)
        {
            Level = level;

            var items = ItemFactory.GetItemsFor(level);
            Principles = (from i in items where i.ItemType == ItemType.Principle select i).ToList();
            Practices = (from i in items where i.ItemType == ItemType.Practice select i).ToList();
        }

        public override bool Equals(object obj)
        {
            var that = obj as CcdLevel;

            if (that == null)
                return false;

            return this.Level == that.Level &&
                   this.Practices.SequenceEqual(that.Practices) &&
                   this.Principles.SequenceEqual(that.Principles);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Level.GetHashCode();
                result = (result * 397) ^ Principles.GetHashCode();
                result = (result * 397) ^ Practices.GetHashCode();
                return result;
            }
        }
    }
}