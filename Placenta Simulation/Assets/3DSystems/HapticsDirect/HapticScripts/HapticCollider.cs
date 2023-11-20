﻿// This code contains 3D SYSTEMS Confidential Information and is disclosed to you
// under a form of 3D SYSTEMS software license agreement provided separately to you.
//
// Notice
// 3D SYSTEMS and its licensors retain all intellectual property and
// proprietary rights in and to this software and related documentation and
// any modifications thereto. Any use, reproduction, disclosure, or
// distribution of this software and related documentation without an express
// license agreement from 3D SYSTEMS is strictly prohibited.
//
// ALL 3D SYSTEMS DESIGN SPECIFICATIONS, CODE ARE PROVIDED "AS IS.". 3D SYSTEMS MAKES
// NO WARRANTIES, EXPRESSED, IMPLIED, STATUTORY, OR OTHERWISE WITH RESPECT TO
// THE MATERIALS, AND EXPRESSLY DISCLAIMS ALL IMPLIED WARRANTIES OF NONINFRINGEMENT,
// MERCHANTABILITY, AND FITNESS FOR A PARTICULAR PURPOSE.
//
// Information and code furnished is believed to be accurate and reliable.
// However, 3D SYSTEMS assumes no responsibility for the consequences of use of such
// information or for any infringement of patents or other rights of third parties that may
// result from its use. No license is granted by implication or otherwise under any patent
// or patent rights of 3D SYSTEMS. Details are subject to change without notice.
// This code supersedes and replaces all information previously supplied.
// 3D SYSTEMS products are not authorized for use as critical
// components in life support devices or systems without express written approval of
// 3D SYSTEMS.
//
// Copyright (c) 2021 3D SYSTEMS. All rights reserved.



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HapticGUI;
//using StackableDecorator;
#if UNITY_EDITOR
using UnityEditor;
#endif



class HapticCollider : MonoBehaviour
{
    private HapticPlugin HPlugin = null;
    //public GrabberControl GControl = null;

    private int NumHitsDampingMax = 3;
    private int NumHitsDamping = 0;

    [Label(title = "Stiffness")]
    [Slider(0, 1)]
    public float hStiffness = 1.0f;

    [Header("Friction Properties")]
    [Label(title = "Static Friction")]
    [Slider(0, 1)]
    public float hFrictionS = 0.0f;
    [Label(title = "Dynamic Friction")]
    [Slider(0, 1)]
    public float hFrictionD = 0.0f;

    void Start()
    {
        if (HPlugin == null)
        {

            HapticPlugin[] HPs = (HapticPlugin[])Object.FindObjectsOfType(typeof(HapticPlugin));
            foreach (HapticPlugin HP in HPs)
            {
                if (HP.CollisionMesh == this.gameObject)
                {
                    HPlugin = HP;
                    

                }
            }

        }

        

        

    }

 


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<HapticCollider>() == null)
        {
            if (HPlugin != null)
            {
                HPlugin.UpdateCollision(collision, true, false, false);
                //GControl.GrabberCollision(collision, gameObject);
                HPlugin.enable_damping = true;
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.GetComponent<HapticCollider>() == null)
        {
            if (HPlugin != null)
            {
                if (NumHitsDamping >= NumHitsDampingMax)
                {
                    HPlugin.enable_damping = false;
                }
                else
                {
                    NumHitsDamping++;
                }

                HPlugin.UpdateCollision(collision, false, true, false);

            }


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<HapticCollider>() == null)
        {
            if (HPlugin != null)
            {
                HPlugin.UpdateCollision(collision, false, false, true);
                NumHitsDamping = 0;
            }
        }
    }

    


}

