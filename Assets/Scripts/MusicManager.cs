using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    public bool playTrackZeroOnStart = false;
    [SerializeField] private AudioClip[] musicTracks = new AudioClip[0];
    private AudioSource _source;
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        if (playTrackZeroOnStart)
            SwitchTrack(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Switches the current track to the given track, plays immediately
    /// </summary>
    /// <param name="trackIndex">The track to play on <see cref="musicTracks"/></param>
    /// <param name="delay">How long to delay playing</param>
    /// <returns>True if switching and playing was successful. False otherwise</returns>
    public bool SwitchTrack(int trackIndex, ulong delay = ulong.MinValue)
    {
        if (trackIndex < 0 || trackIndex >= musicTracks.Length)
        {
            return false;
        }

        if (_source == null)
            return false;

        Stop();
        _source.clip = musicTracks[trackIndex];
        Resume(delay);
        return true;

    }

    /// <summary>
    /// Stops the music immediately, if playing
    /// </summary>
    public void Stop()
    {
        _source.Stop();
    }

    /// <summary>
    /// Resumes music playing if stopped. Must have a audio clip prepared.
    /// </summary>
    /// <remarks>
    /// Call <see cref="SwitchTrack"/> if unsure if audio clip is prepared.
    /// </remarks>
    /// <param name="delay"></param>
    public void Resume(ulong delay = ulong.MinValue)
    {
        _source.Play();
    }


}
