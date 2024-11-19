using UnityEngine;
using UnityEditor;
using System.IO;
using Excel; 
using System.Data;

/// <summary>
/// �༭���ű������ڽ���Ŀ�е�Excel�����ļ�ת��ΪTxt��ʽ�����ڶ�ȡ��
/// �ṩ��һ��Unity�༭���˵�����㿪����ִ��ת��������
/// </summary>
public static class ConfigDataExcelConvertToTxtEditor
{
    /// <summary>
    /// ����Excel�ļ�����Ŀ�е����·��
    /// </summary>
    static string EXCEL_FILES_PATH = "/Excel";
    /// <summary>
    /// ����ת�����Txt�ļ�����·��
    /// </summary>
    static string TO_SAVED_PATH = "/Resources/Config/";

    /// <summary>
    /// TAG
    /// </summary>
    const string TAG = "[ConfigDataExcelConvertToTxtEditor] ";

    /// <summary>
    /// Unity�༭���˵���ص�����������ִ��Excel��Txt��ת��������
    /// ͨ�� Unity �༭���˵�Tools/Config/Excel ת�� Txt���ʴ˺�����
    /// </summary>
    [MenuItem("Tools/Config/Excel ת�� Txt")]
    public static void ExportExcelToTxt()
    {
        // ��ȡExcel�ļ������ļ��еľ���·��
        string assetPath = Application.dataPath + EXCEL_FILES_PATH;
        // ��ȡ���ļ��������е�Excel�ļ���*.xlsx��
        string[] files = Directory.GetFiles(assetPath, "*.xlsx");

        // ��������Excel�ļ����д���
        for (int i = 0; i < files.Length; i++)
        {
            // ���ļ�·���еķ�б���滻Ϊ��б�ܣ�����Ӧ��ͬ����ϵͳ
            files[i] = files[i].Replace('\\', '/');
            // ͨ���ļ�����ȡExcel�ļ�
            using (FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            {
                // ʹ��ExcelReaderFactory����һ�����ڶ�ȡOpenXml��ʽExcel�ļ��Ķ�ȡ��
                var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                // ����ȡ��ת��ΪDataSet�����ڲ���
                DataSet dataSet = excelDataReader.AsDataSet();
                // ��ȡExcel�ļ��еĵ�һ�ű�
                DataTable table = dataSet.Tables[0];
                // ���ú�����DataTable�е�����д�뵽Txt�ļ�
                ReadTableToTxt(files[i], table);
            }
        }

        Debug.Log(TAG+ "ExportExcelToTxt() Excel To Text Finished");

        // ˢ��Unity�༭������Դ���ݿ⣬ȷ���´������ļ���ʶ��
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// ��DataTable�е�����д�뵽Txt�ļ��ĺ�����
    /// </summary>
    /// <param name="filePath">ԭʼExcel�ļ���·��������ȷ�����Txt�ļ������ơ�</param>
    /// <param name="table">����Ҫд�����ݵ�DataTable��</param>
    private static void ReadTableToTxt(string filePath, DataTable table)
    {
        Debug.Log(TAG + "ReadTableToTxt() Read " + filePath);

        // ��ȡ������չ�����ļ�������������Txt�ļ���
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        // ����Txt�ļ�������·��
        string savedFilePath = TO_SAVED_PATH + fileName + ".txt";
        string path = Application.dataPath + savedFilePath;
        // ��鲢ɾ���Ѵ��ڵ�ͬ��Txt�ļ�
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        // �����µ��ļ���������д��Txt�ļ�
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            // ʹ��StreamWriter��װ�ļ���������д���ı�����
            using (StreamWriter sw = new StreamWriter(fs))
            {
                // ����DataTable�е�ÿһ��
                for (int row = 0; row < table.Rows.Count; row++)
                {
                    DataRow dataRow = table.Rows[row];
                    string str = "";
                    // ����ÿһ���е��У����������Ʊ���ָ�ƴ�ӳ��ַ���
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        string val = dataRow[col].ToString();
                        str += val + "\t";
                    }
                    // ��ƴ�Ӻõ��ַ���д���ļ�
                    sw.Write(str);
                    // ����������һ�У���д�뻻�з�
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