using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Notepad_Plus_Plus
{
    /// <summary>
    /// Interaction logic for Line.xaml
    /// </summary>
    public partial class Line : Window
    {
        private TextBox textBox;
        private MainWindow mainWindow;
        private int position;

        public Line()
        {
            InitializeComponent();
        }

        public Line(MainWindow main,TextBox text)
        {
            InitializeComponent();
            this.textBox = text;
            this.mainWindow = main;
            this.position = -1;
        }

        public void goToLine(MainWindow main,TextBox text)
        {
            int x = int.Parse(input.Text);
            if (x < text.LineCount)
            {

                for (int i = 0; i < x; i++)
                    position += lenghtForCaret(i);
                text.ScrollToLine(x);
                text.CaretIndex = position;
                main.setTextBox(text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid line!");
            }
        }

        public int lenghtForCaret(int line)
        {
            string text=textBox.GetLineText(line);
            return text.Length;
        }

        private void goTo(object sender, RoutedEventArgs e)
            {
                goToLine(mainWindow, textBox);
            }
        }
}
