using System;

/// <summary>
/// 泛型单例模式基类，用于创建线程安全的单例对象。
/// </summary>
/// <typeparam name="T">单例类类型，必须继承自Singleton<T>。</typeparam>
public abstract class Singleton<T> where T : Singleton<T>
{
    // 私有静态字段，用于存储单例实例。
    private static T m_Instance;

    // 私有静态锁对象，用于同步访问，确保线程安全。
    private static object m_Locker = new object();

    /// <summary>
    /// 获取单例实例的属性。
    /// </summary>
    public static T Instance
    {
        get
        {
            // 如果实例不存在，则进入锁定代码块。
            if (m_Instance == null)
            {
                lock (m_Locker)
                {
                    // 再次检查实例是否为空，以防在等待锁的过程中已被其他线程创建。
                    if (m_Instance == null)
                    {
                        // 使用反射创建单例实例。
                        m_Instance = Activator.CreateInstance<T>();
                        // 调用派生类的初始化方法。
                        m_Instance.OnSingletonInit();
                    }
                }
            }

            // 返回单例实例。
            return m_Instance;
        }
    }

    /// <summary>
    /// 当派生类从Singleton派生时，可以重写此方法以执行初始化代码。
    /// </summary>
    protected virtual void OnSingletonInit()
    {
        // 默认实现为空，派生类可以添加自己的初始化逻辑。
    }
}