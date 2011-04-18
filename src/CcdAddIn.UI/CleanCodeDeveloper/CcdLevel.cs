using System.Collections.Generic;
using System.Linq;
using NLog;

namespace CcdAddIn.UI.CleanCodeDeveloper
{
    public class CcdLevel
    {
        public Level Level { get; private set; }
        public List<Item> Principles { get; private set; }
        public List<Item> Practices { get; private set; }

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CcdLevel(Level level)
        {
            _logger.Trace("Creating new CcdLevel with level {0}", level);

            Level = level;

            LoadPrinciplesAndPractices();
        }

        private void LoadPrinciplesAndPractices()
        {
            var items = ItemFactory.GetItemsFor(Level);
            Principles = (from i in items where i.ItemType == ItemType.Principle select i).ToList();
            Practices = (from i in items where i.ItemType == ItemType.Practice select i).ToList();

            _logger.Trace("Loaded {0} principles and {1} practices", Principles.Count, Practices.Count);
        }

        public void Advance()
        {
            if (Level != Level.White)
                Level++;
            else
                Level = Level.Red;

            _logger.Trace("Advanced to new level ({0})", Level);
            LoadPrinciplesAndPractices();
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