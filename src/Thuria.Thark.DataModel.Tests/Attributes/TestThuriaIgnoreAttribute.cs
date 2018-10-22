using NUnit.Framework;
using FluentAssertions;

using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataModel.Tests.Attributes
{
  [TestFixture]
  public class TestThuriaIgnoreAttribute
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var tableAttribute = new ThuriaIgnoreAttribute();
      //---------------Test Result -----------------------
      tableAttribute.Should().NotBeNull();
    }
  }
}
