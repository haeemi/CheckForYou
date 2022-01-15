using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckForYou2
{
    class CFU
    {
    }
    class Space
    {
        public List<Student> studentInfo;
        public Student currentStudent;
        public String select=null;

    }

    [Serializable]
   public class Student
    {
        public String name;
        public String grade;
        public String school;
        public String tel;
        public String parent;
        public String hometel;
        public String holding; //보유액
        public String parentTel;
        public bool bro;
        public String memo;
        public List<Course> course=new List<Course>();

        public Student(String name, String grade, String school, String tel, String parent, String parentTel, String hometel, String holding, bool bro)
        {
           this.name = name;
            this.grade = grade;
            this.school = school;
            this.tel = tel;
            this.parent = parent;
            this.parentTel = parentTel;
            this.hometel = hometel;
            this.holding = holding;
            this.bro = bro;
        }
        
        void addCourse(Course course)
        {
            this.course.Add(course);
        }
    }

    [Serializable]
    public class Teacher
    {
        public String teachername=null;
        public List<Lecture> lectures = new List<Lecture>();
        public Teacher(String teachername)
        {
            this.teachername = teachername;
        }

        void addLecture(Lecture lecture)
        {
            this.lectures.Add(lecture);
        }
    }
    [Serializable]
    public class Lecture
    {
        public String lecturename;
        public String lecturetime;
        public String lecturefee;
        public List<Student> students=new List<Student>();
        public List<String> lectureweek=new List<String>();
        public Lecture(String lecturename, String lecturetime, String lecturefee, List<String> lectureweek)
        {
            this.lecturename = lecturename;
            this.lecturetime = lecturetime;
            this.lecturefee = lecturefee;
            this.lectureweek = lectureweek;
        }
        public String getweek()
        {
            String str="";
            for (int i = 0; i < lectureweek.Count; i++) { str += lectureweek[i]; }
                return str;
        }
        void addStudent(Student student)
        {
            this.students.Add(student);
        }

    }

    [Serializable]
    public class Course
    {
        public Lecture courseInfo;
        public Payment payment;
        public Course(Lecture courseInfo, Payment payment)
        {
            this.courseInfo = courseInfo;
            this.payment = payment;
        }
    }


    [Serializable]
    public class Payment
    {
        public String month;
        public bool state;
        public String type;
        public String balance;

        public String discountkind;
        public String discount;
        public String discountreason;

        public String schedule;

        public Payment(String month, bool state=false, String type ="-", String balance = "-")
        {
            this.month = month;
            this.state = state;
            this.type = type;
            this.balance = balance;
        }
    }
}
