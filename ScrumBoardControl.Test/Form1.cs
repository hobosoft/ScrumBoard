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
using System.Windows.Forms;
using System.Threading;
using KtSoft.ScrumControls.Data;
using KtSoft.ScrumControls.Test.Dialog;
using KtSoft.ScrumControls.Region;
using KtSoft.ScrumControls.Events;

namespace KtSoft.ScrumControls.Test
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
        }

        /// <summary>
        /// Creates random appointments.
        /// </summary>
        /// <param name="date">The start date.</param>
        /// <returns></returns>
        private static TaskList CreateRandomAppointments(DateTime date)
        {
            List<Brush> brushes = new List<Brush>();
            brushes.Add(Brushes.LimeGreen);
            brushes.Add(Brushes.PowderBlue);
            brushes.Add(Brushes.DarkGreen);
            brushes.Add(Brushes.Green);
            brushes.Add(Brushes.DimGray);
            brushes.Add(Brushes.Red);
            brushes.Add(Brushes.Yellow);
            brushes.Add(Brushes.Aquamarine);
            brushes.Add(Brushes.Plum);
            brushes.Add(Brushes.Orange);
            brushes.Add(Brushes.Pink);

            List<string> titles = new List<string>();
            titles.Add("Hide from boss");
            titles.Add("Steal office supplies");
            titles.Add("Coffee break");
            titles.Add("Pranks");
            titles.Add("Pretend to be working");
            titles.Add("Surf the internet");
            titles.Add("Set off fire alarm");
            titles.Add("Stare out window");
            titles.Add("Pointless meeting");
            titles.Add("Spin around on chair");
            titles.Add("Eat something");
            titles.Add("Text people");
            titles.Add("Play games on phone");
            titles.Add("Talk to attractive coworker");
            titles.Add("Write job applications for other organisations");
            titles.Add("Online banking");
            titles.Add("Change desktop themes");
            titles.Add("Listen to music");
            titles.Add("Move car to avoid paying for parking");

            //create 7am of last monday
            DateTime timeStart = new DateTime(date.Year, date.Month, date.Day, 7, 0, 0);
            while (timeStart.DayOfWeek != DayOfWeek.Monday)
            {
                timeStart = timeStart.AddDays(-1);
            }

            var tasks = new TaskList();
            var rand = new Random();

			//for (int i = 0; i < 2000; i++)
			//{
			//    int hoursToAdd = rand.Next(0, 11);
			//    int weeksToAdd = rand.Next(0, 30);
			//    int daysToAdd = rand.Next(0, 5);
			//    int minsDuration = rand.Next(1, 10) * 15;

			//    string title = titles[rand.Next(0, 18)];
			//    Brush brush = brushes[rand.Next(0, 10)];

			//    ExtendedAppointment app = new ExtendedAppointment();
			//    app.ColorBlockBrush = brush;
			//    app.Subject = title;
			//    app.DateStart = timeStart.AddDays(7 * weeksToAdd + daysToAdd).AddHours(hoursToAdd);
			//    app.DateEnd = app.DateStart.AddMinutes(minsDuration);

			//    appts.Add(app);
			//}

            for (int i = 0; i < 20; i++)
            {
				int hoursToAdd = rand.Next(0, 18);
				//int weeksToAdd = rand.Next(0, 30);
				//int daysToAdd = rand.Next(5, 7);
				//int minsDuration = rand.Next(1, 10) * 15;

				int state = rand.Next(0, 5);

				string title = titles[hoursToAdd];//"Unpaid overtime";
                Brush brush = brushes[rand.Next(0, 10)];

                TaskItem task = new TaskItem();
                task.ColorBlockBrush = brush;
                task.Subject = title;
				task.State = (TaskState)state;
				//app.DateStart = timeStart.AddDays(7 * weeksToAdd + daysToAdd).AddHours(hoursToAdd);
				//app.DateEnd = app.DateStart.AddMinutes(minsDuration);

                tasks.Add(task);
            }

            //appts.SortAppointments();
            return tasks;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DoubleBuffered = true;

            DateTime weekstart = DateTime.Now;
            TaskList appts = CreateRandomAppointments(weekstart);

            foreach(TaskItem task in appts)
            {
			    scrumScheduleControl1.AddTask(task);
            }

			scrumScheduleControl1.TaskCardMove += new EventHandler<TaskMoveEventArgs>(scrumScheduleControl1_AppointmentMove);
			//weekView1.Date = weekstart;
			//weekView1.Appointments = appts;
			//monthView1.Date = weekstart;
			//monthView1.Appointments = appts;
			//dayView1.Date = weekstart;
			//dayView1.Appointments = appts;
			//dayView2.Date = weekstart;
			//dayView2.Appointments = appts;

			//lblApptCount.Text = "" + dayView1.Appointments.Count;
			//lblCurrentDate.Text = weekstart.ToLongDateString();


			//weekView1.AppointmentCreate += calendar_AppointmentAdd;
			//monthView1.AppointmentCreate += calendar_AppointmentAdd;
			//dayView1.AppointmentCreate += calendar_AppointmentAdd;
			//dayView2.AppointmentCreate += calendar_AppointmentAdd;

			//weekView1.AppointmentMove += calendar_AppointmentMove;
			//monthView1.AppointmentMove += calendar_AppointmentMove;
			//dayView1.AppointmentMove += calendar_AppointmentMove;
			//dayView2.AppointmentMove += calendar_AppointmentMove;

			//weekView1.AppointmentEdit += calendar_AppointmentEdit;
			//monthView1.AppointmentEdit += calendar_AppointmentEdit;
			//dayView1.AppointmentEdit += calendar_AppointmentEdit;
			//dayView2.AppointmentEdit += calendar_AppointmentEdit;



        }

		void scrumScheduleControl1_AppointmentMove(object sender, TaskMoveEventArgs e)
		{
			if (e.Appointment == null)
				return;

			(e.Appointment.Task as TaskItem).State = (e.NewLane as ScrumLane).State;
			//scrumScheduleControl1.RefreshAppointments();
			scrumScheduleControl1.Invalidate();

			            //show a dialog to move the appointment date
            //using (MoveAppointment dialog = new MoveAppointment())
            {
				//dialog.AppointmentOldDateStart = e.Appointment.DateStart;
				//dialog.AppointmentOldDateEnd = e.Appointment.DateEnd;
				//dialog.AppointmentTitle = e.Appointment.Subject;
				////if (e.NewDate != null)
				//{
				//    dialog.AppointmentDateStart = e.NewDate;
				//    dialog.AppointmentDateEnd = new DateTime(e.NewDate.Ticks + (dialog.AppointmentOldDateEnd.Ticks - dialog.AppointmentOldDateStart.Ticks));
				//}
				//DialogResult result = dialog.ShowDialog();
				//if (result == DialogResult.OK)
				//{
				//    //if the user clicked 'save', update the appointment dates
				//    e.Appointment.DateStart = dialog.AppointmentDateStart;
				//    e.Appointment.DateEnd = dialog.AppointmentDateEnd;

				//    //have to tell the controls to refresh appointment display
				//    weekView1.RefreshAppointments();
				//    monthView1.RefreshAppointments();
				//    dayView1.RefreshAppointments();
				//    dayView2.RefreshAppointments();

				//    //get the controls to repaint 
				//    weekView1.Invalidate();
				//    monthView1.Invalidate();
				//    dayView1.Invalidate();
				//    dayView2.Invalidate();
				//}
            }
			//throw new NotImplementedException();
		}

        /// <summary>
        /// Handles the AppointmentEdit event of the calendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KtSoft.ScrumControls.Events.TaskEditEventArgs"/> instance containing the event data.</param>
        private void calendar_AppointmentEdit(object sender, TaskEditEventArgs e)
        {
            //show a dialog to edit the appointment
            using (EditAppointment dialog = new EditAppointment())
            {
				//dialog.AppointmentDateStart = e.Appointment.DateStart;
				//dialog.AppointmentDateEnd = e.Appointment.DateEnd;
				//dialog.AppointmentTitle = e.Appointment.Subject;
				//DialogResult result = dialog.ShowDialog();
				//if (result == DialogResult.OK)
				//{
				//    //if the user clicked 'save', update the appointment dates and title
				//    e.Appointment.DateStart = dialog.AppointmentDateStart;
				//    e.Appointment.DateEnd = dialog.AppointmentDateEnd;
				//    e.Appointment.Subject = dialog.AppointmentTitle;

                    //have to tell the controls to refresh appointment display
					//weekView1.RefreshAppointments();
					//monthView1.RefreshAppointments();
					//dayView1.RefreshAppointments();
					//dayView2.RefreshAppointments();

					////get the controls to repaint 
					//weekView1.Invalidate();
					//monthView1.Invalidate();
					//dayView1.Invalidate();
					//dayView2.Invalidate();
                //}
            }
        }


        /// <summary>
        /// Handles the AppointmentMove event of the calendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KtSoft.ScrumControls.Events.TaskMoveEventArgs"/> instance containing the event data.</param>
        private void calendar_AppointmentMove(object sender, TaskMoveEventArgs e)
        {
            //show a dialog to move the appointment date
            using (MoveAppointment dialog = new MoveAppointment())
            {
				//dialog.AppointmentOldDateStart = e.Appointment.DateStart;
				//dialog.AppointmentOldDateEnd = e.Appointment.DateEnd;
                dialog.AppointmentTitle = e.Appointment.Task.Subject;
				//if (e.NewDate != null)
				//{
				//    dialog.AppointmentDateStart = e.NewDate;
				//    dialog.AppointmentDateEnd = new DateTime(e.NewDate.Ticks + (dialog.AppointmentOldDateEnd.Ticks - dialog.AppointmentOldDateStart.Ticks));
				//}
				//DialogResult result = dialog.ShowDialog();
				//if (result == DialogResult.OK)
				//{
				//    //if the user clicked 'save', update the appointment dates
				//    e.Appointment.DateStart = dialog.AppointmentDateStart;
				//    e.Appointment.DateEnd = dialog.AppointmentDateEnd;

				//    //have to tell the controls to refresh appointment display
				//    weekView1.RefreshAppointments();
				//    monthView1.RefreshAppointments();
				//    dayView1.RefreshAppointments();
				//    dayView2.RefreshAppointments();

				//    //get the controls to repaint 
				//    weekView1.Invalidate();
				//    monthView1.Invalidate();
				//    dayView1.Invalidate();
				//    dayView2.Invalidate();
				//}
            }
        }

        /// <summary>
        /// Handles the AppointmentAdd event of the calendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KtSoft.ScrumControls.Events.TaskCreateEventArgs"/> instance containing the event data.</param>
        private void calendar_AppointmentAdd(object sender, TaskCreateEventArgs e)
        {
            //show a dialog to add an appointment
            //using (NewAppointment dialog = new NewAppointment())
            {
				//if (e.Date != null)
				//{
				//    dialog.AppointmentDateStart = e.Date.Value;
				//    dialog.AppointmentDateEnd = e.Date.Value.AddMinutes(15);
				//}
				//DialogResult result = dialog.ShowDialog();
				//if (result == DialogResult.OK)
				//{
				//    //if the user clicked 'save', save the new appointment 
				//    string title = dialog.AppointmentTitle;
				//    DateTime dateStart = dialog.AppointmentDateStart;
				//    DateTime dateEnd = dialog.AppointmentDateEnd;
				//    e.Control.Appointments.Add(new ExtendedAppointment() { Subject = title, DateStart = dateStart, DateEnd = dateEnd });

				//    //have to tell the controls to refresh appointment display
				//    weekView1.RefreshAppointments();
				//    monthView1.RefreshAppointments();
				//    dayView1.RefreshAppointments();
				//    dayView2.RefreshAppointments();

				//    //get the controls to repaint 
				//    weekView1.Invalidate();
				//    monthView1.Invalidate();
				//    dayView1.Invalidate();
				//    dayView2.Invalidate();
				//}
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the MonthCalendar1 control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        void MonthCalendar1DateSelected(object sender, DateRangeEventArgs e)
        {
			//weekView1.Date = e.Start;
			//monthView1.Date = e.Start;
			//dayView1.Date = e.Start;
			//dayView2.Date = e.Start;

			//if (dayView1.Appointments != null)
			//{
			//    lblApptCount.Text = "" + dayView1.Appointments.Count;
			//}
			//lblCurrentDate.Text = e.Start.ToLongDateString();

			//weekView1.Invalidate();
			//monthView1.Invalidate();
			//dayView1.Invalidate();
			//dayView2.Invalidate();
        }
    }
}
