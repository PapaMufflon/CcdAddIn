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
                    items.Add(ItemName.BewareOptimizations);
                    items.Add(ItemName.FavorCompositionOverInheritance);

                    items.Add(ItemName.BoyscoutRule);
                    items.Add(ItemName.RootCauseAnalysis);
                    items.Add(ItemName.VersionControl);
                    items.Add(ItemName.SimpleRefactorizations);
                    items.Add(ItemName.DailyReflection);
                    break;
                case Level.Orange:
                    items.Add(ItemName.SingleLevelOfAbstraction);
                    items.Add(ItemName.SingleResponsibilityPrinciple);
                    items.Add(ItemName.SeparationOfConcerns);
                    items.Add(ItemName.SourceCodeConventions);

                    items.Add(ItemName.IssueTracking);
                    items.Add(ItemName.AutomaticIntegrationTests);
                    items.Add(ItemName.ReadReadRead);
                    items.Add(ItemName.Reviews);
                    break;
                case Level.Yellow:
                    items.Add(ItemName.InterfaceSegregationPrinciple);
                    items.Add(ItemName.DependencyInversionPrinciple);
                    items.Add(ItemName.LiskovSubstitutionPrinciple);
                    items.Add(ItemName.PrincipleOfLeastAstonishment);
                    items.Add(ItemName.InformationHidingPrinciple);

                    items.Add(ItemName.AutomaticUnitTests);
                    items.Add(ItemName.Mockups);
                    items.Add(ItemName.CodeCoverageAnalysis);
                    items.Add(ItemName.AttendProfessionalEvents);
                    items.Add(ItemName.ComplexRefactorizations);
                    break;
                case Level.Green:
                    items.Add(ItemName.OpenClosedPrinciple);
                    items.Add(ItemName.TellDontAsk);
                    items.Add(ItemName.LawOfDemeter);

                    items.Add(ItemName.ContinuousIntegration);
                    items.Add(ItemName.StaticCodeAnalysis);
                    items.Add(ItemName.InversionOfControlContainer);
                    items.Add(ItemName.ShareExperience);
                    items.Add(ItemName.MeasureFailure);
                    break;
                case Level.Blue:
                    items.Add(ItemName.DesignAndImplementationDoNotOverlap);
                    items.Add(ItemName.ImplementaionMirrorsDesign);
                    items.Add(ItemName.YouAintGonnaNeedIt);

                    items.Add(ItemName.ContinuousDelivery);
                    items.Add(ItemName.IterativeDevelopment);
                    items.Add(ItemName.ComponentOrientation);
                    items.Add(ItemName.TestFirst);
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
                    items.Add(new Item(ItemName.BewareOptimizations, ItemType.Principle));
                    items.Add(new Item(ItemName.FavorCompositionOverInheritance, ItemType.Principle));

                    items.Add(new Item(ItemName.BoyscoutRule, ItemType.Practice));
                    items.Add(new Item(ItemName.RootCauseAnalysis, ItemType.Practice));
                    items.Add(new Item(ItemName.VersionControl, ItemType.Practice));
                    items.Add(new Item(ItemName.SimpleRefactorizations, ItemType.Practice));
                    items.Add(new Item(ItemName.DailyReflection, ItemType.Practice));
                    break;
                case Level.Orange:
                    items.Add(new Item(ItemName.SingleLevelOfAbstraction, ItemType.Principle));
                    items.Add(new Item(ItemName.SingleResponsibilityPrinciple, ItemType.Principle));
                    items.Add(new Item(ItemName.SeparationOfConcerns, ItemType.Principle));
                    items.Add(new Item(ItemName.SourceCodeConventions, ItemType.Principle));

                    items.Add(new Item(ItemName.IssueTracking, ItemType.Practice));
                    items.Add(new Item(ItemName.AutomaticIntegrationTests, ItemType.Practice));
                    items.Add(new Item(ItemName.ReadReadRead, ItemType.Practice));
                    items.Add(new Item(ItemName.Reviews, ItemType.Practice));
                    break;
                case Level.Yellow:
                    items.Add(new Item(ItemName.InterfaceSegregationPrinciple, ItemType.Principle));
                    items.Add(new Item(ItemName.DependencyInversionPrinciple, ItemType.Principle));
                    items.Add(new Item(ItemName.LiskovSubstitutionPrinciple, ItemType.Principle));
                    items.Add(new Item(ItemName.PrincipleOfLeastAstonishment, ItemType.Principle));
                    items.Add(new Item(ItemName.InformationHidingPrinciple, ItemType.Principle));

                    items.Add(new Item(ItemName.AutomaticUnitTests, ItemType.Practice));
                    items.Add(new Item(ItemName.Mockups, ItemType.Practice));
                    items.Add(new Item(ItemName.CodeCoverageAnalysis, ItemType.Practice));
                    items.Add(new Item(ItemName.AttendProfessionalEvents, ItemType.Practice));
                    items.Add(new Item(ItemName.ComplexRefactorizations, ItemType.Practice));
                    break;
                case Level.Green:
                    items.Add(new Item(ItemName.OpenClosedPrinciple, ItemType.Principle));
                    items.Add(new Item(ItemName.TellDontAsk, ItemType.Principle));
                    items.Add(new Item(ItemName.LawOfDemeter, ItemType.Principle));

                    items.Add(new Item(ItemName.ContinuousIntegration, ItemType.Practice));
                    items.Add(new Item(ItemName.StaticCodeAnalysis, ItemType.Practice));
                    items.Add(new Item(ItemName.InversionOfControlContainer, ItemType.Practice));
                    items.Add(new Item(ItemName.ShareExperience, ItemType.Practice));
                    items.Add(new Item(ItemName.MeasureFailure, ItemType.Practice));
                    break;
                case Level.Blue:
                    items.Add(new Item(ItemName.DesignAndImplementationDoNotOverlap, ItemType.Principle));
                    items.Add(new Item(ItemName.ImplementaionMirrorsDesign, ItemType.Principle));
                    items.Add(new Item(ItemName.YouAintGonnaNeedIt, ItemType.Principle));

                    items.Add(new Item(ItemName.ContinuousDelivery, ItemType.Practice));
                    items.Add(new Item(ItemName.IterativeDevelopment, ItemType.Practice));
                    items.Add(new Item(ItemName.ComponentOrientation, ItemType.Practice));
                    items.Add(new Item(ItemName.TestFirst, ItemType.Practice));
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