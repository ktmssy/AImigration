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
    /// 徘徊。未完成
    /// </summary>
    public class SteeringForWander : Steering
    {
        /// <summary>
        /// 半径
        /// </summary>
        public float wanderRadius;

        /// <summary>
        /// 距離
        /// </summary>
        public float wanderDistance;

        /// <summary>
        /// ランダム距離の最大値
        /// </summary>
        public float wanderJitter;

        /// <summary>
        /// 予期速度
        /// </summary>
        private Vector3 desiredVelocity;

        /// <summary>
        /// 円心
        /// </summary>
        private Vector3 circleTarget;

        /// <summary>
        /// 目標地点
        /// </summary>
        private Vector3 wanderTarget;


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
            circleTarget = is2D ? new Vector3(wanderRadius * 0.707f, wanderRadius * 0.707f, 0f) : new Vector3(wanderRadius * 0.707f, 0f, wanderRadius * 0.707f);
        }

        public override Vector3 Force()
        {
            

            return base.Force();
        }
    }
}
