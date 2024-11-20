using Microsoft.VisualBasic;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace timeTable
{
    public partial class Form1 : Form
    {
        private Controller controller;
        private DateTime currentWeek;
        private int panelWidth;
        private List<Label> headerLabels;

        public Form1()
        {
            currentWeek = calcFirstDayOfWeek(DateTime.Today);
            InitializeComponent();
            controller = new Controller(this, panel1.Width, panel1.Height);
            updateCurrWeekLabel(currentWeek);
            float tempFloatWidth = panel1.Width / 7;
            panelWidth = Convert.ToInt32(tempFloatWidth);
            headerLabels = setupColumnHeader();
            updateColumnHeader();
            panel1.Paint += Form1_Paint;
        }

        private List<Label> setupColumnHeader() {
            List<Label> headers = new List<Label>();
            for (int i = 0; i < 7; i++)
            {
                Panel headerPanel = new Panel();
                Label daysOfWeekLabel = new Label();
                daysOfWeekLabel.Text = currentWeek.AddDays(i).DayOfWeek.ToString();
                daysOfWeekLabel.TextAlign = ContentAlignment.MiddleCenter;
                daysOfWeekLabel.Dock = DockStyle.Bottom;
                Label dayLabel = new Label();
                dayLabel.Text = currentWeek.AddDays(i).ToString("dd");
                dayLabel.TextAlign = ContentAlignment.MiddleCenter;
                dayLabel.Dock = DockStyle.Bottom;
                headerPanel.Controls.Add(daysOfWeekLabel);
                headerPanel.Controls.Add(dayLabel);
                headerPanel.SetBounds(panel1.Location.X + (i * panelWidth), panel1.Location.Y - headerPanel.Height, panelWidth, headerPanel.Height);
                Controls.Add(headerPanel);
                headers.Add(dayLabel);
            }
            return headers;
        }

        private void updateColumnHeader()
        {
            for (int i = 0; i < 7; i++)
            {
                headerLabels[i].Text = currentWeek.AddDays(i).ToString("dd");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(controller);
            f2.ShowDialog();
        }

        public void listLessons(List<Lesson> lessons)
        {
            panel1.Controls.Clear();
            
            List<Lesson> thisWeeksLessons = lessons.Where(x => isCurrentWeek(x.StartDate)).ToList();
            foreach (var item in thisWeeksLessons)
            {
                Color foreColor =  (((0.299 * item.LessonColor.R + 0.587 * item.LessonColor.G + 0.114 * item.LessonColor.B) / 255.0)<=0.5)? Color.White : Color.Black;
                Panel p1 = new Panel();
                p1.Tag = item.Id;
                Label l1 = new Label();
                l1.Text = item.Title;
                l1.ForeColor = foreColor;
                l1.BackColor = Color.Transparent;
                l1.Font = new Font(l1.Font,FontStyle.Bold);
                l1.Dock = DockStyle.Top;
                Label l2 = new Label();
                l2.Text = item.Description;
                l2.ForeColor = foreColor;
                l2.BackColor = Color.Transparent;
                l2.Dock = DockStyle.Top;
                Label l3 = new Label();
                l3.Text = item.StartDate.ToString("HH:mm") + " - " + item.EndDate.ToString("HH:mm");
                l3.ForeColor = foreColor;
                l3.BackColor = Color.Transparent;
                l3.Dock = DockStyle.Top;
                p1.Controls.Add(l3);
                p1.Controls.Add(l2);
                p1.Controls.Add(l1);
                p1.SetBounds((int)item.X, (int)item.Y, (int)item.Width, (int)item.Height);
                p1.BackColor = item.LessonColor;
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Edit");
                ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Delete");
                menuItem1.Click += MenuItem1_Click;
                menuItem2.Click += MenuItem2_Click;
                contextMenuStrip.Items.Add(menuItem1);
                contextMenuStrip.Items.Add(menuItem2);
                p1.ContextMenuStrip = contextMenuStrip;
                panel1.Controls.Add(p1);
            }
            panel1.Invalidate();
        }

        private void MenuItem2_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            if (menuItem.GetCurrentParent() is ContextMenuStrip contextMenu && contextMenu.SourceControl is Panel parentPanel)
            {
                controller.deleteLessonById((long)parentPanel.Tag);
            }
        }

        private void MenuItem1_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            if (menuItem.GetCurrentParent() is ContextMenuStrip contextMenu && contextMenu.SourceControl is Panel parentPanel)
            {
                Form2 updateForm = new Form2(controller,controller.getLessonById((long)parentPanel.Tag));
                updateForm.ShowDialog();
            }
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            for (int i = 0; i < 6; i++)
            {
                g.DrawLine(pen, (i+1) * panelWidth, 0, (i+1) * panelWidth, panel1.Height + panel1.Location.Y);
            }
            TimeOnly time = new TimeOnly(8,0);
            TimeOnly limit = new TimeOnly(20,0);
            float scale = (limit.Hour - time.Hour) * 60;
            int j = 0;
            while (time <= limit)
            {
                Label l2 = new Label();
                l2.Text = time.ToString();
                l2.Width = panel1.Location.X;
                int offset = Convert.ToInt32(panel1.Height * (((time.Hour - 8) * 60 + time.Minute) / scale));
                l2.Location = new Point(0, panel1.Location.Y - (l2.Height / 2) + offset);
                l2.TextAlign = ContentAlignment.MiddleCenter;
                this.Controls.Add(l2);
                time = time.AddMinutes(60);
                if(j!=0) {
                    g.DrawLine(pen, new Point(0, offset), new Point(panel1.Width, offset));
                }
                j++;
            }
            pen.Dispose();
        }

        private bool isCurrentWeek(DateTime d)
        {
            DateTime temp = calcFirstDayOfWeek(d);
            return (temp.Year == currentWeek.Year && temp.Month == currentWeek.Month && temp.Day == currentWeek.Day);
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            currentWeek = currentWeek.AddDays(-7);
            updateCurrWeekLabel(currentWeek);
            updateColumnHeader();
            controller.listLessons();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            currentWeek = currentWeek.AddDays(7);
            updateCurrWeekLabel(currentWeek);
            updateColumnHeader();
            controller.listLessons();
        }
        private void updateCurrWeekLabel(DateTime currentWeek)
        {
            label1.Text = currentWeek.ToString("yyyy/MMMM/dd");
        }
        private DateTime calcFirstDayOfWeek(DateTime dateTime)
        {
            int day = (int)dateTime.DayOfWeek + 6;
            day = day % 7;
            return dateTime.AddDays(-day);
        }
        
    }
}
