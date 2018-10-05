using NUnit.Framework;

using Thuria.Thark.Core.Statement;
using Thuria.Thark.StatementBuilder.Models;
using Thuria.Thark.StatementBuilder.Providers;

namespace Thuria.Thark.StatementBuilder.Tests
{
  [TestFixture]
  public class TestRelationshipModel
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var model = new RelationshipModel(RelationshipType.OneToOne, "TestKeyTable", "TestKeyColumn", EqualityOperators.Equals, "TestFkTable", "TestFkColumn");
      //---------------Test Result -----------------------
      Assert.IsNotNull(model);
    }

    [TestCase(RelationshipType.OneToOne, "KeyTable", "KeyColumn", EqualityOperators.Equals, "ForeignKeyTable", "ForeignKeyColumn", "LEFT JOIN [ForeignKeyTable] ON [KeyTable].[KeyColumn] = [ForeignKeyTable].[ForeignKeyColumn]")]
    public void ToString_GivenRelationshipValues_ShouldReturnExpectedStatement(RelationshipType relationshipType, string keyTableName, string keyColumnName, 
                                                                               EqualityOperators equalityOperator, 
                                                                               string foreignKeyTable, string foreignKeyColumn, string expectedStatement)
    {
      //---------------Set up test pack-------------------
      var relationshipModel = new RelationshipModel(relationshipType, keyTableName, keyColumnName, equalityOperator, foreignKeyTable, foreignKeyColumn)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var relationshipTatement = relationshipModel.ToString();
      //---------------Test Result -----------------------
      Assert.AreEqual(expectedStatement, relationshipTatement);
    }

    [TestCase(RelationshipType.OneToOne, "KeyTable", "KeyColumn", EqualityOperators.Equals, "ForeignKeyTable", "ForeignKeyColumn")]
    public void Equals_GivenSameRelationshipData_ShouldReturnTrue(RelationshipType relationshipType, 
                                                                  string keyTableName, string keyColumnName, 
                                                                  EqualityOperators equalityOperator, 
                                                                  string foreignKeyTable, string foreignKeyColumn)
    {
      //---------------Set up test pack-------------------
      var relationshipModel = new RelationshipModel(relationshipType, keyTableName, keyColumnName, equalityOperator, foreignKeyTable, foreignKeyColumn)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareModel = new RelationshipModel(relationshipType, keyTableName, keyColumnName, equalityOperator, foreignKeyTable, foreignKeyColumn)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = relationshipModel.Equals(compareModel);
      //---------------Test Result -----------------------
      Assert.IsTrue(equalResult);
    }

    [TestCase(RelationshipType.OneToOne, "KeyTable", "KeyColumn", EqualityOperators.Equals, "ForeignKeyTable", "ForeignKeyColumn")]
    public void Equals_GivenDifferentRelationshipData_ShouldReturnTrue(RelationshipType relationshipType, 
                                                                       string keyTableName, string keyColumnName,
                                                                       EqualityOperators equalityOperator, 
                                                                       string foreignKeyTable, string foreignKeyColumn)
    {
      //---------------Set up test pack-------------------
      var relationshipModel = new RelationshipModel(relationshipType, keyTableName, keyColumnName, equalityOperator, foreignKeyTable, foreignKeyColumn)
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var compareModel = new RelationshipModel(RelationshipType.OneToOne, "OtherKeyTable", "OtherKeyColumn", EqualityOperators.Like, "OtherFkTable", "OtherFkColumn")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var equalResult = relationshipModel.Equals(compareModel);
      //---------------Test Result -----------------------
      Assert.IsFalse(equalResult);
    }

    [Test]
    public void GetHashCode_ShouldReturnStringHashCode()
    {
      //---------------Set up test pack-------------------
      var relationshipModel = new RelationshipModel(RelationshipType.OneToOne, "KeyTable", "KeyColumn", EqualityOperators.Like, "FkTable", "FkColumn")
        {
          DatabaseProvider = new SqlServerDatabaseProvider()
        };
      var hashCode = 0;
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      Assert.DoesNotThrow(() => hashCode = relationshipModel.GetHashCode());
      //---------------Test Result -----------------------
      Assert.IsNotNull(hashCode);
    }
  }
}
