using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using SQLite;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LeetCode_Patterns
{
    public partial class MainWindow : Window
    {
        private Sql sql = new();
        SQLiteConnection db;
        //private readonly ScriptEngine pythonEngine;
        public MainWindow()
        {
            InitializeComponent();
   
            //webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            webView.EnsureCoreWebView2Async(null);
            // Get an absolute path to the database file
            var databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "leetcode.db");

            db = new SQLiteConnection(databasePath);
            db.CreateTable<Solution>();
            db.CreateTable<Problem>();
            //pythonEngine = Python.CreateEngine();

            //var outputStream = new MemoryStream();
            //pythonEngine.Runtime.IO.SetOutput(outputStream, Encoding.ASCII);
            //ScriptSource scriptSource = pythonEngine.CreateScriptSourceFromString(pythonCode, SourceCodeKind.SingleStatement);
            //ScriptScope scope = pythonEngine.CreateScope();
            //scriptSource.Execute(scope);

            //var result = scope.GetVariable("result").ToString();

            //ScriptSource scriptSource = pythonEngine.CreateScriptSourceFromFile("../../../script.py");

            //var outputStream = new MemoryStream();
            //pythonEngine.Runtime.IO.SetOutput(outputStream, Encoding.ASCII);
            //ScriptScope scope = pythonEngine.CreateScope();
            //scriptSource.Execute(scope);

            //var result = scope.GetVariable("result").ToString();
            //Console.WriteLine(result);

            var problemSet = sql.GetAllProblems(db);
            foreach (var problem in problemSet)
            {
                myListBox.Items.Add(problem);
            }
            myListBox.DisplayMemberPath = "Name";
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
        }

        private void Add_Case_Button_Click(object sender, RoutedEventArgs e)
        {
            Problem problem = new();
            problem.ProblemHTML = problemBox.Text;
            problem.CategoryId = myComboBox.SelectedIndex;
            problem.Name = nameBox.Text;
            myListBox.Items.Add(problem);
            int i = sql.AddProblem(db, problem);
            var f = $"C:\\Code3\\Dec\\LeetCode-Patterns\\python\\{problem.Name.Replace(" ", "-")}.py";
            File.WriteAllText(f, solutionBox.Text);

            Solution solution = new();
            solution.CategoryId = myComboBox.SelectedIndex;
            solution.ProblemId = i;
            solution.Path = f;
            sql.AddSolution(db, solution);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (myComboBox.SelectedItem != null)
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)myComboBox.SelectedItem;
                string selectedText = selectedComboBoxItem.Content.ToString();
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myListBox.SelectedValue != null)
            {
                Problem? selectedItem = myListBox.SelectedValue as Problem;

                var p = sql.GetProblemById(db, selectedItem.Id);
                var s = sql.GetSolutionById(db, selectedItem.Id);

                textEditor.Text = File.ReadAllText(s.Path);
                myComboBox.SelectedIndex = p.CategoryId;
                webView.CoreWebView2.NavigateToString("<script src='https://cdn.tailwindcss.com/3.3.5'></script>" + p.ProblemHTML);
            }
        }
    }
}