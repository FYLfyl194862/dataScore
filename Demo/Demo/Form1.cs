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

namespace Demo
{
    public partial class Form1 : Form
    {
        private DataGridView dataGridView1 = new DataGridView();
        private Button showMultiplicationTableButton = new Button();
        private Button saveButton = new Button();
        private Timer timer = new Timer();
        private Button showTimeButton = new Button();
        public Form1()
        {
            InitializeComponent();
            // 设置窗体属性
            this.Text = "乘法表和时间显示";
            this.Size = new System.Drawing.Size(600, 400);
            // 设置DataGridView的位置和大小
            dataGridView1.Location = new System.Drawing.Point(20, 20);
            dataGridView1.Size = new System.Drawing.Size(300, 300);
            this.Controls.Add(dataGridView1);
            // 设置“显示乘法表”按钮
            showMultiplicationTableButton.Location = new System.Drawing.Point(340, 20);
            showMultiplicationTableButton.Text = "显示乘法表";
            showMultiplicationTableButton.Click += ShowMultiplicationTableButton_Click;
            this.Controls.Add(showMultiplicationTableButton);
            // 设置“保存”按钮
            saveButton.Location = new System.Drawing.Point(340, 60);
            saveButton.Text = "保存";
            saveButton.Click += SaveButton_Click;
            this.Controls.Add(saveButton);
            // 设置“显示时间”按钮
            showTimeButton.Location = new System.Drawing.Point(340, 100);
            showTimeButton.Text = "显示时间";
            showTimeButton.Click += ShowTimeButton_Click;
            this.Controls.Add(showTimeButton);

            // 设置定时器属性
            timer.Interval = 1000; // 1秒
            timer.Tick += Timer_Tick;
        }
        private void ShowMultiplicationTableButton_Click(object sender, EventArgs e)
        {
            // 清空DataGridView
            dataGridView1.Rows.Clear();

            // 添加列（如果尚未添加）
            if (dataGridView1.Columns.Count == 0)
            {
                for (int i = 1; i <= 9; i++)
                {
                    dataGridView1.Columns.Add($"col{i}", $"{i}");
                    dataGridView1.Columns[i - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            // 生成梯形乘法表并添加到DataGridView
            for (int row = 1; row <= 9; row++)
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.CreateCells(dataGridView1);
                for (int col = 1; col <= row; col++)
                {
                    int product = row * col;
                    dataGridViewRow.Cells[col - 1].Value = $"{col} x {row} = {product}";
                }
                dataGridView1.Rows.Add(dataGridViewRow);
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // 创建保存文件对话框
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV文件 (*.csv)|*.csv|文本文件 (*.txt)|*.txt";
            saveFileDialog.Title = "保存乘法表";
            saveFileDialog.FileName = "乘法表";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // 根据文件类型选择导出格式
                if (filePath.EndsWith(".csv"))
                {
                    ExportToCSV(filePath);
                }
                else if (filePath.EndsWith(".txt"))
                {
                    ExportToTXT(filePath);
                }
            }
        }

        private void ExportToCSV(string filePath)
        {
            // 导出为CSV格式
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 1; i <= 9; i++)
                {
                    for (int j = 1; j <= i; j++)
                    {
                        int result = i * j;
                        writer.Write($"{j} x {i} = {result},");
                    }
                    writer.WriteLine();
                }
            }
        }

        private void ExportToTXT(string filePath)
        {
            // 导出为TXT格式
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 1; i <= 9; i++)
                {
                    for (int j = 1; j <= i; j++)
                    {
                        int result = i * j;
                        writer.WriteLine($"{j} x {i} = {result}");
                    }
                }
            }
        }
        private void ShowTimeButton_Click(object sender, EventArgs e)
        {
            // 启动或停止定时器来实时显示时间
            if (timer.Enabled)
            {
                timer.Stop();
                showTimeButton.Text = "显示时间";
            }
            else
            {
                timer.Start();
                showTimeButton.Text = "停止显示";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 在定时器事件中更新时间显示按钮的文本，确保在主线程上更新UI
            this.Invoke((MethodInvoker)delegate
            {
                showTimeButton.Text = DateTime.Now.ToString("HH:mm:ss");
            });
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
