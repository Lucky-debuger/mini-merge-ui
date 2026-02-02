using UnityEngine.UI;
using UnityEngine;

[ExecuteAlways] // [ ] Что это?
[RequireComponent(typeof(GridLayoutGroup))] // [ ] Почему пишем за классом?

public class DynamicGridSize : MonoBehaviour
{
    [Tooltip("Cellsize Y at the reference height")] // [ ] Что это?
    public float referenceCellHeight = 108f; // set the right cellsizeY at the reference resolution
    [Tooltip("Canvas height, should match the root canvas's scaler reference Y")]
    public float referenceHeight = 1080f; // set to 1080 cos the project is 1920x1080
    public bool fixedHeight = false;

    private GridLayoutGroup grid;
    private RectTransform rectTransform; // [ ] Чей?

    private void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        UpdateCellSize();
    }

    private void OnRectTransformDimensionsChange() => UpdateCellSize(); // [ ] Что это за строка?

    #if UNITY_EDITOR
    private void OnValidate() => UpdateCellSize(); // [ ] Это что-то типо сигналов?
    #endif

    private void UpdateCellSize()
    {
        if (grid == null || rectTransform == null) return;

        float scaleFactor = rectTransform.rect.height / referenceHeight; // [ ] Что это за строка?

        grid.cellSize = new Vector2(
            rectTransform.rect.width,
            fixedHeight ? referenceCellHeight : referenceCellHeight * scaleFactor
        );
    }

}
