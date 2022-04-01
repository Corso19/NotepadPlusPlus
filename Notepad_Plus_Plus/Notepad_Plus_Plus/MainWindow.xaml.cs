using Microsoft.Win32;
using Notepad_Plus_Plus.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Notepad_Plus_Plus
{
    public partial class MainWindow : Window
    {
        private List<string> filePath;
        private List<string> clipboard;
        private string text;
        private int caretPosition;
        private int count;
        private Edit edit;
        public MainWindow()
        {
            count = 1;
            InitializeComponent();
            initialize();
            filePath= new List<string>();
            clipboard= new List<string>();
            edit=new Edit();
        }

        private void initialize()
        {
            
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            int i=TextTabs.SelectedIndex;
            if (i != -1)
            {
                int index = TextTabs.SelectedIndex;
                TabItem tabItem = TextTabs.Items[index] as TabItem;
                var data = (tabItem.Content as TextBox).Text;
                if (filePath[index] != null)
                {
                    File.WriteAllText(filePath[index], data);

                }
                else
                {
                    SaveFileDialog dialog = new SaveFileDialog()
                    {
                        Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        File.WriteAllText(dialog.FileName, data);
                        filePath[index] = dialog.FileName;
                    }
                }
                string title = fileName(filePath[index]);
                tabItem.Header = title;
                /*string newTitle = null;
                for (int j = 0; j < title.Length - 1; j++)
                    newTitle += title[j];
                tabItem.Header = newTitle;*/
            }
            else
            {
                MessageBox.Show("You don't have selected a file to save");
            }
        }

        private void SaveAll(object sender, RoutedEventArgs e)
        {
            int tabsCount=TextTabs.Items.Count;
            string title = null;
            if (tabsCount > 0)
            {
                for (int i = 0; i < tabsCount; i++)
                {
                    TabItem tabItem = TextTabs.Items[i] as TabItem;
                    var data = (tabItem.Content as TextBox).Text;
                    if (data == null)
                        MessageBox.Show("Empty File");
                    else
                    {
                        SaveFileDialog dialog = new SaveFileDialog()
                        {
                            Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                        };

                        if (dialog.ShowDialog() == true)
                        {
                            File.WriteAllText(dialog.FileName, data);
                            title = dialog.FileName;
                        }
                    }
                    string newTitle = fileName(title);
                    tabItem.Header = newTitle;
                }
            }
            else
            {
                MessageBox.Show("You don't have any file to save");
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            int index=TextTabs.SelectedIndex;
            string title=null;
            if (index != -1)
            {
                var tabItem = TextTabs.SelectedItem as TabItem;
                var data = (tabItem.Content as TextBox).Text;
                
                if (data == null)
                    MessageBox.Show("Empty File");
                else
                {
                    SaveFileDialog dialog = new SaveFileDialog()
                    {
                        Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        File.WriteAllText(dialog.FileName, data);
                        title=dialog.FileName;
                    }
                }
                string newTitle = fileName(title);
                tabItem.Header = newTitle;
            }
            else
            {
                MessageBox.Show("You don't have selected a file to save");
            }
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            text = textBox.SelectedText;
            caretPosition = textBox.SelectionStart;
            edit.setSelectionChange(textBox.Text, text, caretPosition);
        }


        private void Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tabItem = TextTabs.SelectedItem as TabItem;
            var data = (tabItem.Content as TextBox).Text;
            TextBox textBox = tabItem.Content as TextBox;
            
            
            string title = tabItem.Header.ToString();
            if (!title.EndsWith("*"))
            {
                title = title + "*";
                tabItem.Header = title;
                
            }
            edit.Content = textBox.Text;
        }

        private void NewFile(object sender, RoutedEventArgs e)
        {
            TabItem it = new TabItem();
            TextBox txt = new TextBox();

            txt.AcceptsReturn = true;
            txt.AcceptsTab = true;

            it.Header = "New File (" + count.ToString() + ")";
            count++;
            it.Content = txt;
            txt.TextChanged+=Content_TextChanged;
            txt.SelectionChanged += TextBox_SelectionChanged;

            TextTabs.Items.Add(it);
            filePath.Add(null);
        }


        private string fileName(string name)
        {
            string newName = null;
            int c=name.LastIndexOf('\\');
            name=name.Remove(0,c+1);
            
            return name;
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string FileToOpen in openFileDialog.FileNames)
                {
                    TabItem it = new TabItem();
                    TextBox txt = new TextBox();

                    txt.AcceptsReturn = true;
                    txt.AcceptsTab = true;

                    txt.Text = File.ReadAllText(FileToOpen);
                    txt.TextChanged += Content_TextChanged;
                    txt.SelectionChanged += TextBox_SelectionChanged;

                    it.Header = fileName(FileToOpen);
                    filePath.Add(FileToOpen);

                    it.Content = txt;
                    TextTabs.Items.Add(it);
                }
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CloseFiles(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.Items[index] as TabItem;
                var data = (tabItem.Content as TextBox).Text;
                filePath.RemoveAt(index);
                TextTabs.Items.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("You don't any file to close");
            }
        }

        
        private void Cut(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            string content = (tabItem.Content as TextBox).Text.ToString();
            textBox.Text = edit.cut();
            tabItem.Content = textBox;
        }
        private void Copy(object sender, RoutedEventArgs e)
        {
            edit.copy();
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox= tabItem.Content as TextBox;
            string content = (tabItem.Content as TextBox).Text.ToString();
            textBox.Text = edit.delete();
            tabItem.Content = textBox;
        }
        private void Paste(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                if (edit.paste(caretPosition)!=null)
                {
                    TabItem tabItem = TextTabs.Items[index] as TabItem;
                    TextBox textBox = tabItem.Content as TextBox;
                    textBox.Text = edit.paste(caretPosition);
                    tabItem.Content= textBox;
                }
                else
                {
                    MessageBox.Show("Nothing to paste");
                }
            }
            else
            {
                MessageBox.Show("No file selected");
            }
        }
        private void Lowercase(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.Items[index] as TabItem;
                TextBox textBox = tabItem.Content as TextBox;

                textBox.Text = edit.capsLock(caretPosition,1);
                tabItem.Content = textBox;
            }
            else
            {
                MessageBox.Show("No file selected");
            }
        }
        private void Uppercase(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;

            if (index != -1)
            {
                TabItem tabItem = TextTabs.Items[index] as TabItem;
                TextBox textBox = tabItem.Content as TextBox;

                textBox.Text = edit.capsLock(caretPosition,2);
                tabItem.Content = textBox;
            }
            else
            {
                MessageBox.Show("You don't any file selected");
            }
        }
        

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;

            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox = tabItem.Content as TextBox;

            textBox.SelectAll();
        }
        
        private void Author(object sender, RoutedEventArgs e)
        {
            Author author = new Author();
            author.Show();
        }
        

        private void Find(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = TextTabs.SelectedItem as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            string content = textBox.Text;
            Find find = new Find(content, this,textBox);
            find.Show();
        }

        private void Replace(object sender, RoutedEventArgs e)
        {
            int index=TextTabs.SelectedIndex;
            if (index != -1)
            {
                TabItem tabItem = TextTabs.SelectedItem as TabItem;
                TextBox textBox = tabItem.Content as TextBox;
                string content = textBox.Text;
                Replace replace = new Replace(content,this);
                replace.Show();
            }
        }

        public void setTextBoxContent(string text)
        {
            TabItem tabItem = TextTabs.SelectedItem as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            textBox.Text = text;
        }

        public void setTextBox(TextBox textBoxUpdate)
        {
            TabItem tabItem = TextTabs.SelectedItem as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            textBox = textBoxUpdate;
            tabItem.Content = textBox;
        }

        private void goToLine(object sender, RoutedEventArgs e)
        {
            int index = TextTabs.SelectedIndex;
            TabItem tabItem = TextTabs.Items[index] as TabItem;
            TextBox textBox = tabItem.Content as TextBox;
            Line line=new Line(this,textBox);
            line.Show();
        }
    }
}
