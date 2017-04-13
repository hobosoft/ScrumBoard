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
using System.ComponentModel;
using KtSoft.ScrumControls.Data;
using System.Drawing;
using System.Collections.Generic;

namespace KtSoft.ScrumControls.Region
{
    /// <summary>
    /// A region on the screen that displays a day.
    /// </summary>
    public class ScrumLane : IRegion
    {
        public ScrumLane()
        {
			State = TaskState.Story;
        }

		public ScrumLane(string name, TaskState state)
        {
            this.Name = name;
			State = state;
        }

		public string Name { get; set; }
		//public DateTime Date { get; set; }
		public Rectangle Bounds { get; set; }
		public Rectangle TitleBounds { get; set; }
		public Rectangle BodyBounds { get; set; }

		private List<TaskCard> _lstTaskCard = new List<TaskCard>();

        public List<TaskCard> TaskCards
        {
            get { return _lstTaskCard; }
        }

        public string FormattedName
		{
			get
			{
				return string.Format("{0} ({1})", Name, this._lstTaskCard.Count);
			}
		}

		[DefaultValue(TaskState.Story)]
		public TaskState State
		{
			get;
			set;
		}
    }
}
