using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Hexes {
    public class Hexagon {

        public HexaGrid grid; //The grid which the hex belongs to
        public Vector2 worldPosition; //Center worldPosition of the hex
        public CubeCoordinate gridPosition; //Position in cubeGrid
        public bool pointy = true; //If the hex should have pointy tops or flat tops
        public float size = 1f; //Length of one edge of the hex in units.
        
        private static float edgeToEdge_ { get { return Mathf.Sqrt(3) / 2; } }

        /*public OffsetCoordinate offsetPosition {
            get { return new OffsetCoordinate(gridPosition, pointy, grid.)}
        }*/

        private float cornerToCorner {
            get {
                return size * 2f;
            }
        }

        private float edgeToEdge {
            get {
                return edgeToEdge_ * cornerToCorner;
            }
        }

        public float width {
            get {
                return pointy ? edgeToEdge : cornerToCorner;
            }
        }

        public float heigth {
            get {
                return pointy ? cornerToCorner : edgeToEdge;
            }
        }
        
        public Hexagon(Vector2 pos, bool pointedHex = true, float hexSize = 1f) {
            worldPosition = pos;
            pointy = pointedHex;
            size = hexSize;
        }

        public Vector2 GetCornerGlobalPosition(int i) {
            return worldPosition + GetCornerLocalPosition(i);
        }

        public Vector2 GetCornerLocalPosition(int i) {
            return GetCornerLocalPosition(i, pointy, size);
        }

        public static Vector2 GetCornerLocalPosition(int i, bool pointy, float size) {
            return size*GetCornerLocalPosition(i, pointy);
        }

        public static Vector2 GetCornerLocalPosition(int i, bool pointy) {
            if (pointy) {
                switch (i % 6) {
                    case 0: return new Vector2(0, 1);
                    case 1: return new Vector2(edgeToEdge_, 0.5F);
                    case 2: return new Vector2(edgeToEdge_, -0.5F);
                    case 3: return new Vector2(0, -1);
                    case 4: return new Vector2(-edgeToEdge_, -0.5F);
                    case 5: return new Vector2(-edgeToEdge_, 0.5F);
                }
            } else {
                switch (i % 6) {
                    case 0: return new Vector2(0.5F, edgeToEdge_);
                    case 1: return new Vector2(1, 0);
                    case 2: return new Vector2(0.5F, -edgeToEdge_);
                    case 3: return new Vector2(-0.5F, -edgeToEdge_);
                    case 4: return new Vector2(-1, 0);
                    case 5: return new Vector2(-0.5F, edgeToEdge_);
                }
            }

            throw new InvalidOperationException();
        }
    }
    
    public struct OffsetCoordinate {
        public int column;
        public int row;
        public bool even;
        public bool pointy;
        
        public OffsetCoordinate(int hexColumn, int hexRow, bool evenOffset = true, bool isPointy = true) {
            column = hexColumn;
            row = hexRow;
            even = evenOffset;
            pointy = isPointy;
        }

        public static explicit operator CubeCoordinate(OffsetCoordinate oc) {
            return oc.pointy
                ? oc.even
                    ? OffsetToCubeEvenPointy(oc)
                    : OffsetToCubeOddPointy(oc)
                : oc.even
                    ? OffsetToCubeEvenFlat(oc)
                    : OffsetToCubeOddFlat(oc);
        }

        /* Leave this for now, alternative to the cube-to-offset conversion
                public OffsetCoordinate (CubeCoordinate cc, bool even, bool pointy) {

                    OffsetCoordinate oc;

                    oc = new 

                    oc = pointy
                        ? even
                            ? CubeToOffsetEvenPointy(cc)
                            : CubeToOffsetOddPointy(cc)
                        : even
                            ? CubeToOffsetEvenFlat(cc)
                            : CubeToOffsetOddFlat(cc);

                    return oc;
                }

                private static OffsetCoordinate CubeToOffsetEvenFlat(CubeCoordinate cc) { //FLAT == Q
                    int column = cc.x;
                    int row = -(cc.z + (cc.x + (cc.x & 1)) / 2);
                    return new OffsetCoordinate(column, row, true, false);
                }

                private static OffsetCoordinate CubeToOffsetOddFlat(CubeCoordinate cc) { //FLAT == Q
                    int column = cc.x;
                    int row = -(cc.z + (cc.x - (cc.x & 1)) / 2);
                    return new OffsetCoordinate(column, row, false, false);
                }

                private static OffsetCoordinate CubeToOffsetEvenPointy(CubeCoordinate cc) { //POINTY == R
                    int column = cc.x + (cc.z + (cc.z & 1)) / 2;
                    int row = -cc.z;
                    return new OffsetCoordinate(column, row, true, true);
                }

                private static OffsetCoordinate CubeToOffsetOddPointy(CubeCoordinate cc) { //POINTY == R
                    int column = column = cc.x + (cc.z - (cc.z & 1)) / 2;
                    int row = -cc.z;
                    return new OffsetCoordinate(column, row, false, true);
                }
        */

        public OffsetCoordinate(CubeCoordinate cc, bool pointyHex, bool evenHex) { //Cube to Offset
            pointy = pointyHex;
            even = evenHex;

            if (pointy) {
                row = -cc.z;
                if (even)
                    column = cc.x + (cc.z + (cc.z & 1)) / 2; //# convert cube to even-r offset
                else
                    column = cc.x + (cc.z - (cc.z & 1)) / 2; //# convert cube to odd-r offset
            } else {
                column = cc.x;
                if (even)
                    row = -(cc.z + (cc.x + (cc.x & 1)) / 2); //# convert cube to even-q offset
                else
                    row = -(cc.z + (cc.x - (cc.x & 1)) / 2); //# convert cube to odd-q offset
            }

            /*if (pointy) {
                row = -cc.z;
                column = cc.x - cc.y + (even ? (cc.x < cc.y ? (cc.z & 1) : 0) : (cc.x > cc.y ? -(cc.z & 1) : 0));
            } else {
                column = cc.x;
                row = cc.y - cc.x/2 + (even ? 1+(cc.y < 0 ? -(cc.x & 1) : 0) : (cc.y > 0 ? (cc.x & 1) : 0));
            }*/
            //throw new NotImplementedException();
        }
        

        private static CubeCoordinate OffsetToCubeEvenFlat(OffsetCoordinate oc) {
            int x = oc.column;
            int y = (int) (oc.row-(oc.column + (oc.column & 1))/2f);
            return new CubeCoordinate(x, y);
        }
        private static CubeCoordinate OffsetToCubeOddFlat(OffsetCoordinate oc) {
            int x = oc.column;
            int y = (int) (oc.row - (oc.column - (oc.column & 1)) / 2f);
            return new CubeCoordinate(x, y);
        }
        private static CubeCoordinate OffsetToCubeEvenPointy(OffsetCoordinate oc) {
            int x = (int) (oc.column - (oc.row + (oc.row & 1)) / 2f);
            int y = oc.row;
            return new CubeCoordinate(x, y);
        }
        private static CubeCoordinate OffsetToCubeOddPointy(OffsetCoordinate oc) {
            int x = (int) (oc.column - (oc.row - (oc.row & 1)) / 2f);
            int y = oc.row;
            return new CubeCoordinate(x, y);
        }
    }
    
    public struct CubeCoordinate {
        public int x;
        public int y;
        
        public int z {
            set { x = -value - y; }
            get { return -x - y; }
        }

        public CubeCoordinate(int xVal, int yVal) {
            x = xVal;
            y = yVal;
        }

        public CubeCoordinate (int xVal, int yVal, int zVal) {
            x = xVal;
            y = yVal;
            if (x + y + zVal != 0) throw new ArgumentException("The sum of all cube coordinates must give 0");
        }

        public override bool Equals(object obj) {
            if (obj is CubeCoordinate)
                return (CubeCoordinate) obj == this;
            return base.Equals(obj);
        }

        public override string ToString() {
            return string.Format("({0}, {1}, {2})", x, y, z);
        }

        public override int GetHashCode() {
            return x ^ y;
        }

        public static int GetMissingCoordinate(int a, int b) {
            return -a - b;
        }

        public static bool operator ==(CubeCoordinate a, CubeCoordinate b) {

            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(CubeCoordinate a, CubeCoordinate b) {

            return a.x != b.x || a.y != b.y || a.z != b.z;
        }

        public static CubeCoordinate operator +(CubeCoordinate a, CubeCoordinate b) {

            return new CubeCoordinate(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static CubeCoordinate operator +(CubeCoordinate a) {

            return new CubeCoordinate(+a.x, -+a.y, +a.z);
        }

        public static CubeCoordinate operator -(CubeCoordinate a, CubeCoordinate b) {

            return new CubeCoordinate(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static CubeCoordinate operator -(CubeCoordinate a) {

            return new CubeCoordinate(-a.x, -a.y, -a.z);
        }
    }
}
