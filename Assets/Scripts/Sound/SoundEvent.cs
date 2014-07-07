using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEvent : MonoBehaviour
{
    public string m_SoundEventName;
    public List<AudioClip> m_Sounds;
    private int m_CurrentID;
	// Use this for initialization
	void Start ()
	{
	    m_CurrentID = 0;
	}
	
	// Update is called once per frame
    public void PlaySound(GameObject posObject = null, float volume = 4.0f)
	{
	    int amountOfSounds = m_Sounds.Count;
	    if (amountOfSounds > 0)
	    {
	        m_CurrentID = (m_CurrentID + 1) % amountOfSounds;
            AudioSource.PlayClipAtPoint(m_Sounds[m_CurrentID], posObject != null ? posObject.transform.position : Vector3.zero, volume);
	    }
	}
}