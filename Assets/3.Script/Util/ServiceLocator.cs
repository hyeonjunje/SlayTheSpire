using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRegisterable
{
    public void Init();
}

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator instance;
    public static ServiceLocator Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ServiceLocator>();
                instance.Init();
            }
            return instance;
        }
    }

    private IDictionary<object, IRegisterable> services;

    private void Init()
    {
        services = new Dictionary<object, IRegisterable>();
    }

    public T GetService<T>() where T : MonoBehaviour, IRegisterable
    {
        if (!services.ContainsKey(typeof(T)))
        {
            // ������ �ڽ� �߿� �ִ��� Ȯ���ϰ� �ʱ�ȭ���ش�.
            T manager = FindObjectOfType<T>();
            if(manager != null)
            {
                services[typeof(T)] = manager;
                manager.Init();

                return (T)services[typeof(T)];
            }

            Debug.LogError("ServiceLocator::GetService ���� Ű�Դϴ�.");
            return default(T);
        }

        return (T)services[typeof(T)];
    }
}
