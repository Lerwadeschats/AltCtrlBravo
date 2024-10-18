using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;

public static class GridDetection
{
    
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
    public static GridSquare[,] SetTextureGrid(Texture2D texture, int _unitSize)
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
    public static List<GridSquare> GetAllBlackCases(Texture2D runeTexture, int _unitSize)
    {
        GridSquare[,] textureGrid = SetTextureGrid(runeTexture, _unitSize);
        List<GridSquare> blackCases = new List<GridSquare>();
        Color[] pixels = runeTexture.GetPixels();

        for (int x = 0; x < textureGrid.GetLength(0); x++)
        {
            for (int y = 0; y < textureGrid.GetLength(1); y++)
            {
                GridSquare square = textureGrid[x, y];

                if (runeTexture.GetPixel(square._posX * _unitSize,square._posY * _unitSize) == Color.black)
                {

                    blackCases.Add(square);
                }
            }
        }
        return blackCases;
    }

    public static bool IsDrawingInBlackCases(List<Vector2> points, Texture2D texture, float newSquareSize, Vector2 originPoint)
    {
        List<GridSquare> blackCases = GetAllBlackCases(texture, 50);
        foreach (Vector2 point in points)
        {
            for (int i = 0; i < blackCases.Count; i++)
            {
                GridSquare square = blackCases[i];
                if (square.DoesContainPoint(point, 0.1f, newSquareSize, originPoint))
                {
                    square.isOccupied = true;
                    break;
                }
                else
                {
                    if (i == blackCases.Count - 1)
                    {
                        //Debug.Log(point + " est hors du chemin");
                        return false;
                    }
                }
            }
            
        }
        foreach (GridSquare square in blackCases)
        {
            if (!square.isOccupied)
            {
                //Debug.Log("Dessin pas fini");
                return false;
            }
        }

        return true;
    }

    
}

public class GridSquare
{
    //PosX & posY sont égaux à 1, 2, 3... pas à 50, 100, 150
    public int _posX;
    public int _posY;
    int _size;
    public bool isOccupied;
    public GridSquare(float x, float y, int size)
    {
        _posX = (int)x;
        _posY = (int)y;
        _size = size;
    }

    public bool DoesContainPoint(Vector2 point, float margin, float newSquareSize, Vector2 originPoint)
    {
        float minX = (_posX - margin) * newSquareSize + originPoint.x;
        float maxX = (_posX + 1 + margin) * newSquareSize + originPoint.x;
        
        float minY = (_posY - margin) * newSquareSize + originPoint.y;
        float maxY = ((_posY + 1 + margin) * newSquareSize + +originPoint.y);

        //Debug.Log(minX + " < " + point.x + " < " + maxX + " || " + "(" + _posY  + "-" + margin + ") * " + newSquareSize + " + " + originPoint.y + " = " + minY + " < " + point.y + " < " + maxY);

        if (point.x > minX && 
            point.x < maxX && 
            point.y > minY && 
            point.y < maxY)
        {
            return true;
        }
        return false;
    }

    
}


