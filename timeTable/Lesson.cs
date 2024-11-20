using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timeTable
{
    
    public class Lesson
    {
        public int PanelWidth { get; private set; }
        public int PanelHeight { get; private set; }
        public DateTime StartDate { get ; set; }
        public DateTime EndDate { get; private set; }
        public int Duration { get; set; }
        public int Day {  get; private set; }
        public long Id { get; private set; }

        public float Height { get; private set; }
        public float Width { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public Color LessonColor { get; set; }
        public String Title {  get; set; }
        public String Description {  get; set; }

        public Lesson(DateTime startDate, int duration,Color color, string title, string description, long id,int panelWidth, int panelHeight)
        {
            this.PanelWidth = panelWidth;
            this.PanelHeight = panelHeight;
            this.Id = id;
            this.StartDate = startDate;
            this.Duration = duration;
            this.LessonColor = color;
            this.Title = title;
            this.Description = description;
            calculateData();
        }
        /**<summary>
         * 
         * Calculates and sets:
         * - the EndDate based on the Duration and StartDate.
         * - the Day based on the StartDate.
         * - the Width, X, Height, and Y properties based on the PanelWidth, PanelHeight, and StartDate.
         * </summary>
        */
        public void calculateData()
        {
            DateTime temp = new DateTime(1, 1, 1, 0, 0, 0);
            temp = temp.AddMinutes(this.Duration);
            EndDate = new DateTime(this.StartDate.Ticks + temp.Ticks);
            int tempDay = (int)this.StartDate.DayOfWeek + 6;
            this.Day = tempDay % 7;
            int cellWidth = PanelWidth / 7;
            this.Width = PanelWidth / 7 - 1;
            this.X = this.Day * cellWidth + 1;
            float dayMinutes = 12 * 60;
            this.Height = PanelHeight * (temp.Hour * 60 + temp.Minute) / dayMinutes;
            this.Y = PanelHeight * (((this.StartDate.Hour - 8) * 60 + this.StartDate.Minute) / dayMinutes);
        }
    }
}
