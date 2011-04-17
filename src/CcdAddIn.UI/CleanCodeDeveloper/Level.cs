using System;
using System.Collections.Generic;

namespace CcdAddIn.UI.CleanCodeDeveloper
{
    public enum Level
    {
        Black,
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        White
    }

    public static class ItemFactory
    {
        public static List<ItemName> GetItemNamesFor(Level level)
        {
            var items = new List<ItemName>();

            switch (level)
            {
                case Level.Black:
                    // no principles nor practices
                    break;
                case Level.Red:
                    items.Add(ItemName.DoNotRepeatYourself);
                    items.Add(ItemName.KeepItSimpleStupid);
                    items.Add(ItemName.VorsichtVorOptimierungen);
                    items.Add(ItemName.FavorCompositionOverInheritance);

                    items.Add(ItemName.Pfadfinderregel);
                    items.Add(ItemName.RootCauseAnalysis);
                    items.Add(ItemName.Versionskontrolle);
                    items.Add(ItemName.EinfacheRefaktorisierungen);
                    items.Add(ItemName.TaeglichReflektieren);
                    break;
                case Level.Orange:
                    items.Add(ItemName.SingleLevelOfAbstraction);
                    items.Add(ItemName.SingleResponsibilityPrinciple);
                    items.Add(ItemName.SeparationOfConcerns);
                    items.Add(ItemName.SourceCodeKonventionen);

                    items.Add(ItemName.IssueTracking);
                    items.Add(ItemName.AutomatisierteIntegrationstests);
                    items.Add(ItemName.LesenLesenLesen);
                    items.Add(ItemName.Reviews);
                    break;
                case Level.Yellow:
                    break;
                case Level.Green:
                    break;
                case Level.Blue:
                    break;
                case Level.White:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }

            return items;
        }

        public static List<Item> GetItemsFor(Level level)
        {
            var items = new List<Item>();

            switch (level)
            {
                case Level.Black:
                    // no principles nor practices
                    break;
                case Level.Red:
                    items.Add(new Item(ItemName.DoNotRepeatYourself, ItemType.Principle));
                    items.Add(new Item(ItemName.KeepItSimpleStupid, ItemType.Principle));
                    items.Add(new Item(ItemName.VorsichtVorOptimierungen, ItemType.Principle));
                    items.Add(new Item(ItemName.FavorCompositionOverInheritance, ItemType.Principle));

                    items.Add(new Item(ItemName.Pfadfinderregel, ItemType.Practice));
                    items.Add(new Item(ItemName.RootCauseAnalysis, ItemType.Practice));
                    items.Add(new Item(ItemName.Versionskontrolle, ItemType.Practice));
                    items.Add(new Item(ItemName.EinfacheRefaktorisierungen, ItemType.Practice));
                    items.Add(new Item(ItemName.TaeglichReflektieren, ItemType.Practice));
                    break;
                case Level.Orange:
                    items.Add(new Item(ItemName.SingleLevelOfAbstraction, ItemType.Principle));
                    items.Add(new Item(ItemName.SingleResponsibilityPrinciple, ItemType.Principle));
                    items.Add(new Item(ItemName.SeparationOfConcerns, ItemType.Principle));
                    items.Add(new Item(ItemName.SourceCodeKonventionen, ItemType.Principle));

                    items.Add(new Item(ItemName.IssueTracking, ItemType.Practice));
                    items.Add(new Item(ItemName.AutomatisierteIntegrationstests, ItemType.Practice));
                    items.Add(new Item(ItemName.LesenLesenLesen, ItemType.Practice));
                    items.Add(new Item(ItemName.Reviews, ItemType.Practice));
                    break;
                case Level.Yellow:
                    break;
                case Level.Green:
                    break;
                case Level.Blue:
                    break;
                case Level.White:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }

            return items;
        }
    }
}