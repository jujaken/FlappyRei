using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.GameLogic
{
    public class Score : MonoBehaviour
    {
        [SerializeField] UnityEvent scoreChanged;

        public int Scores { get; private set; } = 0;

        public void UpScores()
        {
            Scores++;

            if (StaticData.MaxScores < Scores)
                StaticData.MaxScores = Scores;

            scoreChanged?.Invoke();
        }
    }
}