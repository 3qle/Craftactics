using System;
using TMPro;

namespace Script.UI
{
    [Serializable]
    public class UIScore
    {
        public TextMeshProUGUI ScoreText, ComboText;
        public int combo;
       public int score;

        public int SetScore(int reward, int coins)
        {
            score = coins;
            combo = reward > 0 ? combo+=1 : 1;
            int j = reward * combo;
            score = reward > 0 ? score += j : score;
            SetText();
            return j;
        }
            

       

        void SetText()
        {
            ComboText.text = $"Combo: {combo}";
            ScoreText.text = $"Coins: {score.ToString()}";
        }
    }
}