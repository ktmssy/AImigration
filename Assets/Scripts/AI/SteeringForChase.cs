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
    /// 追う。要ブラッシュアップ
    /// </summary>
    public class SteeringForChase : Steering
    {
        /// <summary>
        /// 目標
        /// </summary>
        public GameObject Target;

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
            Vector3 toTarget = Target.transform.position - transform.position;

            float relativeAngleCos = 0f;
            float flag = 0f;

            if (is2D)
            {
                relativeAngleCos = Vector2.Dot(transform.up, Target.transform.up);
                flag = Vector2.Dot(new Vector2(toTarget.x, toTarget.y), transform.up);
            }
            else
            {
                relativeAngleCos = Vector3.Dot(transform.forward, Target.transform.forward);
                flag = Vector2.Dot(toTarget, transform.forward);
            }

            //目標と同じ向き
            if (flag > 0f && relativeAngleCos < -0.95f)
            {
                desiredVelocity = toTarget.normalized * maxSpeed;
            }
            else
            {
                Vehicle targetVehicle = Target.GetComponent<Vehicle>();
                //予測時間
                float lookAheadTime = toTarget.magnitude / (maxSpeed + targetVehicle.Velocity.magnitude);
                //予測による速度の計算
                desiredVelocity = (Target.transform.position + targetVehicle.Velocity * lookAheadTime - transform.position).normalized * maxSpeed;
            }

            if (is2D)
            {
                desiredVelocity.z = 0f;
            }
            else if (isPlanar)
            {
                desiredVelocity.y = 0f;
            }

            return desiredVelocity - vehicle.Velocity;
        }
    }
}
