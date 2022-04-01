/// <summary>
/// Logic for Find.xaml
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
    
    public partial class Find : Window
    {
        private List<int> words;
        private int index;
        private string text;
        private string textCopy;
        private string replacingWord;
        private string replacedWord;
        MainWindow mainWindow;
        TextBox textBox;

        public string content
        {
            get { return text; }
            set { text = value; }
        }

        public Find()
        {
            InitializeComponent();
            words = new List<int>();
            index = 0;
        }

        public Find(string content, MainWindow main,TextBox textBox)
        {
            InitializeComponent();
            this.content = content;
            this.textCopy = content;
            this.mainWindow = main;
            words = new List<int>();
            this.index = 0;
            this.textBox = textBox;
        }


        private void FindPrevious_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setTextBoxContent(textCopy);
            if (index == 0)
            {
                MessageBox.Show("Last word found in this file");
            }
            else
            { 
                textBox.Text = replaceWord(words[index]);
                index--;
                mainWindow.setTextBox(textBox);
            }
        }

        private void FindNext_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setTextBoxContent(textCopy);
            if (index==words.Count)
            {
                MessageBox.Show("Last word found in this file");
                index--;
            }
            else {
                textBox.Text = replaceWord(words[index]);
                index++;
                mainWindow.setTextBox(textBox);
            }
        }

        private string replaceWord(int caretIndex)
        {
            string c = content;
            string newContent;
            replacingWord = WordInput.Text;
            replacedWord = "|" + WordInput.Text + "|";
            if (caretIndex != 0)
            {
                newContent = content.Substring(0, caretIndex - 1);
                newContent += replacedWord;
            }
            else
            {
                newContent = replacedWord;
            }
            newContent += content.Substring(caretIndex+replacingWord.Length);
            return newContent;
        }

        private List<int> getDictionary(string text,string word)
        {
            List<int> list = new List<int>();
            for(int i=0;i<text.Length-word.Length;i++)
            {
                bool equal = true;
                for(int j=0;j<word.Length;j++)
                    if(word[j]!=text[i+j])
                        equal=false;
                if(equal)
                    list.Add(i);
            }
            return list;
        }

        private int firstWordPosition(string text, string word)
        {
            for (int i = 0; i < text.Length; i++)
            {
                bool equal = true;
                for (int j = 0; j < word.Length; j++)
                {
                    if (text[j + i] != word[j])
                        equal = false;
                }
                if (equal)
                    return i;
            }
            return -1;
        }

        private void FindAll_Click(object sender, RoutedEventArgs e)
        {
            TextBox t=new TextBox();
            t.Text = textCopy;
            mainWindow.setTextBox(t);
            replacingWord = WordInput.Text;
            replacedWord = "|"+WordInput.Text+"|";
            int caret = firstWordPosition(text, replacingWord);
            if (caret != -1)
            {
                content = textCopy;
                content = content.Replace(replacingWord, replacedWord);
                mainWindow.setTextBoxContent(content);
            }
            else
            {
                MessageBox.Show("No word to match!");
            }
        }

        private void WordInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string wordToFind = WordInput.Text;
            words = getDictionary(text, wordToFind);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.setTextBoxContent(textCopy);
        }
    }
}
