using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Match_3
{
    public partial class Form1 : Form
    {
        private Timer time = new Timer();
        private StartForm start = new StartForm();
        private Button[,] button = new Button[8,8];
        private Random rnd = new Random();
        private Color color;
        private uint Score = 0;

        private int sec = 60;
        public int TIME;
        private int padding = 5;
        private int tik = 0;
        private bool CheckTime = true;

        public Form1()
        {
            InitializeComponent();

            for (int Y = 0; Y < 8; Y++)
            {
                for (int X = 0; X < 8; X++)
                {
                    RandomColor(X, Y);
                }
            }

            sortButton();

            TIME = sec;
            time.Interval = 1000;
            labelTime.Text = "01:00";
            time.Tick += new EventHandler(timer_Tick);
            time.Start();
        }

        private int IndexColor = 0;
        private int checkIndex = 0;
        /// <summary>
        /// Создает и рандомно задает цвет кнопке по указанным координатам
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void RandomColor(in int X, in int Y)
        {
            IndexColor = rnd.Next(1, 6);

            if (IndexColor == checkIndex)
                IndexColor++;

            if (IndexColor == 6)
                IndexColor -= rnd.Next(2, 4);

            checkIndex = IndexColor;

            switch (IndexColor)
            {
                case 1:
                    Initialization_Button(X, Y).BackColor = Color.Red;
                    break;
                case 2:
                    Initialization_Button(X, Y).BackColor = Color.DeepPink;
                    break;
                case 3:
                    Initialization_Button(X, Y).BackColor = Color.Aquamarine;
                    break;
                case 4:
                    Initialization_Button(X, Y).BackColor = Color.Green;
                    break;
                case 5:
                    Initialization_Button(X, Y).BackColor = Color.BurlyWood;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Создает кнопку с опредленными параметрами по указанным координатам
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        private Button Initialization_Button(in int X, in int Y)
        {
            button[Y,X] = new Button();
            button[Y,X].Width = GameBoard.Width / 8 - padding;
            button[Y,X].Height = GameBoard.Height / 8 - padding;
            button[Y,X].FlatStyle = FlatStyle.Standard;
            button[Y,X].Click += Button_Click;
            GameBoard.Controls.Add(button[Y,X]);
            button[Y,X].Location = new Point((GameBoard.Width / 8) * X + 5, (GameBoard.Height / 8) * Y + 3);
            return button[Y,X];
        }

        private void sortButton()
        {
            int count_CoincidenceColor = 0;

            // Проверка 3 в ряд по X
            for (int rows = 0; rows < 8; rows++)
            {
                for (int cols = 1; cols < 8; cols++)
                {
                    if (button[rows,cols].BackColor == button[rows, cols - 1].BackColor && count_CoincidenceColor < 2)
                    {
                        count_CoincidenceColor++;

                        if (button[rows, cols].BackColor == button[rows, cols - 1].BackColor && count_CoincidenceColor == 2)
                        {
                            while (button[rows, cols].BackColor == button[rows, cols - 1].BackColor)
                            {
                                IndexColor = rnd.Next(1, 6);

                                switch (IndexColor)
                                {
                                    case 1:
                                        button[rows, cols].BackColor = Color.Red;
                                        break;
                                    case 2:
                                        button[rows, cols].BackColor = Color.DeepPink;
                                        break;
                                    case 3:
                                        button[rows, cols].BackColor = Color.Aquamarine;
                                        break;
                                    case 4:
                                        button[rows, cols].BackColor = Color.Green;
                                        break;
                                    case 5:
                                        button[rows, cols].BackColor = Color.BurlyWood;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            count_CoincidenceColor = 0;
                        }
                    }
                    else
                        count_CoincidenceColor = 0;
                }
            }

            count_CoincidenceColor = 0;
            // Проверка 3 в ряд по Y
            for (int cols = 0; cols < 8; cols++)
            {
                for (int rows = 1; rows < 8; rows++)
                {
                    if (button[rows, cols].BackColor == button[rows - 1, cols].BackColor && count_CoincidenceColor < 2)
                    {
                        count_CoincidenceColor++;

                        if (button[rows, cols].BackColor == button[rows - 1, cols].BackColor && count_CoincidenceColor == 2)
                        {
                            while (button[rows, cols].BackColor == button[rows - 1, cols].BackColor)
                            {
                                IndexColor = rnd.Next(1, 6);

                                switch (IndexColor)
                                {
                                    case 1:
                                        button[rows, cols].BackColor = Color.Red;
                                        break;
                                    case 2:
                                        button[rows, cols].BackColor = Color.DeepPink;
                                        break;
                                    case 3:
                                        button[rows, cols].BackColor = Color.Aquamarine;
                                        break;
                                    case 4:
                                        button[rows, cols].BackColor = Color.Green;
                                        break;
                                    case 5:
                                        button[rows, cols].BackColor = Color.BurlyWood;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            count_CoincidenceColor = 0;
                        }
                    }
                    else
                        count_CoincidenceColor = 0;
                }
            }
        }

        private Button PreviousButton;
        /// <summary>
        /// Обрабатывает нажатую кнопку и определяет состояние анимации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, EventArgs e)
        {
            var CurrentButton = (Button)sender;

            int sizeX = CurrentButton.Width;
            int sizeY = CurrentButton.Height;

            int SizeButton = sizeX - 2;
            int count = 0;
            bool checkCount = true;
            uint AnimationTime = 0;

            if (PreviousButton != null && PreviousButton != CurrentButton)
            {
                if ((((PreviousButton.Bounds.X - CurrentButton.Bounds.X) <= sizeX * 2) && (PreviousButton.Bounds.Y == CurrentButton.Bounds.Y)) || (((PreviousButton.Bounds.Y - CurrentButton.Bounds.Y) <= sizeY * 2) && (PreviousButton.Bounds.X == CurrentButton.Bounds.X)))
                {
                    MoveButton(sizeX, sizeY, CurrentButton);
                    GlobalMatch_Buttons(CurrentButton);
                    Stop_AnimationButton(PreviousButton.Width, PreviousButton.Height);
                }
                else Stop_AnimationButton(PreviousButton.Width, PreviousButton.Height);
            }
            else if (PreviousButton == null)
            {
                PreviousButton = CurrentButton;
                while (AnimationTime != sec * 10)
                {
                    if (CurrentButton.Width != SizeButton && checkCount)
                    {
                        count++;
                        await Task.Delay(100);
                        CurrentButton.Size = new Size(sizeX - count, sizeY - count);
                    }
                    else
                    {
                        checkCount = false;
                        count--;
                        await Task.Delay(100);
                        CurrentButton.Size = new Size(sizeX - count, sizeY - count);
                    }

                    if (count == 0)
                        checkCount = true;

                    AnimationTime++;
                }
            }
            else if (PreviousButton == CurrentButton)
                Stop_AnimationButton(PreviousButton.Width, PreviousButton.Height);
        }

        /// <summary>
        /// Останавливает анимацию нажатой кнопки
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void Stop_AnimationButton(in int X, in int Y)
        {
            for (int i = 1; i <= 8; i++)
            {
                if (PreviousButton.Bounds.X <= (X * i))
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        if (PreviousButton.Bounds.Y <= (Y * j))
                        {
                            color = PreviousButton.BackColor;
                            PreviousButton.Dispose();
                            PreviousButton = null;
                            Initialization_Button(i - 1, j - 1).BackColor = color;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        int LocationButton = 0;
        /// <summary>
        /// Осуществляет смещение двух выбранных кнопок в противоположные друх другу стороны
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <param name="CurrentButton"></param>
        private void MoveButton(in int sizeX, in int sizeY, Button CurrentButton)
        {
            int locationX = CurrentButton.Bounds.X;
            int locationY = CurrentButton.Bounds.Y;

            if ((CurrentButton.Bounds.X > PreviousButton.Bounds.X) && (PreviousButton.Bounds.Y == locationY) && ((locationX - PreviousButton.Bounds.X) <= sizeX * 2))
            {
                LocationButton = 1;
                while (locationX != PreviousButton.Bounds.X)
                {
                    CurrentButton.Location = new Point(CurrentButton.Bounds.X - 1, locationY);
                    PreviousButton.Location = new Point(PreviousButton.Bounds.X + 1, PreviousButton.Bounds.Y);
                }
            }
            else if ((CurrentButton.Bounds.X < PreviousButton.Bounds.X) && (PreviousButton.Bounds.Y == locationY) && ((PreviousButton.Bounds.X - locationX) <= sizeX * 2))
            {
                LocationButton = 2;
                while (locationX != PreviousButton.Bounds.X)
                {
                    CurrentButton.Location = new Point(CurrentButton.Bounds.X + 1, locationY);
                    PreviousButton.Location = new Point(PreviousButton.Bounds.X - 1, PreviousButton.Bounds.Y);
                }
            }
            else if ((CurrentButton.Bounds.Y < PreviousButton.Bounds.Y) && (PreviousButton.Bounds.X == locationX) && ((PreviousButton.Bounds.Y - locationY) <= sizeY * 2))
            {
                LocationButton = 3;
                while (locationY != PreviousButton.Bounds.Y)
                {
                    CurrentButton.Location = new Point(locationX, CurrentButton.Bounds.Y + 1);
                    PreviousButton.Location = new Point(PreviousButton.Bounds.X, PreviousButton.Bounds.Y - 1);
                }
            }
            else if ((CurrentButton.Bounds.Y > PreviousButton.Bounds.Y) && (PreviousButton.Bounds.X == locationX) && ((locationY - PreviousButton.Bounds.Y) <= sizeY * 2))
            {
                LocationButton = 4;
                while (locationY != PreviousButton.Bounds.Y)
                {
                    CurrentButton.Location = new Point(locationX, CurrentButton.Bounds.Y - 1);
                    PreviousButton.Location = new Point(PreviousButton.Bounds.X, PreviousButton.Bounds.Y + 1);
                }
            }
        }

        private int HeightCurrent;
        private int HeightPrev = 0;
        private int CountDelDuplicate = 1;
        /// <summary>
        /// Выполняет удаление совпавших кнопок по X слева и смещает с анимацией, на место удаленных, кнопки сверху
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="CheckingAction"></param>
        private void ButtonDropX_Left(in int X, in int Y, in bool CheckingAction)
        {
            GameBoard.Controls.Remove(button[Y, X - DuplicateX_Left]);

            while (Y - CountDelDuplicate >= 0)
            {
                color = button[Y - CountDelDuplicate, X - DuplicateX_Left].BackColor;
                if (CountDelDuplicate == 1)
                {
                    HeightPrev = button[Y - CountDelDuplicate, X - DuplicateX_Left].Bounds.Y;
                    HeightCurrent = button[Y, X - DuplicateX_Left].Bounds.Y;
                    button[Y, X - DuplicateX_Left] = null;
                }
                else
                {
                    HeightCurrent = HeightPrev;
                    HeightPrev = button[Y - CountDelDuplicate, X - DuplicateX_Left].Bounds.Y;
                }

                while (button[Y - CountDelDuplicate, X - DuplicateX_Left].Bounds.Y != HeightCurrent)
                    button[Y - CountDelDuplicate, X - DuplicateX_Left].Location = new Point(button[Y - CountDelDuplicate, X - DuplicateX_Left].Bounds.X, button[Y - CountDelDuplicate, X - DuplicateX_Left].Bounds.Y + 1);
                
                GameBoard.Controls.Remove(button[Y - CountDelDuplicate, X - DuplicateX_Left]);
                button[Y - CountDelDuplicate, X - DuplicateX_Left] = null;
                Initialization_Button(X - DuplicateX_Left, Y - (CountDelDuplicate - 1)).BackColor = color;

                CountDelDuplicate++;
            }

            GameBoard.Controls.Remove(button[0, X - DuplicateX_Left]);
            button[0, X - DuplicateX_Left] = null;
            RandomColor(X - DuplicateX_Left, 0);

            if (!CheckingAction)
                Score += 10;
            else 
                Score += 15;

            labelScore.Text = Score.ToString();
            DuplicateX_Left--;
            CountDelDuplicate = 1;
        }

        /// <summary>
        /// Выполняет удаление совпавших кнопок по X справа и смещает с анимацией, на место удаленных, кнопки сверху
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void ButtonDropX_Right(in int X, in int Y)
        {
            GameBoard.Controls.Remove(button[Y, X + DuplicateX_Right]);

            while (Y - CountDelDuplicate >= 0)
            {
                color = button[Y - CountDelDuplicate, X + DuplicateX_Right].BackColor;
                if (CountDelDuplicate == 1)
                {
                    HeightPrev = button[Y - CountDelDuplicate, X + DuplicateX_Right].Bounds.Y;
                    HeightCurrent = button[Y, X + DuplicateX_Right].Bounds.Y;
                    button[Y, X + DuplicateX_Right] = null;
                }
                else
                {
                    HeightCurrent = HeightPrev;
                    HeightPrev = button[Y - CountDelDuplicate, X + DuplicateX_Right].Bounds.Y;
                }

                while (button[Y - CountDelDuplicate, X + DuplicateX_Right].Bounds.Y != HeightCurrent)
                    button[Y - CountDelDuplicate, X + DuplicateX_Right].Location = new Point(button[Y - CountDelDuplicate, X + DuplicateX_Right].Bounds.X, button[Y - CountDelDuplicate, X + DuplicateX_Right].Bounds.Y + 1);
                
                GameBoard.Controls.Remove(button[Y - CountDelDuplicate, X + DuplicateX_Right]);
                button[Y - CountDelDuplicate, X + DuplicateX_Right] = null;
                Initialization_Button(X + DuplicateX_Right, Y - (CountDelDuplicate - 1)).BackColor = color;

                CountDelDuplicate++;
            }

            GameBoard.Controls.Remove(button[0, X + DuplicateX_Right]);
            button[0, X + DuplicateX_Right] = null;
            RandomColor(X + DuplicateX_Right, 0);

            Score += 10;
            labelScore.Text = Score.ToString();
            DuplicateX_Right--;
            CountDelDuplicate = 1;
        }

        /// <summary>
        /// Выполняет удаление совпавших кнопок по Y и смещает с анимацией, на место удаленных, кнопки сверху
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void ButtonDropY(in int X, in int Y)
        {
            RemainingButtons = Y - DuplicateY_Up;

            HeightCurrent = button[Y + DuplicateY_Down, X].Bounds.Y;
            GameBoard.Controls.Remove(button[Y + DuplicateY_Down, X]);
            button[Y + DuplicateY_Down, X] = null;

            if (Overall_Height <= NumNullPlace)
                RandomColor(X, Y + DuplicateY_Down);

            if (RemainingButtons > 0)
            {
                HeightPrev = button[(Y - DuplicateY_Up) - 1, X].Bounds.Y;

                while (button[(Y - DuplicateY_Up) - 1, X].Bounds.Y != HeightCurrent)
                    button[(Y - DuplicateY_Up) - 1, X].Location = new Point(button[(Y - DuplicateY_Up) - 1, X].Bounds.X, button[(Y - DuplicateY_Up) - 1, X].Bounds.Y + 1);
                
                color = button[(Y - DuplicateY_Up) - 1, X].BackColor;
                HeightCurrent = button[(Y - DuplicateY_Up) - 1, X].Bounds.Y;
                GameBoard.Controls.Remove(button[(Y - DuplicateY_Up) - 1, X]);
                button[(Y - DuplicateY_Up) - 1, X].Location = new Point(button[(Y - DuplicateY_Up) - 1, X].Bounds.X, HeightCurrent - HeightPrev);
                Initialization_Button(X, Y + DuplicateY_Down).BackColor = color;
            }

            if (CountScore != 0)
            {
                Score += 15;
                labelScore.Text = Score.ToString();
                CountScore--;
            }
            Overall_Height--;
            DuplicateY_Up++;
            DuplicateY_Down--;
        }

        /// <summary>
        /// Вычисляет количество одинаковые цветов у соседних кнопок (по четырем стронам) с первой нажатой кнопкой
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void Counting_DuplicatesPrev(in int X, in int Y)
        {
            while ((X + DuplicateX_Right < 8) && (button[Y, X + DuplicateX_Right].BackColor == PreviousButton.BackColor))
                DuplicateX_Right++;

            while ((X - DuplicateX_Left >= 0) && (button[Y, X - DuplicateX_Left].BackColor == PreviousButton.BackColor))
                DuplicateX_Left++;

            while ((Y + DuplicateY_Down < 8) && (button[Y + DuplicateY_Down, X].BackColor == PreviousButton.BackColor))
                DuplicateY_Down++;

            while ((Y - DuplicateY_Up >= 0) && (button[Y - DuplicateY_Up, X].BackColor == PreviousButton.BackColor))
                DuplicateY_Up++;

            DuplicateX_Left--;
            DuplicateX_Right--;
            DuplicateY_Down--;
            DuplicateY_Up--;
        }

        /// <summary>
        /// Вычисляет количество одинаковые цветов у соседних кнопок (по четырем стронам) с второй нажатой кнопкой
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="CurrentButton"></param>
        private void Counting_DuplicatesCur(in int X, in int Y, Button CurrentButton)
        {
            while ((X + DuplicateX_Right < 8) && (button[Y, X + DuplicateX_Right].BackColor == CurrentButton.BackColor))
                DuplicateX_Right++;

            while ((X - DuplicateX_Left >= 0) && (button[Y, X - DuplicateX_Left].BackColor == CurrentButton.BackColor))
                DuplicateX_Left++;

            while ((Y + DuplicateY_Down < 8) && (button[Y + DuplicateY_Down, X].BackColor == CurrentButton.BackColor))
                DuplicateY_Down++;

            while ((Y - DuplicateY_Up >= 0) && (button[Y - DuplicateY_Up, X].BackColor == CurrentButton.BackColor))
                DuplicateY_Up++;

            DuplicateX_Left--;
            DuplicateX_Right--;
            DuplicateY_Down--;
            DuplicateY_Up--;
        }

        private int DuplicateX_Right = 1;
        private int DuplicateX_Left = 1;
        private int DuplicateY_Up = 1;
        private int DuplicateY_Down = 1;
        private int RemainingButtons = 1;
        /// <summary>
        /// Обнуляет все совпадения соседних кнопок по цвету у первой и второй нажатой кнопки
        /// </summary>
        private void Zeroing_Duplicate()
        {
            DuplicateX_Right = 1;
            DuplicateX_Left = 1;
            DuplicateY_Up = 1;
            DuplicateY_Down = 1;
        }

        private int Overall_Height;
        private int NumNullPlace;
        private int CountScore;
        /// <summary>
        /// Вычисляет и выполняет смещение кнопок по Y
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void DropY(in int X, in int Y)
        {
            Overall_Height = Y + DuplicateY_Down + 1;
            NumNullPlace = Overall_Height - (Y - DuplicateY_Up);
            CountScore = DuplicateY_Down + DuplicateY_Up + 1;

            while (Overall_Height != 0)
                ButtonDropY(X, Y);
        }

        /// <summary>
        /// Обрабатывает все возможные совпадения у нажатых кнопок
        /// </summary>
        /// <param name="CurrentButton"></param>
        private void GlobalMatch_Buttons(Button CurrentButton)
        {
            Zeroing_Duplicate();
            bool CheckingAction = false;
            int CurX = 0; 
            int CurY = 0;
            int PrevX = 0;
            int PrevY = 0;

            for (CurX = 0; CurX < 8; CurX++)
            {
                if (CurrentButton.Bounds.X <= CurrentButton.Width * (CurX + 1))
                {
                    for (CurY = 0; CurY < 8; CurY++)
                    {
                        if (CurrentButton.Bounds.Y <= CurrentButton.Height * (CurY + 1))
                            break;
                    }
                    break;
                }
            }

            for (PrevX = 0; PrevX < 8; PrevX++)
            {
                if (PreviousButton.Bounds.X <= PreviousButton.Width * (PrevX + 1))
                {
                    for (PrevY = 0; PrevY < 8; PrevY++)
                    {
                        if (PreviousButton.Bounds.Y <= PreviousButton.Height * (PrevY + 1))
                            break;
                    }
                    break;
                }
            }

            switch (LocationButton)
            {
                case 1:
                    button[CurY, CurX] = CurrentButton;
                    button[PrevY, PrevX] = PreviousButton;
                    Counting_DuplicatesPrev(PrevX, PrevY);

                    if (DuplicateX_Right >= 2 && ((DuplicateY_Up <= 1 && DuplicateY_Down == 0) || (DuplicateY_Up == 0 && DuplicateY_Down <= 1)))
                    {
                        while (DuplicateX_Right >= 0)
                            ButtonDropX_Right(PrevX, PrevY);

                        CheckingAction = true;
                    }
                    else if (DuplicateX_Right >= 2 && (DuplicateY_Up >= 2 || DuplicateY_Down >= 2))
                    {
                        while (DuplicateX_Right != 0)
                            ButtonDropX_Right(PrevX, PrevY);

                        DropY(PrevX, PrevY);
                        CheckingAction = true;
                    }
                    else if (DuplicateY_Up >= 2 || DuplicateY_Down >= 2 || (DuplicateY_Up >= 1 && DuplicateY_Down >= 1))
                    {
                        DropY(PrevX, PrevY);
                        CheckingAction = true;
                    }

                    Zeroing_Duplicate();
                    Counting_DuplicatesCur(CurX, CurY, CurrentButton);
                    //MessageBox.Show("" + DuplicateX_Right + " " + )
                    if (DuplicateX_Left >= 2 && ((DuplicateY_Up <= 1 && DuplicateY_Down == 0) || (DuplicateY_Up == 0 && DuplicateY_Down <= 1)))
                    {
                        while (DuplicateX_Left >= 0)
                            ButtonDropX_Left(CurX, CurY, CheckingAction);

                        CheckingAction = true;
                    }
                    else if (DuplicateX_Left >= 2 && (DuplicateY_Up >= 2 || DuplicateY_Down >= 2))
                    {
                        while (DuplicateX_Left != 0)
                            ButtonDropX_Left(CurX, CurY, CheckingAction);

                        DropY(CurX, CurY);
                        CheckingAction = true;
                    }
                    else if (DuplicateY_Up >= 2 || DuplicateY_Down >= 2 || (DuplicateY_Up >= 1 && DuplicateY_Down >= 1))
                    {
                        DropY(CurX, CurY);
                        CheckingAction = true;
                    }

                    if (!CheckingAction)
                        MoveButton(CurrentButton.Width, CurrentButton.Height, CurrentButton);
                    
                    
                    break;
                case 2:
                    button[CurY, CurX] = CurrentButton;
                    button[PrevY, PrevX] = PreviousButton;
                    Counting_DuplicatesPrev(CurX, CurY);
                    break;
                case 3:
                    button[CurY, CurX] = CurrentButton;
                    button[PrevY, PrevX] = PreviousButton;
                    Counting_DuplicatesCur(CurX, CurY, CurrentButton);
                    break;
                case 4:
                    button[CurY, CurX] = CurrentButton;
                    button[PrevY, PrevX] = PreviousButton;
                    Counting_DuplicatesCur(CurX, CurY, CurrentButton);
                    break;
                default:
                    break;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(CheckTime)
                start.Show();
            else
            {
                GameOverForm gameOver = new GameOverForm();
                gameOver.Show();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            tik = --TIME;
            TimeSpan span = TimeSpan.FromMinutes(tik);
            labelTime.Text = span.ToString(@"hh\:mm");

            if (TIME == 0)
            {
                time.Stop();
                CheckTime = false;
                Close();
            }
        }
    }
}
