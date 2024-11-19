using System.Collections.Generic;

/// <summary>
/// �������� �ӿ���
/// </summary>
public interface IConfigData 
{
    /// <summary>
    /// ��������·��
    /// </summary>
    string ConfigFilePath { get; }

    /// <summary>
    /// ��ʼ����������
    /// </summary>
    /// <returns></returns>
    void InitConfigData();

    /// <summary>
    /// ��ȡ������������
    /// </summary>
    /// <returns></returns>
    List<Dictionary<string, string>> GetConfigDataLines();

    /// <summary>
    /// ���� ID  ��ȡ��������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Dictionary<string, string> GetConfigDataById(string id);

    /// <summary>
    /// �ͷ���������
    /// </summary>
    void ReleaseConfigData();
}
