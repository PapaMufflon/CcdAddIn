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
        TäglichReflektieren
    }

    public static class ItemNameExtensions
    {
        public static string ToCorrectString(this ItemName itemName)
        {
            switch (itemName)
            {
                case ItemName.DoNotRepeatYourself:
                    return "Don't Repeat Yourself (DRY)";
                case ItemName.KeepItSimpleStupid:
                    return "Keep It Simple, Stupid (KISS)";
                case ItemName.VorsichtVorOptimierungen:
                    return "Vorsicht vor Optimierungen";
                case ItemName.FavorCompositionOverInheritance:
                    return "Favor Composition over Inheritance (FCoI)";
                case ItemName.Pfadfinderregel:
                    return "Pfadfinderregel";
                case ItemName.RootCauseAnalysis:
                    return "Root Cause Analysis";
                case ItemName.Versionskontrolle:
                    return "Versionskontrolle";
                case ItemName.EinfacheRefaktorisierungen:
                    return "Einfache Refaktorisierungen";
                case ItemName.TäglichReflektieren:
                    return "Täglich reflektieren";
                default:
                    throw new ArgumentOutOfRangeException("itemName");
            }
        }
    }
}