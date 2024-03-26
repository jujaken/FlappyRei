using UnityEngine;
namespace Scripts.UI
{
    public class ExitButton : ButtonBasic
    {
        public override void Execute()
            => Application.Quit();
    }
}