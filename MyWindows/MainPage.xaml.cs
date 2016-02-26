using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// using LRSkipAsync;
// using RsvwrdAsync;
using ChkNamespaceAsync;
using ChkClassAsync;
using ChkFuncAsync;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace MyWindows
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region 初期設定
//        private CS_LRskipAsync lrskip;
//        private CS_RsvwrdAsync rsvwrd;
        private CS_ChkFuncAsync chkfunc;
        private CS_ChkNamespaceAsync chknamespace;
        private CS_ChkClassAsync chkclass;

        Boolean Appflg;
        #endregion

        public MainPage()
        {
            this.InitializeComponent();

            chkfunc = new CS_ChkFuncAsync();
            chknamespace = new CS_ChkNamespaceAsync();
            chkclass = new CS_ChkClassAsync();

            textBox01.Text = "";
            textBox02.Text = "";
            Appflg = false;

            ClearResultTextBox();			// 初期表示をクリアする
        }

        #region ［ＣｈｋＦｕｎｃ］ボタン押下
        private async void button01_Click(object sender, RoutedEventArgs e)
        {   // [ChkFunc]ボタン押下
            // WriteLineResult("\n[ChkFunc]ボタン押下");
            String KeyWord = textBox02.Text;

            // await chkfunc.ClearAsync();
            chkfunc.Is_func = Appflg;
            chkfunc.Wbuf = KeyWord;
            chkfunc.Lno = 10;
            await chkfunc.ExecAsync();

            if (chkfunc.Result != "")
            {   // Ｆｕｎｃｔｉｏｎ検出？
                WriteLineResult("Result : [{0}]", chkfunc.Result);
            }
            if (chkfunc.Is_func)
            {   // [Namespace]検出？
                WriteLineResult("ChkFunc : True");
                Appflg = true;
            }
            else
            {
                WriteLineResult("ChkFunc : False");
                Appflg = false;
            }
        }
        #endregion

        #region ［Ｒｅｓｅｔ］ボタン押下
        private async void button02_Click(object sender, RoutedEventArgs e)
        {   // [Reset]ボタン押下
            ClearResultTextBox();			// 初期表示をクリアする
            await chknamespace.ClearAsync();
            await chkclass.ClearAsync();
            await chknamespace.ClearAsync();

            textBox01.Text = "";
            textBox02.Text = "";
            Appflg = false;
        }
        #endregion

        #region ［ＣｈｋＮａｍｅｓｐａｃｅ］ボタン押下
        private async void button03_Click(object sender, RoutedEventArgs e)
        {   // [ChkNamespace]ボタン押下
            // WriteLineResult("\n[ChkNamespace]ボタン押下");
            String KeyWord = textBox02.Text;

            // await chknamespace.ClearAsync();
            chknamespace.Is_namespace = Appflg;
            chknamespace.Wbuf = KeyWord;
            chknamespace.Lno = 10;
            await chknamespace.ExecAsync();

            if (chknamespace.Result != "")
            {   // Ｆｕｎｃｔｉｏｎ検出？
                WriteLineResult("Result : [{0}]", chknamespace.Result);
            }
            if (chknamespace.Is_namespace)
            {   // [Namespace]検出？
                WriteLineResult("ChkNamespace : True");
                Appflg = true;
            }
            else
            {
                WriteLineResult("ChkNamespace : False");
                Appflg = false;
            }
        }
        #endregion

        #region ［ＣｈｋＣｌａｓｓ］ボタン押下
        private async void button04_Click(object sender, RoutedEventArgs e)
        {   // [ChkClass]ボタン押下
            // WriteLineResult("\n[ChkClass]ボタン押下");
            String KeyWord = textBox02.Text;

            // await chkclass.ClearAsync();
            chkclass.Is_class = Appflg;
            chkclass.Wbuf = KeyWord;
            chkclass.Lno = 10;
            await chkclass.ExecAsync();
            if (chkclass.Result != "")
            {   // Ｆｕｎｃｔｉｏｎ検出？
                WriteLineResult("Result : [{0}]", chkclass.Result);
            }
            if (chkclass.Is_class)
            {   // [Namespace]検出？
                WriteLineResult("ChkClass : True");
                Appflg = true;
            }
            else
            {
                WriteLineResult("ChkClass : False");
                Appflg = false;
            }
        }
        #endregion
    }
}
