using System;
using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataAccess.Tests
{
  public class FakeTestDataModel
  {
    [ThuriaColumn("Id", IsPrimaryKey = true, IsUpdateColumn = false)]
    [ThuriaConditionColumn(DbContextAction.Update)]
    public Guid Id { get; set; }

    public string Name { get; set; }
    public DateTime Date { get; set; }

    [ThuriaColumn("SomeFieldAlias", "SomeField")]
    public string SomeField { get; set; }
  }
}
