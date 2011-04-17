using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Communication
{
    /// <summary>
    /// Value equals true --> advance to next level.
    /// </summary>
    public class EndRetrospectiveEvent : CompositePresentationEvent<bool>
    {
    }
}
