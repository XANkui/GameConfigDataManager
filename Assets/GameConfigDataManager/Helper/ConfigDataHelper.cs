using System.Collections.Generic;

/// <summary>
/// 配置数据辅助类，用于解析和存储配置表数据。
/// </summary>
public class ConfigDataHelper
{
    // 存储配置表中的所有数据，每个元素是一个字典，代表一行数据。
    private List<Dictionary<string, string>> dataDic;

    /// <summary>
    /// 构造函数，接受一个包含配置数据的字符串参数。
    /// </summary>
    /// <param name="str">包含配置数据的字符串，数据以行为单位，每行以制表符分隔。</param>
    public ConfigDataHelper(string str)
    {
        // 初始化数据字典列表。
        dataDic = new List<Dictionary<string, string>>();

        // 按换行符切割字符串，得到每一行的数据。
        string[] lines = str.Split('\n');

        // 第一行是标题行，包含每列的名称。
        string[] title = lines[0].Trim().Split('\t'); // 使用制表符切割标题行。

        // 从第三行开始遍历数据，因为第一行是标题，第二行通常是空的或包含额外信息。
        for (int i = 2; i < lines.Length; i++)
        {
            // 创建新的字典存储当前行的数据。
            Dictionary<string, string> dic = new Dictionary<string, string>();

            // 按制表符切割当前行的数据。
            string[] tempArr = lines[i].Trim().Split("\t");

            // 将切割后的数据添加到字典中，使用标题作为键，数据作为值。
            for (int j = 0; j < tempArr.Length; j++)
            {
                dic.Add(title[j], tempArr[j]);
            }

            // 将填充好的字典添加到数据列表中。
            dataDic.Add(dic);
        }
    }

    /// <summary>
    /// 获取所有行的数据。
    /// </summary>
    /// <returns>包含所有数据的列表，每个元素是一个字典，代表一行数据。</returns>
    public List<Dictionary<string, string>> GetLines()
    {
        return dataDic;
    }

    /// <summary>
    /// 根据ID获取一行数据。
    /// </summary>
    /// <param name="id">要查找的ID。</param>
    /// <returns>包含指定ID的数据的字典，如果未找到则返回null。</returns>
    public Dictionary<string, string> GetOneById(string id)
    {
        // 遍历数据列表。
        for (int i = 0; i < dataDic.Count; i++)
        {
            // 获取当前字典。
            Dictionary<string, string> dic = dataDic[i];

            // 如果字典中的ID与参数相同，返回该字典。
            if (dic.ContainsKey("Id") && dic["Id"] == id)
            {
                return dic;
            }
        }

        // 如果没有找到，返回null。
        return null;
    }

    /// <summary>
    /// 释放存储的数据，清空列表并置为null，以帮助垃圾回收。
    /// </summary>
    public void ReleaseData()
    {
        dataDic.Clear();
        dataDic = null;
    }
}