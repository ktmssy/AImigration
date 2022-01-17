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
    [RequireComponent(typeof(SteeringForArrive))]
    public class SteeringForFollowLeader : Steering
    {
        /// <summary>
        /// 目標
        /// </summary>
        public GameObject Target;

        public float LedearBehingDistance = 2f;

        private Vector3 randomOffset;

        private SteeringForArrive arriveScript;

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
            arriveScript = GetComponent<SteeringForArrive>();
            arriveScript.Target = new GameObject("ArriveTarget");
        }

        public override Vector3 Force()
        {
            if (Target == null)
            {
                return Vector3.zero;
            }
            Vehicle targetVehicle = Target.GetComponent<Vehicle>();
            Vector3 targetPoint = targetVehicle.Velocity.sqrMagnitude < 0.00001f ? Target.transform.position - LedearBehingDistance * (Target.transform.position - transform.position).normalized : Target.transform.position - LedearBehingDistance * targetVehicle.Velocity.normalized;
            arriveScript.Target.transform.position = targetPoint;

            return Vector3.zero;
        }
    }
}
