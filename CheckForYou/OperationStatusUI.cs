using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CheckForYou2
{
    public partial class OperationStatusUI : Form
    {
        private Student student;
        private Teacher teacher;
        private Lecture lecture;
        private Course course;
        private Payment payment;

        private List<Student> students;
        private List<Teacher> teachers;
        private List<Lecture> lectures;

        public OperationStatusUI()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StudentManagementUI sr = new StudentManagementUI(); sr.Show();
            this.Hide();
            sr.Owner = this;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            TeacherManagementUI sr = new TeacherManagementUI(); sr.Show();
            this.Hide();
            sr.Owner = this;
        }




        private void OperationStatusUI_Load(object sender, EventArgs e)
        {
            students = new List<Student>();
            student = null;
            teachers = new List<Teacher>();
            teacher = null;
            lectures = new List<Lecture>();

            loadStudentData();
            loadTeacherData();


            int ticketNum = 0;
            int allticket = 0;
            HashSet<String> teacherstudent = new HashSet<String>();

            String text2 = "<강사별 학생수(티켓수)>"+ "\r\n";
            for (int i = 0; i < teachers.Count; i++)
            {
                for (int j = 0; j < teachers[i].lectures.Count; j++)
                {
                    for (int k = 0; k < teachers[i].lectures[j].students.Count; k++) teacherstudent.Add(teachers[i].lectures[j].students[k].name);
                    ticketNum += teachers[i].lectures[j].students.Count;
                }
                text2 += teachers[i].teachername + " 선생님: " + teacherstudent.Count + " (" + ticketNum + ") " + "\r\n";
                allticket += ticketNum;
                ticketNum = 0;
                teacherstudent.Clear();
            }



            String text1 = "강사수: " + teachers.Count + "\r\n" + "학생수: " + students.Count + "\r\n" + "티켓수: "+allticket+ "\r\n" + "\r\n";

            textBox1.Text = text1+text2;

            
            /*
            String text2 = "<강사별 학생수(티켓수)>\n";
            int ticketNum=0;
            int allticket = 0;
            HashSet<String> teacherstudent = new HashSet<String>();
            for(int i=0; i < teachers.Count; i++)
            {
                for(int j=0; j < teachers[i].lectures.Count; j++)
                {
                    //for (int k = 0; j < teachers[i].lectures[j].students.Count; k++) teacherstudent.Add(teachers[i].lectures[j].students[k].name);
                    ticketNum +=teachers[i].lectures[j].students.Count;
                }
                text2 += teachers[i].teachername + " 선생님: " + teacherstudent.Count + " ("+ticketNum+") \n";
                allticket += ticketNum;
            }
            text2 += "전체: " + students.Count + " (" + allticket + ") \n\n";
            */
        }


        private void loadStudentData()
        {
            if (!File.Exists("a.dat")) return;

            Stream rs = new FileStream("a.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            students.Clear();
            students = (List<Student>)deserializer.Deserialize(rs);
            rs.Close();
        }


        private void loadTeacherData()
        {
            if (!File.Exists("teacher.dat")) return;

            Stream rs = new FileStream("teacher.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            teachers.Clear();
            teachers = (List<Teacher>)deserializer.Deserialize(rs);
            rs.Close();

        }

        private void Button13_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
