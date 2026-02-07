using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using M1.Practice.Domain.Q05_LogAnalyzer;

namespace M1.Practice.Application.Q05_LogAnalyzer
{
    public class LogAnalyzer
    {
        private readonly Regex _regex =
            new Regex(@"ERR\d+");

        public IEnumerable<ErrorSummary>
            GetTopErrors(string filePath, int topN)
        {
            var counts =
                new Dictionary<string, int>();

            foreach (var line in File.ReadLines(filePath))
            {
                var matches =
                    _regex.Matches(line);

                foreach (Match match in matches)
                {
                    var code = match.Value;

                    if (counts.ContainsKey(code))
                        counts[code]++;
                    else
                        counts[code] = 1;
                }
            }

            return counts
                .OrderByDescending(x => x.Value)
                .Take(topN)
                .Select(x =>
                    new ErrorSummary
                    {
                        ErrorCode = x.Key,
                        Count = x.Value
                    });
        }
    }
}
