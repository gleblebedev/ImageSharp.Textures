// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

namespace SixLabors.ImageSharp.Textures.Formats.Dds.Processing.PixelFormats
{
    using System;
    using System.Diagnostics;

    internal class LdrColorA
    {
        public byte r, g, b, a;

        public LdrColorA() { }
        public LdrColorA(byte _r, byte _g, byte _b, byte _a)
        {
            r = _r;
            g = _g;
            b = _b;
            a = _a;
        }

        public ref byte this[int uElement]
        {
            get
            {
                switch (uElement)
                {
                    case 0: return ref r;
                    case 1: return ref g;
                    case 2: return ref b;
                    case 3: return ref a;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public static void InterpolateRGB(LdrColorA c0, LdrColorA c1, int wc, int wcprec, LdrColorA outt)
        {
            int[] aWeights = null;
            switch (wcprec)
            {
                case 2: aWeights = Constants.g_aWeights2; Debug.Assert(wc < 4); break;
                case 3: aWeights = Constants.g_aWeights3; Debug.Assert(wc < 8); break;
                case 4: aWeights = Constants.g_aWeights4; Debug.Assert(wc < 16); break;
                default: Debug.Assert(false); outt.r = outt.g = outt.b = 0; return;
            }
            outt.r = (byte)(c0.r * (uint)(Constants.BC67_WEIGHT_MAX - aWeights[wc]) + c1.r * (uint)aWeights[wc] + Constants.BC67_WEIGHT_ROUND >> Constants.BC67_WEIGHT_SHIFT);
            outt.g = (byte)(c0.g * (uint)(Constants.BC67_WEIGHT_MAX - aWeights[wc]) + c1.g * (uint)aWeights[wc] + Constants.BC67_WEIGHT_ROUND >> Constants.BC67_WEIGHT_SHIFT);
            outt.b = (byte)(c0.b * (uint)(Constants.BC67_WEIGHT_MAX - aWeights[wc]) + c1.b * (uint)aWeights[wc] + Constants.BC67_WEIGHT_ROUND >> Constants.BC67_WEIGHT_SHIFT);
        }

        public static void InterpolateA(LdrColorA c0, LdrColorA c1, int wa, int waprec, LdrColorA outt)
        {
            int[] aWeights = null;
            switch (waprec)
            {
                case 2: aWeights = Constants.g_aWeights2; Debug.Assert(wa < 4); break;
                case 3: aWeights = Constants.g_aWeights3; Debug.Assert(wa < 8); break;
                case 4: aWeights = Constants.g_aWeights4; Debug.Assert(wa < 16); break;
                default: Debug.Assert(false); outt.a = 0; return;
            }
            outt.a = (byte)(c0.a * (uint)(Constants.BC67_WEIGHT_MAX - aWeights[wa]) + c1.a * (uint)aWeights[wa] + Constants.BC67_WEIGHT_ROUND >> Constants.BC67_WEIGHT_SHIFT);
        }

        public static void Interpolate(LdrColorA c0, LdrColorA c1, int wc, int wa, int wcprec, int waprec, LdrColorA outt)
        {
            InterpolateRGB(c0, c1, wc, wcprec, outt);
            InterpolateA(c0, c1, wa, waprec, outt);
        }
    }
}
