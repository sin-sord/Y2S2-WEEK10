using NodeCanvas.Framework;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    public class DetectionAT : ActionTask
    {



        float bestCriteriaValue;
        float currentCriteriaValue;

        public float detectionRadius;
        public LayerMask thiefLayerMask;

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

        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
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
                Debug.DrawLine(agent.transform.position, bestTarget.transform.position, Color.red);

            }

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