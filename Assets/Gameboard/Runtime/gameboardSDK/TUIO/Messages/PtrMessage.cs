﻿namespace Gameboard.TUIO
{
    public class PtrMessage
    {
        public uint s_id;
        public uint tu_id;
        public uint c_id;
        public float x_pos;
        public float y_pos;
        public float angle;
        public float shear;
        public float radius;
        public float press;
        public float? x_vel;
        public float? y_vel;
        public float? p_vel;
        public float? m_acc;
        public float? p_acc;
    }
}