using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace timeTable
{
    internal class Schedule
    {
        private static List<Lesson> lessons = new List<Lesson>();

        public List<Lesson> getLessons()
        {
            return lessons;
        }
        public void readLessonsFromFile(String fileName, int panelWidth, int panelHeight)
        {
            try
            {
                using(StreamReader sr = new StreamReader(fileName))
                {
                    while (!sr.EndOfStream)
                    {
                        String[] pieces = sr.ReadLine().Trim().Split(";");
                        long id = (long)Convert.ToInt32(pieces[0]);
                        DateTime start = Convert.ToDateTime(pieces[1]);
                        int duration = Convert.ToInt32(pieces[2]);
                        String title = pieces[3];
                        String desc = pieces[4];
                        Color color = Color.FromArgb(Convert.ToInt32(pieces[5]));
                        Lesson lesson = new Lesson(start, duration, color,title,desc,id,panelWidth,panelHeight);
                        lessons.Add(lesson);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
            }
        }
        public void addLesson(Lesson lesson)
        {
            lessons.Add(lesson);
        }
        public Lesson getLessonById(long id) 
        {
            return lessons.First(x => x.Id == id);
        }
        public void deleteLessonById(long id)
        {
            lessons.Remove(lessons.First(x => x.Id == id));
        }
        public void updateFile()
        {
            using(StreamWriter sw = new StreamWriter("lessons.txt"))
            {
                foreach (Lesson lesson in lessons)
                {
                    sw.WriteLine("{0};{1};{2};{3};{4};{5}",lesson.Id,lesson.StartDate,lesson.Duration,lesson.Title,lesson.Description,lesson.LessonColor.ToArgb());
                }
            }

        }
        public long generateNewId()
        {
            return lessons.OrderByDescending(x => x?.Id).FirstOrDefault()?.Id + 1 ?? 1;
        }
    }
}
