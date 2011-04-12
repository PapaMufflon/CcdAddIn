using System.Collections.Generic;

namespace CcdAddIn.UI.CleanCodeDeveloper
{
    public class RalfWestphal
    {
        public static bool ShouldAdvance(List<CcdLevel> retrospectives)
        {
            var advice = true;

            if (retrospectives.Count < 21) return false;

            foreach (var level in retrospectives)
            {
                foreach (var practice in level.Practices)
                {
                    if (practice.EvaluationValue <= 80)
                        advice = false;
                }

                foreach (var principle in level.Principles)
                {
                    if (principle.EvaluationValue <= 80)
                        advice = false;
                }
            }

            return advice;
        }
    }
}