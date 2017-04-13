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
using System.Windows.Forms;
using System.ComponentModel;
using KtSoft.ScrumControls.Data;
using KtSoft.ScrumControls.Region;
using KtSoft.ScrumControls.Events;
using KtSoft.ScrumControls.Renderer;

namespace KtSoft.ScrumControls
{
	/// <summary>
	/// The BaseScheduleControl defines properties common to the three schedule controls. 
	/// </summary>
	public partial class ScrumBoardControl : ScrollableControl, System.ComponentModel.ISupportInitialize
	{
        private static int PADDING = 8;
        private static int APPHEIGHT = 50;

        //private ToolTip toolTip;

        /// <summary>
        /// Gets the appointment grid.
        /// </summary>
        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //protected DataGridView AppointmentGrid { get { return hiddenGrid; } }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is dragging an appointment.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is dragging; otherwise, <c>false</c>.
        /// </value>
        protected bool IsDragging { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is initialising.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is initialising; otherwise, <c>false</c>.
		/// </value>
		protected bool IsInitialising { get; set; }
		//private bool IsUpdating = false;
		internal List<ScrumLane> Lanes { get; private set; }

		//private Appointment draggedAppointment = null;
		private TaskCard draggedCard = null;
		//private ScrumLane draggedOldLane = null;


		private TaskList _lstTask = new TaskList();
		/// <summary>
		/// Gets or sets the appointments.
		/// </summary>
		/// <value>
		/// The appointments.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskItem[] Tasks
		{
			get { return _lstTask.ToArray(); }
			//set
			//{
			//	_lstTask = value;
			//	RefreshAppointments();
			//	//BoundsValidTimeSlot = false;
			//	//BoundsValidAppointment = false;
			//	//AppointmentGrid.DataSource = Appointments;
			//	//RefreshAppointments();
			//}
		}

        public TextAlignment HeaderTextAlignment
        {
            get;
            set;
        }

        public bool AddTask(TaskItem task)
        {
            if (_lstTask.Contains(task))
                return true;

            foreach (ScrumLane lane in Lanes)
            {
                if (lane.State == task.State)
                {
                    lane.TaskCards.Add(new TaskCard(task));
                    _lstTask.Add(task);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The appointment list or an appointment in it has changed,
        /// so we need to refresh everything from the underlying
        /// grid control to the boundaries of the appointments.
        /// </summary>
  //      public void RefreshAppointments()
		//{

		//	//sort the appointments by date
		//	//Appointments.SortTasks();

		//	//            if (appointments != null)
		//	//            {
		//	//                var sortedApps =
		//	//                from a in appointments
		//	//                orderby a.DateStart
		//	//                select a;
		//	//                
		//	//                appointments = sortedApps.ToList();
		//	//                appointments.Clear();
		//	//                foreach (var app in sortedApps.ToList())
		//	//                	appointments.Add(app);
		//	//            }
		//	BoundsValidTimeSlot = false;
		//	BoundsValidAppointment = false;
		//	//AppointmentGrid.DataSource = Tasks as List<TaskItem>;
		//	//RefreshAppointments();
		//}


		/// <summary>
		/// Initialise a new control instance and set up the appointment grid.
		/// </summary>
		public ScrumBoardControl()
		{
			startX = 0;
			Lanes = new List<ScrumLane>();

			InitializeComponent();

			//init tooltip
			//this.toolTip = new ToolTip();

			this.DoubleBuffered = true;
			this.MouseMove += ScheduleControl_MouseMove;
			this.MouseDown += ScheduleControl_MouseDown;
			this.MouseUp += ScheduleControl_MouseUp;
			//this.MouseHover +=  ScheduleControl_MouseHover;

			//AppointmentGrid.ColumnCount = 2;
			//AppointmentGrid.Columns[0].Name = "Date";
			//AppointmentGrid.Columns[0].DataPropertyName = "Date";
			//AppointmentGrid.Columns[1].Name = "Subject";
			//AppointmentGrid.Columns[1].DataPropertyName = "Subject";
			//AppointmentGrid.SelectionChanged += grid_SelectionChanged;
			//AppointmentGrid.KeyDown += grid_KeyDown;

			this.ContextMenu = new ContextMenu();
			this.ContextMenu.MenuItems.Add("Add appointment", this.OnNewTaskCardClick);
            HeaderTextAlignment = TextAlignment.Center;

            this.Lanes.Add(new ScrumLane("Story", TaskState.Story));

            this.Lanes.Add(new ScrumLane("To do", TaskState.Todo));

            this.Lanes.Add(new ScrumLane("Work in process", TaskState.Workinprocess));

            this.Lanes.Add(new ScrumLane("To verify", TaskState.Toverify));

            this.Lanes.Add(new ScrumLane("Done", TaskState.Done));
        }






        /// <summary>
        /// Handles the SelectionChanged event of the grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //private void grid_SelectionChanged(object sender, EventArgs e)
        //{
        //	//if we didn't trigger this
        //	if (!IsUpdating)
        //	{
        //		//change the selected appointment and day, if an appointment is selected
        //		//if (AppointmentGrid.SelectedRows != null && AppointmentGrid.SelectedRows.Count > 0)
        //		//{
        //		//	if (AppointmentGrid.SelectedRows[0] != null && AppointmentGrid.SelectedRows[0].DataBoundItem is TaskItem)
        //		//	{
        //		//		//TODO: turn this back into AppointmentRender somehow
        //		//		SelectedAppointment = AppointmentGrid.SelectedRows[0].DataBoundItem as TaskItem;
        //		//		Invalidate();
        //		//	}
        //		//}
        //		//else
        //		{
        //			SelectedTask = null;
        //		}
        //	}

        //}



        /// <summary>
        /// Handles the KeyDown event of the grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="keyEvent">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        //private void grid_KeyDown(object sender, KeyEventArgs keyEvent)
        //{
        //	if (keyEvent.KeyData == Keys.Enter)
        //	{
        //		if (SelectedTask != null)
        //		{
        //			FireAppointmentEdit(SelectedTask);
        //			keyEvent.Handled = true;
        //		}
        //	}
        //}

        /// <summary>
        /// Fires the appointment edit.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        protected void FireAppointmentEdit(TaskItem appointment)
		{
			//EventHandler<TaskEditEventArgs> handler = TaskCardEdit;
			if (TaskCardEdit != null)
			{
				TaskEditEventArgs args = new TaskEditEventArgs(appointment, this);
                TaskCardEdit(this, args);
			}

		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDoubleClick"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			//handle day clicks
			foreach (ScrumLane day in this.Lanes)
			{
				if (day.Bounds.Contains(e.Location))
				{
					foreach (TaskCard appt in day.TaskCards)
					{
						if (appt.Bounds.Contains(e.Location))
						{
							//AppointmentGrid.Focus();
							SelectedLane = day;
							SelectedTask = appt.Task;
							if (SelectedTask != null)
							{

								FireAppointmentEdit(SelectedTask);
							}
							return;
						}
					}
					//they probably want a new appointment
					this.LastRightMouseClickCoords = null;
					OnNewTaskCardClick(this, new EventArgs());
					return;
				}
			}


			base.OnMouseDoubleClick(e);
		}
		private TaskCard DragDropStart(int x, int y)
		{
			return HitTestTaskCard(x, y);
		}

		internal ScrumLane SelectedLane = null;
		protected TaskItem SelectedTask = null;


		/// <summary>
		/// Gets or sets the last right mouse click coordinates.
		/// </summary>
		/// <value>
		/// The last right mouse click coordinates.
		/// </value>
		protected Point? LastRightMouseClickCoords { get; set; }

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseClick"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
		protected override void OnMouseClick(MouseEventArgs e)
		{
			//AppointmentGrid.Focus();

			//record the location of a right mouse click
			if (e.Button == MouseButtons.Right)
			{
				//TODO: for right clicks, should all other code be aborted
				LastRightMouseClickCoords = e.Location;
			}

			//handle day clicks
			foreach (ScrumLane day in this.Lanes)
			{
				if (day.TitleBounds.Contains(e.Location))
				{
					SelectedLane = day;
					this.Invalidate();
					return;
				}
				if (day.BodyBounds.Contains(e.Location))
				{
					foreach (TaskCard appt in day.TaskCards)
					{
						if (appt.Bounds.Contains(e.Location))
						{
							SelectedLane = day;
							SelectedTask = appt.Task;
							//IsUpdating = true;
							//AppointmentGrid.ClearSelection();
							//foreach (DataGridViewRow row in AppointmentGrid.Rows)
							//{
							//	if (row.DataBoundItem == SelectedAppointment)
							//	{
							//		row.Selected = true;
							//		AppointmentGrid.CurrentCell = row.Cells[0];
							//		break;
							//	}
							//}
							//IsUpdating = false;
							this.Invalidate();
							return;
						}
					}
					break;
				}
			}
			base.OnMouseClick(e);
		}




		/// <summary>
		/// Doesn't work
		/// </summary>
		private void ScheduleControl_MouseHover(object sender, EventArgs e)
		{
			//TODO: Doesn't work to a satisfactory level. Needs investigation.
			//        	Point mousePoint = this.PointToClient(MousePosition);
			//        	Appointment appt = HitTestAppointment(mousePoint.X,mousePoint.Y);
			//        	if (appt!=null  )
			//        	{       
			//        		toolTip.Hide(this);
			//            	toolTip.Show(appt.Subject,this,mousePoint.X,mousePoint.Y);
			//        	}
		}






		private int startX;
		private int startY;
		/// <summary>
		/// Handles the MouseDown event of the current control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
		private void ScheduleControl_MouseDown(object sender, MouseEventArgs e)
		{
			//base.OnMouseDown(e);

			// see if we hit an appointment, only do next stuff if we hit one
			if (e.Button == MouseButtons.Left)
			{
				TaskCard card = DragDropStart(e.X, e.Y);
				if (card != null && card.Task != null)//
				{
					draggedCard = card;
					//draggedAppointment = appRegion.Appointment;

					OnMouseClick(e);

					IsDragging = true;

					Image image = DrawTaskCard(card, card.Bounds.Size, true);

					pbDrag.Size = image.Size;
					pbDrag.Image = image;

                    pbDrag.Location = card.Bounds.Location;// e.Y - 6;
					//pbDrag.Left = e.X - 8;
					pbDrag.Visible = true;
					startX = e.X;
					startY = e.Y;
				}
				else
				{
					draggedCard = null;
					//draggedAppointment = null;
				}
			}
		}

		/// <summary>
		/// Handles the MouseMove event of the current control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
		private void ScheduleControl_MouseMove(object sender, MouseEventArgs e)
		{

			//base.OnMouseMove(e);
			if (IsDragging && startX != e.X && startY != e.Y)
			{
				Cursor = Cursors.Hand;
				pbDrag.Visible = true;
				pbDrag.Top = e.Y - 6;
				pbDrag.Left = e.X - 8;
			}
		}

		/// <summary>
		/// Handles the MouseUp event of the current control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
		private void ScheduleControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (IsDragging && startX != e.X && startY != e.Y)
			{
				Cursor = Cursors.Default;
				IsDragging = false;
				pbDrag.Visible = false;
				pbDrag.Size = pbDrag.InitialImage.Size;
				pbDrag.Image = null;

				DragDropEnd(draggedCard, e.X, e.Y);
				//DragDropEnd(draggedAppointment, e.X, e.Y);
				//draggedOldLane = null;
				draggedCard = null;
			}
			else
			{
				Cursor = Cursors.Default;
				IsDragging = false;
				pbDrag.Visible = false;
				OnMouseClick(e);
			}
		}

		/// <summary>
		/// Perform a test on a mouse click location and see what appointment
		/// region they hit.
		/// </summary>
		/// <param name="x">The x coordinate of the click.</param>
		/// <param name="y">The y coordinate of the click.</param>
		/// <returns>The ScrumLane that was clicked</returns>
		internal TaskCard HitTestTaskCard(int x, int y)
		{
			foreach (ScrumLane lane in this.Lanes)
			{
				if (lane.BodyBounds.Contains(x, y))
				{
					foreach (TaskCard card in lane.TaskCards)
					{
						if (card.Bounds.Contains(x, y))
						{
							return card;
						}
					}
					break;
				}
			}
			return null;
		}

		/// <summary>
		/// Perform a test on a mouse click location and see what day
		/// region they hit.
		/// </summary>
		/// <param name="x">The x coordinate of the click.</param>
		/// <param name="y">The y coordinate of the click.</param>
		/// <returns>The ScrumLane that was clicked</returns>
		internal ScrumLane HitTestDayRegion(int x, int y)
		{
			foreach (ScrumLane lane in this.Lanes)
			{
				if (lane.BodyBounds.Contains(x, y))
				{
					return lane;
				}
			}
			return null;
		}

		/// <summary>
		/// Perform a test on a mouse click location and see what date
		/// time value they hit.
		/// </summary>
		/// <param name="x">The x coordinate of the click.</param>
		/// <param name="y">The y coordinate of the click.</param>
		/// <returns>The DateTime that was clicked</returns>
		protected virtual ScrumLane HitTestScurmLane(int x, int y)
		{
            foreach (ScrumLane lane in this.Lanes)
            {
                if (lane.Bounds.Contains(x, y))
                {
                    return lane;
                }
            }

            //TODO: check the hour headers as well

            return null;
        }
		protected virtual ScrumLane GetScurmLane(TaskState state)
		{
            foreach (ScrumLane lane in this.Lanes)
            {
                if (lane.State == state)
                {
                    return lane;
                }
            }

            //TODO: check the hour headers as well

            return null;
        }

        /// <summary>
        /// Notify child form that the drag drop event has ended.
        /// </summary>
        /// <param name="appointment">The dragged appointment</param>
        /// <param name="x">The x coordinate it was dropped on.</param>
        /// <param name="y">The y coordinate it was dropped on.</param>
        protected virtual void DragDropEnd(TaskCard card, int x, int y)
		{
            ScrumLane newLane = HitTestDayRegion(x, y);
			if (newLane != null)
			{
                MoveTaskCard(card, newLane);

                //var newDate = new DateTime(day.Date.Year,day.Date.Month,day.Date.Day,appointment.DateStart.Hour,appointment.DateStart.Minute,appointment.DateStart.Second);
                FireTaskCardMove(card, newLane);
			}
		}

        private void MoveTaskCard(TaskCard card, ScrumLane lane)
        {
            if (card.Task.State == lane.State)
                return;

            foreach (ScrumLane temp in Lanes)
            {
                if (temp.State == card.Task.State)
                {
                    temp.TaskCards.Remove(card);
                }
            }

            lane.TaskCards.Add(card);
        }

        /// <summary>
        /// Fires the appointment move.
        /// </summary>
        /// <param name="card">The appointment.</param>
        /// <param name="newDate">The new date.</param>
        protected void FireTaskCardMove(TaskCard card, ScrumLane newLane)
		{
			//EventHandler<TaskMoveEventArgs> handler = TaskCardMove;
			if (TaskCardMove != null)
			{
				TaskMoveEventArgs args = new TaskMoveEventArgs(card, newLane, this);
                TaskCardMove(this, args);
			}

		}


		/// <summary>
		/// Fires the appointment new.
		/// </summary>
		/// <param name="newApptDate">The new appt date.</param>
		protected void FireTaskCardNew(ScrumLane region)
		{
			//EventHandler<TaskCreateEventArgs> handler = TaskCardCreate;
			if (TaskCardCreate != null)
			{
				TaskCreateEventArgs args = new TaskCreateEventArgs(region, this);
                TaskCardCreate(this, args);
			}

		}

		private void OnNewTaskCardClick(object sender, EventArgs e)
		{
			Point menuLocation = Control.MousePosition;
			menuLocation = PointToClient(menuLocation);
			if (LastRightMouseClickCoords != null)
				menuLocation = LastRightMouseClickCoords.Value;
			ScrumLane region = this.HitTestScurmLane(menuLocation.X, menuLocation.Y);
			if (region != null)
				FireTaskCardNew(region);
			//else
			//    FireAppointmentNew(DateTime.Now);

		}

		/// <summary>
		/// Occurs when [appointment create].
		/// </summary>
		public event EventHandler<TaskCreateEventArgs> TaskCardCreate;
		/// <summary>
		/// Occurs when [appointment move].
		/// </summary>
		public event EventHandler<TaskMoveEventArgs> TaskCardMove;
		/// <summary>
		/// Occurs when [appointment edit].
		/// </summary>
		public event EventHandler<TaskEditEventArgs> TaskCardEdit;


        /// <summary>
        /// Calculate the time slot bounds. This works out the size and
        /// shape that days will take up on the screen. This method
        /// is called from OnPaint only when the property BoundsValidTimeSlot
        /// is set to false (this property is set to false in cases such as 
        /// when the control has been resized or the date shown has 
        /// changed).
        /// </summary>
        protected virtual void CalculateScurmLaneBounds(Graphics g)
        {
            if (Width == 0 || Height == 0)
                return;

            int headerHeight = (int)(RendererCache.Current.Header.GetTextInfo(g, this.Font).Height * 1.2);

            int xCurrent = 0;
            int colWidth = (Width - 1) / Lanes.Count;

            //set up day and hour bounds
            for (int j = 0; j < Lanes.Count; j++)
            {
                ScrumLane lane = Lanes[j] as ScrumLane;

                if (lane != null)
                {
                    int yCurrent = headerHeight;
                    //set up day bounds
                    if (j == Lanes.Count - 1)
                        lane.Bounds = new Rectangle(xCurrent, 0, Width - xCurrent - 1, Height - 1);
                    else
                        lane.Bounds = new Rectangle(xCurrent, 0, colWidth, Height - 1);

                    lane.TitleBounds = new Rectangle(xCurrent, 0, lane.Bounds.Width, headerHeight);
                    lane.BodyBounds = new Rectangle(lane.Bounds.X, headerHeight, lane.Bounds.Width, lane.Bounds.Height - headerHeight);
                }
                xCurrent += colWidth;
            }
            //BoundsValidTimeSlot = true;
        }

        /// <summary>
        /// Calculate the appointment bounds. This works out the size and
        /// shape that the appointments will take up on the screen,
        /// and handles any other calculations such as overlaps. This method
        /// is called from OnPaint only when the property BoundsValidAppointment
        /// is set to false (this property is set to false in cases such as 
        /// when the control has been resized or the appointment list has 
        /// changed).
        /// </summary>
        protected virtual int CalculateTaskCardBounds(Graphics graphics)
        {
            int iMaxHeight = this.Height;
            //int appSeparator = 3;
            //recalculate the appointments
            foreach (ScrumLane lane in this.Lanes)
            {
                lane.TaskCards.Clear();

                int apptX = lane.BodyBounds.X + PADDING;
                int apptY = lane.BodyBounds.Y + PADDING;
                int width = lane.BodyBounds.Width - PADDING * 2;
                foreach (TaskItem task in Tasks)
                {
                    if (task.State == lane.State)
                    {
                        TaskCard appRegion = new TaskCard(task);
                        appRegion.Bounds = new Rectangle(apptX, apptY, width, APPHEIGHT);
                        lane.TaskCards.Add(appRegion);

                        apptY += APPHEIGHT + PADDING;
                    }
                }

                iMaxHeight = Math.Max(iMaxHeight, apptY);
            }
            //BoundsValidAppointment = true;

            return iMaxHeight;
        }


        /// <summary>
        /// Signals the object that initialization is starting.
        /// </summary>
        public void BeginInit()
		{
			IsInitialising = true;
			//hiddenGrid.SuspendLayout();
			SuspendLayout();
		}

		/// <summary>
		/// Signals the object that initialization is complete.
		/// </summary>
		public void EndInit()
		{
			IsInitialising = false;
			//hiddenGrid.ResumeLayout(false);
			ResumeLayout(false);
		}


		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			//BoundsValidTimeSlot = false;
			//BoundsValidAppointment = false;
			Invalidate();
		}

        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int SB_ENDSCROLL = 0x8;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL)
            {
                if (m.WParam.ToInt32() != SB_ENDSCROLL)
                    Invalidate();
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
		{
            SetScrollInfo();

            Brush brush = new SolidBrush(this.BackColor);

            int iScrollPos = 0;
            if (VerticalScroll.Visible)
                iScrollPos = VerticalScroll.Value;

            int headerHeight = (int)(RendererCache.Current.Header.GetTextInfo(e.Graphics, this.Font).Height * 1.2);

            int xCurrent = 0;
            int laneWidth = (ClientSize.Width - 1) / Lanes.Count;

            Image img = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics g = Graphics.FromImage(img);

            for (int j = 0; j < Lanes.Count; j++)
            {
                ScrumLane lane = Lanes[j] as ScrumLane;
                if (lane != null)
                {
                    //int yCur = headerHeight - iScrollPos;
                    //set up day bounds

                    Rectangle bounds;
                    if (j == Lanes.Count - 1)
                        bounds = new Rectangle(xCurrent, 0, ClientSize.Width - xCurrent - 1, ClientSize.Height - 1);
                    else
                        bounds = new Rectangle(xCurrent, 0, laneWidth, ClientSize.Height - 1);

                    lane.Bounds = bounds;

                    lane.TitleBounds = new Rectangle(xCurrent, 0, bounds.Width, headerHeight);
                    lane.BodyBounds = new Rectangle(bounds.X, headerHeight, bounds.Width, bounds.Height - headerHeight);

                    //paint header
                    RendererCache.Current.Header.DrawBox(g, Font, lane.TitleBounds, lane.FormattedName, HeaderTextAlignment);
                    RendererCache.Current.Control.DrawBox(g, Font, lane.BodyBounds, brush);

                    bool bSelected = false;
                    int cardX = lane.BodyBounds.X + PADDING;
                    int cardY = lane.BodyBounds.Y + PADDING - iScrollPos;
                    int width = lane.BodyBounds.Width - PADDING * 2;

                    Rectangle rcClip = lane.BodyBounds;
                    rcClip.Inflate(-1, -1);
                    g.SetClip(rcClip);

                    foreach (TaskCard card in lane.TaskCards)
                    {
                        if (card.Task == SelectedTask)
                            bSelected = true;
                        else
                            bSelected = false;

                        Rectangle cardBounds = new Rectangle(cardX, cardY, width, APPHEIGHT);

                        card.Bounds = cardBounds;
                        cardY += APPHEIGHT + PADDING;

                        if (cardBounds.Top > rcClip.Bottom || cardBounds.Bottom < rcClip.Top)
                            continue;

                        DrawTaskCard(g, card, bSelected);
                        //Image image = DrawTaskCard(card, cardBounds.Size, bSelected);

                        //g.DrawImage(image, cardBounds.Location);


                    }

                    g.ResetClip();
                }
                xCurrent += laneWidth;
            }

            g.Dispose();

            e.Graphics.DrawImage(img, ClientRectangle.Location);
        }


        private void DrawTaskCard(Graphics g, TaskCard card, bool bSelected)
        {
            if (card == null)
                return;

            if (bSelected)
            {
                RendererCache.Current.AppointmentSelected.DrawBox(g, Font, card.BodyBounds, card.FormattedSubject);
                if (card.ColourBlockBounds != Rectangle.Empty)
                {
                    RendererCache.Current.AppointmentSelected.DrawBox(g, Font, card.ColourBlockBounds, card.Task.ColorBlockBrush);
                }
            }
            else
            {
                RendererCache.Current.Appointment.DrawBox(g, Font, card.BodyBounds, card.FormattedSubject);
                if (card.ColourBlockBounds != Rectangle.Empty)
                {
                    RendererCache.Current.Appointment.DrawBox(g, Font, card.ColourBlockBounds, card.Task.ColorBlockBrush);

                }
            }
        }

        protected virtual Image DrawTaskCard(TaskCard app, Size size, bool bSelected)
		{
            if (app == null || size.IsEmpty)
                return null;

            Image img = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(img);

            Rectangle bounds = new Rectangle(0, 0, size.Width - 1, size.Height - 1);

            Rectangle bodyBounds = bounds;
            Rectangle colourBlockBounds = Rectangle.Empty;
            if (bounds != Rectangle.Empty && bounds.Width > 5)
            {
                bodyBounds = new Rectangle() { Width = bounds.Width - 3, Height = bounds.Height, X = bounds.X + 3, Y = bounds.Y };
                colourBlockBounds = new Rectangle() { Width = 3, Height = bounds.Height, X = bounds.X, Y = bounds.Y };
            }
            else
            {
                bodyBounds = bounds;
                colourBlockBounds = Rectangle.Empty;
            }

            if (bSelected)
            {
                RendererCache.Current.AppointmentSelected.DrawBox(g, Font, bodyBounds, app.FormattedSubject);
                if (colourBlockBounds != Rectangle.Empty)
                {
                    RendererCache.Current.AppointmentSelected.DrawBox(g, Font, colourBlockBounds, app.Task.ColorBlockBrush);
                }
            }
            else
            {
                RendererCache.Current.Appointment.DrawBox(g, Font, bodyBounds, app.FormattedSubject);
                if (colourBlockBounds != Rectangle.Empty)
                {
                    RendererCache.Current.Appointment.DrawBox(g, Font, colourBlockBounds, app.Task.ColorBlockBrush);
                }
            }

            return img;
        }


        private void SetScrollInfo()
        {
            int iMaxCount = 0;
            foreach (ScrumLane lane in Lanes)
            {
                iMaxCount = Math.Max(iMaxCount, lane.TaskCards.Count);
            }

            Graphics g = this.CreateGraphics();
            int headerHeight = (int)(RendererCache.Current.Header.GetTextInfo(g, this.Font).Height * 1.2);
            g.Dispose();

            int height = headerHeight + (APPHEIGHT + PADDING) * iMaxCount + PADDING;//, Height);

            int width = ClientSize.Width;


            if (height > Height)
            {
                //VerticalScroll.Visible = true;
                //VerticalScroll.Enabled = true;
                //VerticalScroll.Maximum = height;
                VScroll = true;
            }
            else
            {
                VScroll = false;
                //VerticalScroll.Visible = false;
                //VerticalScroll.Enabled = false;
                //VerticalScroll.Maximum = Height;
            }
            width = this.ClientSize.Width;
            AutoScroll = true;
            AutoScrollMinSize = new Size(width, height);
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);

            //VerticalScroll.Value = se.NewValue;

            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            Invalidate();
        }
    }
}