using System.Collections.Generic;

namespace NCommons.Rules
{
    public class ProcessResult
    {
        readonly IList<ReturnValue> _returnItems = new List<ReturnValue>();
        readonly IList<RuleValidationFailure> _validationFailures = new List<RuleValidationFailure>();

        public IEnumerable<RuleValidationFailure> ValidationFailures
        {
            get { return _validationFailures; }
        }

        public IEnumerable<ReturnValue> ReturnItems
        {
            get { return _returnItems; }
        }

        public bool Successful
        {
            get { return _validationFailures.Count == 0; }
        }

        public ProcessResult AddReturnItem(ReturnValue returnItem)
        {
            _returnItems.Add(returnItem);
            return this;
        }

        public ProcessResult AddValidationFailure(RuleValidationFailure failure)
        {
            _validationFailures.Add(failure);
            return this;
        }

        public void Merge(ProcessResult processResult)
        {
            foreach (RuleValidationFailure failure in processResult.ValidationFailures)
                AddValidationFailure(failure);

            foreach (ReturnValue returnItem in processResult.ReturnItems)
            {
                AddReturnItem(returnItem);
            }
        }
    }
}