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
        TaeglichReflektieren
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
                default:
                    throw new ArgumentOutOfRangeException("itemName");
            }
        }
    }
}