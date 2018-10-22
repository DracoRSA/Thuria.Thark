using System;

namespace Thuria.Thark.DataModel.Tests
{
  public class ThuriaPocoDataModel
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsActive { get; set; }
  }
}
