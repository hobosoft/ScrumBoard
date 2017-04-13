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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace KtSoft.ScrumControls.Renderer
{
    /// <summary>
    /// String formats.
    /// </summary>
    internal class StringFormats
    {
        /// <summary>String format to left.</summary>
        public static readonly StringFormat Left = new StringFormat() { Alignment = StringAlignment.Near, Trimming = StringTrimming.EllipsisCharacter };
        /// <summary>String format to right.</summary>
        public static readonly StringFormat Right = new StringFormat() { Alignment = StringAlignment.Far, Trimming = StringTrimming.EllipsisCharacter };
        /// <summary>String format to center.</summary>
        public static readonly StringFormat Center = new StringFormat() { Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
    }
}