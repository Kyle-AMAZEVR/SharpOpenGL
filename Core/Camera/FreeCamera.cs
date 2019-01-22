﻿using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using Core;

namespace Core.Camera
{
    public class FreeCamera : CameraBase
    {
        public FreeCamera()
            : base(OpenTK.MathHelper.PiOver6, 1.0f, 1.0f, 10000.0f)
        {
            LookAtDir = new Vector3(1, 0, 0);
            EyeLocation = new Vector3(5, 5, 5);
            Destination = EyeLocation;
        }

        public FreeCamera(float fFOV, float fAspectRatio, float fNear, float fFar)            
            : base(fFOV, fAspectRatio, fNear, fFar)
        {
            LookAtDir = new Vector3(1, 0, 0);
            EyeLocation = new Vector3(5, 5, 5);
            Destination = EyeLocation;
        }

        public Vector3 GetLookAtDir()
        {
            return m_RotationMatrix.Row2;
        }

        private Vector3 GetMoveDirection()
        {
            if (MoveKeyDictionary[Key.W] && MoveKeyDictionary[Key.A])
            {
                return (m_RotationMatrix.Row2 + m_RotationMatrix.Row0).Normalized();
            }
            else if (MoveKeyDictionary[Key.W] && MoveKeyDictionary[Key.D])
            {
                return (m_RotationMatrix.Row2 - m_RotationMatrix.Row0).Normalized();
            }
            else if (MoveKeyDictionary[Key.S] && MoveKeyDictionary[Key.A])
            {
                return (-m_RotationMatrix.Row2 + m_RotationMatrix.Row0).Normalized();
            }
            else if (MoveKeyDictionary[Key.S] && MoveKeyDictionary[Key.D])
            {
                return (-m_RotationMatrix.Row2 - m_RotationMatrix.Row0).Normalized();
            }
            else if (MoveKeyDictionary[Key.W])
            {
                return (m_RotationMatrix.Row2).Normalized();
            }            
            else if(MoveKeyDictionary[Key.S])
            {
                return -m_RotationMatrix.Row2;
            }
            else if(MoveKeyDictionary[Key.A])
            {
                return m_RotationMatrix.Row0;
            }
            else if(MoveKeyDictionary[Key.D])
            {
                return -m_RotationMatrix.Row0;
            }
            else if(MoveKeyDictionary[Key.Z])
            {
                return Vector3.UnitY;
            }
            else if(MoveKeyDictionary[Key.X])
            {
                return -Vector3.UnitY;
            }

            return Vector3.UnitX;
        }

        protected void Move()
        {
            var vMoveDir = GetMoveDirection();
            var vMove = Vector3.Multiply(vMoveDir, GetMoveAmount());
            Destination = EyeLocation + vMove;
        }
        
        public override void MoveForward()
        {
            var vMoveDir = m_RotationMatrix.Row2;
            var vMove = Vector3.Multiply(vMoveDir, GetMoveAmount());
            Destination = EyeLocation + vMove;
        }

        public override void MoveBackward()
        {
            var vMoveDir = m_RotationMatrix.Row2;
            var vMove = Vector3.Multiply(vMoveDir, GetMoveAmount());
            Destination = EyeLocation - vMove;
        }

        public override void MoveRight()
        {
         
            var RightDir = m_RotationMatrix.Row0;
            var vMove = Vector3.Multiply(RightDir, GetMoveAmount());
            Destination = EyeLocation - vMove;
        }

        public override void MoveLeft()
        {
         
            var RightDir = m_RotationMatrix.Row0;
            var vMove = Vector3.Multiply(RightDir, GetMoveAmount());
            Destination = EyeLocation + vMove;
        }        

        public override void MoveUpward()
        {
         
            var vMove = Vector3.Multiply(UpDir, GetMoveAmount());
            Destination = EyeLocation + vMove;
        }

        public override void MoveDownward()
        {
         
            var vMove = Vector3.Multiply(UpDir, GetMoveAmount());
            Destination = EyeLocation - vMove;
        }

        public override void RotateRight()
        {
            Yaw -= m_fRotateAmount;
        }

        public override void RotateLeft()
        {
            Yaw += m_fRotateAmount;
        }

        private void UpdateMoveSpeed()
        {
            TimeSpan span = DateTime.Now - LastKeyStrokeTime;

            if(span.TotalSeconds < 0.1)
            {
                SpeedIndex++;
                SpeedIndex = Math.Min(SpeedIndex, SpeedList.Count - 1);
            }
            LastKeyStrokeTime = DateTime.Now;
        }

        private float GetMoveAmount()
        {
            return 30.0f;
        }

        public override void OnKeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (IsLocked)
            {
                return;
            }

            if (MoveKeys.Contains(e.Key))
            {
                bMoving = false;
                SpeedIndex = 0;
            }

            if(MoveKeyDictionary.ContainsKey(e.Key))
            {
                MoveKeyDictionary[e.Key] = false;
            }
        }

        public override void OnKeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (IsLocked)
            {
                return;
            }

            if (MoveKeys.Contains(e.Key))
            {
                MoveStarted = EyeLocation;
                bMoving = true;
                UpdateMoveSpeed();
            }

            if(MoveKeyDictionary.ContainsKey(e.Key))
            {
                MoveKeyDictionary[e.Key] = true;
                Move();
            }

            if (e.Key == OpenTK.Input.Key.E)
            {
                RotateRight();
            }
            else if(e.Key == OpenTK.Input.Key.Q)
            {
                RotateLeft();
            }
        }

        public override void Tick(double fDeltaSeconds)
        {
            if(bMoving)
            {
                Elapsed += (float)fDeltaSeconds;
                var vDir = (Destination - MoveStarted);
                vDir.Normalize();
                EyeLocation += vDir * (float)fDeltaSeconds * SpeedList[SpeedIndex];
            }
            else
            {
                Elapsed = 0;
            }

            if((DateTime.Now - LastKeyStrokeTime).TotalSeconds > 1)
            {
                SpeedIndex = 0;
            }

            UpdateViewMatrix();
            UpdateProjMatrix();
        }

        public override void UpdateViewMatrix()
        {
            m_RotationMatrix = Matrix3.CreateRotationY(Yaw);
            ViewMatrix = Matrix4.LookAt(EyeLocation, EyeLocation + Vector3.Multiply(m_RotationMatrix.Row2, 1.0f), Vector3.UnitY);
        }

        public float Yaw { get; set; } = -90.0f;

        protected float Pitch = 0;

        protected float fMoveAmount = 6;

        protected float m_fRotateAmount = OpenTK.MathHelper.DegreesToRadians(3);

        protected Matrix3 m_RotationMatrix = new Matrix3();

        public Vector3 Destination;

        protected Vector3 MoveStarted;

        protected float MoveSeconds = 0.1f;

        protected float Elapsed = 0;

        bool bMoving = false;
        bool bRotating = false;

        int SpeedIndex = 0;

        protected List<float> SpeedList = new List<float> {32,64,128,256,512,1024};

        private List<OpenTK.Input.Key> MoveKeys = new List<OpenTK.Input.Key>{ Key.W, Key.A, Key.S, Key.D, Key.Z, Key.X };

        private Dictionary<OpenTK.Input.Key, bool> MoveKeyDictionary = new Dictionary<Key, bool>
        {
            {Key.W, false },
            {Key.A, false },
            {Key.S, false },
            {Key.D, false },
            {Key.Z, false },
            {Key.X, false }
        };

        DateTime LastKeyStrokeTime;
    }
}
