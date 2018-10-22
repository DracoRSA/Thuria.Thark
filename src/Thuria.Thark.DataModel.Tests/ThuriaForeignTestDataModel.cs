using System;
using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataModel.Tests
{
  [ThuriaTable("ThuriaForeignTest")]
  public class ThuriaForeignTestDataModel
  {
    public Guid Id { get; set; }
    public Guid ThuriaTestId { get; set; }
  }
}