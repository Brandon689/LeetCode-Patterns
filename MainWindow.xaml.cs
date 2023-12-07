using SQLite;
using System.IO;
using System.Text;
using System.Windows;
using TheArtOfDev.HtmlRenderer.WPF;

namespace LeetCode_Patterns
{
    public partial class MainWindow : Window
    {
        private Sql sql = new();
        string htmlContent;
        SQLiteConnection db;
        public MainWindow()
        {//<script src='https://cdn.tailwindcss.com/3.3.5'></script>
            InitializeComponent();

            textEditor.Text = @"class Solution:
    def longestPalindrome(self, s: str) -> str:
        if len(s) <= 1:
            return s
        
        Max_Len=1
        Max_Str=s[0]
        for i in range(len(s)-1):
            for j in range(i+1,len(s)):
                if j-i+1 > Max_Len and s[i:j+1] == s[i:j+1][::-1]:
                    Max_Len = j-i+1
                    Max_Str = s[i:j+1]

        return Max_Str";
            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            webView.EnsureCoreWebView2Async(null);
            // Get an absolute path to the database file
            var databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "leetcode.db");

            db = new SQLiteConnection(databasePath);
            db.CreateTable<Solution>();
            db.CreateTable<Problem>();
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            htmlContent = "<script src='https://cdn.tailwindcss.com/3.3.5'></script>" + sql.a(db).ProblemHTML;
            webView.CoreWebView2.NavigateToString(htmlContent);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Problem s = new();
            s.ProblemHTML = paste.Text;
            sql.AddProblem(db, s);
        }
    }
}