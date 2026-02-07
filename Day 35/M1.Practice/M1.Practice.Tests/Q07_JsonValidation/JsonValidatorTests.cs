using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M1.Practice.Application.Q07_JsonValidation;

namespace M1.Practice.Tests.Q07_JsonValidation
{
    [TestClass]
    public class JsonValidatorTests
    {
        [TestMethod]
        public void ValidateBatch_ShouldReturnCorrectCounts()
        {
            var inputs = new List<string>
            {
                // Valid
                @"{
                    ""Name"":""Raj"",
                    ""Email"":""raj@test.com"",
                    ""Age"":30,
                    ""PAN"":""ABCDE1234F""
                }",

                // Invalid email
                @"{
                    ""Name"":""Sam"",
                    ""Email"":""wrong"",
                    ""Age"":25,
                    ""PAN"":""ABCDE1234F""
                }",

                // Invalid JSON
                @"{ bad json }"
            };

            var validator =
                new JsonBatchValidator();

            var report =
                validator.ValidateBatch(inputs);

            Assert.AreEqual(3, report.Total);
            Assert.AreEqual(1, report.Valid);
            Assert.AreEqual(2, report.Invalid);
            Assert.AreEqual(2, report.Errors.Count);
        }
    }
}
