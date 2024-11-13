using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class UIShakerShake : MonoBehaviour
{
    [SerializeField] Shaker _shaker;

    [SerializeField] float _shakePositionSpeed = 1f;
    [SerializeField] float _shakeForcePositionX = 0.2f;
    [SerializeField] float _shakeForcePositionY = 0.2f;
    [SerializeField] float _shakeRotationSpeed = 1f;
    [SerializeField] float _shakeForceRotation = 0.2f;
    [SerializeField] float _timeLerpReplace = 0.2f;

    Vector3 _initialPosition;
    Vector3 _initialRotation;
    Vector3 _seed;
    Vector3 _lastPosition;
    float _timer = 0f;
    Coroutine _coroutine;

    private void Awake()
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localEulerAngles;
    }

    void Start()
    {
        _shaker.OnShakeStarted += OnShakeStarted;
        _shaker.OnShakePaused += OnShakePaused;
    }
    private void OnShakeStarted()
    {
        if (_coroutine == null)
        {
            _seed.x = Random.Range(0,50);
            _seed.y = Random.Range(0,50);
            _seed.z = Random.Range(0,50);
            _coroutine = StartCoroutine(RoutineShake());
        }
    }

    private void OnShakePaused()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            transform.localPosition = _initialPosition;
            transform.localEulerAngles = _initialRotation;
        }
    }


    IEnumerator RoutineShake()
    {
        while (true) 
        {
            Move();
            Rotate();
            yield return null;
            _timer += Time.deltaTime;
        }
    }

    void Move()
    {
        float force = _timer * _shakePositionSpeed;
        float sampleX = Mathf.PerlinNoise(_seed.x + force, 0f);
        float offsetX = (sampleX * 2 - 1) * _shakeForcePositionX;
        float sampleY = Mathf.PerlinNoise(_seed.y + force, 1f);
        float offsetY = (sampleY * 2 - 1) * _shakeForcePositionY;
        transform.localPosition = _initialPosition + new Vector3(offsetX,offsetY);
    }

    void Rotate()
    {
        float force = _timer * _shakeRotationSpeed;
        float sample = Mathf.PerlinNoise(_seed.z + force, 0.5f);
        float offsetRotation = (sample * 2 - 1) * _shakeForceRotation;
        transform.localEulerAngles = _initialRotation + new Vector3(0f, 0f, offsetRotation);
    }

    IEnumerator Replace(Vector3 position,Vector3 rotation)
    {
        float timer = 0f;
        Vector3 startPosition = transform.localPosition;
        Vector3 startRotation = transform.localEulerAngles;
        while (timer < _timeLerpReplace)
        {
            timer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPosition, position, timer / _timeLerpReplace);
            transform.localEulerAngles = Vector3.Lerp(startRotation, rotation, timer / _timeLerpReplace);
            yield return null;
        }
    }
}
