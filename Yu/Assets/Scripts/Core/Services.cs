﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Services
{
    private static Dictionary<Type, object> m_services = new Dictionary<Type, object>();


    public static bool Has<T>()
    {
        return m_services.ContainsKey(typeof(T));
    }


    public static T Get<T>()
    {
        return (T)m_services[typeof(T)];
    }


    public static T Set<T>(T service)
    {
        if (m_services.ContainsKey(typeof(T)))
        {
            Debug.LogWarning("Service exists, not replacing, use reset and then set if you really want to do this!");
            return default(T);
        }

        m_services[typeof(T)] = service;
        return service;
    }


    public static void Reset<T>()
    {
        if (Has<T>())
        {
            T item = Get<T>();

            if (item is IDestroyable)
            {
                ((IDestroyable)item).Destroy();
            }

            m_services.Remove(typeof(T));
        }
    }


    public static void ResetAll()
    {
        foreach(var service in m_services.Values)
        {
            if (service is IDestroyable)
            {
                ((IDestroyable)service).Destroy();
            }
        }

        m_services.Clear();
    }
}
