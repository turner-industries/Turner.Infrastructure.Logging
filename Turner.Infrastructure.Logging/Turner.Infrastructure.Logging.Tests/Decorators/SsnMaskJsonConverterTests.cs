using System;
using FluentAssertions;
using NUnit.Framework;
using Turner.Infrastructure.Logging.Decorators;

namespace Turner.Infrastructure.Logging.Tests.Decorators
{
    public class SsnMaskJsonConverterTests
    {
        private SsnMaskJsonConverter _converter;
        private TestJsonWriter _testJsonWriter;

        [SetUp]
        public void Setup()
        {
            _converter = new SsnMaskJsonConverter();
            _testJsonWriter = new TestJsonWriter();
        }

        [Test]
        public void CanConvert_TargetIsString_ReturnsTrue()
        {
            var result = _converter.CanConvert(typeof(string));

            result.Should().BeTrue();
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(DateTime))]
        [TestCase(typeof(MaskJsonConverter))]
        public void CanConvert_TargetIsNotAString_ReturnsFalse(Type objectType)
        {
            var result = _converter.CanConvert(objectType);

            result.Should().BeFalse();
        }

        [Test]
        public void ReadJson_AnyCondition_ThrowsException()
        {
            TestDelegate act = () => _converter.ReadJson(null, null, null, null);

            Assert.Throws<NotImplementedException>(act);
        }

        [Test]
        public void WriteJson_ValueDoesNotMatchPattern_DoesNotApplyMask()
        {
            const string value = "DoesNotMatchPattern";

            _converter.WriteJson(_testJsonWriter, value, null);

            _testJsonWriter.WrittenValues.Count.Should().Be(1);
            _testJsonWriter.WrittenValues[0].Should().Be(value);
        }

        [TestCase("SSN")]
        [TestCase("ssn")]
        [TestCase("SocialSecurityNumber")]
        [TestCase("SOCIALSECURITYNUMBER")]
        [TestCase("socialsecuritynumber")]
        public void WriteJson_ValueMatchesPatternAndPropertyNameIsPii_AppliesMask(string propertyName)
        {
            const string value = "123456789";

            _testJsonWriter.WriteStartObject();
            _testJsonWriter.WritePropertyName(propertyName);

            _converter.WriteJson(_testJsonWriter, value, null);

            _testJsonWriter.WriteEndObject();

            _testJsonWriter.WrittenValues.Count.Should().Be(1);
            _testJsonWriter.WrittenValues[0].Should().Be("xxxxx6789");
        }

        [Test]
        public void WriteJson_ValueMatchesPatternAndPropertyNameIsNotPii_DoesNotApplyMask()
        {
            const string value = "123456789";

            _testJsonWriter.WriteStartObject();
            _testJsonWriter.WritePropertyName("x?@3");

            _converter.WriteJson(_testJsonWriter, value, null);

            _testJsonWriter.WriteEndObject();

            _testJsonWriter.WrittenValues.Count.Should().Be(1);
            _testJsonWriter.WrittenValues[0].Should().Be(value);
        }
    }
}
