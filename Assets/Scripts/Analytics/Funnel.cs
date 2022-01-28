using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Analytics;

public class Funnel
{
    static Funnel _instance;

    public static Funnel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Funnel();
            }

            return _instance;
        }
    }

    public struct FunnelEvent
    {
        public string name;
        public Dictionary<string, object> data;
    }

    public List<FunnelEvent> funnelEvents = new List<FunnelEvent>();

    public Funnel()
    {
        Timer();
    }

    public async void Timer()
    {
        while (Application.isPlaying)
        {
            await Task.Delay(120_000);

            try
            {
                foreach (FunnelEvent funnelEvent in funnelEvents)
                {
                    Analytics.CustomEvent(funnelEvent.name, funnelEvent.data);
                }
            }
            catch
            {
                break;
            }

            funnelEvents.Clear();
        }
    }
}
