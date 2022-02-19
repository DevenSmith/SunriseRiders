using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

///<Summary>
/// an event manager found at
/// https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events#5cf5960fedbc2a281acd21fa
///</Summary>
namespace Devens
{
    public class EventManager : MonoBehaviour {

        private Dictionary <string, UnityEvent> eventDictionary;

        private static EventManager _eventManager;

        public static EventManager Instance
        {
            get
            {
                if (!_eventManager)
                {
                    _eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

                    if (!_eventManager)
                    {
                        Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
                    }
                    else
                    {
                        _eventManager.Init (); 
                    }
                }

                return _eventManager;
            }
        }

        void Init ()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, UnityEvent>();
            }
        }

        public static void StartListening (string eventName, UnityAction listener)
        {
            if (Instance.eventDictionary.TryGetValue (eventName, out var thisEvent))
            {
                thisEvent.AddListener (listener);
            } 
            else
            {
                thisEvent = new UnityEvent ();
                thisEvent.AddListener (listener);
                Instance.eventDictionary.Add (eventName, thisEvent);
            }
        }

        public static void StopListening (string eventName, UnityAction listener)
        {
            if (_eventManager == null) return;
            if (Instance.eventDictionary.TryGetValue (eventName, out var thisEvent))
            {
                thisEvent.RemoveListener (listener);
            }
        }

        public static void TriggerEvent (string eventName)
        {
            if (Instance.eventDictionary.TryGetValue (eventName, out var thisEvent))
            {
                thisEvent.Invoke ();
            }
        }
    }
}
