using UnityEngine;
using UnityEditor;
using System.IO;
using Excel; 
using System.Data;

/// <summary>
/// 编辑器脚本，用于将项目中的Excel配置文件转换为Txt格式，便于读取。
/// 提供了一个Unity编辑器菜单项，方便开发者执行转换操作。
/// </summary>
public static class ConfigDataExcelConvertToTxtEditor
{
    /// <summary>
    /// 定义Excel文件在项目中的相对路径
    /// </summary>
    static string EXCEL_FILES_PATH = "/Excel";
    /// <summary>
    /// 定义转换后的Txt文件保存路径
    /// </summary>
    static string TO_SAVED_PATH = "/Resources/Config/";

    /// <summary>
    /// TAG
    /// </summary>
    const string TAG = "[ConfigDataExcelConvertToTxtEditor] ";

    /// <summary>
    /// Unity编辑器菜单项回调函数，用于执行Excel到Txt的转换操作。
    /// 通过 Unity 编辑器菜单Tools/Config/Excel 转成 Txt访问此函数。
    /// </summary>
    [MenuItem("Tools/Config/Excel 转成 Txt")]
    public static void ExportExcelToTxt()
    {
        // 获取Excel文件所在文件夹的绝对路径
        string assetPath = Application.dataPath + EXCEL_FILES_PATH;
        // 获取该文件夹下所有的Excel文件（*.xlsx）
        string[] files = Directory.GetFiles(assetPath, "*.xlsx");

        // 遍历所有Excel文件进行处理
        for (int i = 0; i < files.Length; i++)
        {
            // 将文件路径中的反斜杠替换为正斜杠，以适应不同操作系统
            files[i] = files[i].Replace('\\', '/');
            // 通过文件流读取Excel文件
            using (FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            {
                // 使用ExcelReaderFactory创建一个用于读取OpenXml格式Excel文件的读取器
                var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                // 将读取器转换为DataSet，便于操作
                DataSet dataSet = excelDataReader.AsDataSet();
                // 读取Excel文件中的第一张表
                DataTable table = dataSet.Tables[0];
                // 调用函数将DataTable中的数据写入到Txt文件
                ReadTableToTxt(files[i], table);
            }
        }

        Debug.Log(TAG+ "ExportExcelToTxt() Excel To Text Finished");

        // 刷新Unity编辑器的资源数据库，确保新创建的文件被识别
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 将DataTable中的数据写入到Txt文件的函数。
    /// </summary>
    /// <param name="filePath">原始Excel文件的路径，用于确定输出Txt文件的名称。</param>
    /// <param name="table">包含要写入数据的DataTable。</param>
    private static void ReadTableToTxt(string filePath, DataTable table)
    {
        Debug.Log(TAG + "ReadTableToTxt() Read " + filePath);

        // 获取不带扩展名的文件名，用于生成Txt文件名
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        // 构造Txt文件的完整路径
        string savedFilePath = TO_SAVED_PATH + fileName + ".txt";
        string path = Application.dataPath + savedFilePath;
        // 检查并删除已存在的同名Txt文件
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        // 创建新的文件流，用于写入Txt文件
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            // 使用StreamWriter封装文件流，便于写入文本数据
            using (StreamWriter sw = new StreamWriter(fs))
            {
                // 遍历DataTable中的每一行
                for (int row = 0; row < table.Rows.Count; row++)
                {
                    DataRow dataRow = table.Rows[row];
                    string str = "";
                    // 遍历每一行中的列，将数据以制表符分隔拼接成字符串
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        string val = dataRow[col].ToString();
                        str += val + "\t";
                    }
                    // 将拼接好的字符串写入文件
                    sw.Write(str);
                    // 如果不是最后一行，则写入换行符
                    if (row != table.Rows.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }

        Debug.Log(TAG + "ReadTableToTxt() Saved  " + savedFilePath);
    }
}