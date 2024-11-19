using System;

/// <summary>
/// ���͵���ģʽ���࣬���ڴ����̰߳�ȫ�ĵ�������
/// </summary>
/// <typeparam name="T">���������ͣ�����̳���Singleton<T>��</typeparam>
public abstract class Singleton<T> where T : Singleton<T>
{
    // ˽�о�̬�ֶΣ����ڴ洢����ʵ����
    private static T m_Instance;

    // ˽�о�̬����������ͬ�����ʣ�ȷ���̰߳�ȫ��
    private static object m_Locker = new object();

    /// <summary>
    /// ��ȡ����ʵ�������ԡ�
    /// </summary>
    public static T Instance
    {
        get
        {
            // ���ʵ�������ڣ��������������顣
            if (m_Instance == null)
            {
                lock (m_Locker)
                {
                    // �ٴμ��ʵ���Ƿ�Ϊ�գ��Է��ڵȴ����Ĺ������ѱ������̴߳�����
                    if (m_Instance == null)
                    {
                        // ʹ�÷��䴴������ʵ����
                        m_Instance = Activator.CreateInstance<T>();
                        // ����������ĳ�ʼ��������
                        m_Instance.OnSingletonInit();
                    }
                }
            }

            // ���ص���ʵ����
            return m_Instance;
        }
    }

    /// <summary>
    /// ���������Singleton����ʱ��������д�˷�����ִ�г�ʼ�����롣
    /// </summary>
    protected virtual void OnSingletonInit()
    {
        // Ĭ��ʵ��Ϊ�գ��������������Լ��ĳ�ʼ���߼���
    }
}