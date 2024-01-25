using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TangkaUI
{
	public class DivisionCreator : MonoBehaviour
	{
        [SerializeField] GameObject divisionPrefab;

		[SerializeField] DialHandle handle;

        private void Awake()
        {
            CreateDivision(handle.IntervalAngle);
        }

        void CreateDivision(int intervalAngle)
        {
            int divisionTotalCount = 360 / intervalAngle;
            for (int i = 0; i < divisionTotalCount; i++)
            {
                var division = Instantiate(divisionPrefab, transform);

                int angle = i * intervalAngle;
                division.transform.rotation = Quaternion.Euler(0, 0, angle);
                division.name += $"_{angle}";
            }
        }
    } 
}
