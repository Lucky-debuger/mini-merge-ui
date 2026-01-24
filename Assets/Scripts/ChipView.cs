using UnityEngine;

public class ChipView : MonoBehaviour
{
    public Chip ChipModel { get; private set; }

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] levelSprites;

    private int DefaultSortingOrder;

    private void Awake() // [ ] Почему не в Init. Или Start?
    {
        DefaultSortingOrder = spriteRenderer.sortingOrder;
    }

    public void Init(Chip chipModel)
    {
        ChipModel = chipModel;
        Refresh();
    }

    public void Refresh()
    {
        spriteRenderer.sprite = levelSprites[(int)ChipModel.Type];
    }

    public void SetSortingOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    public void ResetSortingOrder()
    {
        spriteRenderer.sortingOrder = DefaultSortingOrder;
    }
}