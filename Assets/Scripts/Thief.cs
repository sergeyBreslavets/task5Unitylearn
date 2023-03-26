using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRender;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _points;
    private int _curentTarget = 0;

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }

        _animator.SetBool("isRun", true);
    }

    private void Update()
    {   
        Transform target = _points[_curentTarget];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        
        float direction = transform.position.x - target.position.x;
        bool isRightDirection = direction < 0;
        _spriteRender.flipX = !isRightDirection;

        if (transform.position == target.position)
        {
            _curentTarget++;

            if (_curentTarget == _path.childCount)
            {
                _curentTarget = 0;
            }
        }
    }
}
