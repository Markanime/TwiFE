using UnityEngine;

public class BoxSizeByText : MonoBehaviour
{
    public float border = 0;
    public float speed = 50;
    public TextPrinter textPrinter;
    public TypingAnimation TypingAnimation;

    private RectTransform m_rect;
    private Vector2 m_pos;

    private void Awake()
    {
        m_rect = GetComponent<RectTransform>();
        m_pos = m_rect.anchoredPosition;
    }

    private void OnEnable()
    {
        m_rect.sizeDelta = new Vector2(1,1);
        m_rect.anchoredPosition = m_pos;
    }

    // Update is called once per frame
    void Update()
    {
        m_rect.sizeDelta = Vector2.MoveTowards(m_rect.sizeDelta, NewSize(), speed * Time.deltaTime);
        m_rect.anchoredPosition = new Vector2(m_pos.x - border / 2, m_pos.y + border / 2);
    }

    private Vector2 NewSize()
    {
        int characters = TypingAnimation.Length;
        float sizeX = textPrinter.RealSize.x;
        float sizeY = textPrinter.RealSize.y;

        int maxLineChars = (int)(textPrinter.GetComponent<RectTransform>().sizeDelta.x / textPrinter.size);
        bool multiline = characters > maxLineChars;
        float width = multiline ? sizeX * maxLineChars : sizeX * characters;
        float height = multiline ? sizeY * ((characters/maxLineChars)+1) : sizeY;
        return new Vector2(width + border, height + border);
    }
}
