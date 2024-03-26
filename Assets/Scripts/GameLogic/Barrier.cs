using UnityEngine;

namespace Scripts.GameLogic
{
    public class Barrier : MonoBehaviour
    {
        [SerializeField] Rect changeZone;

        public Vector3 MinVector => new(changeZone.x - changeZone.width / 2, changeZone.y - changeZone.height / 2);
        public Vector3 MaxVector => new(changeZone.x + changeZone.width / 2, changeZone.y + changeZone.height / 2);
    }
}
