using System.ComponentModel;

namespace EntityFrameworkCore.MasterSlave.DbContext
{
  public enum RoundRobinPolicy
  {
    /// <summary>
    /// 随机轮询
    /// </summary>
    [Description("随机轮询")]
    RandomPolling = 0,

    /// <summary>
    /// 加权轮询
    /// </summary>
    [Description("加权轮询")]
    WeightedPolling = 1,

    /// <summary>
    /// 平滑加权轮询
    /// </summary>
    [Description("平滑加权轮询")]
    SmoothWeightedPolling = 2
  }
}
