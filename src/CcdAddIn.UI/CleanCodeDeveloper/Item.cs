namespace CcdAddIn.UI.CleanCodeDeveloper
{
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

        public string NameAsString { get { return Name.ToCorrectString(); } }
    }
}