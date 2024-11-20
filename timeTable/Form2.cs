using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timeTable
{
    public partial class Form2 : Form
    {
        private Controller controller;
        private Color selectedColor;
        private Lesson? updateableLesson;
        public Form2(Controller controller,Lesson? updateableLesson = null)
        {
            InitializeComponent();
            SetMyCustomFormat();
            this.controller = controller;
            selectedColor = Color.AliceBlue;
            this.updateableLesson = updateableLesson;
            if (updateableLesson != null)
            {
                dateTimePicker1.Value = updateableLesson.StartDate;
                dateTimePicker2.Value = new DateTime(dateTimePicker2.MinDate.Year, dateTimePicker2.MinDate.Month, dateTimePicker2.MinDate.Day, updateableLesson.StartDate.Hour, updateableLesson.StartDate.Minute, updateableLesson.StartDate.Second);
                selectedColor = updateableLesson.LessonColor;
                textBox1.Text = updateableLesson.Title;
                textBox2.Text = updateableLesson.Description;
                textBox3.Text = updateableLesson.Duration.ToString();
            }
            button2.BackColor = selectedColor;
        }

        public void SetMyCustomFormat()
        {
            dateTimePicker2.Value = new DateTime(dateTimePicker2.MinDate.Year, dateTimePicker2.MinDate.Month, dateTimePicker2.MinDate.Day, 12, 0, 0);
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "HH - mm";
            dateTimePicker2.MinDate = new DateTime(dateTimePicker2.MinDate.Year,dateTimePicker2.MinDate.Month,dateTimePicker2.MinDate.Day,8,0,0);
            dateTimePicker2.MaxDate = new DateTime(dateTimePicker2.MinDate.Year,dateTimePicker2.MinDate.Month,dateTimePicker2.MinDate.Day,18,30,0);
            dateTimePicker2.ShowUpDown = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();


            colorDialog.Color = Color.AliceBlue;


            if (colorDialog.ShowDialog() == DialogResult.OK)
            {

                selectedColor = colorDialog.Color;
                button2.BackColor = selectedColor;

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime dt1 = dateTimePicker1.Value;
            int year = dt1.Year;
            int month = dt1.Month;
            int day = dt1.Day;
            DateTime dt2 = dateTimePicker2.Value;
            int hour = dt2.Hour;
            int minute = dt2.Minute;
            DateTime dateToPass = new DateTime(year, month, day, hour, minute, 0);
            int dur;
            try
            {
                dur = int.Parse(textBox3.Text);
            }
            catch (Exception ex)
            {
                dur = 90;
            }

            String title = textBox1.Text;
            String classRoom = textBox2.Text;
            if (updateableLesson is null)
            {
                controller.addLesson(dateToPass, dur, selectedColor, title, classRoom);
            }
            else
            {
                controller.updateLessonById(updateableLesson.Id,dateToPass,dur,selectedColor,title,classRoom);
            }
            this.Close();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
