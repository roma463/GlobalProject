using System.Collections;
using UnityEngine;

public class ChoisePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _prefabPlayer;
    [SerializeField] private int _countPlayer;
    [SerializeField] private float _distancePlayer;
    [SerializeField] private Transform _playerParant;
    private Coroutine _corutine;
    private int _currentPlayer;

    private void Start()
    {
        float positionPlayers = 0;
        int directionOffectPrefab = 1;
        for (int i = 0; i < _countPlayer; i++)
        {
            var player = Instantiate(_prefabPlayer,  _playerParant);
             player.transform.position = new Vector2(positionPlayers * directionOffectPrefab, _playerParant.position.y);
            if (i%2==0)
                positionPlayers += _distancePlayer;
            directionOffectPrefab *= -1;
        }
    }
    private void Update()
    {
        if (_corutine == null)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (_currentPlayer < 5)
                    _corutine = StartCoroutine(MovementPlayers(1));
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_currentPlayer > -5)
                    _corutine = StartCoroutine(MovementPlayers(-1));
            }
        }
    }
    private IEnumerator MovementPlayers(int ditection)
    {
        _currentPlayer += ditection;
        var targetPosition = (Vector2)_playerParant.position + Vector2.right * _distancePlayer * ditection;
        while((Vector2)_playerParant.position != targetPosition)
        {
            _playerParant.position = Vector2.MoveTowards(_playerParant.position, targetPosition, .5f);
            yield return null;
        }
        _corutine = null;
    }
}
