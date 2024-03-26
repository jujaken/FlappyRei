using UnityEngine.SceneManagement;


namespace Scripts.UI
{
    public class RestartButton : ButtonBasic
    {
        void Awake()
             => Hide();

        public override void Execute()
             => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}