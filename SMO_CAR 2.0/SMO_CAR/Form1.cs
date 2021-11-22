using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMO_CAR
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }


        public static List<PictureBox> BusyMech = new List<PictureBox>();
        public static List<PictureBox> NotBusyMech = new List<PictureBox>();
        public static List<PictureBox> SleepMech = new List<PictureBox>();
        public static List<PictureBox> NowUsualCar = new List<PictureBox>();
        public static List<PictureBox> NowPoliceCar = new List<PictureBox>();
        public static List<PictureBox> QueueCar = new List<PictureBox>();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.BackgroundColor = Color.AliceBlue;

            dataGridView1.ColumnCount = 5;
            dataGridView1.RowCount = 4;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Width = 70;
            }
            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 450;
            dataGridView1.Columns[3].Width = 95;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].HeaderText = "Время";
            dataGridView1.Columns[1].HeaderText = "Событие";
            dataGridView1.Columns[2].HeaderText = "Очередь";
            dataGridView1.Columns[3].HeaderText = "Профилактика";
            dataGridView1.Columns[4].HeaderText = "Механик";
        }

        private void B_Generate_Click(object sender, EventArgs e)
        {
            _clear();
            Garage.HOW_WORK = Convert.ToInt32(HowHourWork.Value * 2);
            Garage.MIN_CAR = Convert.ToInt32(MinUsualCar.Value);
            Garage.MAX_CAR = Convert.ToInt32(MaxUsualCar.Value);
            Garage.MIN_POLICE_TIME = Convert.ToInt32(MinPoliceCarTime.Value * 2);
            Garage.MAX_POLICE_TIME = Convert.ToInt32(MaxPoliceCarTime.Value * 2);

            B_Generate.Enabled = false;
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(100, 100);
            pictureBox.Image = Image.FromFile(@"E:\vs проекты\SMO_CAR 2.0\SMO_CAR\Images\MechWork.png");
            pictureBox.Location = new Point(20, 50);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            BusyMech.Add(pictureBox);
            groupBox1.Controls.Add(BusyMech[BusyMech.Count - 1]);

            PictureBox pictureBox2 = new PictureBox();
            pictureBox2.Size = new Size(100, 100);
            pictureBox2.Image = Image.FromFile(@"E:\vs проекты\SMO_CAR 2.0\SMO_CAR\Images\MechNotWork.png");
            pictureBox2.Location = new Point(120, 50);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            NotBusyMech.Add(pictureBox2);
            groupBox1.Controls.Add(NotBusyMech[NotBusyMech.Count - 1]);

            PictureBox pictureBox3 = new PictureBox();
            pictureBox3.Size = new Size(100, 100);
            pictureBox3.Image = Image.FromFile(@"E:\vs проекты\SMO_CAR 2.0\SMO_CAR\Images\HouseMech.png");
            pictureBox3.Location = new Point(1300, 20);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            SleepMech.Add(pictureBox3);
            groupBox1.Controls.Add(SleepMech[SleepMech.Count - 1]);

            PictureBox pictureBox4 = new PictureBox();
            pictureBox4.Size = new Size(100, 100);
            pictureBox4.Image = Image.FromFile(@"E:\vs проекты\SMO_CAR 2.0\SMO_CAR\Images\UsualCar.png");
            pictureBox4.Location = new Point(400, 50);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            NowUsualCar.Add(pictureBox4);
            groupBox1.Controls.Add(NowUsualCar[NowUsualCar.Count - 1]);

            PictureBox pictureBox5 = new PictureBox();
            pictureBox5.Size = new Size(100, 100);
            pictureBox5.Image = Image.FromFile(@"E:\vs проекты\SMO_CAR 2.0\SMO_CAR\Images\PoliceCar.png");
            pictureBox5.Location = new Point(500, 50);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            NowPoliceCar.Add(pictureBox5);
            groupBox1.Controls.Add(NowPoliceCar[NowPoliceCar.Count - 1]);


            Random random = new Random();
            Garage.policeCarComeInterval = random.Next(Garage.MIN_POLICE_TIME, Garage.MAX_POLICE_TIME + 1);
            dataGridView1.RowCount = (int)numDay.Value * 48 + 1;
            Garage.Tact = 0;
            timer1.Interval = 1;
            timer1.Tick += new EventHandler(_update);
            timer1.Start();
            
        }


        private void _update(object myObject, EventArgs e)
        {
            NowUsualCar[0].Visible = false;
            NowPoliceCar[0].Visible = false;
            if (Garage.Tact < dataGridView1.RowCount)
            {
                Garage._event = "";
                dataGridView1[0, Garage.Tact].Value = Garage._getDay();
                Garage._work();
                Formuls._ShetMent();
                dataGridView1[1, Garage.Tact - 1].Value = Garage._event;
                dataGridView1[2, Garage.Tact - 1].Value = Queue.fixCar.Count;
                dataGridView1[3, Garage.Tact - 1].Value = Queue.profCar.Count;
                if (Garage.workTime <= Garage.HOW_WORK && Garage.sleep)
                {
                    dataGridView1[4, Garage.Tact - 1].Value = Convert.ToString(Mechanic.workingTime * 30) + " мин.";
                    if (Garage.workTime == Garage.HOW_WORK)
                    {
                        Garage.sleep = false;
                    }
                }
                else
                {
                    dataGridView1[4, Garage.Tact - 1].Value = "Zzz...";
                }

                if (Garage.workTime < Garage.HOW_WORK)
                {
                    SleepMech[0].Visible = false;

                    if (Mechanic.workingTime != 0)
                    {
                        BusyMech[0].Visible = true;
                        NotBusyMech[0].Visible = false;
                        if (Mechanic.nowCar.type == "usual")
                        {
                            NowPoliceCar[0].Visible = false;
                            NowUsualCar[0].Visible = true;
                        }
                        else if (Mechanic.nowCar.type == "police")
                        {
                            NowPoliceCar[0].Visible = true;
                            NowUsualCar[0].Visible = false;
                        }


                    }
                    else
                    {
                        BusyMech[0].Visible = false;
                        NotBusyMech[0].Visible = true;
                    }

                }
                else
                {
                    SleepMech[0].Visible = true;
                    NotBusyMech[0].Visible = false;
                    BusyMech[0].Visible = false;

                }


                if (QueueCar.Count != Queue.fixCar.Count)
                {
                    if (QueueCar.Count != 0)
                    {
                        for (int i = 0; i < QueueCar.Count; i++)
                        {
                            groupBox1.Controls.Remove(QueueCar[i]);

                        }
                        QueueCar.Clear();

                    }

                }
                if (Queue.fixCar.Count != 0)
                {
                    _GeneratePictureCar(0);
                    groupBox1.Controls.Add(QueueCar[0]);
                    if (Queue.fixCar.Count > 1)
                    {
                        for (int i = 1; i < Queue.fixCar.Count; i++)
                        {
                            _GeneratePictureCar(i);
                            groupBox1.Controls.Add(QueueCar[i]);
                        }
                    }


                }
                if (Garage.Tact == dataGridView1.RowCount - 1)
                {
                    MessageBox.Show(
                    "Готово!",
                    "Моделирование завершено!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                    textBox1.Text = Convert.ToString(Math.Round(Formuls.ShetRandMent /( Garage.Tact + 1)));
                    B_Generate.Enabled = true;
                }
            }
            
        }

        private void _GeneratePictureCar(int i)
        {
            PictureBox picturebox7 = new PictureBox();
            picturebox7.Size = new Size(100, 100);
            picturebox7.Location = new Point(20 + i * 100, 150);
            picturebox7.SizeMode = PictureBoxSizeMode.StretchImage;
            picturebox7.Visible = true;
            if (Queue.fixCar[i].type == "usual")
            {
                picturebox7.Image = Image.FromFile(@"E:\vs проекты\SMO_CAR 2.0\SMO_CAR\Images\QueueCarUsual.png");

            }
            else
            {
                picturebox7.Image = Image.FromFile(@"E:\vs проекты\SMO_CAR 2.0\SMO_CAR\Images\QueueCarPolice.png");
            }
            QueueCar.Add(picturebox7);

        }


    
        private void button1_Click(object sender, EventArgs e)
        {
            USLOV form = new USLOV();
            form.Show();
        }

        public void _clear()
        {
            textBox1.Text = "";
            Formuls.ShetRandMent = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1[j, i].Value = "";

                }
            }

            for (int i = 0; i < BusyMech.Count; i++)
            {
                groupBox1.Controls.Remove(BusyMech[i]);
            }
            for (int i = 0; i < NotBusyMech.Count; i++)
            {
                groupBox1.Controls.Remove(NotBusyMech[i]);
            }
            for (int i = 0; i < SleepMech.Count; i++)
            {
                groupBox1.Controls.Remove(SleepMech[i]);
            }
            for (int i = 0; i < NowUsualCar.Count; i++)
            {
                groupBox1.Controls.Remove(NowUsualCar[i]);
            }
            for (int i = 0; i < NowPoliceCar.Count; i++)
            {
                groupBox1.Controls.Remove(NowPoliceCar[i]);
            }
            for (int i = 0; i < QueueCar.Count; i++)
            {
                groupBox1.Controls.Remove(QueueCar[i]);
            }

            Queue.profCar.Clear();
            Queue.fixCar.Clear();
            Mechanic.workingTime = 0;
            Mechanic.nowCar.type = "usual";
            Garage.HOW_WORK = 0;
            Garage.MIN_CAR = 0;
            Garage.MAX_CAR = 0;
            Garage.MIN_POLICE_TIME = 0;
            Garage.MAX_POLICE_TIME = 0;
            Garage.carComeInterval = 0;
            Garage.policeCarComeInterval = 0;
            Garage.Tact = 0;
            Garage.workTime = 0;
            BusyMech.Clear();
            NotBusyMech.Clear();
            SleepMech.Clear();
            NowUsualCar.Clear();
            NowPoliceCar.Clear();
            QueueCar.Clear();
        }
    }
}
