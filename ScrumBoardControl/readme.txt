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



INTRODUCTION

This control suite is a prototype of a set of calendar controls for a windows 
forms .NET application. They were created as a proof-of-concept that an outlook
style calendar could be integrated into a large windows forms application 
without the overhead of using a third party library.

Three controls are included - DayScheduleControl, WeekScheduleControl and 
MonthScheduleControl. The controls render a series of appointments in different 
layouts. I am unlikely to have time to enhance them further so will release 
them in the hope they useful to others.


FEATURES

DayScheduleControl is the closest in appearance to outlook. It supports one-day 
and five-day views.

WeekScheduleControl displays a 7-day week. Unlike DayScheduleControl, it does 
not show hour slots.

MonthScheduleControl displays a whole month of appointments.

The requirements were:
- support Windows 7 styles
- support keyboard access
- be accessible to the disabled
- don't use much memory

The controls have a hidden DataGridView control in order to save time setting up
the keyboard access and the accessibility features. 

The VisualStyleRenderer is used to pick up the current Windows theme and display
with the colours from that. If visual styles are disabled the regular windows
colours are used.

Speed was the primary consideration in the design of the app, so there isn't 
much usage of events. Likewise day and appointment items are not controls 
themselves - that would be slow to render. 

The controls don't include default dialogs for moving/editing/new appointments,
but the demo project includes sample dialogs wired up to those three events. 
Dragging and dropping appointments is supported.

The demo application loads up a couple of months worth of random dummy data.


NOT YET IMPLEMENTED

Missing features in the current version:
- Lots more stuff on the controls should be configurable in properties.
- Support for full-day or multiple-day appointments.
- Tooltips when you hover over an appointment (to display the full subject).
- XP, high contrast mode, high DPI support.
- DayScheduleControl doesn't support weekends, out-of-hours appointments, or 
scrolling.
- Controls haven't been tested under a screen reader, the DataGridView may not
have all the properties set for it to be readable.
- The DayScheduleControl handles overlapping appointments, but the maths it uses
isn't very good.
- Time slot navigation/selection with keyboard or mouse.
- Better highlight of current day.


