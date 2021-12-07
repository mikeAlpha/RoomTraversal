using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Movement/Grid")]
    [Description("Move the player based on Grid")]
    public class GridMove : ActionTask<Transform>
    {
        public Transform PlayerPos;
        public Vector3 TargetPos;
        public float timeToMove = 0.1f;
        public bool moving = false;
        public bool repeating;

        public string TagToCheck;

        protected override void OnUpdate()
        {
            if (Input.GetKey(KeyCode.W) && !moving)
            {
                if (CheckBound(Vector3.forward))
                    StartCoroutine(Move(Vector3.forward));
            }

            if (Input.GetKey(KeyCode.A) && !moving)
            {
                if (CheckBound(-Vector3.right))
                    StartCoroutine(Move(-Vector3.right));
            }

            if (Input.GetKey(KeyCode.S) && !moving)
            {
                if (CheckBound(-Vector3.forward))
                    StartCoroutine(Move(-Vector3.forward));
            }

            if (Input.GetKey(KeyCode.D) && !moving)
            {
                if (CheckBound(Vector3.right))
                    StartCoroutine(Move(Vector3.right));
            }

            if (!repeating)
                EndAction();
        }

        IEnumerator Move(Vector3 dir)
        {
            moving = true;
            float completedTime = 0f;

            TargetPos = PlayerPos.transform.position + dir;

            while (completedTime < timeToMove)
            {
                PlayerPos.transform.position = Vector3.Lerp(PlayerPos.transform.position, TargetPos, completedTime / timeToMove);
                completedTime += Time.deltaTime;
                yield return null;
            }

            moving = false;
        }

        bool CheckBound(Vector3 dir)
        {

            RaycastHit hitInfo;
            Debug.DrawRay(PlayerPos.transform.position, dir);
            if (Physics.Raycast(PlayerPos.transform.position, dir, out hitInfo, 1f))
            {
                if (hitInfo.collider.tag == TagToCheck)
                {
                    Debug.Log("Hit");
                    return false;
                }
            }
            return true;
        }
    }
}
