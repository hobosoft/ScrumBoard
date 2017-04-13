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
using KtSoft.ScrumControls.Data;

namespace KtSoft.ScrumControls.Events
{
	public class TaskEditEventArgs : EventArgs
	{
        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>
        /// The control.
        /// </value>
		public ScrumBoardControl Control{get;set;}
        /// <summary>
        /// Gets or sets the appointment.
        /// </summary>
        /// <value>
        /// The appointment.
        /// </value>
		public  TaskItem Task{get;set;}
        /// <summary>
        /// Prevents a default instance of the <see cref="TaskEditEventArgs"/> class from being created.
        /// </summary>
		private TaskEditEventArgs()
		{
		}
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskEditEventArgs"/> class.
        /// </summary>
        /// <param name="task">The appointment.</param>
        /// <param name="control">The control.</param>
		public TaskEditEventArgs(TaskItem task, ScrumBoardControl control)
		{
			Task=task;
			Control=control;
		}

	}
}
