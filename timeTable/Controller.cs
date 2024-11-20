using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timeTable
{
    public class Controller
    {
        private Schedule schedule;
        private Form1 mainForm;
        private int panelWidth;
        private int panelHeight;
        public Controller(Form1 f1, int panelWidth, int panelHeight) { 
            this.schedule = new Schedule();
            this.mainForm = f1;
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            this.schedule.readLessonsFromFile("lessons.txt",panelWidth,panelHeight);
            this.listLessons();
        }
        public void addLesson(DateTime start, int duration,Color color, String title, String classRoom)
        {

            Lesson lesson = new Lesson(start, duration, color, title, classRoom, getNewId(), panelWidth, panelHeight);
            schedule.addLesson(lesson);
            schedule.updateFile();
            mainForm.listLessons(schedule.getLessons());
        }
        public void listLessons()
        {
            mainForm.listLessons(schedule.getLessons());
        }
        public Lesson getLessonById(long id)
        {
            return schedule.getLessonById(id);
        }
        public void deleteLessonById(long id) {
            schedule.deleteLessonById(id);
            schedule.updateFile();
            mainForm.listLessons(schedule.getLessons());
        }
        public void updateLessonById(long id, DateTime start, int duration, Color color, String title, String classRoom)
        {

            Lesson updateableLesson = schedule.getLessonById(id);
            updateableLesson.StartDate = start;
            updateableLesson.Duration = duration;
            updateableLesson.Title = title;
            updateableLesson.LessonColor = color;
            updateableLesson.Description = classRoom;
            updateableLesson.calculateData();
            schedule.updateFile();
            mainForm.listLessons(schedule.getLessons());
        }
        private long getNewId()
        {
            return schedule.generateNewId();
        }
    }
}
