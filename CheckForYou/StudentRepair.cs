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

    public partial class StudentRepair : Form, IFormMessage
    {
        int changeIndex = -1;
        Student student;
        List<Student> students;
        public Student receiveStudent(Student s)
        {
            return s;
        }
        public StudentRepair()
        {
            InitializeComponent();
            loadStudentData();
        }

        public Student receiveStudent()
        {
            return student;
        }


        private void StudentRepair_Load(object sender, EventArgs e)
        {
            if (changeIndex != -1)
            {

                this.textBox1.Text = students[changeIndex].name;
                this.comboBox1.Text = students[changeIndex].grade;
                this.comboBox2.Text = students[changeIndex].school;
                this.textBox2.Text = students[changeIndex].tel;
                this.textBox3.Text = students[changeIndex].parent;
                this.textBox4.Text = students[changeIndex].parentTel;
                this.textBox5.Text = students[changeIndex].hometel;
                this.textBox6.Text = students[changeIndex].holding;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {         
            this.Hide();
            String name=textBox1.Text.ToString();
            String grade=comboBox1.Text.ToString();
            String school=comboBox2.Text.ToString();
            String tel=textBox2.Text.ToString();
            String parent = textBox3.Text.ToString() ;
            String parentTel = textBox4.Text.ToString();
            String hometel=textBox5.Text.ToString();
            String holding=textBox6.Text.ToString();
            bool bro;
            if (radioButton1.Checked == true) bro = true; else bro=false;
            student = new Student(name, grade, school, tel, parent, parentTel, hometel, holding, bro);           
            students.Add(student);
            saveStudentData();
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
            students = new List<Student>();

            if (!File.Exists("a.dat")) return;

            Stream rs = new FileStream("a.dat", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            students.Clear();
            students = (List<Student>)deserializer.Deserialize(rs);
            rs.Close();
        }


        public void Receive(string str)
        {
            changeIndex = students.FindIndex(x => x.name == str);
        }

    }

    public interface IFormMessage
    {
        void Receive(string str);
    }

}
