using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Desk : MonoBehaviour
{
    [SerializeField] private Card card;
    [SerializeField] private Sprite cardBack;

    [SerializeField] private Sprite[] sprites;
    private List<Sprite> pairs;

    Card first;
    Card second;

    public Manager gm;

    int foundPairs;

    public bool run;

    public bool turn; //0 = player 1, 1 = player 2
    public int player1;
    public int player2;
    public float time;

    public float cardTurnTime;
    public float cardTurnTimeAlt;

    public void Generate()
    {
        foundPairs = 0;
        run = true;
        time = 0f;
        turn = false;
        player1 = 0;
        player2 = 0;
        first = null;
        second = null;
        DoubleSprites();
        CreateCards();
    }
    private void Update()
    {
        if (!run)
            return;
        time += Time.deltaTime;
        gm.currentTime.text = Mathf.RoundToInt(time).ToString();

    }
    private void DoubleSprites()
    {
        pairs = new List<Sprite>();
        for (int i = 0; i < sprites.Length; i++)
        {
            pairs.Add(sprites[i]);
            pairs.Add(sprites[i]);
        }
        for (int i = pairs.Count - 1; i > 0; i--)
        {
            int ri = Random.Range(0, i + 1);

            Sprite nan = pairs[i];
            pairs[i] = pairs[ri];
            pairs[ri] = nan;
        }
    }
    public void ClickCard(Card card)
    {
        if (!card.selected)
        {
            if (first == null)
            {
                first = card;
                card.Show();
                return;
            }
            if (second == null)
            {
                second = card;
                StartCoroutine(Pair(first, second));
                first = null;
                second = null;
                card.Show();
            }
            else
                return;
        }
    }
    IEnumerator Pair(Card a, Card b)
    {
        yield return new WaitForSeconds(cardTurnTime);

        if (a.cardFront == b.cardFront)
        {
            foundPairs++;

            if (!turn)
                player1++;
            if (turn)
                player2++;

            if (foundPairs >= pairs.Count / 2)
                gm.FinishGame();
            else
                gm.cardChooseCorrect.Play();
        }
        else
        {
            gm.cardChooseIncorrect.Play();
            a.Hide();
            b.Hide();
        }

        if (gm.mode == 1)
        {
            turn = !turn;
            //gm.currentFirstScore.text = player1.ToString();
            //gm.currentSecondScore.text = player2.ToString();
            if (!turn)
                gm.currentTurn.text = "1.";
            if (turn)
                gm.currentTurn.text = "2.";
        }
    }
    private void CreateCards()
    {
        for (int i = 0; i < pairs.Count; i++)
        {
            Card c = Instantiate(card, transform);
            c.Set(pairs[i], cardBack);
            c.desk = this;
        }
    }
}