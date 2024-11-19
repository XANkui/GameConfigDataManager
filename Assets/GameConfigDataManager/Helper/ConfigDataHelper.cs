using System.Collections.Generic;

/// <summary>
/// �������ݸ����࣬���ڽ����ʹ洢���ñ����ݡ�
/// </summary>
public class ConfigDataHelper
{
    // �洢���ñ��е��������ݣ�ÿ��Ԫ����һ���ֵ䣬����һ�����ݡ�
    private List<Dictionary<string, string>> dataDic;

    /// <summary>
    /// ���캯��������һ�������������ݵ��ַ���������
    /// </summary>
    /// <param name="str">�����������ݵ��ַ�������������Ϊ��λ��ÿ�����Ʊ���ָ���</param>
    public ConfigDataHelper(string str)
    {
        // ��ʼ�������ֵ��б�
        dataDic = new List<Dictionary<string, string>>();

        // �����з��и��ַ������õ�ÿһ�е����ݡ�
        string[] lines = str.Split('\n');

        // ��һ���Ǳ����У�����ÿ�е����ơ�
        string[] title = lines[0].Trim().Split('\t'); // ʹ���Ʊ���и�����С�

        // �ӵ����п�ʼ�������ݣ���Ϊ��һ���Ǳ��⣬�ڶ���ͨ���ǿյĻ����������Ϣ��
        for (int i = 2; i < lines.Length; i++)
        {
            // �����µ��ֵ�洢��ǰ�е����ݡ�
            Dictionary<string, string> dic = new Dictionary<string, string>();

            // ���Ʊ���иǰ�е����ݡ�
            string[] tempArr = lines[i].Trim().Split("\t");

            // ���и���������ӵ��ֵ��У�ʹ�ñ�����Ϊ����������Ϊֵ��
            for (int j = 0; j < tempArr.Length; j++)
            {
                dic.Add(title[j], tempArr[j]);
            }

            // �����õ��ֵ���ӵ������б��С�
            dataDic.Add(dic);
        }
    }

    /// <summary>
    /// ��ȡ�����е����ݡ�
    /// </summary>
    /// <returns>�����������ݵ��б�ÿ��Ԫ����һ���ֵ䣬����һ�����ݡ�</returns>
    public List<Dictionary<string, string>> GetLines()
    {
        return dataDic;
    }

    /// <summary>
    /// ����ID��ȡһ�����ݡ�
    /// </summary>
    /// <param name="id">Ҫ���ҵ�ID��</param>
    /// <returns>����ָ��ID�����ݵ��ֵ䣬���δ�ҵ��򷵻�null��</returns>
    public Dictionary<string, string> GetOneById(string id)
    {
        // ���������б�
        for (int i = 0; i < dataDic.Count; i++)
        {
            // ��ȡ��ǰ�ֵ䡣
            Dictionary<string, string> dic = dataDic[i];

            // ����ֵ��е�ID�������ͬ�����ظ��ֵ䡣
            if (dic.ContainsKey("Id") && dic["Id"] == id)
            {
                return dic;
            }
        }

        // ���û���ҵ�������null��
        return null;
    }

    /// <summary>
    /// �ͷŴ洢�����ݣ�����б���Ϊnull���԰����������ա�
    /// </summary>
    public void ReleaseData()
    {
        dataDic.Clear();
        dataDic = null;
    }
}