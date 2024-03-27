using Scripts.Data;
using Scripts.GameLogic;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class ScoreText : UIBasic
    {
        [SerializeField] GameObject score;
        [SerializeField] GameObject scoreDataRepo;

        private TMP_Text textMesh;

        private int Scores => score.GetComponent<Score>().Scores;
        private int MaxScores => scoreDataRepo.GetComponent<ScoreDataRepo>().GetData();

        void Start()
        {
            textMesh = gameObject.GetComponent<TMP_Text>();
            UpdateScores();
            Hide();
        }

        public void UpdateScores()
           =>  textMesh.text = $"{Scores}/{MaxScores}";
    }
}