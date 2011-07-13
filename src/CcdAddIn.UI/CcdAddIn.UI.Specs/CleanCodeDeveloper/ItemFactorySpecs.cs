using System;
using System.Collections.Generic;
using System.Linq;
using CcdAddIn.UI.CleanCodeDeveloper;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Specs.CleanCodeDeveloper
{
    class ItemFactorySpecs
    {
        [Subject(typeof(ItemFactory))]
        public class Given_a_clean_code_developer_level_When_comparing_the_items_with_the_itemNames : WithFakes
        {
            private static Dictionary<Level, List<Item>> _items = new Dictionary<Level, List<Item>>();
            private static Dictionary<Level, List<ItemName>> _itemNames = new Dictionary<Level, List<ItemName>>();

            Establish context = () =>
            {
                foreach (Level level in Enum.GetValues(typeof(Level)))
                {
                    _items.Add(level, ItemFactory.GetItemsFor(level));
                    _itemNames.Add(level, ItemFactory.GetItemNamesFor(level));
                }
            };

            Because of = () => { };

            It should_have_the_same_items = () =>
            {
                foreach (var key in _items.Keys)
                {
                    _itemNames[key].ShouldContainOnly(_items[key].Select(x => x.Name));
                }
            };
        }
    }
}
