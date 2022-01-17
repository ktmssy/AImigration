/******************************
 *
 *　作成者：楊志庄
 *　作成日：#DATE#
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
    /// <summary>
    /// 到達
    /// </summary>
    public class SteeringForArrive : Steering
    {
        /// <summary>
        /// 目標
        /// </summary>
        public GameObject Target;

        /*/// <summary>
        /// 到達距離
        /// </summary>
        public float ArrivalDistance = 0.3f;*/

        /// <summary>
        /// 原則半径
        /// </summary>
        [Tooltip("0の場合はMaxSpeedになります")]
        public float SlowDownDistance = 0f;

        /// <summary>
        /// 予期速度
        /// </summary>
        private Vector3 desiredVelocity;

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
            if(SlowDownDistance==0f)
            {
                SlowDownDistance = maxSpeed;
            }
        }

        public override Vector3 Force()
        {
            //目標への距離
            Vector3 toTarget = Target.transform.position - transform.position;

            if (is2D)
            {
                toTarget.z = 0f;
            }
            else if (isPlanar)
            {
                toTarget.y = 0f;
            }

            float distance = toTarget.magnitude;
            if (distance > SlowDownDistance)
            {
                desiredVelocity = toTarget.normalized * maxSpeed;
            }
            else
            {
                desiredVelocity = toTarget - vehicle.Velocity;
            }

            return desiredVelocity - vehicle.Velocity;
        }

        public void OnDrawGizmos()
        {
            if (Target)
            {
                Gizmos.DrawWireSphere(Target.transform.position, SlowDownDistance);
            }
        }
    }
}
