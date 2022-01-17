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
    /// 離れる
    /// </summary>
    public class SteeringForFlee : Steering
    {
        /// <summary>
        /// 目標
        /// </summary>
        public GameObject Target;

        /// <summary>
        /// 目標から離れる距離
        /// </summary>
        public float FearDistance = 20f;

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
        }

        public override Vector3 Force()
        {
            if (Vector3.Distance(Target.transform.position, transform.position) >= FearDistance)
            {
                return Vector3.zero;
            }

            desiredVelocity = (transform.position - Target.transform.position).normalized * maxSpeed;

            if (is2D)
            {
                desiredVelocity.z = 0f;
            }
            else if (isPlanar)
            {
                desiredVelocity.y = 0f;
            }

            //変更値
            return desiredVelocity - vehicle.Velocity;
        }

        public void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, FearDistance);
        }
    }
}
