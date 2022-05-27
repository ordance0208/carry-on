using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridScaler : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    private RectTransform gridRect;
    [SerializeField] private float spacePercentage;
    [SerializeField] private RectTransform referenceCanvas;

    private void Awake()
    {
        gridRect = grid.GetComponent<RectTransform>();
    }

    private void Start()
    {
        Debug.Log(gridRect.rect.width);
        float spacing = (gridRect.rect.width - (gridRect.rect.width * (1f - spacePercentage))) / (grid.constraintCount - 1f);
        float size = (gridRect.rect.width - (gridRect.rect.width * spacePercentage)) / grid.constraintCount;
        grid.spacing =  new Vector2 (spacing, spacing);
        grid.cellSize = new Vector2(size, size);
    }

}
