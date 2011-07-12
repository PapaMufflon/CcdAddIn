using System;

namespace CcdAddIn.UI.CleanCodeDeveloper
{
    [Serializable]
    public class Item
    {
        public ItemName Name { get; private set; }
        public ItemType ItemType { get; private set; }
        public int EvaluationValue { get; set; }

        public Item(ItemName name, ItemType type)
        {
            Name = name;
            ItemType = type;
        }

        // for de/serializing
        private Item() {}

        public string NameAsString { get { return Name.ToCorrectString(); } }

        public override bool Equals(object obj)
        {
            var that = obj as Item;

            if (that == null)
                return false;

            return this.Name == that.Name &&
                   this.ItemType == that.ItemType &&
                   this.EvaluationValue == that.EvaluationValue;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Name.GetHashCode();
                result = (result * 397) ^ ItemType.GetHashCode();
                result = (result * 397) ^ EvaluationValue;
                return result;
            }
        }
    }
}