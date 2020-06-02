using System;
using System.Collections.Generic;

using Thuria.Thark.Core.DataAccess;
using Thuria.Thark.DataModel.Attributes;

namespace Thuria.Thark.DataModel.Tests
{
  [ThuriaTable("ThuriaTestOne")]
  public class ThuriaTestDataModel
  {
    [ThuriaColumn("Id", IsPrimaryKey = true, IsInsertColumn = false, IsUpdateColumn = false)]
    public Guid Id { get; set; }

    [ThuriaColumn("DisplayName", "Name")]
    [ThuriaConditionColumn(DbContextAction.Create)]
    [ThuriaConditionColumn(DbContextAction.Update)]
    public string Name { get; set; }

    [ThuriaConditionColumn(DbContextAction.Create, false)]
    [ThuriaConditionColumn(DbContextAction.Update, false)]
    public string Description { get; set; }

    [ThuriaColumn("Modified")]
    public DateTime ModifiedDate { get; set; }

    [ThuriaColumn("IsActive", IsInsertColumn = false)]
    [ThuriaConditionColumn(DbContextAction.Update, false)]
    public bool IsActive { get; set; }

    [ThuriaRelationship("ForeignTest", TharkRelationshipType.OneToOne, "Id", "ThuriaTestId")]
    public ThuriaForeignTestDataModel ForeignTestDataModel { get; set; }

    [ThuriaRelationship("ForeignTestMultiple", TharkRelationshipType.OneToMany, "Id", "ThuriaTestId")]
    public IEnumerable<ThuriaForeignTestDataModel> AllForeignTests { get; set; }
  }
}
