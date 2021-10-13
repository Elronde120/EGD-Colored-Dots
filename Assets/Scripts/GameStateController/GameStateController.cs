using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameStateController : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        Game
    };

    public GameState CurrentState => _currentState;
    private GameState _currentState = GameState.Menu;

    [SerializeField] private MusicController musicController;
    [FormerlySerializedAs("spawner")] [SerializeField] private DotManager manager;
    [SerializeField] private CameraZoom zoom;
    [SerializeField] private UIAlphaSetter mainMenuUIAlphaSetter;
    [SerializeField] private UIAlphaSetter dotsUIAlphaSetter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGameState()
    {
        StartCoroutine(_ToGameStateCor());
    }
    
    public void ToMenuState()
    {
        StartCoroutine(_ToMenuStateCor());
    }

    private IEnumerator _ToGameStateCor()
    {
        musicController.FadeOutStop();
        mainMenuUIAlphaSetter.LerpAlpha(0, 0.5f, false);
        zoom.ZoomLerp(10, 1.5f);
        yield return new WaitForSeconds(1.5f);
        manager.ShowDots(1.5f);
        dotsUIAlphaSetter.LerpAlpha(1,  1.5f, true);
        yield return new WaitForSeconds(2.5f);
        musicController.FadeInResume();
        _currentState = GameState.Game;
    }

    private IEnumerator _ToMenuStateCor()
    {
        musicController.FadeOutStop();
        dotsUIAlphaSetter.LerpAlpha(0,  1.5f, false);
        manager.HideDots(1.5f);
        yield return new WaitForSeconds(1f);
        zoom.ZoomLerp(15, 1.5f);
        yield return new WaitForSeconds(1.5f);
        mainMenuUIAlphaSetter.LerpAlpha(1, 1.5f, true);
        musicController.FadeInResume();
        _currentState = GameState.Menu;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
