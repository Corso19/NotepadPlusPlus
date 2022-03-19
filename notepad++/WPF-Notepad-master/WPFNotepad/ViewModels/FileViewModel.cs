using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPFNotepad.Models;

namespace WPFNotepad.ViewModels
{
    /// <summary>
    /// View model for the file toolbar.
    /// </summary>
    public class FileViewModel
    {
        public DocumentModel Document { get; private set; }

        //Toolbar commands
        public ICommand NewCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand ExitCommand { get; }
        public ObservableCollection<DocumentModel> TabsCollection { get; set; }
        
        private List<TabItem> Tabz;
    
        

        public FileViewModel(DocumentModel document)
        {
            Document = document;
            NewCommand = new RelayCommand(NewFile);
            SaveCommand = new RelayCommand(SaveFile, () => !Document.isEmpty);
            SaveAsCommand = new RelayCommand(SaveFileAs);
            OpenCommand = new RelayCommand(OpenFile);
            ExitCommand = new RelayCommand(ExitProgram);
            TabsCollection = new ObservableCollection<DocumentModel>();
        }

        public void NewFile()
        {
            /*Document.FileName = string.Empty;
            Document.FilePath = string.Empty;
            Document.Text = string.Empty;*/

            //TabItem defaultTab = new TabItem();
            //TextBox defaultBox = new TextBox();

            //defaultTab.Content = defaultBox;
            //Tabz.Add(defaultTab);
            DocumentModel documentModel = new DocumentModel();
            documentModel.FileName = "New1";


            TabsCollection.Add(documentModel);


        }

        public void ExitProgram()
        {
            System.Environment.Exit(0);
        }

        private void SaveFile()
        {
            File.WriteAllText(Document.FilePath, Document.Text);
        }

        private void SaveFileAs()
        {
            /*var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt";
            if(saveFileDialog.ShowDialog() == true)
            {
                DockFile(saveFileDialog);
                File.WriteAllText(saveFileDialog.FileName, Document.Text);
            }*/

            
        }

        private void OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                DockFile(openFileDialog);
                Document.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void DockFile<T>(T dialog) where T : FileDialog
        {
            Document.FilePath = dialog.FileName;
            Document.FileName = dialog.SafeFileName;
        }
    }
}
