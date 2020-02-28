using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private List<Dialog> dialogs;
    [SerializeField]
    private GameObject dialogPanel;

    private int currentDialogIndex;

    private void Awake()
    {
        currentDialogIndex = 0;
    }

    private void Start()
    {
        EventHandler.Instance.AddListener(EventType.DialogOff, OnDialogOff);

        dialogs.ForEach(d =>
        {
            d.gameObject.SetActive(false);
        });

        dialogs[currentDialogIndex].gameObject.SetActive(true);
    }

    private void OnDialogOff()
    {
        if (currentDialogIndex < (dialogs.Count - 1))
        {
            Debug.Log(currentDialogIndex);
            currentDialogIndex++;
            dialogs[currentDialogIndex].gameObject.SetActive(true);
            Debug.Log(currentDialogIndex);
        } else
        {
            dialogPanel.SetActive(false);
        }
    }
}
