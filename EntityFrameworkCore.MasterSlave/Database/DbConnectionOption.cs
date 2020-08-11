namespace EntityFrameworkCore.MasterSlave.Database
{
  using System.Collections.Generic;
  public class DbConnectionOption
  {
    public string MasterConnectionConfig { get; set; }

    public List<SlaveConnectionConfig> SlaveConnectionConfigs { get; set; }

    public DbType DbType { get; set; }

    public bool CQRSEnabled { get; set; }

    public RoundRobinPolicy RoundRobinPolicy { get; set; } = RoundRobinPolicy.RandomPolling;
  }
}
