﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace OpenTKImageViewer.glhelper
{
    class Sampler : IGlObject
    {
        private int id;

        public Sampler(TextureMinFilter minFilter, TextureMagFilter magFilter)
        {
            id = GL.GenSampler();

            GL.SamplerParameter(id, SamplerParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.SamplerParameter(id, SamplerParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.SamplerParameter(id, SamplerParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);

            
            GL.SamplerParameter(id, SamplerParameterName.TextureMinFilter, (int)minFilter);
            GL.SamplerParameter(id, SamplerParameterName.TextureMagFilter, (int)magFilter);
        }

        public void Bind(int bindingIndex)
        {
            GL.BindSampler(bindingIndex, id);
        }

        public void Dispose()
        {
            if(id != 0)
            {
                GL.DeleteSampler(id);
                id = 0;
            }
        }
    }
}
