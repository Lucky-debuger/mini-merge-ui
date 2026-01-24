using UnityEngine;
using UnityEngine.PlayerLoop;

public class SlotView : MonoBehaviour
{
    private Slot _slotModel;

    public void Initialize(Slot slotModel)
    {
        _slotModel = slotModel;
    }
}
