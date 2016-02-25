using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MyWindows
{

    public sealed partial class MainPage : Page
    {
        //出力用テキストボックス
        TextBox _TextBoxResult = null;
        public TextBox TextBoxResult {
            get{
                if (_TextBoxResult == null)
                {
                    var tb = FindFirstElement<TextBox>(this);
                    TextBoxResult = tb;
                }
                return _TextBoxResult;
            }
            set { 
                _TextBoxResult = value;
                ScrollViewer.SetVerticalScrollBarVisibility(_TextBoxResult, ScrollBarVisibility.Visible);
                _TextBoxResult.AcceptsReturn = true;
                _TextBoxResult.TextWrapping = TextWrapping.Wrap;
                ScrollViewerOfTextBoxResult = FindFirstElement<ScrollViewer>(TextBoxResult);
            } 
        }
        //出力用テキストボックスのスクロールビュワー
        ScrollViewer _sv = null;       
        public ScrollViewer ScrollViewerOfTextBoxResult{
            get{
                if (_sv == null)
                {
                    _sv =  FindFirstElement<ScrollViewer>(TextBoxResult);
                }
                return _sv;
            }
            set{ _sv = value;}
        }


        //テキストボックスクリア
        void ClearResultTextBox()
        {
            TextBoxResult.Text = "";
        }
        //結果を表示するテキストボックスに出力
        void WriteResult(string format, params object[] args)
        {
            TextBoxResult.Text += string.Format(format, args);
        }
        //結果を表示するテキストボックスに改行を出力
        void WriteLineResult()
        {
            TextBoxResult.Text += Environment.NewLine;
            ScrollViewerOfTextBoxResult.ChangeView(null, ScrollViewerOfTextBoxResult.ScrollableHeight, null);
        }
        //結果を表示するテキストボックスに出力（改行付）
        void WriteLineResult(string format, params object[] args)
        {
            WriteResult(format, args);
            WriteLineResult();
            //出力ウィンドウに出力
            Debug.WriteLine(format, args);
        }
        //指定した型を持つ子要素を取得する
        private static T FindFirstElement<T>(DependencyObject parent) where T : class
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            T element = default(T);
            for (int i = 0; i < childrenCount; i++)
            {
                var childObj = VisualTreeHelper.GetChild(parent, i);
                element = childObj as T;
                if (element == null)
                {
                    element = FindFirstElement<T>(childObj);
                }
                if (element != null)
                {
                    break;
                }
            }
            return element;
        }


    }
}