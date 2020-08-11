namespace EntityFrameworkCore.MasterSlave.DbContext
{
  using System.ComponentModel;

  public enum DbType
  {
    /// <summary>
    /// MySQL数据库
    /// </summary>
    [Description("MySQL数据库")]
    MySQL = 0,

    /// <summary>
    /// SqlServer数据库
    /// </summary>
    [Description("SqlServer数据库")]
    SqlServer = 1,

    /// <summary>
    /// Sqlite数据库
    /// </summary>
    [Description("Sqlite数据库")]
    Sqlite = 2,

    /// <summary>
    /// PostgreSQL数据库
    /// </summary>
    [Description("PostgreSQL数据库")]
    PostgreSQL = 3,

    /// <summary>
    /// Oracle数据库
    /// </summary>
    [Description("Oracle数据库")]
    Oracle = 4
  }
}
