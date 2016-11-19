using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    const string uiCanvasName = "UICanvas";
    const string playerNameTextName = "PlayerName";
    const string playerHealthIndicatorName = "Health";
    public GameObject UIPanelPrefab;
    public Vector2 uiOffset = new Vector2(0.0f, 0.05f);
    GameObject uiInstance;
    RectTransform instanceTransform;
    Text playerNameBox;
    RectTransform uiCanvas;

    void Start()
    {
        if (uiInstance != null) return;
        uiInstance = Instantiate(UIPanelPrefab);
        Transform textTransform = uiInstance.transform.FindChild(playerNameTextName);
        playerNameBox = textTransform.GetComponent<Text>();
        instanceTransform = uiInstance.GetComponent<RectTransform>();

        GameObject uiCan = GameObject.Find(uiCanvasName);
        uiInstance.transform.SetParent(uiCan.transform, false);
        uiCanvas = uiCan.GetComponent<RectTransform>();
    }

    public void SetPlayerName(string name)
    {
        if (uiInstance == null)
        {
            Start();
        }
        playerNameBox.text = name;
    }

    public void UpdatePosition(Vector3 pos)
    {
        if(uiInstance == null)
        {
            Start();
        }

        Vector2 viewPos = Camera.main.WorldToViewportPoint(pos);
        viewPos += uiOffset;
        Vector2 screenPoint = new Vector2(
            viewPos.x * uiCanvas.sizeDelta.x - uiCanvas.sizeDelta.x * 0.5f,
            viewPos.y * uiCanvas.sizeDelta.y);

        instanceTransform.anchoredPosition = screenPoint;
    }
}
