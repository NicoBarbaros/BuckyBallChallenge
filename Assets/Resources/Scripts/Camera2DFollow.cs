using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
		public Transform player1;
		public Transform player2;
		public Transform endPosition;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;


        private float mOffsetZ;
        private Vector3 mLastTargetPosition;
        private Vector3 mCurrentVelocity;
        private Vector3 mLookAheadPos;
		private Vector3 startingPosition;
		private float distance1;
		private float distance2;

		private bool start = true;
        // Use this for initialization
        private void Start()
        {
			start = true;
			startingPosition = player1.position;
			mLastTargetPosition = startingPosition;
			mOffsetZ = (transform.position - startingPosition).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
			distance1 = Vector3.Distance (endPosition.position, player1.position);
			distance2 = Vector3.Distance (endPosition.position, player2.position);
			if (distance1 < distance2) 
			{
				Debug.Log("Target is player1");
				AttachToFrontPlayer(player1);

			}

			else
			{
				AttachToFrontPlayer(player2);
			}
//			while(start)
//			{
//				if (this.transform.position == startingPosition) 
//				{
//					Debug.Log("Im here");
//					start  = false;
//				}
//			}


           
        }

		void AttachToFrontPlayer(Transform target)
		{
			// only update lookahead pos if accelerating or changed direction
			float xMoveDelta = (target.position - mLastTargetPosition).x;
			
			bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
			
			if (updateLookAheadTarget)
			{
				mLookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
			}
			else
			{
				mLookAheadPos = Vector3.MoveTowards(mLookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
			}
			
			Vector3 aheadTargetPos = target.position + mLookAheadPos + Vector3.forward*mOffsetZ;
			Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref mCurrentVelocity, damping);
			
			transform.position = newPos;
			
			mLastTargetPosition = target.position;
		}
    }
}
