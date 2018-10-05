namespace Thuria.Thark.Core.Statement.Providers
{
  public interface IDatabaseProvider
  {
    DatabaseProviderType DatabaseProviderType { get; }
    string StatementOpenQuote { get; }
    string StatementCloseQuote { get; }
  }
}
