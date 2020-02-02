﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageFramework.DirectX;
using ImageFramework.Model;

namespace ImageFramework.Utility
{
    /// <summary>
    /// texture cache that is synchronized with images model
    /// </summary>
    class ImageModelTextureCache : ITextureCache
    {
        private readonly Stack<ITexture> textures = new Stack<ITexture>(2);
        private readonly ImagesModel images;

        public ImageModelTextureCache(ImagesModel images)
        {
            this.images = images;
            images.PropertyChanged += ImagesOnPropertyChanged;
        }

        public ITexture GetTexture()
        {
            if (textures.Count > 0) return textures.Pop();

            // make new texture with the current configuration
            return images.CreateEmptyTexture();
        }

        public void StoreTexture(ITexture tex)
        {
            Debug.Assert(tex != null);
            if (IsCompatibleWith(tex))
            {
                // can be used for later
                textures.Push(tex);
            }
            else
            {
                // immediately discard (incompatible image)
                tex.Dispose();
            }
        }

        public bool IsCompatibleWith(ITexture tex)
        {
            Debug.Assert(tex != null);
            return images.HasMatchingProperties(tex);
        }

        private void Clear()
        {
            foreach (var tex in textures)
            {
                tex.Dispose();
            }
            textures.Clear();
        }

        private void ImagesOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(ImagesModel.NumLayers) ||
                args.PropertyName == nameof(ImagesModel.NumMipmaps) ||
                args.PropertyName == nameof(ImagesModel.Size) ||
                args.PropertyName == nameof(ImagesModel.ImageType))
                Clear();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}