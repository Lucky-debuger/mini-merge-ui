using UnityEngine;

public class SlotView : MonoBehaviour
{
    public Slot SlotModel { get; private set; }

    public void Init(Slot slotModel)
    {
        SlotModel = slotModel;
    }
}
