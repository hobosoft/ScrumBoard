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
using System.Drawing;
using KtSoft.ScrumControls.Data;

namespace KtSoft.ScrumControls.Region
{
	public class TaskCard : IRegion
	{
        public TaskItem Task
        {
            get;
            private set;
        }

		public string FormattedSubject
		{
			get { return Task.Subject; }// string.Format("{0} - {1}", Appointment.DateStart.ToString("h:mm tt"), Appointment.Subject); }
		}

		public TaskCard(TaskItem task)
		{
			Task = task;
			ColourBlockBounds=Rectangle.Empty;
		}

		private	Rectangle _bounds;
		public Rectangle Bounds
		{
			get{return _bounds; }
			set
			{
                _bounds = value;
				if (_bounds.Width>5 &&value!=Rectangle.Empty)
				{
					BodyBounds=new Rectangle(){Width=value.Width-3,Height=value.Height,X=value.X+3,Y=value.Y};
					ColourBlockBounds = new Rectangle(){Width=3,Height=value.Height,X=value.X,Y=value.Y};
					//SelectTopBounds = new Rectangle(){Width=value.Width,Height=3,X=value.X,Y=value.Y-3};
					//SelectBottomBounds = new Rectangle(){Width=value.Width,Height=3,X=value.X,Y=value.Y+value.Height};
				}
				else
				{
					BodyBounds= _bounds;
					ColourBlockBounds = Rectangle.Empty;
				}
			}
		}
		public Rectangle ColourBlockBounds { get; set; }
		public Rectangle BodyBounds { get; set; }
		//public Rectangle SelectTopBounds { get; set; }
		//public Rectangle SelectBottomBounds { get; set; }
	}
}