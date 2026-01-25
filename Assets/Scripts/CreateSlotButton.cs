using System;
using UnityEngine;
using UnityEngine.UI;

public class CreateSlotButton : MonoBehaviour
{
    [SerializeField] private Button button;

    public event Action OnButtonClick;

    private void Awake()
    {
        button.onClick.AddListener(OnSlotButtonClick);
    }

    private void OnSlotButtonClick()
    {
        OnButtonClick?.Invoke();
    }
}
