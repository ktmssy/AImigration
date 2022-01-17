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
    /// コントローラー策のBase
    /// </summary>
    public class Steering : MonoBehaviour
    {
        /// <summary>
        /// コントローラーの比重
        /// </summary>
        public float Weight = 1f;

        /// <summary>
        /// コントローラー力の計算
        /// </summary>
        /// <returns>Vector3.zero</returns>
        public virtual Vector3 Force()
        {
            return Vector3.zero;
        }
    }
}
