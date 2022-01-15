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
    public partial class TeacherManagementUI : Form
    {
       private Student student;
        private Teacher teacher;
        private Lecture lecture;
        private Course course;
        private Payment payment;

        private List<Student> students;
        private List<Teacher> teachers;
        private List<Lecture> lectures;


        public TeacherManagementUI()
        {
            InitializeComponent();          
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StudentManagementUI sr = new StudentManagementUI(); sr.Show();
            this.Hide();
            sr.Owner = this;
        }
  

        private void Button3_Click(object sender, EventArgs e)
        {
            OperationStatusUI sr = new OperationStatusUI(); sr.Show();
            this.Hide();
            sr.Owner = this;
        }



        private void TeacherManagementUI_Load(object sender, EventArgs e)
        {
            students = new List<Student>();
            student = null;
            teachers = new List<Teacher>();
            teacher = null;
            lectures = new List<Lecture>();

            DataGridViewTextBoxColumn teacherColum = new DataGridViewTextBoxColumn();
            teacherColum.HeaderText = "강사이름";
            teacherColum.Name = "강사이름";
            dataGridView1.Columns.Add(teacherColum);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            

            DataGridViewTextBoxColumn Colum1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Colum2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Colum3 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Colum4 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Colum5 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Colum6 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Colum7 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Colum8 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Colum9 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Colum10 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Colum11 = new DataGridViewTextBoxColumn();
            Colum1.HeaderText = "강사";
            Colum1.Name = "강사";
            Colum2.HeaderText = "과정";
            Colum2.Name = "과정";
            Colum3.HeaderText = "요일";
            Colum3.Name = "요일";
            Colum4.HeaderText = "시간";
            Colum4.Name = "시간";
            Colum5.HeaderText = "수강료";
            Colum5.Name = "수강료";

            Colum6.HeaderText = "학년";
            Colum6.Name = "학년";
            Colum7.HeaderText = "이름";
            Colum7.Name = "이름";
            Colum8.HeaderText = "학생번호";
            Colum8.Name = "학생번호";
            Colum9.HeaderText = "부모님";
            Colum9.Name = "부모님";
            Colum10.HeaderText = "부모님 번호";
            Colum10.Name = "부모님 번호";
            Colum11.HeaderText = "집전화";
            Colum11.Name = "집전화";

            dataGridView2.Columns.Add(Colum1);
            dataGridView2.Columns.Add(Colum2);
            dataGridView2.Columns.Add(Colum3);
            dataGridView2.Columns.Add(Colum4);
            dataGridView2.Columns.Add(Colum5);

            dataGridView3.Columns.Add(Colum6);
            dataGridView3.Columns.Add(Colum7);
            dataGridView3.Columns.Add(Colum8);
            dataGridView3.Columns.Add(Colum9);
            dataGridView3.Columns.Add(Colum10);
            dataGridView3.Columns.Add(Colum11);

            loadTeacherData();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            if (textBox2.Text == null) return;
            teacher = new Teacher(textBox1.Text.ToString());
            int index = dataGridView1.SelectedCells[0].RowIndex;
            string m_type = dataGridView1.Rows[index].Cells[0].FormattedValue.ToString();
            teachers.RemoveAt( teachers.FindIndex(x => x.teachername ==m_type));
            teachers.Add(teacher);
            saveTeacherData();
            loadTeacherData();
        }


        private void deleteInfo(string name)
        {
            int Index = students.FindIndex(x => x.name == name);
            students.RemoveAt(Index);
            saveStudentData();
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
            string[] row = {""};
            foreach (Teacher p in teachers)
            {
                row[0] = p.teachername.ToString();
                dataGridView1.Rows.Add(row);
            }

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == null) return;
            teacher = new Teacher(textBox2.Text.ToString());

            teachers.Add(teacher); label2.Text = teachers[0].teachername;
                saveTeacherData();
                loadTeacherData();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            int index = dataGridView1.SelectedCells[0].RowIndex;
            string m_type = dataGridView1.Rows[index].Cells[0].FormattedValue.ToString();
            teachers.RemoveAt(teachers.FindIndex(x => x.teachername == m_type));
            saveTeacherData();
            loadTeacherData();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
                if (dataGridView1.CurrentCell == null) return;
                int index = dataGridView1.SelectedCells[0].RowIndex;
                string m_type = dataGridView1.Rows[index].Cells[0].FormattedValue.ToString();
                label2.Text = m_type;
                LectureRepair sr = new LectureRepair(); sr.Show();
                sr.Owner = this;
            
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell == null) return;
            int index = dataGridView2.SelectedCells[0].RowIndex;
            string teacher = dataGridView2.Rows[index].Cells[0].FormattedValue.ToString();
            string lecture = dataGridView2.Rows[index].Cells[1].FormattedValue.ToString();
            int teacherIndex = teachers.FindIndex(x => x.teachername == teacher);
            int lectureIndex = teachers[teacherIndex].lectures.FindIndex(x => x.lecturename == lecture);

            LectureRepair sr = new LectureRepair(); sr.Show();
            sr.Owner = this;

            lectures.RemoveAt(lectureIndex);
            saveTeacherData();
            loadTeacherData();
            updateGrid2(teacher, teacherIndex);



        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            int index = dataGridView1.SelectedCells[0].RowIndex;
            string m_type = dataGridView1.Rows[index].Cells[0].FormattedValue.ToString();
            int Index = teachers.FindIndex(x => x.teachername == m_type);
            updateGrid2(m_type, Index);
        }

        void updateGrid2(String teachername, int teacherIndex)
        {
            dataGridView2.Rows.Clear();
            string[] row = { "", "", "", "", "" };
            lectures = teachers[teacherIndex].lectures;
            foreach (Lecture p in lectures)
            {
                row[0] = teachername;
                row[1] = p.lecturename;
                row[2] = p.getweek();
                row[3] = p.lecturetime;
                row[4] = p.lecturefee;
                dataGridView2.Rows.Add(row);
            }
        }


        void updateGrid3(String teachername, int teacherIndex, int lectureIndex)
        {
            dataGridView3.Rows.Clear();
            string[] row = { "", "", "", "", "" ,""};

            List<Student>s = teachers[teacherIndex].lectures[lectureIndex].students;
            foreach (Student p in s)
            {
                row[0] = p.grade;
                row[1] = p.name;
                row[2] = p.tel;
                row[3] = p.parent;
                row[4] = p.parentTel;
                row[5] = p.hometel;
                dataGridView3.Rows.Add(row);
            }
        }


        private void Button9_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell == null) return;
            int index = dataGridView2.SelectedCells[0].RowIndex;
            string teacher = dataGridView2.Rows[index].Cells[0].FormattedValue.ToString();
            string lecture = dataGridView2.Rows[index].Cells[1].FormattedValue.ToString();
            int teacherIndex = teachers.FindIndex(x => x.teachername == teacher);
            int lectureIndex = teachers[teacherIndex].lectures.FindIndex(x => x.lecturename == lecture);
            lectures.RemoveAt(lectureIndex);
            saveTeacherData();
            loadTeacherData();
            updateGrid2(teacher, teacherIndex);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            loadStudentData();
            loadTeacherData();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell == null) return;
            int index = dataGridView2.SelectedCells[0].RowIndex;
            string teacher = dataGridView2.Rows[index].Cells[0].FormattedValue.ToString();
            string lecture = dataGridView2.Rows[index].Cells[1].FormattedValue.ToString();
            int teacherIndex = teachers.FindIndex(x => x.teachername == teacher);
            int lectureIndex = teachers[teacherIndex].lectures.FindIndex(x => x.lecturename == lecture);
            updateGrid3(teacher, teacherIndex, lectureIndex);
        }
    }
}
