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

    public void Stop()
    {
        _source.Stop();
    }

    public void Resume(ulong delay = ulong.MinValue)
    {
        _source.Play();
    }


}
