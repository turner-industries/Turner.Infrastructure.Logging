using System;
using NUnit.Framework;
using Turner.Infrastructure.Logging.Decorators;

namespace Turner.Infrastructure.Logging.Tests.Decorators
{
    [TestFixture]
    public class MaskJsonConverterTests
    {
        private MaskJsonConverter _converter;
        private TestJsonWriter _testJsonWriter;
        private const string Mask = "-";

        [SetUp]
        public void Setup()
        {
            _converter = new MaskJsonConverter(SsnMaskJsonConverter.RegExPatterns.Ssn, Mask);
            _testJsonWriter = new TestJsonWriter();
        }

        [Test]
        public void CanConvert_TargetIsString_ReturnsTrue()
        {
            var result = _converter.CanConvert(typeof(string));

            Assert.IsTrue(result);
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(DateTime))]
        [TestCase(typeof(MaskJsonConverter))]
        public void CanConvert_TargetIsNotAString_ReturnsFalse(Type objectType)
        {
            var result = _converter.CanConvert(objectType);

            Assert.IsFalse(result);
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

            Assert.AreEqual(1, _testJsonWriter.WrittenValues.Count);
            Assert.AreEqual(value, _testJsonWriter.WrittenValues[0]);
        }

        [Test]
        public void WriteJson_ValueMatchesPattern_AppliesMask()
        {
            const string value = "123456789";

            _converter.WriteJson(_testJsonWriter, value, null);

            Assert.AreEqual(1, _testJsonWriter.WrittenValues.Count);
            Assert.AreEqual("-----6789", _testJsonWriter.WrittenValues[0]);
        }
    }
}
