﻿using System;
using System.Numerics;
using ImGuiNET;

namespace Phoenix.Import.Application.UI
{
    public static class Widgets
    {
        public static void RenderSpinner(Vector2 position, float radius, int thickness)
        {
            float time = (float)ImGui.GetTime();
            uint color = ImGui.ColorConvertFloat4ToU32(new Vector4(0.5f, 0.5f, 0.5f, 1));
            ImDrawListPtr drawList = ImGui.GetWindowDrawList();

            int num_segments = 30;
            int start = (int)MathF.Abs(MathF.Sin(time * 1.8f) * (num_segments - 5));
            float a_min = MathF.PI * 2.0f * (start) / num_segments;
            float a_max = MathF.PI * 2.0f * ((float)num_segments - 3) / num_segments;
            var centre = new Vector2(position.X + radius, position.Y + radius + ImGui.GetStyle().FramePadding.Y);

            drawList.PathClear();
            for (int i = 0; i < num_segments; i++)
            {
                float a = a_min + (i / (float)num_segments) * (a_max - a_min);
                var location = new Vector2(centre.X + MathF.Cos(a + time * 8) * radius, centre.Y + MathF.Sin(a + time * 8) * radius);
                drawList.PathLineTo(location);
            }
            drawList.PathStroke(color, false, thickness);
        }
    }
}
