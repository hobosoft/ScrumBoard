/*
    Copyright 2011 Andrew Sydney
 
    This file is part of KtSoft.ScrumControls.

    KtSoft.ScrumControls is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    KtSoft.ScrumControls is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with KtSoft.ScrumControls.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Windows.Forms.VisualStyles;
using System.Drawing;

namespace KtSoft.ScrumControls.Renderer
{
    /// <summary>
    /// Renderer when visual styles are enabled.
    /// </summary>
    internal class VisualStyleWrapper : IRenderer
    {
        private readonly VisualStyleRenderer visualStyleRenderer;
        private readonly Pen borderPen;
        public VisualStyleWrapper(VisualStyleRenderer renderer, Pen borderPen)
        {
            visualStyleRenderer = renderer;
            this.borderPen = borderPen;
        }
        public void DrawBox(Graphics g, Font font, Rectangle bounds, string text, TextAlignment textAlignment, bool drawBackground, BorderInfo borders,bool bold)
        {
            DrawBox(g, font, bounds, text, textAlignment, drawBackground, borders, bold,null);
        }

        public void DrawBox(Graphics g, Font font, Rectangle bounds, string text, TextAlignment textAlignment, bool drawBackground, BorderInfo borders)
        {
            DrawBox(g, font, bounds, text, textAlignment, drawBackground, borders, false);
        }

        public void DrawBox(Graphics g, Font font, Rectangle bounds,Brush backgroundColorOverride)
        {
            DrawBox(g, font, bounds, string.Empty, TextAlignment.Left, true, BorderInfo.Default,false,backgroundColorOverride);
        }

        public void DrawBox(Graphics g, Font font, Rectangle bounds)
        {
            DrawBox(g, font, bounds, string.Empty, TextAlignment.Left, true, BorderInfo.Default);
        }

        public void DrawLine(Graphics g, int x1, int y1, int x2, int y2)
        {
            g.DrawLine(borderPen, x1, y1, x2, y2);
        }

        public TextInfo GetTextInfo(Graphics graphics,Font font)
        {
            TextMetrics metrics = visualStyleRenderer.GetTextMetrics(graphics);
            TextInfo info = new TextInfo();
            info.Height = metrics.Height;
            return info;
        }

        public void DrawBox(Graphics g, Font font, Rectangle bounds, string text, TextAlignment textAlignment, bool drawBackground, BorderInfo borders,bool bold,Brush backgroundColorOverride)
        {
            //draw the background
            if (drawBackground)
            {
           	if (backgroundColorOverride!=null)            		
            	{
            		g.FillRectangle(backgroundColorOverride, bounds);
            	}
            	else{
                 visualStyleRenderer.DrawBackground(g, bounds);
            }
            }

            //draw the borders
            if (borders != null)
            {
                //handle overlaps of the border - BorderInfo.OverlapLeftRightBottom
                if (borders.All && borders.OverlapBottomRight)
                {
                    //calculate border, paint with overlap
                    g.DrawRectangle(borderPen, bounds);
                }
            }

            //draw the text
            if (!string.IsNullOrEmpty(text))
            {
				Rectangle rcText = bounds;
				rcText.Inflate(-2, -2);
                //TODO: bold
                if (textAlignment == TextAlignment.Left)
					visualStyleRenderer.DrawText(g, rcText, string.Format(" {0}", text), false,
                                                 System.Windows.Forms.TextFormatFlags.Left |
                                                 System.Windows.Forms.TextFormatFlags.EndEllipsis |
                                                 System.Windows.Forms.TextFormatFlags.VerticalCenter);
                else if (textAlignment == TextAlignment.Right)
					visualStyleRenderer.DrawText(g, rcText, string.Format("{0} ", text), false,
                                                 System.Windows.Forms.TextFormatFlags.Right |
                                                 System.Windows.Forms.TextFormatFlags.EndEllipsis |
                                                 System.Windows.Forms.TextFormatFlags.VerticalCenter);
                else
					visualStyleRenderer.DrawText(g, rcText, text, false,
                                                 System.Windows.Forms.TextFormatFlags.HorizontalCenter |
                                                 System.Windows.Forms.TextFormatFlags.EndEllipsis |
                                                 System.Windows.Forms.TextFormatFlags.VerticalCenter);

            }
        }

        public void DrawBox(Graphics g, Font font, Rectangle bounds, string text, bool drawBackground, BorderInfo borders)
        {
            DrawBox( g,  font,  bounds,  text, TextAlignment.Left,  drawBackground,  borders);
        }

        public void DrawBox(Graphics g, Font font, Rectangle bounds, string text)
        {
            DrawBox(g, font, bounds, text, TextAlignment.Left, true, BorderInfo.Default);
        }

		public void DrawBox(Graphics g, Font font, Rectangle bounds, string text, TextAlignment textAlignment)
		{
			DrawBox(g, font, bounds, text, textAlignment, true, BorderInfo.Default);
		}
    }
}