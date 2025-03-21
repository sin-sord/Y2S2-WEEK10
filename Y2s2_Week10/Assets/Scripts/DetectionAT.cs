using NodeCanvas.Framework;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    public class DetectionAT : ActionTask
    {

        public float detectionRadius;
        public LayerMask thiefLayerMask;
        public BBParameter<Transform> targetTransform;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {

            Transform bestTarget = null;
            float closestDistance = detectionRadius;

            Collider[] thiefColliders = Physics.OverlapSphere(agent.transform.position, detectionRadius, thiefLayerMask);

            if (thiefColliders.Length == 0)
            {
                return;
            }

            foreach (Collider thiefCollider in thiefColliders)
            {

                float currentDistance = Vector3.Distance(agent.transform.position, thiefCollider.transform.position);
                if (currentDistance < closestDistance)
                {
                    bestTarget = thiefCollider.transform;
                    closestDistance = currentDistance;
                }
            }


            if (bestTarget != null)
            {
                targetTransform.value = bestTarget;
                EndAction(true);
                //                Debug.DrawLine(agent.transform.position, bestTarget.transform.position, Color.red);

            }
            else
            {
                EndAction(false);
            }
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {

        }

        //Called when the task is disabled.
        protected override void OnStop()
        {

        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }
    }
}