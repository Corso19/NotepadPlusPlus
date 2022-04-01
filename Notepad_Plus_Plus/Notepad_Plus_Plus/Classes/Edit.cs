using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Notepad_Plus_Plus.Classes
{
    internal class Edit
    {
        private string copyText;
        private string content;
        private int caret;
        private string selection;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        public int Caret
        {
            get { return caret; }
            set { caret = value; }
        }
        public string Selection
        {
            get { return selection; }
            set { selection = value; }
        }

        public Edit()
        {
            copyText = null;
        }

        public void setSelectionChange(string text, string selectText, int caret)
        {
            this.Content = text;
            this.caret = caret;
            this.selection = selectText;
        }

        public string delete()
        {
            return content.Remove(caret, selection.Length);
        }

        public void copy()
        {
            copyText = selection;
        }

        public string cut()
        {
            copy();
            return delete();
        }

        public string paste(int caret)
        {
            return content.Insert(caret, copyText);
        }

        public string capsLock(int caret,int op)
        {
            content = delete();
            switch (op)
            {
                case 1:
                    content = content.Insert(caret, selection.ToLower());
                    break;
                case 2:
                    content = content.Insert(caret, selection.ToUpper());
                    break;
                default:
                    break;
            }
            return content;
        }

    }
}
