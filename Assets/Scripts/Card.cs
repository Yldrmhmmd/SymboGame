using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

public class Card : MonoBehaviour
{
    public Image cardImage;

    public Sprite cardBack;
    public Sprite cardFront;

    public bool selected;

    public Desk desk;

    private void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        cardImage.sprite = cardBack;
    }
    public void OnCardClick()
    {
        desk.ClickCard(this);
    }
    public void Set(Sprite f, Sprite b)
    {
        cardFront = f;
        cardBack = b;
    }
    public void Show()
    {
        float t = desk.cardTurnTime;
        transform.localScale = new Vector3(1f, 1f, 1f);

        Tween.Rotation(transform,
            new Vector3(0f, 180f, 0f),
            t);

        Tween.Delay(t / 2 - desk.cardTurnTimeAlt, () => cardImage.sprite = cardFront);
        Tween.Delay(t / 2 - desk.cardTurnTimeAlt, () => transform.localScale = new Vector3(-1f, 1f, 1f));
        selected = true;
    }
    public void Hide()
    {
        float t = desk.cardTurnTime;
        transform.localScale = new Vector3(-1f, 1f, 1f);

        Tween.Rotation(transform,
            new Vector3(0f, 0f, 0f),
            t);

        Tween.Delay(t / 2 - desk.cardTurnTimeAlt, ()=> cardImage.sprite = cardBack);
        Tween.Delay(t / 2 - desk.cardTurnTimeAlt, ()=> transform.localScale = new Vector3(1f, 1f, 1f));
        selected = false; 
    }
}