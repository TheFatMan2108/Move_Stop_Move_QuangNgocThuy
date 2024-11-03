using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class OffscreenIndicator : MonoBehaviour
{
    public static OffscreenIndicator Instance { get; private set; }
    public Camera activeCamera;
    public List<Indicator> targetIndicators = new List<Indicator>();
    public GameObject objectIndicator;
    public float timeCheck = .1f;
    public Vector2 offset;
    private void Awake()
    {
        if(Instance==null)Instance = this;
    }
    private void Start()
    {
        InstanIndicator();
    }
    private void Update()
    {
        foreach (var item in targetIndicators)
        {
            UpdatePosition(item);
        }
    }
    public void InstanIndicator()
    {
        foreach (var item in targetIndicators)
        {
            if (item.indicatorUI == null)
            {
                item.indicatorUI = Instantiate(objectIndicator).transform;
                item.indicatorUI.SetParent(transform);
            }
            var rectransform = item.indicatorUI.GetComponent<RectTransform>();
            if (rectransform == null)
            {
                rectransform = item.indicatorUI.gameObject.AddComponent<RectTransform>();
            }
            item.rectTransform = rectransform;
            if (item.character == null)item.character = item.target.GetComponent<CharacterBase>();
            if (item.txtScore == null)item.txtScore = item.indicatorUI.GetComponentInChildren<TextMeshProUGUI>();
            if (item.image == null) item.image = item.indicatorUI.GetComponent<Image>();
            (item.character as NPC).SetIndicator(item);
            SetScore(item.target);

        }
    }
    public void SetColor(Transform target, Color color)
    {
        foreach (var item in targetIndicators)
        {
            if (target == item.target)
            {
                item.image.color = color;
                return;
            }
        }
    }
    public void SetScore(Transform target)
    {
        foreach (var item in targetIndicators)
        {
            if (target == item.target)
            {
                item.txtScore.text = item.character.score.ToString();
                return;
            }
        }
        
    }

    void UpdatePosition(Indicator indicator)
    {
        var rect = indicator.rectTransform.rect;
        var indicatorPosition = activeCamera.WorldToScreenPoint(indicator.target.position);
        if (indicatorPosition.z < 0)
        {
            indicatorPosition.y = -indicatorPosition.y;
            indicatorPosition.x = -indicatorPosition.x;
        }
        var newPoint = new Vector3(indicatorPosition.x, indicatorPosition.y, indicatorPosition.z);
        indicatorPosition.x = Mathf.Clamp(indicatorPosition.x,rect.width/2,Screen.width-rect.width/2)+offset.x;
        indicatorPosition.y = Mathf.Clamp(indicatorPosition.y,rect.height/2,Screen.height-rect.height/2)+offset.y;
        indicatorPosition.z = 0;
        indicator.indicatorUI.up = (newPoint-indicatorPosition).normalized;
        indicator.indicatorUI.position = Vector3.Lerp(indicator.indicatorUI.position, indicatorPosition,10*Time.deltaTime);
        float xRote = indicator.rectTransform.eulerAngles.x;
        float yRote = indicator.rectTransform.eulerAngles.y;
        if (xRote > 10)
        {
            xRote = 90;
        }
        if (yRote > 10)
        {
            yRote = 0;
        }
        indicator.rectTransform.eulerAngles = new Vector3(xRote, yRote,0);
    }
    [System.Serializable]
    public class Indicator
    {
        [Tooltip("Target vào object của mình")]
        public Transform target;
        public CharacterBase character;
        [Tooltip("Hiển thị lên UI")]
        public Transform indicatorUI;
        public TextMeshProUGUI txtScore;
        public Image image;
        [HideInInspector]
        public RectTransform rectTransform;
    }
}
