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
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;

namespace KtSoft.ScrumControls.Data
{
    /// <summary>
    /// Holds appointment data.
    /// </summary>
    public class TaskItem
    {
		public TaskItem()
		{
			//State = TaskState.
		}
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

		/// <summary>
		/// 任务描述
		/// </summary>
		public string Description { get; set; }

        /// <summary>
        /// Gets or sets the color block brush for the appointment border.
        /// </summary>
        /// <value>
        /// The color block brush.
        /// </value>
        public Brush ColorBlockBrush { get; set; }



		[DefaultValue(TaskState.Story)]
		public TaskState State
		{
			get;
			set;
		}
	}

	public enum TaskState : int
	{
		Story			= 0,
		Todo			= 1,
		Workinprocess	= 2,
		Toverify		= 3,
		Done			= 4,
	}

    public class TaskList : List<TaskItem>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskList"/> class.
        /// </summary>
        public TaskList()
        {
        }

        /// <summary>
        /// Sorts the appointments.
        /// </summary>
        public void SortTasks()
        {
            //var sortedApps =
            //    from a in this.ToList<Appointment>()
            //    orderby a.DateStart
            //    select a;

            //base.Clear();
            //foreach (var app in sortedApps.ToList())
            //{
            //    base.Add(app);
            //}

        }

    }
}
