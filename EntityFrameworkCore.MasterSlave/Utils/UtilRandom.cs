namespace EntityFrameworkCore.MasterSlave.Utils
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Reflection;
  using EntityFrameworkCore.MasterSlave.Database;

  public class UtilRandom
  {
    public static Random Random = new Random();

    // 加权轮询
    public static int GetWeightedPollingIndex(List<SlaveConnectionConfig> configs)
    {
      int maxValue = configs.Sum(c => c.Weight);

      var num = Random.Next(1, maxValue);
      var result = 0;
      var endValue = 0;
      foreach (var item in configs)
      {
        var index = configs.IndexOf(item);
        var beginValue = index == 0 ? 0 : configs[index - 1].Weight;
        endValue += item.Weight;
        result = index;
        if (num >= beginValue && num <= endValue)
          break;
      }

      return result;
    }

    // 平滑加权轮询
    public static int GetSmoothWeightedPollingIndex(List<SlaveConnectionConfig> configs)
    {
      int index = -1;
      int total = 0;

      foreach (var item in configs)
      {
        item.Current += item.Weight;
        total += item.Weight;
        if (index == -1 || configs[index].Current < item.Current)
        {
          index = configs.IndexOf(item);
        }
      }

      configs[index].Current -= total;
      return index;
    }

    // 随机轮询
    public static int GetRandomPollingIndex(List<SlaveConnectionConfig> configs)
    {
      return Random.Next(0, configs.Count);
    }

    public static SlaveConnectionConfig GetSlaveConnectionConfig(List<SlaveConnectionConfig> configs, RoundRobinPolicy roundRobinPolic)
    {
      var index = roundRobinPolic switch
      {
        RoundRobinPolicy.RandomPolling => GetRandomPollingIndex(configs),
        RoundRobinPolicy.WeightedPolling => GetWeightedPollingIndex(configs),
        RoundRobinPolicy.SmoothWeightedPolling => GetSmoothWeightedPollingIndex(configs),
        _ => 0
      };

      return configs[index];
    }

    public static string GetConnectionString(List<SlaveConnectionConfig> configs, RoundRobinPolicy roundRobinPolicy)
    {
      return GetSlaveConnectionConfig(configs, roundRobinPolicy).ConnectionString;
    }

    public static string GetConnectionString(DbConnectionOption option)
    {
      return GetSlaveConnectionConfig(option.SlaveConnectionConfigs, option.RoundRobinPolicy).ConnectionString;
    }
  }
}
