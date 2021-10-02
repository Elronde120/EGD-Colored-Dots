using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private DotSpawner spawner;
    
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

    private IEnumerator _ToGameStateCor()
    {
        musicController.FadeOutStop();
        yield return new WaitForSeconds(1.5f);
        spawner.DrawDots(1.5f);
        yield return new WaitForSeconds(2.5f);
        musicController.FadeInResume();
        _currentState = GameState.Game;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
