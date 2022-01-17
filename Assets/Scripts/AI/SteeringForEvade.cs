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
    /// <summary>
    /// 避ける
    /// </summary>
    public class SteeringForEvade : Steering
    {
        /// <summary>
        /// 目標
        /// </summary>
        public GameObject Target;

        /// <summary>
        /// 予期速度
        /// </summary>
        private Vector3 desiredVelocity;

        /// <summary>
        /// 避け始まる距離
        /// </summary>
        public float AvoidDistance=5f;

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
        }

        public override Vector3 Force()
        {
            Vector3 toTarget = Target.transform.position - transform.position;

            if (toTarget.magnitude > AvoidDistance)
            {
                return Vector3.zero;
            }

            Vehicle targetVehicle = Target.GetComponent<Vehicle>();

            float lookAheadTime = toTarget.magnitude / (maxSpeed + targetVehicle.Velocity.magnitude);

            desiredVelocity = (transform.position - (Target.transform.position + targetVehicle.Velocity * lookAheadTime)).normalized * maxSpeed;

            return desiredVelocity - vehicle.Velocity;
        }

        public void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, AvoidDistance);
        }
    }
}
