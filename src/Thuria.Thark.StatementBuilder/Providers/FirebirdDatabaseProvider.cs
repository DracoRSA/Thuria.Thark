﻿using Thuria.Thark.Core.Statement;
using Thuria.Thark.Core.Statement.Providers;

namespace Thuria.Thark.StatementBuilder.Providers
{
  public class FirebirdDatabaseProvider : IDatabaseProvider
  {
    public DatabaseProviderType DatabaseProviderType { get; } = DatabaseProviderType.Firebird;
    public string StatementOpenQuote { get; } = "\"";
    public string StatementCloseQuote { get; } = "\"";
  }
}