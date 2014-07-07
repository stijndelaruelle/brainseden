using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private static Dictionary<string, SoundEvent> m_SoundEvents;

	// Use this for initialization
	void Start ()
	{
	    m_SoundEvents = new Dictionary<string, SoundEvent>();
	    m_SoundEvents.Clear();
        foreach (SoundEvent soundEvent in GetComponents<SoundEvent>())
        {
            m_SoundEvents.Add(soundEvent.m_SoundEventName, soundEvent);
        }
	}

    public static void PlaySound(string name, GameObject posObject = null, float volume = 4.0f)
    {
        if (m_SoundEvents.ContainsKey(name))
        {
            m_SoundEvents[name].PlaySound(posObject, volume);
        }
    }
}