using System;

namespace CcdAddIn.UI.CleanCodeDeveloper
{
    public enum ItemName
    {
        DoNotRepeatYourself,
        KeepItSimpleStupid,
        BewareOptimizations,
        FavorCompositionOverInheritance,

        BoyscoutRule,
        RootCauseAnalysis,
        VersionControl,
        SimpleRefactorizations,
        DailyReflection,

        SingleLevelOfAbstraction,
        SingleResponsibilityPrinciple,
        SeparationOfConcerns,
        SourceCodeConventions,

        IssueTracking,
        AutomaticIntegrationTests,
        ReadReadRead,
        Reviews,

        InterfaceSegregationPrinciple,
        DependencyInversionPrinciple,
        LiskovSubstitutionPrinciple,
        PrincipleOfLeastAstonishment,
        InformationHidingPrinciple,

        AutomaticUnitTests,
        Mockups,
        CodeCoverageAnalysis,
        AttendProfessionalEvents,
        ComplexRefactorizations,

        OpenClosedPrinciple,
        TellDontAsk,
        LawOfDemeter,

        ContinuousIntegration,
        StaticCodeAnalysis,
        InversionOfControlContainer,
        ShareExperience,
        MeasureFailure,

        DesignAndImplementationDoNotOverlap,
        ImplementaionMirrorsDesign,
        YouAintGonnaNeedIt,

        ContinuousDelivery,
        IterativeDevelopment,
        ComponentOrientation,
        TestFirst
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
                case ItemName.BewareOptimizations:
                    return Resources.Resource.BewareOptimizations;
                case ItemName.FavorCompositionOverInheritance:
                    return Resources.Resource.FavorCompositionOverInheritance;

                case ItemName.BoyscoutRule:
                    return Resources.Resource.BoyscoutRule;
                case ItemName.RootCauseAnalysis:
                    return Resources.Resource.RootCauseAnalysis;
                case ItemName.VersionControl:
                    return Resources.Resource.VersionControl;
                case ItemName.SimpleRefactorizations:
                    return Resources.Resource.SimpleRefactorizations;
                case ItemName.DailyReflection:
                    return Resources.Resource.DailyReflection;

                case ItemName.SingleLevelOfAbstraction:
                    return Resources.Resource.SingleLevelOfAbstraction;
                case ItemName.SingleResponsibilityPrinciple:
                    return Resources.Resource.SingleResponsibilityPrinciple;
                case ItemName.SeparationOfConcerns:
                    return Resources.Resource.SeparationOfConcerns;
                case ItemName.SourceCodeConventions:
                    return Resources.Resource.SourceCodeConventions;

                case ItemName.IssueTracking:
                    return Resources.Resource.IssueTracking;
                case ItemName.AutomaticIntegrationTests:
                    return Resources.Resource.AutomaticIntegrationtests;
                case ItemName.ReadReadRead:
                    return Resources.Resource.ReadReadRead;
                case ItemName.Reviews:
                    return Resources.Resource.Reviews;


                case ItemName.InterfaceSegregationPrinciple:
                    return Resources.Resource.InterfaceSegregationPrinciple;
                case ItemName.DependencyInversionPrinciple:
                    return Resources.Resource.DependencyInversionPrinciple;
                case ItemName.LiskovSubstitutionPrinciple:
                    return Resources.Resource.LiskovSubstitutionPrinciple;
                case ItemName.PrincipleOfLeastAstonishment:
                    return Resources.Resource.PrincipleOfLeastAstonishment;
                case ItemName.InformationHidingPrinciple:
                    return Resources.Resource.InformationHidingPrinciple;

                case ItemName.AutomaticUnitTests:
                    return Resources.Resource.AutomaticUnitTests;
                case ItemName.Mockups:
                    return Resources.Resource.Mockups;
                case ItemName.CodeCoverageAnalysis:
                    return Resources.Resource.CodeCoverageAnalysis;
                case ItemName.AttendProfessionalEvents:
                    return Resources.Resource.AttendProfessionalEvents;
                case ItemName.ComplexRefactorizations:
                    return Resources.Resource.ComplexRefactorizations;

                case ItemName.OpenClosedPrinciple:
                    return Resources.Resource.OpenClosedPrinciple;
                case ItemName.TellDontAsk:
                    return Resources.Resource.TellDontAsk;
                case ItemName.LawOfDemeter:
                    return Resources.Resource.LawOfDemeter;

                case ItemName.ContinuousIntegration:
                    return Resources.Resource.ContinousIntegration;
                case ItemName.StaticCodeAnalysis:
                    return Resources.Resource.StaticCodeAnalysis;
                case ItemName.InversionOfControlContainer:
                    return Resources.Resource.InversionOfControlContainer;
                case ItemName.ShareExperience:
                    return Resources.Resource.ShareExperience;
                case ItemName.MeasureFailure:
                    return Resources.Resource.MeasureFailure;

                case ItemName.DesignAndImplementationDoNotOverlap:
                    return Resources.Resource.DesignAndImplementationDoNotOverlap;
                case ItemName.ImplementaionMirrorsDesign:
                    return Resources.Resource.ImplementationMirrorsDesign;
                case ItemName.YouAintGonnaNeedIt:
                    return Resources.Resource.YouAintGonnaNeedIt;

                case ItemName.ContinuousDelivery:
                    return Resources.Resource.ContinuousDelivery;
                case ItemName.IterativeDevelopment:
                    return Resources.Resource.IterativeDevelopment;
                case ItemName.ComponentOrientation:
                    return Resources.Resource.ComponentOrientation;
                case ItemName.TestFirst:
                    return Resources.Resource.TestFirst;

                default:
                    throw new ArgumentOutOfRangeException("itemName");
            }
        }
    }
}