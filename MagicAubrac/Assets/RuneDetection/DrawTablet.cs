using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class DrawTablet : MonoBehaviour
{
    public DrawingAction _inputActions;

    bool isDrawing = false;

    Vector2 _beginningPos;
    Vector2 _currentPos;

    List<Vector2> _drawPos = new List<Vector2>();

    SpriteRenderer _gridSprite;

    [SerializeField] List<RuneObject> _allRunes = new List<RuneObject>();
    public List<RuneObject> _drawnRunes = new List<RuneObject>();

    TrailRenderer _drawingTrail;
    [SerializeField] TrailRenderer _drawingPrefab;

    public Shaker shaker;

    //Debug
    [Header("Debug variables")]
    
    List<Vector2> _blackCasesPos = new List<Vector2>();
    RuneObject rune;
    GameObject tile;
    private void Awake()
    {
        _gridSprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        print("Activated");

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
        print("Deactivated");
        shaker.RemoveRune();
        _drawnRunes.Clear();
        _inputActions.DrawingMap.Draw.Disable();
    }

    public void OnDraw(InputAction.CallbackContext context)
    {
        print("fjeajfkleajfkleafjeabkjceazbncezkljvszjvezkbvkjezv");
        if (context.started)
        {
            _blackCasesPos.Clear();
            _drawPos.Clear();
            if(_drawingTrail != null)
            {
                Destroy(_drawingTrail.gameObject);
            }

            //Start drawing
            isDrawing = true;
            _beginningPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());

            //Trail
            _drawingTrail = Instantiate(_drawingPrefab, _beginningPos, Quaternion.identity);
            _drawingTrail.AddPosition(_beginningPos);
        }
        else if (context.canceled)
        {
            isDrawing = false;
            GetDrawnRune(_drawPos);
        }
    }

    //Add runes
    void GetDrawnRune(List<Vector2> drawPosList)
    {
        float squareSizeGrid = _gridSprite.bounds.size.x / 8;
        Vector2 originPos = new Vector2(_gridSprite.bounds.min.x, _gridSprite.bounds.min.y);

        foreach (var rune in _allRunes)
        {
            if (GridDetection.IsDrawingInBlackCases(drawPosList, rune._runeDetectionMap, squareSizeGrid, originPos, 0.2f) && !_drawnRunes.Contains(rune))
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
        if (isDrawing)
        {
            _currentPos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
            _drawPos.Add(_currentPos);
            _drawingTrail.transform.position = _currentPos;
        }
    }

    void LockMouseInRange()
    {
        //A faire
    }

}
