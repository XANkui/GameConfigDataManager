using System.Collections.Generic;

/// <summary>
/// 配置数据 接口类
/// </summary>
public interface IConfigData 
{
    /// <summary>
    /// 配置数据路径
    /// </summary>
    string ConfigFilePath { get; }

    /// <summary>
    /// 初始化配置数据
    /// </summary>
    /// <returns></returns>
    void InitConfigData();

    /// <summary>
    /// 获取所有配置数据
    /// </summary>
    /// <returns></returns>
    List<Dictionary<string, string>> GetConfigDataLines();

    /// <summary>
    /// 根据 ID  获取配置数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Dictionary<string, string> GetConfigDataById(string id);

    /// <summary>
    /// 释放配置数据
    /// </summary>
    void ReleaseConfigData();
}
