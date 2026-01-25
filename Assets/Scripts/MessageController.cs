using UnityEngine;

public class MessageController : MonoBehaviour
{
    public static MessageController Instance;

    [SerializeField] private MessageView messagePrefab;
    [SerializeField] private Transform parent;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string text)
    {
        var msg = Instantiate(messagePrefab, parent);
        msg.Show(text);
    }
}
