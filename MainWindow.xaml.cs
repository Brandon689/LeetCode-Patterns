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
        string htmlContent;
        SQLiteConnection db;
        private readonly ScriptEngine pythonEngine;
        public MainWindow()
        {//<script src='https://cdn.tailwindcss.com/3.3.5'></script>
            InitializeComponent();

       
            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            webView.EnsureCoreWebView2Async(null);
            // Get an absolute path to the database file
            var databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "leetcode.db");

            db = new SQLiteConnection(databasePath);
            db.CreateTable<Solution>();
            db.CreateTable<Problem>();
            pythonEngine = Python.CreateEngine();



            //var outputStream = new MemoryStream();
            //pythonEngine.Runtime.IO.SetOutput(outputStream, Encoding.ASCII);
            //ScriptSource scriptSource = pythonEngine.CreateScriptSourceFromString(pythonCode, SourceCodeKind.SingleStatement);
            //ScriptScope scope = pythonEngine.CreateScope();
            //scriptSource.Execute(scope);

            //var result = scope.GetVariable("result").ToString();

            ScriptSource scriptSource = pythonEngine.CreateScriptSourceFromFile("../../../script.py");

            var outputStream = new MemoryStream();
            pythonEngine.Runtime.IO.SetOutput(outputStream, Encoding.ASCII);
            ScriptScope scope = pythonEngine.CreateScope();
            scriptSource.Execute(scope);

            var result = scope.GetVariable("result").ToString();
            ;
            Console.WriteLine(result);

            var item = sql.pl(db);
            foreach (var ut in item)
            {
                string newItem = ut.Id.ToString();
                myListBox.Items.Add(newItem);
            }
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            //htmlContent = "<script src='https://cdn.tailwindcss.com/3.3.5'></script>" + sql.a(db).ProblemHTML;
            //webView.CoreWebView2.NavigateToString(htmlContent);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Problem s = new();
            s.ProblemHTML = problemBox.Text;
            s.CategoryId = myComboBox.SelectedIndex;

            int i = sql.AddProblem(db, s);
            var f = "C:\\Code3\\Dec\\LeetCode-Patterns\\python\\script.py";
            File.WriteAllText(f, solutionBox.Text);


            Solution v = new();
            v.CategoryId = myComboBox.SelectedIndex;
            v.ProblemId = i;
            v.Path = f;
            sql.AddSolution(db, v);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected item from the ComboBox
            if (myComboBox.SelectedItem != null)
            {
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)myComboBox.SelectedItem;
                string selectedText = selectedComboBoxItem.Content.ToString();
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected item content from the ListBox
            if (myListBox.SelectedValue != null)
            {
                string selectedItemContent = myListBox.SelectedValue.ToString();

                int x = int.Parse(selectedItemContent);

                var p = sql.a(db,x);
                var s = sql.s(db,x);

                textEditor.Text = File.ReadAllText(s.Path);
                myComboBox.SelectedIndex = p.CategoryId;
                htmlContent = "<script src='https://cdn.tailwindcss.com/3.3.5'></script>" + p.ProblemHTML;
                webView.CoreWebView2.NavigateToString(htmlContent);
            }
        }
    }
}