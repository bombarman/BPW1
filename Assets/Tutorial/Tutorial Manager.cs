using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _messages;

    private int _messageIndex = 0;

    private void Start()
    {
        OpenMessage();
    }

    private void CloseMessages()
    {
        foreach (var message in _messages)
        {
            message.SetActive(false);
        }
    }

    public void IncreaseIndex()
    {
        _messageIndex++;
        OpenMessage();
    }

    private void OpenMessage()
    {
        CloseMessages();
        _messages[_messageIndex].SetActive(true);   
    }
}
