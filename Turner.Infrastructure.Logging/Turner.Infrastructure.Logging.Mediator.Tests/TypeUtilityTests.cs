using System;
using FluentAssertions;
using NUnit.Framework;

namespace Turner.Infrastructure.Logging.Mediator.Tests
{
    [TestFixture]
    public class TypeUtilityTests
    {
        [Test]
        public void ContainsAttribute_TypeDoesNotHasAttribute_ReturnsTrue()
        {
            var result = TypeUtility.ContainsAttribute(typeof(TestClassWithNoAttribute),
                typeof(ContainsAttributeTestAttribute));

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsAttribute_TypeHasAttribute_ReturnsTrue()
        {
            var result = TypeUtility.ContainsAttribute(typeof(TestClassWithAttribute),
                typeof(ContainsAttributeTestAttribute));

            result.Should().BeTrue();
        }

        [Test]
        public void GetPrettyName_OfType_ReturnsPrettyName()
        {
            var result = TypeUtility.GetPrettyName(typeof(TestPrettyName));

            result.Should().Be("TypeUtilityTests.TestPrettyName");
        }

        [Test]
        public void GetPrettyName_OfGenericType_ReturnsPrettyName()
        {
            var result = TypeUtility.GetPrettyName(typeof(TestPrettyName<int>));

            result.Should().Be("TestPrettyName<Int32>");
        }

        class TestPrettyName { }

        class TestPrettyName<T> { }

        class TestClassWithNoAttribute { }

        [ContainsAttributeTest]
        class TestClassWithAttribute { }

        class ContainsAttributeTestAttribute : Attribute { }
    }
}
