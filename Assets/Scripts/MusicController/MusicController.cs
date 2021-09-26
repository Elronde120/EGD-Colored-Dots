using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicController : MonoBehaviour
{
    [SerializeField] private float maxVolume = 0.5f;
    private int _maxSongIndex = 0;
    private int _previousSongIndex = -1;
    private void Start()
    {
        //tell music manager to start playing a song, but fade in (for all songs)
        _maxSongIndex = MusicManager.instance.MusicTracks.Length;
        MusicManager.instance.SwitchTrack(_ChooseNextSongIndex(_previousSongIndex), false);
        MusicManager.instance.SetVolume(0);
        MusicManager.instance.Resume();
        MusicManager.instance.SmoothChangeVolume(maxVolume, 1.5f);

    }

    private void Update()
    {
        if (_IsCurrentSongDone())
        {
            //if the current song ended, choose next song and fade in
            MusicManager.instance.SwitchTrack(_ChooseNextSongIndex(_previousSongIndex), false);
            MusicManager.instance.SetVolume(0);
            MusicManager.instance.Resume();
            MusicManager.instance.SmoothChangeVolume(maxVolume, 1.5f);
        }
        else if (_IsCurrentSongEnding())
        {
            //if the current song is ending, fade out.
            MusicManager.instance.SmoothChangeVolume(0, 1.5f);
        }
        
        
        
    }
    
    private int _ChooseNextSongIndex(int previousSongIndex)
    {
        int result = previousSongIndex;
        while (result == previousSongIndex)
        {
            result = Random.Range(0, _maxSongIndex);
        }

        return result;
    }

    private bool _IsCurrentSongEnding()
    {
        float timeRemaining = MusicManager.instance.GetTimeRemaining();
        if (timeRemaining == -99f)
        {
            return false;
        }

        return timeRemaining < 1.5f;
    }

    private bool _IsCurrentSongDone()
    {
        float timeRemaining = MusicManager.instance.GetTimeRemaining();
        if (timeRemaining == -99f)
        {
            return false;
        }

        return timeRemaining < 0.0001f;
    }
}