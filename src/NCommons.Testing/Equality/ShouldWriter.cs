using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCommons.Testing.Equality
{
    public class ShouldWriter : IWriter
    {
        readonly List<EqualityResult> _results = new List<EqualityResult>();

        public void Write(EqualityResult content)
        {
            _results.Add(content);
        }

        public string GetFormattedResults()
        {
            var sb = new StringBuilder();
            _results
                .Where(x => x.Status.Equals(false))
                .Where(IsLeaf)
                .ToList()
                .ForEach(x =>
                         sb.Append(string.Format("For {0}, expected {1} but found {2}.{3}",
                                                 x.Member,
                                                 x.Expected.ToUsefulString(),
                                                 x.Actual.ToUsefulString(),
                                                 Environment.NewLine)));
            return sb.ToString();
        }

        public bool IsLeaf(EqualityResult result)
        {
            return _results.Where(x => x.Member.StartsWith(result.Member + ".") || x.Member.StartsWith(result.Member + "[")).Count() == 0;
        }

        
    }
}