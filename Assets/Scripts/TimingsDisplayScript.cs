using System;
using System.Collections.Generic;
using Managers;
using Modules;
using UnityEngine;

namespace DefaultNamespace
{
    public class TimingsDisplayScript : MonoBehaviour
    {
        [NonSerialized]
        public List<TimingRegistration> Timings;

        private static int width = 100;
        private static int height = 20;
        Rect rect = new (0, 0, width, height);
        Vector3 offset = new (0f, 0f, 0.5f); // height below the target position
    
        private void OnGUI()
        {
            if (Timings == null || Timings.Count == 0) return;

            var timingsCount = Timings.Count;
            var z = new TimingRegistration[timingsCount];
            Timings.CopyTo(0, z, 0, timingsCount);
            
            Vector3 point = Camera.main.WorldToScreenPoint(transform.position - offset);

            var x = point.x;
            var y = point.y;
            
            for (int i = 0; i < z.Length; i++)
            {
                var timer = z[i];
                
                var cdWidth = Mathf.Lerp(6f, width, timer.WaitTimeRemainingPercentage);
                
                Rect r = new (0, 0, width, height);
                r.x = x - (width / 2);
                r.y = Screen.height - y - r.height;

                r.width = cdWidth;
                GUI.Box(r, $"");
                r.width = width;
                
                GUI.Box(r, timer.Name);

                var character = timer.Autorun ? "x" : "↻";

                if (timer.AllowAutorunToggle)
                {
                    var result = GUI.Button(new Rect(r.x + width / 4 * 3,r.y,width / 4, height), character);
                    if (result)
                    {
                        timer.ToggleAutorun();
                    }
                }

                y -= height;
            }
        }
    }
}