using UnityEngine;
using UnityEngine.UI;

public class DynamicGridSize2 : MonoBehaviour
{
    // Links
    [SerializeField] private RectTransform parentRectTransform; // Относительно кого мы хотим менять размер
    [SerializeField] private GridLayoutGroup gridLayoutGroup; // Кого мы хотим менять

    [SerializeField] private Vector2 referenceCellSize;
    [SerializeField] private Vector2 referenceRectTransformCellSize;

    private void Awake()
    {
        // referenceCellSize = gridLayoutGroup.cellSize; // [ ] Ведь если оставлю так, то будет плохо? Т.к. ссылочный тип данных
        referenceCellSize = new Vector2(
            gridLayoutGroup.cellSize.x, 
            gridLayoutGroup.cellSize.y
        );

        referenceRectTransformCellSize = new Vector2(
            parentRectTransform.rect.width,
            parentRectTransform.rect.height
        );
    }

    private void Update()
    {
        UpdateGrid();
    } 

    private void UpdateGrid()
    {
        gridLayoutGroup.cellSize = new Vector2(
            parentRectTransform.rect.width*referenceCellSize.x / referenceRectTransformCellSize.x,
           parentRectTransform.rect.height*referenceCellSize.y /referenceRectTransformCellSize.y
        );
        Debug.Log($"parentRectTransform.rect.width: {parentRectTransform.rect.width}");
        Debug.Log($"parentRectTransform.rect.width*referenceCellSize.x / parentRectTransform.rect.width: {parentRectTransform.rect.width*referenceCellSize.x / parentRectTransform.rect.width}");
        Debug.Log($"gridLayoutGroup.cellSize: {gridLayoutGroup.cellSize}");
    }

}
