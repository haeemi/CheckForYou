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

    public partial class LectureRepair : Form
    {

        Student student;
        Teacher teacher;
        Lecture lecture;
        Course course;
        Payment payment;

        List<Student> students;
        List<Student> selectedStudents;
        List<Teacher> teachers;

        public LectureRepair()
        {
            InitializeComponent();
        }

        private void LectureRepair_Load(object sender, EventArgs e)
        {

            students = new List<Student>();
            student = null;
            selectedStudents = new List<Student>();
            teachers = new List<Teacher>();
            teacher = null;
            lecture = null;
            course = null;
            payment = null;

            DataGridViewTextBoxColumn teacherColum = new DataGridViewTextBoxColumn();
            teacherColum.HeaderText = "강사이름";
            teacherColum.Name = "강사이름";
            dataGridView1.Columns.Add(teacherColum);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            DataGridViewTextBoxColumn gradeColum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn nameColum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn schoolColum = new DataGridViewTextBoxColumn();

            gradeColum.HeaderText = "학년";
            nameColum.HeaderText = "이름";
            schoolColum.HeaderText = "학교";       

            gradeColum.Name = "grade";
            nameColum.Name = "name";
            schoolColum.Name = "school";

            dataGridView2.Columns.Add(gradeColum);
            dataGridView2.Columns.Add(nameColum);
            dataGridView2.Columns.Add(schoolColum);

            DataGridViewTextBoxColumn gradeColum2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn nameColum2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn schoolColum2 = new DataGridViewTextBoxColumn();

            gradeColum2.HeaderText = "학년";
            nameColum2.HeaderText = "이름";
            schoolColum2.HeaderText = "학교";

            gradeColum2.Name = "grade";
            nameColum2.Name = "name";
            schoolColum2.Name = "school";

            dataGridView3.Columns.Add(gradeColum2);
            dataGridView3.Columns.Add(nameColum2);
            dataGridView3.Columns.Add(schoolColum2);

            loadTeacherData();
            loadStudentData();
        }

        private void saveStudentData()
        {
            Stream ws = new FileStream("a.dat", FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(ws, students);
            ws.Close();
        }

        private void loadStudentData()
        {
            if (!File.Exists("a.dat")) return;

            Stream rs = new FileStream("a.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            students.Clear();
            students = (List<Student>)deserializer.Deserialize(rs);
            rs.Close();

            dataGridView2.Rows.Clear();
            string[] row = { "","","" };
            foreach (Student p in students)
            {
                row[0] = p.grade.ToString();
                row[1] = p.name.ToString();
                row[2] = p.school.ToString();
                dataGridView2.Rows.Add(row);
            }

        }


        private void saveTeacherData()
        {
            Stream ws = new FileStream("teacher.dat", FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(ws, teachers);
            ws.Close();
        }

        private void loadTeacherData()
        {
            if (!File.Exists("teacher.dat")) return;

            Stream rs = new FileStream("teacher.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            teachers.Clear();
            teachers = (List<Teacher>)deserializer.Deserialize(rs);
            rs.Close();

            dataGridView1.Rows.Clear();
            string[] row = { "" };
            foreach (Teacher p in teachers)
            {
                row[0] = p.teachername.ToString();
                dataGridView1.Rows.Add(row);
            }

        }


        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            int index = dataGridView1.SelectedCells[0].RowIndex;
            string m_type = dataGridView1.Rows[index].Cells[0].FormattedValue.ToString();
         //   teachers.RemoveAt(teachers.FindIndex(x => x.teachername == m_type));
        //     teachers.Add(teacher);
            label8.Text = m_type;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell == null) return;
            int index = dataGridView2.SelectedCells[0].RowIndex;
            string m_type = dataGridView2.Rows[index].Cells[1].FormattedValue.ToString();
            label10.Text = m_type;
            if (selectedStudents.FindIndex(s => s.name == m_type)!=-1) return;
            int i = students.FindIndex(x => x.name == m_type);
            selectedStudents.Add(students[i]);
            updateGrid3();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentCell == null) return;
            int index = dataGridView3.SelectedCells[0].RowIndex;
            string m_type = dataGridView3.Rows[index].Cells[1].FormattedValue.ToString();
            selectedStudents.RemoveAt(selectedStudents.FindIndex(x => x.name == m_type));
            updateGrid3();
        }

        public void updateGrid3()
        {
            dataGridView3.Rows.Clear();
            string[] row = { "", "", "" };
            foreach (Student p in selectedStudents)
            {
                row[0] = p.grade.ToString();
                row[1] = p.name.ToString();
                row[2] = p.school.ToString();
                dataGridView3.Rows.Add(row);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            int index = dataGridView1.SelectedCells[0].RowIndex;
            string teachername = dataGridView1.Rows[index].Cells[0].FormattedValue.ToString();
            int teacherIndex = teachers.FindIndex(x => x.teachername == teachername);
            List<String> week = new List<String>();

            if (checkBox1.Checked) week.Add("월");
            if (checkBox2.Checked) week.Add("화");
            if (checkBox3.Checked) week.Add("수");
            if (checkBox4.Checked) week.Add("목");
            if (checkBox5.Checked) week.Add("금");
            if (checkBox6.Checked) week.Add("토");
            if (checkBox7.Checked) week.Add("일");
            if (week == null) return;
            
            lecture = new Lecture(textBox2.Text.ToString(), textBox1.Text.ToString(), textBox3.Text.ToString(), week);

            if (selectedStudents.Count == 0) return;
            for (int i = 0; i < selectedStudents.Count; i++)
            {
                lecture.students.Add(selectedStudents[i]);
            }

            //List<Lecture> ls = new List<Lecture>();
            //ls.Add(lecture);
            List<Lecture> ls = new List<Lecture>();
            for (int i = 0; i < teachers[teacherIndex].lectures.Count; i++) { ls.Add(teachers[teacherIndex].lectures[i]); }
            ls.Add(lecture);

            teachers[teacherIndex].lectures = ls;

            /*
            payment = new Payment("6");
            course = new Course(lecture, payment);
            if (selectedStudents.Count != 0)
            {
                for (int i = 0; i < selectedStudents.Count; i++)
                {
                    int studentIndex = students.FindIndex(x => x.name == selectedStudents[i].name);
                    students[studentIndex].course.Add(new Course(lecture, payment));
                }
            }
            */
            saveStudentData();
            saveTeacherData();
            this.Hide();
        }
    }
}
