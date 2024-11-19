using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 配置数据基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseConfigData<T> : Singleton<T>, IConfigData
                                        where T : BaseConfigData<T>
{
    /// <summary>
    /// TAG
    /// </summary>
    const string TAG = "[BaseConfigData] ";

    /// <summary>
    /// 配置数据助手
    /// </summary>
    protected ConfigDataHelper m_ConfigDataHelper;
    /// <summary>
    /// 文本资源
    /// </summary>
    protected TextAsset m_TextAsset;

    /// <summary>
    /// 配置数据路径
    /// </summary>
    public abstract string ConfigFilePath {get;}

    /// <summary>
    /// 初始化配置数据
    /// </summary>
    /// <returns></returns>
    public virtual void InitConfigData()
    {
        // 加载卡牌数据
        if (m_TextAsset == null) {
            m_TextAsset = Resources.Load<TextAsset>(ConfigFilePath);
            if (m_TextAsset != null)
            {
                Debug.Log(TAG + "InitConfigData():\n" + m_TextAsset.text);
                m_ConfigDataHelper = new ConfigDataHelper(m_TextAsset.text);
            }
            else {
                Debug.LogError(TAG + $"InitConfigData(): {ConfigFilePath} is error, please check.");
            }
        }
    }

    //// <summary>
    /// 获取所有配置数据
    /// </summary>
    /// <returns></returns>
    public virtual List<Dictionary<string, string>> GetConfigDataLines()
    {
        return m_ConfigDataHelper?.GetLines();
    }


    /// <summary>
    /// 根据 ID  获取配置数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual Dictionary<string, string> GetConfigDataById(string id)
    {
        return m_ConfigDataHelper?.GetOneById(id);
    }

    /// <summary>
    /// 释放配置数据
    /// </summary>
    public virtual void ReleaseConfigData() {
        if (m_TextAsset != null) {
            Resources.UnloadAsset(m_TextAsset);
            m_TextAsset = null;
        }

        if (m_ConfigDataHelper != null) { 
            m_ConfigDataHelper.ReleaseData();
            m_ConfigDataHelper = null;
        }
    }
}

