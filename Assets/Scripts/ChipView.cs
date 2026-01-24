using UnityEngine.UI;
using UnityEngine;

public class ChipView : MonoBehaviour
{
    [SerializeField] private Image image; 
    private Chip _chipModel;

    // [ ] Нужно ли знать ChipView о SlotView?

    private void Initialize(Chip chipModel)
    {
        _chipModel = chipModel;
    }
}
