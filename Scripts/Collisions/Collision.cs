using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Collisions
{

    public class Epsilon
    {
        public static float EPSILON = 0.00000001f;
    }
    //https://noonat.github.io/intersect/#aabb-vs-swept-aabb 
    public class Hit
    {
        public AABB collider;
        public Vector2 pos;
        public Vector2 delta;
        public Vector2 normal;
        public float time;

        public Hit(AABB _collider)
        {
            collider = _collider;
            pos = new Vector2();
            delta = new Vector2();
            normal = new Vector2();
            time = 0;
        }
    }

    public class Sweep
    {
        public Hit hit;
        public Vector2 pos;
        public float time;

        public Sweep()
        {
            hit = null;
            pos = new Vector2();
            time = 1;
        }
    }


    public class AABB
    {
        public Vector2 pos;
        public Vector2 half;

        public AABB(Vector2 _pos, Vector2 _half)
        {
            pos = _pos;
            half = _half;
        }

        public Hit intersectPoint(Vector2 point)
        {
            var dx = point.X - this.pos.X;
            var px = this.half.X - MathF.Abs(dx);
            if (px <= 0)
            {
                return null;
            }

            var dy = point.Y - this.pos.Y;
            var py = this.half.Y - MathF.Abs(dy);
            if (py <= 0)
            {
                return null;
            }

            Hit hit = new Hit(this);
            if (px < py)
            {
                var sx = MathF.Sign(dx);
                hit.delta.X = px * sx;
                hit.normal.X = sx;
                hit.pos.X = this.pos.X + (this.half.X * sx);
                hit.pos.Y = point.Y;
            }
            else
            {
                var sy = MathF.Sign(dy);
                hit.delta.Y = py * sy;
                hit.normal.Y = sy;
                hit.pos.X = point.X;
                hit.pos.Y = this.pos.Y + (this.half.Y * sy);
            }
            return hit;


        }

        public Hit intersectAABB(AABB box)
        {
            var dx = box.pos.X - this.pos.X;
            var px = (box.half.X + this.half.X) - MathF.Abs(dx);
            if (px <= 0)
            {
                return null;
            }

            var dy = box.pos.Y - this.pos.Y;
            var py = (box.half.Y + this.half.Y) - MathF.Abs(dy);
            if (py <= 0)
            {
                return null;
            }

            Hit hit = new Hit(this);
            if (px < py)
            {
                var sx = MathF.Sign(dx);
                hit.delta.X = px * sx;
                hit.normal.X = sx;
                hit.pos.X = this.pos.X + (this.half.X * sx);
                hit.pos.Y = box.pos.Y;
            }
            else
            {
                var sy = MathF.Sign(dy);
                hit.delta.Y = py * sy;
                hit.normal.Y = sy;
                hit.pos.X = box.pos.X;
                hit.pos.Y = this.pos.Y + (this.half.Y * sy);
            }
            return hit;
        }

        public Hit intersectSegment(Vector2 pos, Vector2 delta, float paddingX = 0, float paddingY = 0)
        {
            float scaleX = 1.0f / delta.X;
            float scaleY = 1.0f / delta.Y;
            float signX = MathF.Sign(scaleX);
            float signY = MathF.Sign(scaleY);
            var nearTimeX = (this.pos.X - signX * (this.half.X + paddingX) - pos.X) * scaleX;
            var nearTimeY = (this.pos.Y - signY * (this.half.Y + paddingY) - pos.Y) * scaleY;
            var farTimeX = (this.pos.X + signX * (this.half.X + paddingX) - pos.X) * scaleX;
            var farTimeY = (this.pos.Y + signY * (this.half.Y + paddingY) - pos.Y) * scaleY;

            if (nearTimeX > farTimeY || nearTimeY > farTimeX)
            {
                return null;
            }

            var nearTime = nearTimeX > nearTimeY ? nearTimeX : nearTimeY;
            var farTime = farTimeX < farTimeY ? farTimeX : farTimeY;

            if (nearTime >= 1 || farTime <= 0)
            {
                return null;
            }

            Hit hit = new Hit(this);
            hit.time = MathHelper.Clamp(nearTime, 0, 1);
            if (nearTimeX > nearTimeY)
            {
                hit.normal.X = -signX;
                hit.normal.Y = 0;
            }
            else
            {
                hit.normal.X = 0;
                hit.normal.Y = -signY;
            }
            hit.delta.X = (1.0f - hit.time) * -delta.X;
            hit.delta.Y = (1.0f - hit.time) * -delta.Y;
            hit.pos.X = pos.X + delta.X * hit.time;
            hit.pos.Y = pos.Y + delta.Y * hit.time;
            return hit;
        }

        public Sweep sweepAABB(AABB box, Vector2 delta)
        {
            Sweep sweep = new Sweep();

            if (delta.X == 0 && delta.Y == 0)
            {
                sweep.pos.X = box.pos.X;
                sweep.pos.Y = box.pos.Y;
                sweep.hit = this.intersectAABB(box);
                if (sweep.hit != null)
                {
                    sweep.time = 0;
                    sweep.hit.time = 0;
                }
                else
                {
                    sweep.time = 1;
                }
                return sweep;
            }
            else
            {
                sweep.hit = this.intersectSegment(box.pos, delta, box.half.X, box.half.Y);
                if (sweep.hit != null)
                {
                    sweep.time = MathHelper.Clamp(sweep.hit.time - Epsilon.EPSILON, 0, 1);
                    sweep.pos.X = box.pos.X + delta.X * sweep.time;
                    sweep.pos.Y = box.pos.Y + delta.Y * sweep.time;
                    var direction = new Vector2(delta.X, delta.Y);
                    direction.Normalize();
                    sweep.hit.pos.X = MathHelper.Clamp(
                      sweep.hit.pos.X + direction.X * box.half.X,
                      this.pos.X - this.half.X, this.pos.X + this.half.X);
                    sweep.hit.pos.Y = MathHelper.Clamp(
                      sweep.hit.pos.Y + direction.Y * box.half.Y,
                      this.pos.Y - this.half.Y, this.pos.Y + this.half.Y);
                }
                else
                {
                    sweep.pos.X = box.pos.X + delta.X;
                    sweep.pos.Y = box.pos.Y + delta.Y;
                    sweep.time = 1;
                }
                return sweep;
            }
        }
    }  
}
