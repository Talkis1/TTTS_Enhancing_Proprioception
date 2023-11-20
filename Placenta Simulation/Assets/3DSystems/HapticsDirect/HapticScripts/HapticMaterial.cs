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

public class HapticMaterial : MonoBehaviour
{


    [HideInInspector]
    public int MaterialID;

    [Header("Basic Properties")]
    [FieldProperties("Grabbing", true)]
    [Tooltip("When this option is enabled, the game object can be grabbed with the virtual stylus.")]
    public bool bGrabbing = false;

    [Label(title = "Mass")]
    [Tooltip("Defines the object mass in kg.")]
    [Slider(0, 1)]
    public float hMass = 0.0f;
    [Label(title = "Stiffness")]
    [Tooltip("The stiffness Haptic material surface directly translates to how hard a surface feels.")]
    [Slider(0, 1)]
    public float hStiffness = 0.2f;
    [Label(title = "Damping")]
    [Tooltip("Damping reduces the spring effect of the surface.")]
    [Slider(0, 1)]
    public float hDamping = 0.0f;      //!< Higher values are less 'bouncy'.

    [Header("Physics")]
    [Label(title = "Impulse Depth")]
    [Slider(0, 20)]
    public float hImpulseD = 0.0f;


    [Header("Viscosity Properties")]
    [Label(title = "Viscosity")]
    [Tooltip("It adds a viscous force to the local force sent to the haptic device.")]
    [Slider(0, 1)]
    public float hViscosity = 0.0f;
        

    [Header("Friction Properties")]
    [Label(title = "Static Friction")]
    [Tooltip("The surface of the object (eg: plane) how hard it is to slide along the surface starting from a complete stop. A param value of 0 is a completely frictionless surface and a value of 1 is the maximum amount of static friction the haptic device is capable of rendering.")]
    [Slider(0, 1)]
    public float hFrictionS = 0.0f;
    [Label(title = "Dynamic Friction")]
    [Tooltip("This property defines how hard it is to slide along the surface once already moving. A param value of 0 is a completely frictionless surface and a value of 1 is the maximum amount of dynamic friction the haptic device is capable of rendering.")]
    [Slider(0, 1)]
    public float hFrictionD = 0.0f;



    [Header("Contant Force Properties")]
    [StackableField]
    [Label(title = "Direction")]
    [Tooltip("Defines the direction of the force vector.")]
    public Vector3 hConstForceDir = new Vector3(0.0f,0.0f,0.0f);
    [Label(title = "Magnitude")]
    [Tooltip("Defines the magnitude of the force.")]
    [Slider(0, 1)]
    public float hConstForceMag = 0.0f;
    [StackableField]
    [Label(title = "Use Contact Normal")]
    [Tooltip("If this option is activated, the contact normal is used as direction for the force vector.")]
    public bool UseContactNormalCF = false;
    [StackableField]
    [Label(title = "Contact Normal Inverse")]
    [Tooltip("If this option is activated, the inverse contact normal is used as direction for the force vector.")]
    public bool ContactNormalInverseCF = false;

    [Header("Spring Properties")]
    [StackableField]
    [Label(title = "Spring Anchor")]
    [Tooltip("Defines the anchor position of the spring.")]
    public Vector3 hSpringDir = new Vector3(0.0f, 0.0f, 0.0f);
    [Label(title = "Spring Anchor Object")]
    [Tooltip("A game object can be used as spring anchor.")]
    [StackableField]
    public GameObject SpringAnchorObj;
    [Label(title = "Spring Magnitude")]
    [Tooltip("Defines the magnitude of the spring force.")]
    [Slider(0, 1)]
    public float hSpringMag = 0.0f;
        

    [Header("Pop Through Properties")]

    [Label(title = "Popthrough (Force in N)")]
    [Slider(0, 7)]
    public float hPopthAbs = 0.0f;

    private int NumHitsDampingMax = 3;
    private int NumHitsDamping = 0;

    private HapticPlugin HPlugin = null;

    private void OnEnable()
    {     

        System.TimeSpan t = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1);
        int secondsSinceEpoch = (int)t.TotalSeconds;
        MaterialID = secondsSinceEpoch;
        int z1 = UnityEngine.Random.Range(0, 1000000);
        int z2 = UnityEngine.Random.Range(0, 1000000);
        MaterialID = MaterialID + z1 + z2;
    }

    void Start()
    {
        if (HPlugin == null)
        {

            HapticPlugin[] HPs = (HapticPlugin[])Object.FindObjectsOfType(typeof(HapticPlugin));
            foreach (HapticPlugin HP in HPs)
            {
                if (HP.GrabObject == this.gameObject)
                {
                    HPlugin = HP;


                }
            }

        }



    }

    private void FixedUpdate()
    {
        HapticPlugin[] HPs = (HapticPlugin[])Object.FindObjectsOfType(typeof(HapticPlugin));
        foreach (HapticPlugin HP in HPs)
        {
            if (HP.GrabObject == this.gameObject)
            {
                HPlugin = HP;


            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (HPlugin != null)
        { if(this == HPlugin.CollisionMesh)
            {
                
                HPlugin.UpdateCollision(collision, true, false, false);
                
            }

           Rigidbody matRB = this.GetComponent<Rigidbody>();
            FixedJoint joint = (FixedJoint)HPlugin.CollisionMesh.GetComponent(typeof(FixedJoint));
            if (joint != null)
            {
                if (joint.connectedBody == matRB)
                {
                    HPlugin.UpdateCollision(collision, true, false, false);
                    HPlugin.enable_damping = true;
                }
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {

        if (HPlugin != null)
        {
            

            Rigidbody matRB = this.GetComponent<Rigidbody>();
            FixedJoint joint = (FixedJoint)HPlugin.CollisionMesh.GetComponent(typeof(FixedJoint));
            if (joint != null)
            {
                if (joint.connectedBody == matRB)
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

    }

    private void OnCollisionExit(Collision collision)
    {
        if (HPlugin != null)
        {

            NumHitsDamping = 0;
            Rigidbody matRB = this.GetComponent<Rigidbody>();
            FixedJoint joint = (FixedJoint)HPlugin.CollisionMesh.GetComponent(typeof(FixedJoint));
            if (joint != null)
            {
                if (joint.connectedBody == matRB)
                {
                    HPlugin.UpdateCollision(collision, false, false, true);
                }
            }
        }
        
    }


}



public class FieldPropertiesAttributeMat : PropertyAttribute 
{
    public string NewName { get; private set; }
    public bool ShowPropOnly { get; private set; }
   


    public FieldPropertiesAttributeMat(string name, bool showOnly, float value, float min, float max)
    {
        NewName = name;
        ShowPropOnly = showOnly;
        
       
    }
}

public class DisplayOnlyAttributeMat : PropertyAttribute
{

}
#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(FieldPropertiesAttribute))]
public class FieldPropertiesEditorMat : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        
        GUI.enabled = (attribute as FieldPropertiesAttribute).ShowPropOnly;
        EditorGUI.PropertyField(position, property, new GUIContent((attribute as FieldPropertiesAttribute).NewName));
        
        GUI.enabled = true;
        
        

    }
}
#endif
