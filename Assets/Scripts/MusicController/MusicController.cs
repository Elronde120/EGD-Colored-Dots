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
        FadeInResume();
        
        MusicManager.instance.SongAlmostDone += () =>
        {
            MusicManager.instance.SmoothChangeVolume(0, 1.2f);
        };

        MusicManager.instance.SongDone = () =>
        {
            int nextSongindex = _ChooseNextSongIndex(_previousSongIndex);
            _previousSongIndex = nextSongindex;
            MusicManager.instance.SwitchTrack(nextSongindex, false);
            MusicManager.instance.Resume();
            MusicManager.instance.SmoothChangeVolume(maxVolume, 1.5f);
        };

    }

    private void Update()
    {

    }

    //To be called by game state manager
    public void FadeOutStop()
    {
        StartCoroutine(_FadeOutStopCor());
    }

    private IEnumerator _FadeOutStopCor()
    {
        MusicManager.instance.SmoothChangeVolume(0, 1.5f);
        yield return new WaitForSeconds(1.5f);
        MusicManager.instance.Stop();
    }
    
    //To be called by game state manager
    public void FadeInResume()
    {
        int nextSongindex = _ChooseNextSongIndex(_previousSongIndex);
        _previousSongIndex = nextSongindex;
        MusicManager.instance.SwitchTrack(_ChooseNextSongIndex(nextSongindex), false);
        MusicManager.instance.SetVolume(0);
        MusicManager.instance.Resume();
        MusicManager.instance.SmoothChangeVolume(maxVolume, 1.5f);
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
