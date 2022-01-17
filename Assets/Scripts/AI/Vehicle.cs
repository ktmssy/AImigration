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
    /// 移動ステータス
    /// </summary>
    public class Vehicle : MonoBehaviour
    {
        /// <summary>
        /// 最大速度
        /// </summary>
        public float MaxSpeed = 10f;

        /// <summary>
        /// 受けられる最大力
        /// </summary>
        public float MaxForce = 100f;

        /// <summary>
        /// 質量
        /// </summary>
        public float Mass = 1f;

        /// <summary>
        /// 最大速度の2乗
        /// </summary>
        protected float sqrMaxSpeed;

        /// <summary>
        /// 速度
        /// </summary>
        public Vector3 Velocity;

        /// <summary>
        /// コントローラー要素リスト
        /// </summary>
        private Steering[] steerings;

        /// <summary>
        /// 全要素を計算したコントローラー力
        /// </summary>
        private Vector3 steeringForce;

        /// <summary>
        /// 加速度
        /// </summary>
        protected Vector3 acceleration;

        /// <summary>
        /// 2Dですか？Z軸を無視する
        /// </summary>
        public bool Is2D;

        /// <summary>
        /// 平面上ですか？Y軸を無視する
        /// </summary>
        public bool IsPlanar;

        /// <summary>
        /// 向き変更の速度
        /// </summary>
        public float damping = 0.9f;

        /// <summary>
        /// 減速するときの係数
        /// </summary>
        public float FrictionRatio = 0.9f;

        /// <summary>
        /// タイマー
        /// </summary>
        private float timer;

        /// <summary>
        /// 計算の時間間隔
        /// </summary>
        public float ComputeInterval = 0.2f;

        protected virtual void Start()
        {
            steeringForce = Vector3.zero;
            steerings = GetComponents<Steering>();
            timer = 0f;
            sqrMaxSpeed = MaxSpeed * MaxSpeed;
        }

        protected void Update()
        {
            timer += Time.deltaTime;
            steeringForce = Vector3.zero;
            if (timer >= ComputeInterval)
            {
                foreach (Steering s in steerings)
                {
                    if (s.enabled)
                    {
                        steeringForce += s.Force() * s.Weight;
                    }
                }
                //コントローラー力を最大値にオーバーしないように
                steeringForce = Vector3.ClampMagnitude(steeringForce, MaxForce);
                //加速度を算出
                acceleration = steeringForce / Mass;
                //タイマーのリセット
                timer = 0f;
            }
        }

        public void OnDrawGizmos()
        {
           // Gizmos.DrawLine(transform.position, transform.position + Velocity.normalized * 3f);
            Gizmos.DrawLine(transform.position, transform.position + acceleration.normalized * 3f);
        }
    }
}
