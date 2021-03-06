﻿using System.Collections.Generic;
using System.Linq;
using NLog;

namespace CcdAddIn.UI.CleanCodeDeveloper
{
    public class RalfWestphal : IRalfWestphal
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public bool ShouldAdvance(List<CcdLevel> retrospectives)
        {
            var advice = true;

            if (retrospectives.Count < 21)
            {
                _logger.Trace("There are {0} retrospectives - too few", retrospectives.Count);
                return false;
            }

            foreach (var level in retrospectives.Take(21))
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

            _logger.Trace("Ralf Westphal, should I advance? {0}", advice);
            return advice;
        }
    }
}