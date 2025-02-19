using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AnimationBlicks : MonoBehaviour
{
    [SerializeField] private Transform[] _blocks;
    [SerializeField] private float _delayActiveBlock;
    [SerializeField] private float _speedAnimation;
    [SerializeField] private float _startPosition;
    [SerializeField] private bool _hideIsStart;

    [SerializeField] private AudioClip _clipHit;
    [SerializeField] private UnityEvent _endAnimation;

    private Vector3[] _targetPositionsBlocks;
    private bool[] _stateAnimationBlocks;

    private void Start()
    {
        _targetPositionsBlocks = new Vector3[_blocks.Length];
        _stateAnimationBlocks = new bool[_blocks.Length];

        for (int i = 0; i < _blocks.Length; i++)
        {
            _targetPositionsBlocks[i] = _blocks[i].position;

            if (_hideIsStart)
                _blocks[i].position = (Vector2)_blocks[i].position + (Vector2.down * Mathf.Abs(_startPosition));
        }
    }

    public void StartAnimation() 
    {
        StartCoroutine(StartAnimationWithDelay());
    }

    private IEnumerator StartAnimationWithDelay()
    {
        for (int i = 0; i < _blocks.Length; i++)
        {
            StartCoroutine(MovementBlock(i, _blocks[i], _targetPositionsBlocks[i]));
            yield return new WaitForSeconds(_delayActiveBlock);
        }
    }

    private IEnumerator MovementBlock(int idBlock,Transform block, Vector3 targetPosition)
    {

        while(block.position != targetPosition)
        {
            block.position = Vector3.MoveTowards(block.position, targetPosition, _speedAnimation * Time.deltaTime);
            yield return null;
        }
        CompleteBlockAnimation(idBlock);
    }

    private void CompleteBlockAnimation(int blockId)
    {
        _stateAnimationBlocks[blockId] = true;
        AudioController.StartPlayHit(_clipHit);

        if(_stateAnimationBlocks.All(p=>p == true))
        {
            _endAnimation?.Invoke();
        }
    }
}
