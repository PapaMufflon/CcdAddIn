using System;

namespace CcdAddIn.UI.CleanCodeDeveloper
{
    public enum ItemName
    {
        DoNotRepeatYourself,
        KeepItSimpleStupid,
        VorsichtVorOptimierungen,
        FavorCompositionOverInheritance,

        Pfadfinderregel,
        RootCauseAnalysis,
        Versionskontrolle,
        EinfacheRefaktorisierungen,
        TaeglichReflektieren,

        SingleLevelOfAbstraction,
        SingleResponsibilityPrinciple,
        SeparationOfConcerns,
        SourceCodeKonventionen,

        IssueTracking,
        AutomatisierteIntegrationstests,
        LesenLesenLesen,
        Reviews
    }

    public static class ItemNameExtensions
    {
        public static string ToCorrectString(this ItemName itemName)
        {
            switch (itemName)
            {
                case ItemName.DoNotRepeatYourself:
                    return Resources.Resource.DoNotRepeatYourself;
                case ItemName.KeepItSimpleStupid:
                    return Resources.Resource.KeepItSimpleStupid;
                case ItemName.VorsichtVorOptimierungen:
                    return Resources.Resource.BewareOptimizations;
                case ItemName.FavorCompositionOverInheritance:
                    return Resources.Resource.FavorCompositionOverInheritance;

                case ItemName.Pfadfinderregel:
                    return Resources.Resource.BoyscoutRule;
                case ItemName.RootCauseAnalysis:
                    return Resources.Resource.RootCauseAnalysis;
                case ItemName.Versionskontrolle:
                    return Resources.Resource.VersionControl;
                case ItemName.EinfacheRefaktorisierungen:
                    return Resources.Resource.SimpleRefactorizations;
                case ItemName.TaeglichReflektieren:
                    return Resources.Resource.DailyReflection;

                case ItemName.SingleLevelOfAbstraction:
                    return Resources.Resource.SingleLevelOfAbstraction;
                case ItemName.SingleResponsibilityPrinciple:
                    return Resources.Resource.SingleResponsibilityPrinciple;
                case ItemName.SeparationOfConcerns:
                    return Resources.Resource.SeparationOfConcerns;
                case ItemName.SourceCodeKonventionen:
                    return Resources.Resource.SourceCodeConventions;

                case ItemName.IssueTracking:
                    return Resources.Resource.IssueTracking;
                case ItemName.AutomatisierteIntegrationstests:
                    return Resources.Resource.AutomaticIntegrationtests;
                case ItemName.LesenLesenLesen:
                    return Resources.Resource.ReadReadRead;
                case ItemName.Reviews:
                    return Resources.Resource.Reviews;

                default:
                    throw new ArgumentOutOfRangeException("itemName");
            }
        }
    }
}