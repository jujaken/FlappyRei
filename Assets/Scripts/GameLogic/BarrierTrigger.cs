using UnityEngine;

namespace Scripts.GameLogic
{
    public class BarrierTrigger : MonoBehaviour
    {
        private GameObject score;

        void Start()
            => score = GameObject.Find("Score");

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == null || collision.gameObject.name != "Player" || score == null) return;
            score.GetComponent<Score>().UpScores();
        }
    }
}
