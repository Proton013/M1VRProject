using UnityEngine;

public class EndPanelController : MonoBehaviour
{
    [SerializeField] private GameObject lastLevel = default;
    [SerializeField] private GameObject panel = default;

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        if (lastLevel.transform.childCount == 0)
        {
            panel.SetActive(true);
        }
    }
}
