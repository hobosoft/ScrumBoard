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
namespace KtSoft.ScrumControls.Renderer
{
    internal class BorderInfo
    {
        private static BorderInfo noBorderInfo = null;
        public static BorderInfo None
        {
            get
            {
                if (noBorderInfo == null)
                    noBorderInfo = new BorderInfo() { OverlapBottomRight = true, Left=false, Right=false,Top=false,Bottom=false };
                return noBorderInfo;
            }
        }
        private static BorderInfo defaultBorderInfo = null;
        public static BorderInfo Default
        {
            get
            {
                if (defaultBorderInfo == null)
                    defaultBorderInfo = new BorderInfo() { OverlapBottomRight = true,All=true };
                return defaultBorderInfo;
            } }
        /// <summary>
        /// Whether to paint a top border
        /// </summary>
        public bool Top { get; set; }
        /// <summary>
        /// Whether to paint a left border
        /// </summary>
        public bool Left { get; set; }
        /// <summary>
        /// Whether to paint a bottom border
        /// </summary>
        public bool Bottom { get; set; }
        /// <summary>
        /// Whether to paint a right border
        /// </summary>
        public bool Right { get; set; }
        /// <summary>
        /// Whether to paint all borders
        /// </summary>
        public bool All
        {
            get { return Top && Left && Bottom && Right; }
            set
            {
                if (value)
                {
                    Top = true;
                    Left = true;
                    Right = true;
                    Bottom = true;
                }
            }
        }
        /// <summary>
        /// Whether to overlap the right and bottom borders by 1 pixel.
        /// Only valid with BorderInfo.All set to true.
        /// </summary>
        public bool OverlapBottomRight { get; set; }
    }
}