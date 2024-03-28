using Scripts.GameLogic;
using UnityEngine;

namespace Scripts.Data
{
    public class ScoreDataRepo : MonoBehaviour
    {
        [SerializeField] GameObject score;

        public string DataKey { get; private set; } = "player_score";

        private int CurScores => score.GetComponent<Score>().Scores;

        public int GetData()
            => PlayerPrefs.GetInt(DataKey, 0);

        public void UpdateData()
        {
            if (CurScores > GetData())
                PlayerPrefs.SetInt(DataKey, CurScores);
        }
    }
}