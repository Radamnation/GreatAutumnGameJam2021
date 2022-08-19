using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class SiteLock : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void RedirectTo();

    [DllImport("__Internal")]
    private static extern void StartGameEvent();

    public bool m_testAtStartup;
    public bool m_permitLocalHost;
    public string[] m_permittedRemoteHosts;
    public string[] m_permittedLocalHosts;
    public string m_bounceToURL;

    void Reset()
    {
        m_testAtStartup = true;
        m_permitLocalHost = true;
        m_permittedLocalHosts = new string[] { "file://", "http://localhost/", "http://localhost:", "https://localhost/", "https://localhost:" };
    }

    void Start()
    {
#if UNITY_EDITOR
#elif UNITY_WEBGL
        PiracyCheck();
#endif
    }

    public void PiracyCheck()
    {
        // if it's not a valid remote host, bounce the user to the proper URL
        if (!IsValidHost())
        {
            Bounce();
            return;
        }

        StartGameEvent();
    }

    public void Bounce()
    {
        RedirectTo();
    }

    public bool IsValidHost()
    {
        if (IsValidLocalHost())
            return true;

        if (IsValidRemoteHost())
            return true;

        return false;
    }

    public bool IsValidLocalHost()
    {
        if (m_permitLocalHost)
            return (IsValidHost(m_permittedLocalHosts));

        return false;
    }

    public bool IsValidRemoteHost()
    {
        return (IsValidHost(m_permittedRemoteHosts));
    }

    private bool IsValidHost(string[] hosts)
    {
        // check current host against each of the given hosts
        foreach (string host in hosts)
            if (Application.absoluteURL.IndexOf(host) == 0)
                return true;

        return false;
    }
}