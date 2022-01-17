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
    public class AILocomotion : Vehicle
    {
        /// <summary>
        /// キャラクターコントローラー
        /// </summary>
        private CharacterController controller;

        private Rigidbody rigidbody;

        /// <summary>
        /// リジッドボディ2D
        /// </summary>
        private Rigidbody2D rigidbody2D;

        /// <summary>
        /// 毎回の移動距離
        /// </summary>
        private Vector3 moveDistance;

        protected override void Start()
        {
            controller = GetComponent<CharacterController>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            moveDistance = Vector2.zero;

            base.Start();
        }

        private void FixedUpdate()
        {
            //速度の計算
            Velocity += acceleration * Time.fixedDeltaTime;

            //最大速度の制限
            if (Velocity.sqrMagnitude > sqrMaxSpeed)
            {
                Velocity = Velocity.normalized * MaxSpeed;
            }

            //減速
            if(acceleration.sqrMagnitude==0)
            {
                Velocity *= FrictionRatio;
            }

            //移動距離の計算
            moveDistance = Velocity * Time.fixedDeltaTime;

            //平面上だったらY軸を無視する
            if (IsPlanar)
            {
                Velocity.y = 0f;
                moveDistance.y = 0f;
            }


            if (Is2D)
            {
                //２DだったらZ軸を無視する
                Velocity.z = 0f;
                moveDistance.z = 0f;

                //rigidbody2D使えるならリジッドボディで移動、使えないならtransformで移動
                if (rigidbody2D == null || rigidbody2D.isKinematic)
                {
                    transform.position += new Vector3(moveDistance.x, moveDistance.y, 0f);
                }
                else
                {
                    rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(moveDistance.x, moveDistance.y));
                }
            }
            else
            {
                //コントローラーがあればコントローラーで移動
                if (controller)
                {
                    controller.SimpleMove(Velocity);
                }
                //rigidbody使えるならリジッドボディで移動、使えないならtransformで移動
                else if (rigidbody == null || rigidbody.isKinematic)
                {
                    transform.position += new Vector3(moveDistance.x, moveDistance.y, 0f);
                }
                else
                {
                    rigidbody.MovePosition(rigidbody.position + moveDistance);
                }
            }


            //向きの更新
            if (Velocity.sqrMagnitude > 0.00001f)
            {
                //今の向きと速度の方向でLerp
                Vector3 newForward = Vector3.Slerp(transform.forward, Velocity, damping * Time.fixedDeltaTime);

                if (IsPlanar)
                {
                    newForward.y = 0f;
                }

                if (Is2D)
                {
                    newForward.z = 0f;
                    transform.up = newForward;
                }
                else
                {
                    transform.forward = newForward;
                }




            }

            //移動アニメーション
            //gameObject.animation.Play("walk");
        }
    }
}
