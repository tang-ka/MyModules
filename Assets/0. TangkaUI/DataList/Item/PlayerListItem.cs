using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
    public class PlayerListItem : ToggleListItem<PlayerData>
    {
        [Header("Information")]
        [SerializeField] Text nickName;
        [SerializeField] Text level;
        [SerializeField] Text job;
        [Header("Stat")]
        [SerializeField] Text STR;
        [SerializeField] Text DEX;
        [SerializeField] Text INT;
        [SerializeField] Text LUK;

        public override void Set(PlayerData data)
        {
            base.Set(data);

            nickName.text = data.nickName;
            level.text = data.level.ToString();

            var jobString = data.job.ToString();
            job.text = jobString[0].ToString();
            job.text += jobString.Substring(1, jobString.Length - 1).ToLower();

            STR.text = data.STR.ToString();
            DEX.text = data.DEX.ToString();
            INT.text = data.INT.ToString();
            LUK.text = data.LUK.ToString();
        }
    }
}
