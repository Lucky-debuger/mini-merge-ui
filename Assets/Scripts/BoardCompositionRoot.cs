using UnityEngine;

public class BoardCompositionRoot : MonoBehaviour
{
    [SerializeField] private ChipView chipViewPrefab;
    [SerializeField] private SlotView slotViewPrefab;
    [SerializeField] private Transform contentTransform;
    [SerializeField] private Transform canvasTransform;

    private BoardController _boardController;

    private void Awake()
    {
        _boardController = new BoardController(chipViewPrefab, slotViewPrefab, contentTransform, canvasTransform);
    }
}
