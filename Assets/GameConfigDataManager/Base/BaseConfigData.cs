using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������ݻ���
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
    /// ������������
    /// </summary>
    protected ConfigDataHelper m_ConfigDataHelper;
    /// <summary>
    /// �ı���Դ
    /// </summary>
    protected TextAsset m_TextAsset;

    /// <summary>
    /// ��������·��
    /// </summary>
    public abstract string ConfigFilePath {get;}

    /// <summary>
    /// ��ʼ����������
    /// </summary>
    /// <returns></returns>
    public virtual void InitConfigData()
    {
        // ���ؿ�������
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
    /// ��ȡ������������
    /// </summary>
    /// <returns></returns>
    public virtual List<Dictionary<string, string>> GetConfigDataLines()
    {
        return m_ConfigDataHelper?.GetLines();
    }


    /// <summary>
    /// ���� ID  ��ȡ��������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual Dictionary<string, string> GetConfigDataById(string id)
    {
        return m_ConfigDataHelper?.GetOneById(id);
    }

    /// <summary>
    /// �ͷ���������
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

