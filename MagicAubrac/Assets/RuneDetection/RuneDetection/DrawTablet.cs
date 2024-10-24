
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using IIMEngine.SFX;
using static UnityEngine.Rendering.DebugUI;

public class DrawTablet : MonoBehaviour
{
    public DrawingAction _inputActions;

    bool _isDrawing;

    Vector2 _beginningPos;
    [SerializeField] Vector2 _currentPos;

    [SerializeField] List<Vector2> _drawPos = new List<Vector2>();

    SpriteRenderer _gridSprite;

    [SerializeField] List<RuneObject> _allRunes = new List<RuneObject>();
    [SerializeField] UIRunes _uiRunes;
    public List<RuneObject> _drawnRunes = new List<RuneObject>();

    List<TrailRenderer> _drawingTrail = new List<TrailRenderer>();
    TrailRenderer _currentTrail;
    [SerializeField] TrailRenderer _drawingPrefab;

    public Shaker shaker;

    [SerializeField] GameObject _cursorPrefab;

    CursorTablet _cursor;

    //Debug
    [Header("Debug variables")]
    [SerializeField] List<Vector2> _blackCasesPos = new List<Vector2>();
    RuneObject rune;
    [SerializeField] GameObject tile;
    List<GameObject> _blackCases = new List<GameObject>();

    [Foldout("Audio")]
    [SerializeField] string clipDrawing;
    [Foldout("Audio")]
    [SerializeField] string clipWrongRune;

    private void Awake()
    {
        _gridSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    

    private void OnEnable()
    {
        
        _cursor = Instantiate(_cursorPrefab, NewPosOnGrid(Pointer.current.position.ReadValue()), Quaternion.identity).GetComponent<CursorTablet>();
        Cursor.visible = false;
        _inputActions.DrawingMap.Draw.Enable();

        //Debug => afficher une rune en grisé
        /*rune = _allRunes[Random.Range(0, _allRunes.Count)];
        float squareSizeGrid = _gridSprite.bounds.size.x / 8;
        Vector2 originPos = new Vector2(_gridSprite.bounds.min.x, _gridSprite.bounds.min.y);
        
        foreach (GridSquare blackCase in GridDetection.GetAllBlackCases(rune._runeDetectionMap, 50))
        {
            _blackCasesPos.Add(new Vector2(blackCase._posX, blackCase._posY));
            _blackCases.Add(Instantiate(tile, new Vector2(blackCase._posX * squareSizeGrid + originPos.x + squareSizeGrid / 2, blackCase._posY * squareSizeGrid + originPos.y + squareSizeGrid / 2), Quaternion.identity));
            
        }*/
        //

    }

    private void OnDisable()
    {
        Destroy(_cursor.gameObject);
        Cursor.visible = true;
        ResetRunes();
        _inputActions.DrawingMap.Draw.Disable();
    }

    public void OnDraw(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            if (context.started)
            {
                SFXsManager.Instance.PlaySound(clipDrawing);
                _cursor.StartParticles();
/*                _blackCasesPos.Clear();*/
                _isDrawing = true;
                _beginningPos = NewPosOnGrid(Pointer.current.position.ReadValue());
                //Trail
                _currentTrail = Instantiate(_drawingPrefab, _beginningPos, Quaternion.identity);
                _drawingTrail.Add(_currentTrail);
                _currentTrail.AddPosition(_beginningPos);
            }
            else if (context.canceled)
            {
                SFXsManager.Instance.StopSound(clipDrawing);
                _cursor.StopParticles();
                _isDrawing = false;
            }
        }
        
    }

    public void ResetRunes()
    {
        shaker.RemoveRune();
        _drawnRunes.Clear();
        _uiRunes.ResetRunes();
    }

    public void ValidateRuneDrawing()
    {
        GetDrawnRune();
        ResetDrawing();
    }

    public void ResetDrawing()
    {

        
        _drawPos.Clear();

        if (_drawingTrail.Count > 0)
        {
            foreach (var trail in _drawingTrail)
            {
                Destroy(trail.gameObject);
            }
            _drawingTrail.Clear();
        }
    }
    void GetDrawnRune()
    {
        if (_gridSprite == null) return;
        float squareSizeGrid = _gridSprite.bounds.size.x / 8;
        Vector2 originPos = new Vector2(_gridSprite.bounds.min.x, _gridSprite.bounds.min.y);
        /*foreach (var blackCase in _blackCases)
        {
            DebugIsInSquare(blackCase);
        }*/
        bool isGood = false;
        foreach (var rune in _allRunes)
        {
            if (GridDetection.IsDrawingInBlackCases(_drawPos, rune._runeDetectionMap, squareSizeGrid, originPos, 0.2f) && !_drawnRunes.Contains(rune))
            {
                if (_drawnRunes.Count < 3)
                {
                    shaker.AddToShaker(rune);
                    _drawnRunes.Add(rune);
                    _uiRunes.UpdateUIRunes(_drawnRunes);
                    isGood = true;
                }
            }
        }
        if (!isGood)
        {
            SFXsManager.Instance.PlaySound(clipWrongRune);   
        }
        //Debug
        /*if (GridDetection.IsDrawingInBlackCases(_drawPos, rune._runeDetectionMap, squareSizeGrid, originPos, 0.2f) && !_drawnRunes.Contains(rune))
        {
            Debug.Log("Ca marche tsais");

            if (_drawnRunes.Count < 3)
            {

                shaker.AddToShaker(rune);
                _drawnRunes.Add(rune);
                _uiRunes.UpdateUIRunes(_drawnRunes);
            }
        }*/
    }

    private void Update()
    {
        _currentPos = NewPosOnGrid(Pointer.current.position.ReadValue());
        _cursor.transform.position = _currentPos;

        if (_isDrawing)
        {
            if (!_drawPos.Contains(_currentPos))
            {
                _drawPos.Add(_currentPos);
            }
            
            if(_currentTrail != null)
                _currentTrail.transform.position = _currentPos;
        }
    }

    Vector2 NewPosOnGrid(Vector2 position)
    {
        Vector2 screenSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        float newPosX = position.x / screenSize.x;
        float newPosY = position.y / screenSize.y;

        Vector2 newPos = new Vector2(newPosX, newPosY);
        newPos = Camera.main.ScreenToWorldPoint(newPos);

        Vector2 gridSize = new Vector2(_gridSprite.bounds.size.x, _gridSprite.bounds.size.y);

        
        newPosX *= gridSize.x;
        newPosY *= gridSize.y;

        newPosX += _gridSprite.bounds.min.x;
        newPosY += _gridSprite.bounds.min.y;

        return new Vector2(newPosX, newPosY);
    }

    void DebugIsInSquare(GameObject square)
    {
        SpriteRenderer sprite = square.GetComponent<SpriteRenderer>();
        foreach(Vector2 pos in _drawPos)
        {
            if(pos.x > square.transform.position.x - sprite.bounds.size.x && pos.x < square.transform.position.x + sprite.bounds.size.x && pos.y > square.transform.position.y - sprite.bounds.size.y && pos.y < square.transform.position.y + sprite.bounds.size.y)
            {
                sprite.color = Color.green;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(Vector2 pos in _drawPos)
        {
            Gizmos.DrawSphere(pos, 0.08f);
        }
    }

}
