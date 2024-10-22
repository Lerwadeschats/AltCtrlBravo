
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class DrawTablet : MonoBehaviour
{
    public DrawingAction _inputActions;

    bool _isDrawing;

    Vector2 _beginningPos;
    Vector2 _currentPos;

    List<Vector2> _drawPos = new List<Vector2>();

    SpriteRenderer _gridSprite;

    [SerializeField] List<RuneObject> _allRunes = new List<RuneObject>();
    public List<RuneObject> _drawnRunes = new List<RuneObject>();

    List<TrailRenderer> _drawingTrail = new List<TrailRenderer>();
    TrailRenderer _currentTrail;
    [SerializeField] TrailRenderer _drawingPrefab;

    public Shaker shaker;

    [SerializeField] GameObject _cursorPrefab;

    GameObject _cursor;
    //Debug
    [Header("Debug variables")]
    
    List<Vector2> _blackCasesPos = new List<Vector2>();
    RuneObject rune;
    GameObject tile;
    
    private void Awake()
    {
        _gridSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        
        _cursor = Instantiate(_cursorPrefab, NewPosOnGrid(Pointer.current.position.ReadValue()), Quaternion.identity);
        Cursor.visible = false;
        _inputActions.DrawingMap.Draw.Enable();

        //Debug => afficher une rune en grisé
        /*rune = _runes[Random.Range(0, _runes.Count)];
        float squareSizeGrid = _grid.GetComponent<SpriteRenderer>().bounds.size.x / 8;
        Vector2 originPos = new Vector2(_grid.GetComponent<SpriteRenderer>().bounds.min.x, _grid.GetComponent<SpriteRenderer>().bounds.min.y);
        foreach (GridSquare blackCase in GridDetection.GetAllBlackCases(rune._runeDetectionMap, 50))
        {
            Instantiate(tile, new Vector2(blackCase._posX * squareSizeGrid + originPos.x + squareSizeGrid / 2, blackCase._posY * squareSizeGrid + originPos.y + squareSizeGrid / 2), Quaternion.identity);
        }*/
        //

    }

    private void OnDisable()
    {

        Destroy(_cursor);
        Cursor.visible = true;
        shaker.RemoveRune();
        _drawnRunes.Clear();
        _inputActions.DrawingMap.Draw.Disable();
    }

    public void OnDraw(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            if (context.started)
            {
                _isDrawing = true;
                _beginningPos = NewPosOnGrid(Pointer.current.position.ReadValue());
                //Trail
                _currentTrail = Instantiate(_drawingPrefab, _beginningPos, Quaternion.identity);
                _drawingTrail.Add(_currentTrail);
                _currentTrail.AddPosition(_beginningPos);
            }
            else if (context.canceled)
            {
                _isDrawing = false;
            }
        }
        
    }

    public void ValidateRuneDrawing()
    {
        GetDrawnRune();
        ResetDrawing();
    }

    public void ResetDrawing()
    {

        _blackCasesPos.Clear();
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
    //Add runes
    void GetDrawnRune()
    {
        float squareSizeGrid = _gridSprite.bounds.size.x / 8;
        Vector2 originPos = new Vector2(_gridSprite.bounds.min.x, _gridSprite.bounds.min.y);

        foreach (var rune in _allRunes)
        {
            if (GridDetection.IsDrawingInBlackCases(_drawPos, rune._runeDetectionMap, squareSizeGrid, originPos, 0.2f) && !_drawnRunes.Contains(rune))
            {
                
                if(_drawnRunes.Count < 3)
                {
                    shaker.AddToShaker(rune);
                    _drawnRunes.Add(rune);
                    
                }
            }
        }
    }

    private void Update()
    {
        _currentPos = NewPosOnGrid(Pointer.current.position.ReadValue());
        _cursor.transform.position = _currentPos;
        if (_isDrawing)
        {
            
            _drawPos.Add(_currentPos);
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



}
