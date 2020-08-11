
namespace EntityFrameworkCore.MasterSlave.Utils
{
  using System;
  using System.ComponentModel;
  using System.Reflection;

  class Util
  {

    /// <summary>
    /// 获取类型的Description特性描述信息
    /// </summary>
    /// <param name="type">类型对象</param>
    /// <param name="inherit">是否搜索类型的继承链以查找描述特性</param>
    /// <returns>返回Description特性描述信息，如不存在则返回类型的全名</returns>
    public static string GetEnumDescription(Enum enumValue)
    {
      if (enumValue is null)
      {
        throw new ArgumentNullException(nameof(enumValue));
      }

      string value = enumValue.ToString();
      FieldInfo field = enumValue.GetType().GetField(value);
      DescriptionAttribute desc = field.GetCustomAttribute<DescriptionAttribute>(false);    //获取描述属性

      return desc == null ? $"{enumValue.GetType().FullName}.{value}" : desc.Description;
    }
  }
}
