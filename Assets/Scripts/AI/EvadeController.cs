/******************************
 *
 *　作成者：楊志庄
 *　作成日：2022年01月17日
 *
 ******************************
 *
 *　更新履歴...編集者
 *　1.
 *　2.
 *　3.
 *
 ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YoShiSho
{
    public class EvadeController : MonoBehaviour
    {
        /// <summary>
        /// 目標
        /// </summary>
        public GameObject Target;

        public float LedearBehingDistance = 2f;

        public float evadeDistance;
        private float sqrEvadeDIstance;

        /// <summary>
        /// 予期速度
        /// </summary>
        private Vector3 desiredVelocity;

        private SteeringForEvade evadeScript;

        private Vehicle vehicle;
        private float maxSpeed;
        private bool isPlanar;
        private bool is2D;

        private void Start()
        {
            vehicle = GetComponent<Vehicle>();
            maxSpeed = vehicle.MaxSpeed;
            isPlanar = vehicle.IsPlanar;
            is2D = vehicle.Is2D;
            sqrEvadeDIstance = evadeDistance * evadeDistance;
            evadeScript = GetComponent<SteeringForEvade>();
        }

        private void Update()
        {
            if (Target == null)
            {
                return;
            }
            Vehicle targetVehicle = Target.GetComponent<Vehicle>();
            Vector3 targetAheadPoint = Target.transform.position + targetVehicle.Velocity.normalized * LedearBehingDistance;

            Vector3 toTarget = transform.position - targetAheadPoint;

            if (is2D)
            {
                toTarget.z = 0f;
            }
            else if (isPlanar)
            {
                toTarget.y = 0f;
            }

            if (toTarget.sqrMagnitude < sqrEvadeDIstance)
            {
                evadeScript.enabled = true;
            }
            else
            {
                evadeScript.enabled = false;
            }
        }
    }
}
