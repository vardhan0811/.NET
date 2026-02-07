using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using M1.Practice.Domain.Q07_JsonValidation;

namespace M1.Practice.Application.Q07_JsonValidation
{
    public class JsonBatchValidator
    {
        private readonly Regex _emailRegex =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private readonly Regex _panRegex =
            new(@"^[A-Z]{5}[0-9]{4}[A-Z]$");

        public ValidationReport ValidateBatch(
            List<string> jsonPayloads)
        {
            var report = new ValidationReport
            {
                Total = jsonPayloads.Count
            };

            for (int i = 0; i < jsonPayloads.Count; i++)
            {
                var json = jsonPayloads[i];

                try
                {
                    var app =
                        JsonSerializer.Deserialize<
                            CustomerApplication>(json);

                    var errors =
                        Validate(app);

                    if (errors.Count == 0)
                    {
                        report.Valid++;
                    }
                    else
                    {
                        report.Invalid++;

                        foreach (var err in errors)
                        {
                            report.Errors.Add(
                                new ValidationError
                                {
                                    RecordIndex = i,
                                    Message = err
                                });
                        }
                    }
                }
                catch
                {
                    report.Invalid++;

                    report.Errors.Add(
                        new ValidationError
                        {
                            RecordIndex = i,
                            Message = "Invalid JSON format"
                        });
                }
            }

            return report;
        }

        // -------------------------------------

        private List<string> Validate(
            CustomerApplication app)
        {
            var errors = new List<string>();

            if (app == null)
            {
                errors.Add("Empty payload");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(app.Name))
                errors.Add("Name required");

            if (!_emailRegex.IsMatch(app.Email ?? ""))
                errors.Add("Invalid email");

            if (app.Age < 18 || app.Age > 60)
                errors.Add("Invalid age");

            if (!_panRegex.IsMatch(app.PAN ?? ""))
                errors.Add("Invalid PAN");

            return errors;
        }
    }
}
