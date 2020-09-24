using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculaTOR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Calc calc = new Calc();
        private int id = 0;
        private int priceP = 0;
        private double priceF = 0;
        public string iItem = "000";
        public string numberTel = "";//н.телефона
        public string adress = "";//адрес
        public string requisites = "";//реквизиты
        public string bank = ""; //банк
        public string account = ""; //счет
        private string[] agree = new string[8];

        public List<string> tag = new List<string>();
        Dictionary<int, int> dictionarySummF = new Dictionary<int, int>();

        private void Form1_Load(object sender, EventArgs e)
        {
            RegKey regKey = new RegKey();
            if (!regKey.ReadKey())
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                regKey.Show();
            }
            else
            {
                regKey.Dispose();
                int counAgree = 0;
                foreach (var item in calc.RunReadSetting())
                {
                    if (item.Count > 1)
                    {
                        if (item[0].Substring(0, 2) == "НФ")
                        {
                            if (!comboBox1.Items.Contains(item[1]))
                            {
                                comboBox1.ResetText();
                                comboBox1.Items.Add(item[1]);
                            }
                        }

                        if (item[0].Substring(0, 2) == "НМ")
                        {
                            if (!comboBox2.Items.Contains(item[1]))
                            {
                                comboBox2.ResetText();
                                comboBox2.Items.Add(item[1]);
                            }
                        }
                    }
                    if (item.Count == 10)
                    {
                        label9.Text = item[0];//организация
                        numberTel = item[1];//н.телефона
                        adress = item[2];//адрес
                        requisites = item[3];//реквизиты
                        bank = item[4];//банк
                        account = item[5];//банк
                        //rezerv1 = item[6];//запасные строки для доработки данных
                        //rezerv2 = item[7];//запасные строки для доработки данных
                        //rezerv3 = item[8];//запасные строки для доработки данных
                        //rezerv4 = item[9];//запасные строки для доработки данных
                    }
                    if (item.Count == 8)
                    {
                        foreach (var value in item)
                        {
                            agree[counAgree] = item[counAgree];//соглашения
                            counAgree++;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRow();
            //if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox2.Text))
            //{
            //    MessageBox.Show("Не все поля заполнены");
            //    return;
            //}
            //for (int i = 0; dataGridView1.RowCount > i; i++)
            //{
            //    if (!SumNotEqualsZero())
            //        return;

            //    //if (dataGridView1["Freza", i].Value.ToString() == comboBox1.Text &&
            //    //    dataGridView1["Material", i].Value.ToString() == comboBox2.Text &&
            //    //    dataGridView1["Color", i].Value.ToString() == textBox1.Text)
            //    //{
            //    //    MessageBox.Show("Такой элемент уже есть в списке!");
            //    //    return;
            //    //}
            //}

            //dataGridView1.Rows.Add(1);
            //if (dataGridView1.RowCount == 1)
            //    dataGridView1["Id", 0].Value = 1;
            //else
            //    dataGridView1["Id", dataGridView1.RowCount - 1].Value = Convert.ToInt16(dataGridView1["Id", dataGridView1.RowCount - 2].Value) + 1;

            //int iRows = dataGridView1.Rows.Count - 1;
            //dataGridView1["Freza", iRows].Value = comboBox1.Text;
            //dataGridView1["Material", iRows].Value = comboBox2.Text;
            //dataGridView1["Color", iRows].Value = textBox1.Text;
            //dataGridView1["Comment", iRows].Value = textBox2.Text;
            //textBox2.ResetText();
            //comboBox1.Enabled = false;
            //comboBox2.Enabled = false;
        }

        private void AddRow()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            for (int i = 0; dataGridView1.RowCount > i; i++)
            {
                if (!SumNotEqualsZero())
                    return;

                //if (dataGridView1["Freza", i].Value.ToString() == comboBox1.Text &&
                //    dataGridView1["Material", i].Value.ToString() == comboBox2.Text &&
                //    dataGridView1["Color", i].Value.ToString() == textBox1.Text)
                //{
                //    MessageBox.Show("Такой элемент уже есть в списке!");
                //    return;
                //}
            }

            dataGridView1.Rows.Add(1);
            if (dataGridView1.RowCount == 1)
                dataGridView1["Id", 0].Value = 1;
            else
                dataGridView1["Id", dataGridView1.RowCount - 1].Value = Convert.ToInt16(dataGridView1["Id", dataGridView1.RowCount - 2].Value) + 1;

            int iRows = dataGridView1.Rows.Count - 1;
            dataGridView1["Freza", iRows].Value = comboBox1.Text;
            dataGridView1["Material", iRows].Value = comboBox2.Text;
            dataGridView1["Color", iRows].Value = textBox1.Text;
            dataGridView1["Comment", iRows].Value = textBox2.Text;
            textBox2.ResetText();
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            setting.Show();
        }

        private bool SumNotEqualsZero()
        {
            for (int i = 0; dataGridView1.RowCount > i; i++)
            {
                if (dataGridView1["Height", i].Value == null || dataGridView1["Width", i].Value == null || dataGridView1["Col", i].Value == null)
                {
                    MessageBox.Show("Сначала укажите все параметры в списке!");
                    return false;
                }
            }
            return true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!SumNotEqualsZero())
                return;

            tag.Clear();
            iItem = "000";
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            foreach (var item in calc.RunReadSetting())
            {
                if (item.Count > 1)
                {
                    if (item[0].Substring(0, 2).ToString() == "НФ" && item[1] == comboBox1.Text)
                    {
                        iItem = item[0].Substring(2);//записывает номер значения в списке файла
                        comboBox1.Tag = item[0].ToString();
                        tag.Add(item[0]);
                        comboBox2.Tag = "НМ" + item[0].Substring(2);

                        foreach (var items in calc.RunReadSetting())
                        {
                            if (items.Count > 1)
                                if (iItem == item[0].Substring(2) && !tag.Contains(items[0]) && items[1] == comboBox2.Text)
                                {
                                    comboBox2.Items.Remove(items[1]);
                                }
                                else if (items[0].Substring(2) == iItem && items[0].Substring(0, 2).ToString() == "НМ"
                                        && !comboBox2.Items.Contains(items[1]))
                                {
                                    comboBox2.Items.Add(items[1]);
                                }
                        }
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!SumNotEqualsZero())
                return;

            Summ();
            priceF = 0;
            foreach (var item in calc.RunReadSetting())

                if (item.Count > 1)
                {
                    if (item[0].Substring(0, 2).ToString() == "НМ" && item[1] == comboBox2.Text)
                    {
                        iItem = item[0].Substring(2);//записывает номер значения в списке файла
                        comboBox2.Tag = item[0].ToString();

                        foreach (var items in calc.RunReadSetting())

                            if (items.Count > 1)
                            {
                                if (items[0].Substring(2).ToString() == iItem//номер текущей записи = номеру выбранной
                                    && tag.Contains(comboBox1.Tag) //проверка соответствия первого фильтра
                                    && tag.Contains("НФ" + comboBox2.Tag.ToString().Substring(2)))//
                                {
                                    if (items[0].Substring(0, 2) == "ЦФ")
                                    {
                                        priceF = Convert.ToDouble(items[1]) + priceP;
                                        label1.Text = "Цена за м2: " + priceF + " р.";
                                    }
                                }
                            }
                    }
                }
                else continue;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if ((dataGridView1[e.ColumnIndex, e.RowIndex].OwningColumn.Name == "Col" || dataGridView1[e.ColumnIndex, e.RowIndex].OwningColumn.Name == "Width" || dataGridView1[e.ColumnIndex, e.RowIndex].OwningColumn.Name == "Height") &&
                dataGridView1["Height", e.RowIndex].Value != null && dataGridView1["Width", e.RowIndex].Value != null && dataGridView1["Col", e.RowIndex].Value != null)
            {
                try
                {
                    int iRows = dataGridView1.Rows.Count - 1;
                    double amount = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["Height"].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["Width"].Value);
                    if (dataGridView1.Rows[e.RowIndex].Cells["Sum"].Value != null)
                    {
                        id = Convert.ToInt16(dataGridView1["Id", e.RowIndex].Value);
                    }
                    else
                    {
                        id = Convert.ToInt16(dataGridView1["Id", dataGridView1.RowCount - 1].Value);
                        dictionarySummF.Add(id, (int)priceF);
                    }

                    dataGridView1["Area", e.RowIndex].Value = Math.Round(amount * (Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["Col"].Value) / 1000000), 2);//площадь изделия д*ш

                    dataGridView1["Sum", e.RowIndex].Value = Math.Round(Convert.ToDouble(dataGridView1["Area", e.RowIndex].Value) * (dictionarySummF[id] + priceP)); //деньги
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Что-то пошло не так: " + ex.Message);
                }
            }
            else return;

            if (dataGridView1.Rows[e.RowIndex].Cells["Col"].Value != null)
            {
                Summ(); //Обновляет суммы
                if (SumNotEqualsZero())
                {
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == "Del" && dataGridView1.RowCount > 0)
                    if ((MessageBox.Show(null, "Удалить элемент?", "Удаление.", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {

                        dictionarySummF.Remove((int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                        Summ();//Обновляет суммы
                    }
                    if (SumNotEqualsZero())
                    {
                        comboBox1.Enabled = true;
                        comboBox2.Enabled = true;
                    }
        }

        private void Summ()
        {
            try
            {
                int totalSumm = 0;
                double quantityM2 = 0;
                double quantityCol = 0;
                for (int i = 0; dataGridView1.Rows.Count > i; i++)
                {
                    quantityCol += Convert.ToDouble(dataGridView1.Rows[i].Cells["Col"].Value); //кол-во штук
                    quantityM2 += Convert.ToDouble(dataGridView1.Rows[i].Cells["Area"].Value); //д*ш размеры
                    totalSumm += Convert.ToInt32(dataGridView1.Rows[i].Cells["Sum"].Value); //всего денег
                }
                textBox3.Text = quantityCol.ToString();
                textBox4.Text = quantityM2.ToString();
                textBox5.Text = totalSumm.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                foreach (var item in calc.RunReadSetting())
                    if (item.Count > 1)
                        if (item[0].Substring(0, 2) == "ЦП")
                            priceP = Convert.ToInt32(item[1]);
            }
            else
                priceP = 0;
            int tempPriceF = (int)priceF + priceP;
            label1.Text = "Цена за м2: " + tempPriceF + " р.";
            Summ(); //Обновляет суммы
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(null, "Очистить список?", "Очистка.", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                dataGridView1.Rows.Clear();
                dictionarySummF.Clear();
                textBox3.ResetText();
                textBox4.ResetText();
                textBox5.ResetText();
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
            }
        }

        /*---- КНОПКА ОФОРМИТЬ И ПЕЧАТАТЬ -----*/

        StringFormat[] format_columns = new StringFormat[8];
        private void button2_Click(object sender, EventArgs e)
        {
            if (!SumNotEqualsZero()) return;

            for (int i = 0; i < format_columns.Length; i++)
            {
                format_columns[i] = new StringFormat();
                format_columns[i].Alignment = StringAlignment.Center;
                format_columns[i].LineAlignment = StringAlignment.Center;
            }
            // Проверка с запретом на печать незаполненной таблицы при формировании
            if (dataGridView1.Rows.Count > 0 && !string.IsNullOrEmpty(textBox5.Text) && textBox5.Text != "0")
            {
                printPreviewDialog1.DesktopBounds = Screen.PrimaryScreen.Bounds;
                printDocument1.PrinterSettings.Copies = 2;
                //printDocument2.PrinterSettings.Copies = 2;
                DialogResult result_print = printDialog1.ShowDialog();

                if (result_print == DialogResult.OK)
                {
                    if (checkBox2.Checked)
                        printPreviewDialog1.ShowDialog();//Предварительный просмотр вариант для офиса и клиента
                    else
                        printDocument1.Print();//Печать второго док-та для офиса и клиента
                    copiesIsFrezer = false;
                    printDocument2.Print(); //Печать второго док-та для Фрезера и маляра
                    printDocument2.Print(); //Печать второго док-та для Фрезера и маляра
                }
            }
            else
                MessageBox.Show("Нет данных для формирования документа!");
        }

        /*---- НИЖЕ ПЕЧАТНЫЙ КОД ----*/
        private int number_PP = 0;
        private bool copiesIsFrezer=false; //Печать копии для фрезеровщика
//        private double final_price = 0; // Окончательная (обшая) цена
        private bool HeadIsPrinted = false; // Заголовок листа
        private bool TableIsPrinted = false;
        //private bool HeadTableIsPrinted = false;// Шапка таблицы
        private int addToHeightAndWidth;
        private int currentPrintRowNumber = 0; //Номер
        private int columns_startX = 50;
        private int columns_height = 30; //высота строки таблицы
        private int[] columns_width;// = { 30, 110, 75, 75, 40, 50, 80, 270 }; //ширины столбцов таблицы

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            columns_width = new int []{ 30, 110, 75, 75, 40, 50, 80, 270 }; //ширины столбцов таблицы
            int page_Y = 0;
            Graphics page = e.Graphics;

            string[] columns_name = new string[]
            {
            "№", "Фреза", "Высота", "Ширина", "Шт.", "м2", "Цена", "Примечание"
            };

            Font font = new Font("Times New Roman", 12);
            Font font1 = new Font("Times New Roman", 9);
            int oneLineHeight = font.Height;

            if (!HeadIsPrinted) // Шапка документа
            {
                page_Y = oneLineHeight * 3;
                page.DrawString("Заказ наряд от " + DateTime.Now.ToShortDateString() + ", № " + textBox8.Text, font, Brushes.Black, 300, page_Y);

                page_Y = oneLineHeight * 5;
                page.DrawString(label9.Text + "\n\r" + adress + "\n\rТелефон: " + numberTel, font, Brushes.Black, columns_startX, page_Y);

                page_Y = oneLineHeight * 9;
                page.DrawString(requisites, font1, Brushes.Black, columns_startX, page_Y);
                page_Y = oneLineHeight * 10;
                page.DrawString(bank + "\r\n" + account, font1, Brushes.Black, columns_startX, page_Y);

                page_Y = page_Y + oneLineHeight*2 ;
                page.DrawString("Заказчик: " + textBox6.Text + ",  Телефон: " + textBox7.Text, font, Brushes.Black, 50, page_Y);

                string patinaCursor = "Да";
                if (!checkBox1.Checked) patinaCursor = "Нет";

                page_Y = oneLineHeight * 13;
                page.DrawString("Цвет: " + textBox1.Text + " / примечание:_______________________,   Патина: " + patinaCursor, font, Brushes.Black, columns_startX, page_Y);
                page_Y = oneLineHeight * 14;
                //----конец шапки док.
                HeadIsPrinted = true;
            //}
            //if (!HeadTableIsPrinted) // Шапка документа
            //{
                //Печатаем первую строку-заголовок таблицы
                using (Pen pen = new Pen(Brushes.Black, 2))
                {
                    page_Y = page_Y + oneLineHeight;
                    int columns_X = columns_startX;
                    for (int i = 0; i < columns_name.Length; i++)
                    {
                        Rectangle rect = new Rectangle(columns_X, page_Y, columns_width[i], columns_height);
                        page.DrawString(columns_name[i], new Font(FontFamily.GenericSansSerif, 11), Brushes.Black, rect, format_columns[i]);
                        page.DrawRectangle(pen, rect);
                        columns_X += columns_width[i];
                    }
                }
                //HeadTableIsPrinted = true;
            }

            //Печатаем содержимое таблицы
            using (Pen pen = new Pen(Brushes.Black, 2))
            {
                if (dataGridView1.RowCount > 0 && TableIsPrinted == false)
                {
                    int columnsTotal_X = columns_startX;
                    for (int rowN = currentPrintRowNumber; rowN < dataGridView1.RowCount; rowN++)
                    {
                        page_Y = page_Y + columns_height;
                        if (page_Y > e.MarginBounds.Height) //если выходим за рамки области печати по вертикали, то переходим на печать следующей страницы
                        {
                            e.HasMorePages = true;
                            currentPrintRowNumber = rowN;
                            return;
                        }
                        number_PP++;
                        string[] columns_data = new string[8];
                        columns_data[0] = number_PP.ToString();
                        columns_data[1] = dataGridView1.Rows[rowN].Cells["Freza"].Value.ToString();
                        columns_data[2] = dataGridView1.Rows[rowN].Cells["Height"].Value.ToString();
                        columns_data[3] = dataGridView1.Rows[rowN].Cells["Width"].Value.ToString();
                        columns_data[4] = dataGridView1.Rows[rowN].Cells["Col"].Value.ToString();
                        columns_data[5] = dataGridView1.Rows[rowN].Cells["Area"].Value.ToString();
                        columns_data[6] = dataGridView1.Rows[rowN].Cells["Sum"].Value.ToString();
                        columns_data[7] = dataGridView1.Rows[rowN].Cells["Comment"].Value.ToString();
                        //if (columns_data[6] == "") columns_data[6] = "0";
                        //final_price = final_price + Convert.ToDouble(columns_data[6]);  //НАЧАЛО СЛОЖЕНИЯ ЦЕНЫ ЗА ВСЕ ЭЛЕМЕНТЫ В СПИСКе

                        int columns_X = columns_startX;
                        for (int i = 0; i < columns_data.Length; i++)
                        {
                            Rectangle rect = new Rectangle(columns_X, page_Y, columns_width[i], columns_height);
                            page.DrawString(columns_data[i], new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, rect, format_columns[i]);
                            page.DrawRectangle(pen, rect);
                            columns_X += columns_width[i];
                        }
                        columnsTotal_X = columns_X - columns_width[0];
                        TableIsPrinted = true;
                    }
                }
                else if (dataGridView1.RowCount == 0)
                {
                    page_Y = page_Y + columns_height;
                    TableIsPrinted = true;
                }

                //Проверим поместятся ли 5 последние строки Заказ-наряда на листе
                if ((oneLineHeight * 7 + page_Y) > e.MarginBounds.Height && page_Y > 0)
                {
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    e.HasMorePages = false; //page_Y>0 - это защита от возможного получения бесконечного числа страниц, 
                    //когда высота 4-х строк всегда больше высоты страницы, напечатаем их как получится
                }
            }
            page_Y = page_Y + oneLineHeight*2;

            page.DrawString("ИТОГО: " + textBox3.Text + " " + label6.Text + ",  " + textBox4.Text + " " + label7.Text + ",  " + textBox5.Text + " " + label8.Text
                        , font, Brushes.Black, columns_startX + columns_width[0], page_Y);
            page_Y = page_Y + oneLineHeight * 2;
            string agreeData = agree[0] + "\n" + agree[1] + "\n" + agree[2] + "\n" + agree[3] + "\n" + agree[4] + "\n" + agree[5] + "\n" + agree[6] + "\n" + agree[7];
            SizeF textSize = page.MeasureString(agreeData, font1, e.MarginBounds.Width);
            page.DrawString(agreeData, font1, Brushes.Black, new RectangleF(50, page_Y, textSize.Width+100, textSize.Height));

            page_Y = page_Y + oneLineHeight + (int)textSize.Height;
            page.DrawString("Подпись заказчика___________________\n\r(все вышеперечисленные условия принимаются)", font1, Brushes.Black, 200, page_Y);
        }

        //ДЛЯ ФРЕЗЕРОВЩИКА(+2мм) и МАЛЯРА
        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            columns_width = new int[] { 30, 135, 125, 75, 75, 40, 220 }; //ширины столбцов таблицы
            int page_Y = 0;
            Graphics page = e.Graphics;

            string[] columns_name = new string[]
            {
            "№", "Фреза", "Цвет", "Высота", "Ширина", "Шт.", "Примечание"
            };

            Font font = new Font("Times New Roman", 12);
            Font font1 = new Font("Times New Roman", 9);
            int oneLineHeight = font.Height;

            if (!HeadIsPrinted) // Шапка документа
            {
                string workTo = "";
                if (copiesIsFrezer)
                    workTo = "ФРЕЗЕРОВЩИКА";
                else
                workTo = "МАЛЯРА";

                page_Y = oneLineHeight * 2;
                page.DrawString("Заказ наряд № "+ textBox8.Text +" / для: " + workTo, font, Brushes.Black, 300, page_Y);

                page_Y = page_Y + oneLineHeight ;
                page.DrawString(label9.Text + "\n\n\rЗаказчик: " + textBox6.Text, font, Brushes.Black, 50, page_Y);

                page_Y = page_Y + oneLineHeight*3;
                page.DrawString("Дата приема: " + DateTime.Now.ToShortDateString() + "   Готовность: " + DateTime.Now.AddMonths(1).ToShortDateString(), font, Brushes.Black, 50, page_Y);
                page_Y = page_Y + oneLineHeight * 2;
                page.DrawString(@"Материал покрытия: "+ comboBox2.Text + " / примечание:_______________________________", font, Brushes.Black, 50, page_Y);

                page_Y = page_Y + oneLineHeight ;
                //----конец шапки док.

                //Печатаем первую строку-заголовок таблицы
                using (Pen pen = new Pen(Brushes.Black, 2))
                {
                    page_Y = page_Y + oneLineHeight;
                    int columns_X = columns_startX;
                    for (int i = 0; i < columns_name.Length; i++)
                    {
                        Rectangle rect = new Rectangle(columns_X, page_Y, columns_width[i], columns_height);
                        page.DrawString(columns_name[i], new Font(FontFamily.GenericSansSerif, 11), Brushes.Black, rect, format_columns[i]);
                        page.DrawRectangle(pen, rect);
                        columns_X += columns_width[i];
                    }
                }
                HeadIsPrinted = true;
            }

            //Печатаем содержимое таблицы
            using (Pen pen = new Pen(Brushes.Black, 2))
            {
                if (dataGridView1.RowCount > 0 && TableIsPrinted == false)
                {
                    for (int rowN = currentPrintRowNumber; rowN < dataGridView1.RowCount; rowN++)
                    {
                        page_Y = page_Y + columns_height;
                        if (page_Y > e.MarginBounds.Height) //если выходим за рамки области печати по вертикали, то переходим на печать следующей страницы
                        {
                            e.HasMorePages = true;
                            currentPrintRowNumber = rowN;
                            return;
                        }
                        number_PP++;
                        string[] columns_data = new string[7];
                        columns_data[0] = number_PP.ToString();
                        columns_data[1] = dataGridView1.Rows[rowN].Cells["Freza"].Value.ToString();
                        columns_data[2] = dataGridView1.Rows[rowN].Cells["Color"].Value.ToString();
                        if (copiesIsFrezer)
                            addToHeightAndWidth = 2;
                        else
                            addToHeightAndWidth = 0;
                        columns_data[3] = (Convert.ToInt16(dataGridView1.Rows[rowN].Cells["Height"].Value) + addToHeightAndWidth).ToString();
                        columns_data[4] = (Convert.ToInt16(dataGridView1.Rows[rowN].Cells["Width"].Value) + addToHeightAndWidth).ToString();
                        columns_data[5] = dataGridView1.Rows[rowN].Cells["Col"].Value.ToString();
                        columns_data[6] = dataGridView1.Rows[rowN].Cells["Comment"].Value.ToString();

                        int columns_X = columns_startX;
                        for (int i = 0; i < columns_data.Length; i++)
                        {
                            Rectangle rect = new Rectangle(columns_X, page_Y, columns_width[i], columns_height);
                            page.DrawString(columns_data[i], new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, rect, format_columns[i]);
                            page.DrawRectangle(pen, rect);
                            columns_X += columns_width[i];
                        }
                    }
                    page_Y = page_Y + columns_height;
                    TableIsPrinted = true;
                }
                else if (dataGridView1.RowCount == 0)
                {
                    page_Y = page_Y + columns_height;
                    TableIsPrinted = true;
                }

                //Проверим поместятся ли 4 последние строки Заказ-наряда на листе
                if ((oneLineHeight * 4 + page_Y) > e.MarginBounds.Height && page_Y > 0)
                {
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    e.HasMorePages = false; //page_Y>0 - это защита от возможного получения бесконечного числа страниц, 
                    //когда высота 4-х строк всегда больше высоты страницы, напечатаем их как получится
                }
                copiesIsFrezer = true;
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CalculaTOR, v. 1.1\nCopyright (с) 2020, Соломянюк Андрей\nПо вопросам разработки ПО обращаться: +7(931)6055903.\n\n" +
 @"УСЛОВИЯ ЛИЦЕНЗИИ:
Если вы не согласны с условиями лицензионного соглашения то не используйте данную программу.
Эти условия распространяются также на любые
- обновления,
- дополнения,
- службы Интернета и
- услуги по технической поддержке.
При соблюдении вами условий данной лицензии вам предоставляются следующие права.
1. Вы можете установить и использовать любое количество копий программного обеспечения на ваших устройствах в рамках проекта, для которого написана данная программа.
2. Вы можете копировать и распространять программное обеспечение (ПО) при условии, что:
При копировании ПО, об этом будут осведомлены автор данной программы и обладатели данной копии.

Автор оставляет за собой все остальные права.",
                "О программе");
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegistryKey saveKey = Registry.CurrentUser.CreateSubKey(Properties.Settings.Default.puth);
            if (Convert.ToDateTime(saveKey.GetValue("eDate")) < DateTime.Now)
                saveKey.SetValue("eDate", DateTime.Now);
            saveKey.Close();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TableIsPrinted = false;
            HeadIsPrinted = false;
            number_PP = 0;
        }

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TableIsPrinted = false;
            HeadIsPrinted = false;
            number_PP = 0;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13) // если нажат ентер на любой части формы, то добавить строку
            AddRow();
        }
    }
}
