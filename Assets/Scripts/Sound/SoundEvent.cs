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
    public void PlaySound(GameObject posObject = null, float volume = 4.0f, float minDistance = 4.5f)
	{
	    int amountOfSounds = m_Sounds.Count;
	    if (amountOfSounds > 0)
	    {
	        m_CurrentID = (m_CurrentID + 1) % amountOfSounds;
            //AudioSource.PlayClipAtPoint(m_Sounds[m_CurrentID], posObject != null ? posObject.transform.position : Vector3.zero, volume);
			AudioSource audioSource = PlayClipAtPointCustom(m_Sounds[m_CurrentID],posObject != null ? posObject.transform.position : Vector3.zero);
			audioSource.volume = volume;
			audioSource.minDistance = minDistance;
		}
	}

	private AudioSource PlayClipAtPointCustom(AudioClip clip,Vector3 position)
	{
		GameObject tempAS = new GameObject("PlayClipAtPointAudioSource"); // create the temp object
		tempAS.transform.position = position; // set its position
		AudioSource audioSource = tempAS.AddComponent<AudioSource>(); // add an audio source
		audioSource.clip = clip; // define the clip
		audioSource.Play(); // start the sound
		Destroy(tempAS, clip.length); // destroy object after clip duration
		return audioSource; // return the AudioSource reference
	}
}