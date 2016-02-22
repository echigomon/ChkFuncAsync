using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LRSkipAsync;
using RsvwrdAsync;

namespace ChkFuncAsync
{
    public class CS_ChkFuncAsync
    {
        #region 共有領域
        CS_LRskipAsync lrskip;           // 両側余白情報を削除
        CS_RsvwrdAsync rsvwrd;           // 予約語を確認する

        private static String _wbuf;
        private static Boolean _empty;
        public String Wbuf
        {
            get
            {
                return (_wbuf);
            }
            set
            {
                _wbuf = value;
                if (_wbuf == null)
                {   // 設定情報は無し？
                    _empty = true;
                }
                else
                {   // 整形処理を行う
                    if (lrskip == null)
                    {   // 未定義？
                        lrskip = new CS_LRskipAsync();
                    }
                    lrskip.ExecAsync(_wbuf);
                    _wbuf = lrskip.Wbuf;

                    // 作業の為の下処理
                    if (_wbuf.Length == 0 || _wbuf == null)
                    {   // バッファー情報無し
                        // _wbuf = null;
                        _empty = true;
                    }
                    else
                    {
                        _empty = false;
                    }
                }
            }
        }
        private static String _result;     // [Function]ＬＢＬ情報
        public String Result
        {
            get
            {
                return (_result);
            }
            set
            {
                _result = value;
            }
        }

        // 予約語：未定義
        const int RSV_NONE = 0;     // 未定義

        private static Boolean _Is_func;
        public Boolean Is_func
        {
            get
            {
                return (_Is_func);
            }
            set
            {
                _Is_func = value;
            }
        }
        private static int _lno;        // 行Ｎｏ．
        public int Lno
        {
            get
            {
                return (_lno);
            }
            set
            {
                _lno = value;
            }
        }
        #endregion

        #region コンストラクタ
        public CS_ChkFuncAsync()
        {   // コンストラクタ
            _wbuf = null;       // 設定情報無し
            _empty = true;

            _Is_func = false;  // [Function]フラグ：false
            rsvwrd = new CS_RsvwrdAsync();           // 予約語を確認する
        }
        #endregion

        #region モジュール
        public async Task ClearAsync()
        {   // 作業領域の初期化
            _wbuf = null;       // 設定情報無し
            _empty = true;

            _Is_func = false;  // [Function]フラグ：false
        }

        public async Task ExecAsync()
        {   // Function評価
            if (!_empty)
            {   // バッファーに実装有り
                await rsvwrd.ExecAsync(_wbuf);     // 評価情報の予約語確認を行う

                if (_Is_func)
                {   // [Function]フラグは、true？
                    if (!rsvwrd.Is_class)
                    {   // 評価情報は、非予約語？
                        // ＬＢＬ情報に、class名を登録する
                        _result = "C " + _wbuf + _lno.ToString();
                        _Is_func = false;       // [class]フラグ：false
                    }
                }
                else
                {   // [Function]フラグは、false
                    if (rsvwrd.Is_class)
                    {   // 評価情報は、"class"？
                        _Is_func = true;       // [class]フラグ：true
                        _result = "";
                        rsvwrd.Is_class = false;
                    }
                }
            }
        }
        #endregion
    }
}
