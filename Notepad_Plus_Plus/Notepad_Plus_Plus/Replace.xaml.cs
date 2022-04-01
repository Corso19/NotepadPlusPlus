/// <summary>
/// Interaction logic for Replace.xaml
/// </summary>

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
    
    public partial class Replace : Window
    {
        private string text;
        public string content
        {
            get { return text; }
            set { text = value; }
        }

        MainWindow mainWindow;

        private string replacingWord;
        private string replacedWord;
        public Replace()
        {
            InitializeComponent();
        }

        public Replace(string content,MainWindow main)
        {
            InitializeComponent();
            this.content = content;
            this.mainWindow = main;
        }

        private void ReplaceAll(object sender, RoutedEventArgs e)
        {
            replacingWord = WordInput.Text;
            replacedWord = WordReplace.Text;
            int caret = firstWordPosition(text, replacingWord);
            if (caret != -1)
            {
                content = content.Replace(replacingWord, replacedWord);
                mainWindow.setTextBoxContent(content);
            }
            else
            {
                MessageBox.Show("No word to match!");
            }
        }

        private int firstWordPosition(string text,string word)
        {
            for(int i=0;i<text.Length;i++)
            {
                bool equal = true;
                for(int j=0;j<word.Length;j++)
                {
                    if(text[j+i]!=word[j])
                        equal=false;
                }
                if (equal)
                    return i;
            }
            return -1;
        }

        private void ReplaceWord(object sender, RoutedEventArgs e)
        {
            replacingWord = WordInput.Text;
            replacedWord = WordReplace.Text;
            int caretPosition=firstWordPosition(text,replacingWord);
            if (caretPosition != -1)
            {
                content = content.Remove(caretPosition, replacingWord.Length);
                content = content.Insert(caretPosition, replacedWord);
                mainWindow.setTextBoxContent(content);
            }
            else
            {
                MessageBox.Show("No word to match!");
            }
        }
    }
}
