﻿// COPYRIGHT 2009, 2010, 2011, 2012, 2013 by the Open Rails project.
// 
// This file is part of Open Rails.
// 
// Open Rails is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Open Rails is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Open Rails.  If not, see <http://www.gnu.org/licenses/>.

// This file is the responsibility of the 3D & Environment Team. 

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ORTS
{
    [DebuggerDisplay("TileX = {TileX}, TileZ = {TileZ}, Size = {Size}")]
    public class WaterTile : RenderPrimitive
    {
        public static VertexDeclaration PatchVertexDeclaration;

        static KeyValuePair<float, Material>[] WaterLayers;

        static int PatchVertexStride;

        readonly Viewer3D Viewer;
        readonly int TileX, TileZ, Size;
        readonly VertexBuffer VertexBuffer;
        readonly IndexBuffer IndexBuffer;
        readonly int PrimitiveCount;

        Matrix xnaMatrix = Matrix.Identity;

        public WaterTile(Viewer3D viewer, Tile tile)
        {
            Viewer = viewer;
            TileX = tile.TileX;
            TileZ = tile.TileZ;
            Size = tile.Size;

            if (PatchVertexDeclaration == null)
                LoadStaticData();

            LoadGeometry(Viewer.GraphicsDevice, tile, out PrimitiveCount, out IndexBuffer, out VertexBuffer);
        }

        void LoadStaticData()
        {
            if (Viewer.ENVFile.WaterLayers != null)
            WaterLayers = Viewer.ENVFile.WaterLayers.Select(layer => new KeyValuePair<float, Material>(layer.Height, Viewer.MaterialManager.Load("Water", Viewer.Simulator.RoutePath + @"\envfiles\textures\" + layer.TextureName))).ToArray();
  
            PatchVertexDeclaration = new VertexDeclaration(Viewer.GraphicsDevice, VertexPositionNormalTexture.VertexElements);
            PatchVertexStride = VertexPositionNormalTexture.SizeInBytes;
            
        }

        [CallOnThread("Updater")]
        public void PrepareFrame(RenderFrame frame)
        {
            var dTileX = TileX - Viewer.Camera.TileX;
            var dTileZ = TileZ - Viewer.Camera.TileZ;
            var mstsLocation = new Vector3(dTileX * 2048 - 1024 + 1024 * Size, 0, dTileZ * 2048 - 1024 + 1024 * Size);

            if (Viewer.Camera.InFov(mstsLocation, Size * 1448f))
            {
                xnaMatrix.M41 = mstsLocation.X;
                xnaMatrix.M43 = -mstsLocation.Z;
                foreach (var waterLayer in WaterLayers)
                {
                    xnaMatrix.M42 = mstsLocation.Y + waterLayer.Key;
                    frame.AddPrimitive(waterLayer.Value, this, RenderPrimitiveGroup.World, ref xnaMatrix);
                }
            }
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Indices = IndexBuffer;
            graphicsDevice.Vertices[0].SetSource(VertexBuffer, 0, PatchVertexStride);
            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 17 * 17, 0, PrimitiveCount);
        }

        void LoadGeometry(GraphicsDevice graphicsDevice, Tile tile, out int primitiveCount, out IndexBuffer indexBuffer, out VertexBuffer vertexBuffer)
        {
            primitiveCount = 0;
            var waterLevels = new ORTSMath.Matrix2x2(tile.WaterNW, tile.WaterNE, tile.WaterSW, tile.WaterSE);

            var indexData = new List<short>(16 * 16 * 2 * 3);
            for (var z = 0; z < 16; ++z)
            {
                for (var x = 0; x < 16; ++x)
                {
                    var patch = tile.GetPatch(x, z);

                    if (!patch.WaterEnabled)
                        continue;

                    var nw = (short)(z * 17 + x);  // Vertex index in the north west corner
                    var ne = (short)(nw + 1);
                    var sw = (short)(nw + 17);
                    var se = (short)(sw + 1);

                    primitiveCount += 2;

                    if ((z & 1) == (x & 1))  // Triangles alternate
                    {
                        indexData.Add(nw);
                        indexData.Add(se);
                        indexData.Add(sw);
                        indexData.Add(nw);
                        indexData.Add(ne);
                        indexData.Add(se);
                    }
                    else
                    {
                        indexData.Add(ne);
                        indexData.Add(se);
                        indexData.Add(sw);
                        indexData.Add(nw);
                        indexData.Add(ne);
                        indexData.Add(sw);
                    }
                }
            }
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(short), indexData.Count, BufferUsage.WriteOnly);
            indexBuffer.SetData(indexData.ToArray());
            var vertexData = new List<VertexPositionNormalTexture>(16 * 16);
            for (var z = 0; z < 17; ++z)
            {
                for (var x = 0; x < 17; ++x)
                {
                    var U = (float)x * 4;
                    var V = (float)z * 4;

                    var a = (float)x / 16;
                    var b = (float)z / 16;

                    var e = (a - 0.5f) * 2048 * Size;
                    var n = (b - 0.5f) * 2048 * Size;

                    var y = ORTSMath.Interpolate2D(a, b, waterLevels);

                    vertexData.Add(new VertexPositionNormalTexture(new Vector3(e, y, n), Vector3.UnitY, new Vector2(U, V)));
                }
            }
            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalTexture), vertexData.Count, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertexData.ToArray());
        }

        [CallOnThread("Loader")]
        internal static void Mark()
        {
            foreach (var material in WaterLayers.Select(kvp => kvp.Value))
                material.Mark();
        }
    }
}
