
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class Hp_script_player2:MonoBehaviour
    {
        Vector3 localscale;

        // Use this for initialization
        void Start()
        {
            localscale = transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            if (Char_move_test_2.healtAmount >= 0)
            {
                localscale.x = Char_move_test_2.healtAmount;
                transform.localScale = localscale;
            }
        }
    }
}