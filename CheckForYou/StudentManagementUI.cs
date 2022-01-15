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
    public partial class StudentManagementUI : Form, IFormMessage
    {
        private List<Student> students = null;
        private Student student = null;

        private Teacher teacher;
        private Lecture lecture;
        private Course course;
        private Payment payment;

        private List<Teacher> teachers;
        private List<Lecture> lectures;

        public StudentManagementUI()
        {
            InitializeComponent();
        }

        #region IFormMessage 멤버
        public void Receive(string str)
        {
        }
        #endregion

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            StudentRepair sr = new StudentRepair(); sr.Show();
            sr.Owner = this;

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            int index = dataGridView1.SelectedCells[0].RowIndex;
            string m_type = dataGridView1.Rows[index].Cells[1].FormattedValue.ToString();
            deleteInfo(m_type);
            StudentRepair sr = new StudentRepair(); sr.Show();
            sr.Owner = this;
        }

        private void StudentManagementUI_Load(object sender, EventArgs e)
        {
            students = new List<Student>();
            student = null;
            teachers = new List<Teacher>();
            teacher = null;
            lectures = new List<Lecture>();

            DataGridViewTextBoxColumn gradeColum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn nameColum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn telColum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn parentColum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn parentTelColum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn homeTelColum = new DataGridViewTextBoxColumn();

            gradeColum.HeaderText = "학년";
            nameColum.HeaderText = "이름";
            telColum.HeaderText = "학생번호";
            parentColum.HeaderText = "부모님";
            parentTelColum.HeaderText = "부모님 번호";
            homeTelColum.HeaderText = "집전화";

            gradeColum.Name = "grade";
            nameColum.Name = "name";
            telColum.Name = "tel";
            parentColum.Name = "parent";
            parentTelColum.Name = "parentTel";
            homeTelColum.Name = "homeTel";

            dataGridView1.Columns.Add(gradeColum);
            dataGridView1.Columns.Add(nameColum);
            dataGridView1.Columns.Add(telColum);
            dataGridView1.Columns.Add(parentColum);
            dataGridView1.Columns.Add(parentTelColum);
            dataGridView1.Columns.Add(homeTelColum);


            if (!File.Exists("a.dat")) return;

            Stream rs = new FileStream("a.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            students.Clear();
            students = (List<Student>)deserializer.Deserialize(rs);
            rs.Close();

            dataGridView1.Rows.Clear();
            string[] row = { "", "", "", "", "", "" };
            foreach (Student p in students)
            {
                row[0] = p.grade.ToString();
                row[1] = p.name.ToString();
                row[2] = p.tel.ToString();
                row[3] = p.parent.ToString();
                row[4] = p.parentTel.ToString();
                row[5] = p.hometel.ToString();
                dataGridView1.Rows.Add(row);
            }

            DataGridViewTextBoxColumn lectureColum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn weekColum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn timeColum = new DataGridViewTextBoxColumn();
  
            lectureColum.HeaderText = "과정";
            weekColum.HeaderText = "요일";
            timeColum.HeaderText = "시간";

            lectureColum.Name = "과정";
            weekColum.Name = "요일";
            timeColum.Name = "시간";

            dataGridView2.Columns.Add(lectureColum);
            dataGridView2.Columns.Add(timeColum);
            dataGridView2.Columns.Add(weekColum);
      
            //dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            // dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

        }


        private void Button10_Click(object sender, EventArgs e)
        {
            loadStudentData();
            loadTeacherData();
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

        private void Button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            int index = dataGridView1.SelectedCells[0].RowIndex;
            string m_type = dataGridView1.Rows[index].Cells[1].FormattedValue.ToString();

            deleteInfo(m_type);
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

            dataGridView1.Rows.Clear();
            string[] row = { "", "", "", "", "", "" };
            foreach (Student p in students)
            {
                row[0] = p.grade.ToString();
                row[1] = p.name.ToString();
                row[2] = p.tel.ToString();
                row[3] = p.parent.ToString();
                row[4] = p.parentTel.ToString();
                row[5] = p.hometel.ToString();
                dataGridView1.Rows.Add(row);
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            int index = dataGridView1.SelectedCells[0].RowIndex;
            string m_type = dataGridView1.Rows[index].Cells[1].FormattedValue.ToString();
            int Index = students.FindIndex(x => x.name == m_type);
            students[Index].memo = textBox1.Text;
            textBox1.Text = "메모가 저장되었습니다.";
            saveStudentData();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            loadTeacherData();
            if (dataGridView1.CurrentCell == null) return;
            int index = dataGridView1.SelectedCells[0].RowIndex;
            string m_type = dataGridView1.Rows[index].Cells[1].FormattedValue.ToString();
            int Index = students.FindIndex(x => x.name == m_type);
            textBox1.Text = students[Index].memo;
            lectures.Clear();
            for (int i = 0; i < teachers.Count; i++)
            {
                for (int j = 0; j < teachers[i].lectures.Count; j++)
                {
                    for (int k = 0; k < teachers[i].lectures[j].students.Count; k++)
                        if (teachers[i].lectures[j].students[k].name == m_type) { lectures.Add(teachers[i].lectures[j]); break; }
                }
            }

            dataGridView2.Rows.Clear();
            string[] row = { "", "", "" };
            foreach (Lecture p in lectures)
            {
                row[0] = p.lecturename;
                row[1] = p.lecturetime;
                row[2] = p.getweek();
                dataGridView2.Rows.Add(row);
            }
        }




        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = " ";
            //  int Index = students.FindIndex(x => x.name == m_type);
            //  textBox1.Text = students[Index].memo;
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

        private void Button3_Click(object sender, EventArgs e)
        {
            OperationStatusUI sr = new OperationStatusUI(); sr.Show();
            this.Hide();
            sr.Owner = this;
        }



        private void Button13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
