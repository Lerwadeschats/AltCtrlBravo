using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDetection : MonoBehaviour
{
    GridSquare[,] grid;
    [SerializeField] int _unitSize;
    [SerializeField] float marginErrorPos = 10f;
    [SerializeField] Texture2D _texture;
    
    private void Start()
    {
        grid = SetTextureGrid(_texture);
        
    }
    

    /*public GridSquare[,] NewGrid(int width,  int height, Vector2 beginningPos)
    {
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                this.grid[x, y] = new GridSquare(beginningPos.x - ((x - (width / 2)) * _unitSize), beginningPos.y - ((y - (height / 2))) * _unitSize, _unitSize);
            }
        }
        return grid;
    }*/

    /*public List<Vector2> GetAllBlackCases(Texture2D runeTexture)
    {
        List<Vector2> blackCases = new List<Vector2>();
        Color[] pixels = runeTexture.GetPixels();
        
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i] == Color.black)
            {
                Vector2 pos = new Vector2(i % runeTexture.width - 1, (int)Mathf.Floor(i / runeTexture.width - 1));
                blackCases.Add(pos);
            }
        }
        return blackCases;
    }*/

    

    //Set a grid on the texture
    public GridSquare[,] SetTextureGrid(Texture2D texture)
    {
        GridSquare[,] textureGrid = new GridSquare[texture.width/_unitSize, texture.height/ _unitSize];
        for (int x = 0; x < texture.width/ _unitSize; x ++)
        {
            for (int y = 0; y < texture.height/_unitSize; y ++)
            {
                textureGrid[x, y] = new GridSquare(x, y, _unitSize);
            }
        }
        return textureGrid;
    }

    //Get black cases on texture
    public List<GridSquare> GetAllBlackCases(Texture2D runeTexture, GridSquare[,] textureGrid)
    {
        List<GridSquare> blackCases = new List<GridSquare>();
        Color[] pixels = runeTexture.GetPixels();

        for (int x = 0; x < textureGrid.GetLength(0); x++)
        {
            for (int y = 0; y < textureGrid.GetLength(1); y++)
            {
                GridSquare square = textureGrid[x, y];

                if (runeTexture.GetPixel(square._posX * _unitSize, square._posY * _unitSize) == Color.black)
                {
                    blackCases.Add(square);
                }
            }

        }

        return blackCases;
    }

    public bool DoesBlackCasesContainPoint(Vector2 point)
    {
        List<GridSquare> blackCases = GetAllBlackCases(_texture, grid);
        foreach (GridSquare square in blackCases)
        {
            if (square.DoesContainPoint(point, marginErrorPos))
            {
                return true;
            }
        }
        return false;
    }

    
}

public class GridSquare
{
    public int _posX;
    public int _posY;
    int _size;
    public bool _hasBeenChecked = false;
    public GridSquare(float x, float y, int size)
    {
        _posX = (int)x;
        _posY = (int)y;
        _size = size;
    }

    public bool DoesContainPoint(Vector2 point, float margin)
    {
        if((point.x > _posX - margin && point.y < _posX + _size + margin) && point.y > _posY - margin && point.y < _posY + _size + margin)
        {
            _hasBeenChecked = true;
            Debug.Log("caaaaaaaa");
            return true;
        }
        return false;
    }

    
}


