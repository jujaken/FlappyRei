using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.GameLogic
{
    public class BarrierSpawner : MonoBehaviour
    {
        [SerializeField] Rect gameZone;
        [SerializeField] GameObject barrierProtype;
        [SerializeField] int maxNumBarriers;

        [SerializeField] float moveSpeed;
        [SerializeField] float durationAcceleration;
        [SerializeField] AnimationCurve boostAnimation;

        [SerializeField] UnityEvent WasRunned;
        [SerializeField] UnityEvent WasStopped;

        public bool IsActive { get; private set; }

        private readonly List<GameObject> barriers = new();

        void FixedUpdate()
        {
            if (!IsActive) return;

            var lastBarrier = barriers[maxNumBarriers - 1];

            for (int i = 0; i < maxNumBarriers; i++)
            {
                var barrier = barriers[i];
                BarrierMove(barrier);
                if (NeedReset(barrier))
                    SetBarrierPosWithDist(barrier, lastBarrier);

                lastBarrier = barrier;
            }
        }

        public void Run()
        {
            if (barriers.Count == 0)
                CreateBarriers();

            IsActive = true;
            WasRunned?.Invoke();
        }

        public void Stop()
        {
            IsActive = false;
            WasStopped?.Invoke();
        }

        public void Clear()
            => barriers.ForEach(b => { barriers.Remove(b); Destroy(b); });

        private float currentTime = 0;
        private float CalcMoveDistance()
        {
            if (currentTime < durationAcceleration)
                currentTime += Time.deltaTime;

            return moveSpeed * boostAnimation.Evaluate(currentTime);
        }
        private GameObject CreateBarrier()
        {
            var barrier = Instantiate(barrierProtype);
            barriers.Add(barrier);
            return barrier;
        }

        private void CreateBarriers()
        {
            Clear();
            var lastBarrier = CreateBarrier();
            SetBarrierOriginPosition(lastBarrier);

            for (int i = 1; i < maxNumBarriers; i++)
            {
                var barrier = CreateBarrier();
                SetBarrierPosWithDist(barrier, lastBarrier);
                lastBarrier = barrier;
            }
        }

        private void SetBarrierPosWithDist(GameObject barrier, GameObject lastBarrier)
        {
            var lastPos = lastBarrier.transform.position;
            barrier.transform.position = new Vector3(lastPos.x + gameZone.width / maxNumBarriers + BarrierRandomX(barrier), BarrierRandomY(barrier), lastPos.z);
        }

        private float BarrierRandomY(GameObject barrier)
        {
            var barCom = barrier.GetComponent<Barrier>();
            return Random.Range(barCom.MinVector.y, barCom.MaxVector.y);
        }

        private float BarrierRandomX(GameObject barrier)
        {
            var barCom = barrier.GetComponent<Barrier>();
            return Random.Range(barCom.MinVector.x, barCom.MaxVector.x);
        }

        private void SetBarrierOriginPosition(GameObject barrier)
            => barrier.transform.position = new Vector3(GetGameZoneMaxX(), 0, gameObject.transform.position.z);

        private void BarrierMove(GameObject barrier)
            => barrier.transform.position += Vector3.left * CalcMoveDistance();

        private bool NeedReset(GameObject barrier)
            => barrier.transform.position.x < GetGameZoneMinX();

        private float GetGameZoneMinX()
            => gameZone.x - gameZone.width / 2;

        private float GetGameZoneMaxX()
            => gameZone.x + gameZone.width / 2;

        private float GetGameZoneMinY()
            => gameZone.y - gameZone.height / 2;

        private float GetGameZoneMaxY()
            => gameZone.y + gameZone.height / 2;
    }
}