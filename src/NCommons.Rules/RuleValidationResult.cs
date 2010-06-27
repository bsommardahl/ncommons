using System.Collections.Generic;

namespace NCommons.Rules
{
    public class RuleValidationResult
    {
        readonly IList<RuleValidationFailure> _failures = new List<RuleValidationFailure>();

        public bool IsValid
        {
            get { return _failures.Count == 0; }
        }

        public IEnumerable<RuleValidationFailure> Failures
        {
            get { return _failures; }
        }

        public void AddValidationFailure(RuleValidationFailure failure)
        {
            _failures.Add(failure);
        }
    }
}